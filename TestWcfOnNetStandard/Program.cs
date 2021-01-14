using NetStandard20ToWcf;
using System;
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
        }
    }
}
