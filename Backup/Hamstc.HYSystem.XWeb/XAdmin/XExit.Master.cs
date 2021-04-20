using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XExit : AdminMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //第一种方式:客服端：修改cookie
            HttpCookie hcookie = new HttpCookie("ALOGIN");
            hcookie.Values.Add("ALOGIN", "1255444");
            Response.Cookies.Add(hcookie);
            ////第二种方式：服务器：修改登录签名
            //bllXSysAdmin.UpdateLoginSign(new XModel.XSysAdmin()
            //{
            //    SAID = CurrAdmin.SAID,
            //    LoginSign = Guid.NewGuid().ToString(),
            //    UpdateTime = DateTime.Now
            //});

            RedirectToLogin();
        }
    }
}