using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hamstc.HYSystem.BLL;
using M = Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb
{
    public class MemberMaster : System.Web.UI.MasterPage
    {
        public XMemberBLL bllXMember;
        public XNoticeBLL bllXNotice;
        public XMemberInviteBLL bllXMemberInvite;

        public MemberMaster()
        {
            bllXMember = new XMemberBLL();
            bllXNotice = new XNoticeBLL();
            bllXMemberInvite = new XMemberInviteBLL();
        }

        #region //当前会员
        private M.XMember _currMember;
        public M.XMember CurrMember
        {
            get
            {
                if (_currMember == null)
                {
                    HttpCookie hcookie = Request.Cookies["MLOGIN"];
                    if (hcookie != null)
                    {
                        //根据登录签名获取对应用户对象
                        _currMember = bllXMember.GetOneByLogin(hcookie["MSIGN"]);
                    }
                }
                return _currMember;
            }
        }
        #endregion

        #region //跳转到登录页
        public void RedirectToLogin()
        {
            Response.Redirect("./?cmd=member_login");
        }
        #endregion

        public string CreateID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }
    }
}