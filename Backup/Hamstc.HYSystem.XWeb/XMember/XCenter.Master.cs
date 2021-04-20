using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XCenter : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null) RedirectToLogin();
                else
                {
                    litLoginName.Text = CurrMember.NickName;
                    litGrade.Text = CurrMember.Grade.ToString();
                }
            }
        }

        public string getFriendCount()
        {
            string tmp = "100";
            //获取好友数量
            string tmpPCodeStr = CurrMember.PCodeStr + CurrMember.InviteCode + "|";
            tmp = bllXMember.GetFriendCount(tmpPCodeStr).ToString();
            return tmp;
        }
    }
}