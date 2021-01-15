using NetStandard20ToWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betenbough48
{
    public class ReportWrapper48
    {
        public async Task GetReportsAsync()
        {
            var report = new ReportUtilities();
            var catalogItems = await report.GetReportsAsync();
            Console.WriteLine(string.Join("\n", catalogItems.Select(i => i.Name).ToList()));
        }

        public void PrintReports()
        {
            var report = new ReportUtilities();
            var catalogItems = report.GetReports();
            Console.WriteLine(string.Join("\n", catalogItems.Select(i => i.Name).ToList()));
        }

        public string GetReports()
        {
            var report = new ReportUtilities();
            var catalogItems = report.GetReports();
            return string.Join("\n", catalogItems.Select(i => i.Name).ToList());
        }
    }
}
