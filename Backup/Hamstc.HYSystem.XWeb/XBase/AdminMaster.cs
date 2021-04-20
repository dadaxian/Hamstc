using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hamstc.HYSystem.BLL;
using Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb
{
    public class AdminMaster:System.Web.UI.MasterPage
    {
        public int mPageIndex = 1, mPageSize = 20; //当前页码，每页数量

        public XSysAdminBLL bllXSysAdmin;
        public XNoticeBLL bllXNotice;
        public XMemberBLL bllXMember;
        public XMemberInviteBLL bllXMemberInvite;
        public AdminMaster()
        {
            bllXSysAdmin = new XSysAdminBLL();
            bllXNotice = new XNoticeBLL();
            bllXMember = new XMemberBLL();
            bllXMemberInvite = new XMemberInviteBLL();
        }

        #region//当前管理员
        private XSysAdmin _currAdmin;
        public XSysAdmin CurrAdmin
        {
            get{
                if (_currAdmin == null)
                {
                    HttpCookie hcookie = Request.Cookies["ALOGIN"];
                    if (hcookie != null)
                    {
                        //根据登录签名获取对应用户对象
                        _currAdmin = bllXSysAdmin.GetOneByLogin(hcookie["ALOGIN"]);
                    }
                }
                return _currAdmin;
            }
        }

        #endregion

        #region //消息弹窗提示
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        protected void JscriptMsg(string msgtitle, string url)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", " + callback + ")";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        #endregion

        #region//跳转到登录页
        public void RedirectToLogin()
        {
            Response.Redirect("./?cmd=admin_login");
        }

        #endregion

        #region//生成ID
        public string CreateID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        #endregion
    }
}