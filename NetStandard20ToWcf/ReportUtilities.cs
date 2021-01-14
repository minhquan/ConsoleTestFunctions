using ReportService = ReportingService2005;
using ReportExecutionService = ReportExecution2005;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Linq;

namespace NetStandard20ToWcf
{
    public class ReportUtilities
    {
        public async Task<ReportService.CatalogItem[]> GetReportsAsync()
        {
            var bind = new BasicHttpBinding();
            bind.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            bind.MaxReceivedMessageSize = 64000000;

            var endpoint = new EndpointAddress("http://sqllive/ReportServer/ReportService2005.asmx");

            var service = new ReportService.ReportingService2005SoapClient(bind, endpoint);
            service.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            service.ChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            return (await service.ListChildrenAsync("/Betenbough", true)).CatalogItems;
        }

        public async Task<string> ExecuteReportAsync(string reportPath, Dictionary<string, string> reportParameters)
        {
            var bind = new BasicHttpBinding();
            bind.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            bind.MaxReceivedMessageSize = 64000000;

            var endpoint = new EndpointAddress("http://sqllive/ReportServer/ReportExecution2005.asmx");

            var service = new ReportExecutionService.ReportExecutionServiceSoapClient(bind, endpoint);
            service.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            service.ChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            var userHeader = new ReportExecutionService.TrustedUserHeader();

            // Load the report
            var loadReportResponse = await service.LoadReportAsync(userHeader, reportPath, null);
            var reportInfo = loadReportResponse.executionInfo;

            // Translate the given report parameters to an array of ParameterValue objects and set them on the report
            await service.SetExecutionParametersAsync(loadReportResponse.ExecutionHeader, userHeader, GetParameterValues(reportInfo, reportParameters), "en-us");

            // Render the report
            var result = (await service.RenderAsync(new ReportExecutionService.RenderRequest(loadReportResponse.ExecutionHeader, userHeader, "PDF", null))).Result;

            // Write the result out to a temp file
            return TempFiles.GetTempPath(result, "pdf");
        }

        /// <summary>
        /// Translates the given report parameter values to an array of ParameterValue objects in the format that the
        /// ReportExecutionService expects.
        /// </summary>
        private static ReportExecutionService.ParameterValue[] GetParameterValues(ReportExecutionService.ExecutionInfo reportInfo, Dictionary<string, string> reportParameters)
        {
            // Get a list of the parameters that are in the report.  If the call specifies
            // additional parameters that aren't in the report, this will filter them out
            var parametersInReport = from rp in reportInfo.Parameters
                                     join p in reportParameters on rp.Name equals p.Key
                                     select p;

            var parameterValues = new List<ReportExecutionService.ParameterValue>();
            foreach (var parameter in parametersInReport)
            {
                // If the value is for a multi-select parameter, it will be comma-separated.  If that is the case, the
                // ReportExecutionService actually expects those to be broken out into separate values ... instead of
                // all together in in a comma-separated list in a single ParameterValue object.  So do that translation
                // here if the value is a comma-separated list.
                var value = parameter.Value;
                if (value.Contains(','))
                {
                    var multiValues = value.Split(',');
                    foreach (var singleValue in multiValues)
                        parameterValues.Add(new ReportExecutionService.ParameterValue() { Name = parameter.Key, Value = singleValue });
                }
                else
                {
                    // This parameter value wasn't a comma-separated list ... just add it as a normal param value.
                    parameterValues.Add(new ReportExecutionService.ParameterValue() { Name = parameter.Key, Value = value });
                }
            }

            return parameterValues.ToArray();
        }
    }
}
