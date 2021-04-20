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
            return string.Format("http://{0}/?cmd=member_register&ic={1}", GetCurrentHostPort(), CurrMember.InviteCode);
            //return string.Format("http://localhost:3550/?cmd=member_register&ic={0}", CurrMember.InviteCode);
        }
        public string GetCurrentHostPort()
        {
            //            Request.ServerVariables("SERVER_NAME")'当前域名
            //Request.ServerVariables("SERVER_PORT")'当前端口
            //Request.ServerVariables("SCRIPT_NAME")'当前文件名
            //Request.ServerVariables("QUERY_STRING")'当前页面的传入参数
            //Request.ServerVariables("HTTP_USER_AGENT")'取得当前浏览器信息
            //Request("remote_addr")'取得IP
            //Request.ServerVariables("HTTP_REFERER")'上个页面地址
            //Request.ServerVariables("HTTP_HOST")'获取当前域名
            //当采用SERVER_NAME时返回的是不带端口号的URL
            return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
        }
    }
}