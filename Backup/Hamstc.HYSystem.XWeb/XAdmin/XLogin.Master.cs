using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XLogin : AdminMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //登录处理
            string tipMsg = "";

            if (txtUserName.Text.Length == 0 || txtPassword.Text.Length == 0 || txtYZM.Text.Length == 0)
            {
                if (txtUserName.Text.Length == 0) tipMsg = "用户名不能为空！";
                if (txtPassword.Text.Length == 0) tipMsg = "密码不能为空！";
                if (txtYZM.Text.Length == 0) tipMsg = "验证码不能为空！";

            }
            else
            {
                if (Session["XCVCode"] != null && txtYZM.Text.ToLower() == Session["XCVCode"].ToString().ToLower())
                {
                    XSysAdmin aobj = bllXSysAdmin.GetOneByLogin(txtUserName.Text, txtPassword.Text);
                    if (aobj != null)
                    {
                        if (aobj.IsDisable == 0)
                        {
                            string sign = Guid.NewGuid().ToString();
                            HttpCookie hcookie = new HttpCookie("ALOGIN");
                            hcookie.Values.Add("ALOGIN", sign);
                            aobj.LoginSign = sign;
                            aobj.UpdateTime = DateTime.Now;
                            if (bllXSysAdmin.UpdateLoginSign(aobj))
                            {
                                Response.Cookies.Add(hcookie);
                                tipMsg = "";
                            }
                        }
                        else
                        {
                            tipMsg = "此账号已被禁用，无法登陆！";
                        }
                    }
                    else
                    {
                        tipMsg = "用户名或者密码错误！";
                    }
                }
                else
                {
                    tipMsg = "验证码错误！";
                }
            }
            if (tipMsg.Length > 0)
            {

                Panel1.Visible = true;
                lit4Tip.Text = tipMsg;

            }
            else
            {
                //登录成功
                Response.Redirect("./?cmd=admin_main");
            }     
        }
    }
}