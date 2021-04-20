using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.UControl
{
    public partial class UCMemberHeader : System.Web.UI.UserControl
    {
        public string NavTitle { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            litTitle.Text = NavTitle;
        }
    }
}