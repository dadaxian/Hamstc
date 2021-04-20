using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hamstc.HYSystem.XWeb.XBase;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XHome : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null)
                {
                    RedirectToLogin();
                }
                else
                {
                    litLoginName.Text = CurrMember.LoginName;
                    litGrade.Text = CurrMember.Grade.ToString();
                }
            }
        }
    }
}