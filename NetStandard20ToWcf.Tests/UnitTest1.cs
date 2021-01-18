using System;
using Xunit;

namespace NetStandard20ToWcf.Tests
{
    public class UnitTest1
    {
        private const string CompanyReportFolder = "/Betenbough";

        [Fact]
        public void GetReports_GeneralCall_ReturnsSomeReports()
        {
            /// Arrange

            /// Act
            // Make the call to pull the reports
            var reports = new ReportUtilities().GetReports();

            /// Assert
            // Ensure some reports returned
            Assert.True(reports.Length > 0);
        }
    }
}
