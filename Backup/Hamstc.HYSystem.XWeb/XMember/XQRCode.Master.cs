using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XQRCode : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null) RedirectToLogin();
            }
        }
        public string GetRegUrl()
        {
            return string.Format("http://localhost:3550/?cmd=member_register&ic={0}", CurrMember.InviteCode);
        }
    }
}