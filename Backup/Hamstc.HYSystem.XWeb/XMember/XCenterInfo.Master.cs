using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XCenterInfo : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null) RedirectToLogin();
                else
                {
                    litWXNC.Text = CurrMember.NickName;
                    litWXH.Text = CurrMember.WXCode;
                    litSJHM.Text = CurrMember.Phone;
                }
            }
        }
    }
}