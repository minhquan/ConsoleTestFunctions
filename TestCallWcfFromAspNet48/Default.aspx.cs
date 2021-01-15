using Betenbough48;
using BetenboughStandard20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestCallWcfFromAspNet48
{
    public partial class Default : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            //Label1.Text = new ReportWrapper48().GetReports();
            Label1.Text = await new ReportWrapper().GetReportsAsync();


        }
    }
}