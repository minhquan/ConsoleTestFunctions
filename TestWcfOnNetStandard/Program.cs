using NetStandard20ToWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWcfOnNetStandard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var report = new ReportUtilities();
            var catalogItems = await report.GetReportsAsync();
            Console.WriteLine(string.Join("\n", catalogItems.Select(i => i.Name).ToList()));

            // Execute the vendor payslip report
            var param = new Dictionary<string, string>();
            param.Add("Vendors", "48365");
            param.Add("DocNums", "2018-1914-2");

            var path = await report.ExecuteReportAsync("/Betenbough/Production/Vendor Pay Slip by DocNum - Auto", param);
            Console.WriteLine(path);
        }
    }
}
