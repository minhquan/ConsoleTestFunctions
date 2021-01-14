using ReportingService2005;
using System;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;

namespace NetStandard20ToWcf
{
    public class ReportUtilities
    {
        public async Task<CatalogItem[]> GetReportsAsync()
        {
            var bind = new BasicHttpBinding();
            bind.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            bind.MaxReceivedMessageSize = 64000000;

            var endpoint = new EndpointAddress("http://sqllive/ReportServer/ReportService2005.asmx");

            var service = new ReportingService2005SoapClient(bind, endpoint);
            service.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            service.ChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            return (await service.ListChildrenAsync("/Betenbough", true)).CatalogItems;
        }
    }
}
