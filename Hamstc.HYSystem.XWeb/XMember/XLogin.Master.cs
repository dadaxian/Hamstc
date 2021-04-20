using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M = Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XLogin : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string msg = "";
            //第一步：空值判断
            if (username.Text.Length == 0 || password.Text.Length == 0 || vcode.Text.Length == 0)
            {
                if (username.Text.Length == 0)
                {
                    msg = "请输入用户名";
                }
                if (password.Text.Length == 0)
                {
                    msg = "请输入密码";
                }
                if (vcode.Text.Length == 0)
                {
                    msg = "请输入验证码";
                }
            }
            else
            {
                //第二步：验证码判断
                //验证码不区别大小写
                if (Session["XCVCode"] != null && vcode.Text.ToLower() == Session["XCVCode"].ToString().ToLower())//转换成小写再比较
                {
                    //第三步：用户名和密码判断
                    M.XMember mobj = bllXMember.GetOneByLogin(username.Text, XCommon.Uitl.MD5(password.Text));
                    if (mobj != null)
                    {
                        //第四步：禁用状态判断
                        if (mobj.IsDeleted == 0)
                        {
                            string sign = Guid.NewGuid().ToString();//生成一起签名
                            HttpCookie hcookie = new HttpCookie("MLOGIN");
                            hcookie.Values.Add("MSIGN", sign);
                            mobj.LoginSign = sign;
                            mobj.UpdateTime = DateTime.Now;
                            if (bllXMember.Update(mobj))//修改登录签名到数据库中
                            {
                                Response.Cookies.Add(hcookie);//把Cookie发送到客户端
                                msg = "";
                            }
                        }
                        else
                        {
                            msg = "此账号已被禁用，无法登录!";
                        }
                    }
                    else
                    {
                        msg = "用户名或密码错误!";
                    }
                }
                else
                {
                    msg = "验证码错误!";
                }
                if (msg.Length > 0)
                {
                    litMsg.Text = msg;
                    phMsg.Visible = true;
                }
                else
                {
                    Response.Redirect("./?cmd=member_home");
                }

            }
        }
    }
}