using NetStandard20ToWcf;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BetenboughStandard20
{
    public class ReportWrapper
    {
        public async Task PrintReportsAsync()
        {
            var report = new ReportUtilities();
            var catalogItems = await report.GetReportsAsync();
            Console.WriteLine(string.Join("\n", catalogItems.Select(i => i.Name).ToList()));
        }

        public async Task<string> GetReportsAsync()
        {
            var report = new ReportUtilities();
            var catalogItems = await report.GetReportsAsync();
            return string.Join("\n", catalogItems.Select(i => i.Name).ToList());
        }
    }
}
