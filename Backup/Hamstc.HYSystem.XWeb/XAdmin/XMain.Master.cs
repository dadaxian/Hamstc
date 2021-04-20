using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XAdmin : AdminMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrAdmin == null)
                {
                    RedirectToLogin();
                }
                else
                {
                    litName.Text = CurrAdmin.LoginName;
                }
            }
           
        }
    }
}