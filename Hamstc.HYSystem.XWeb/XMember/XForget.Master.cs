using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XForget : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            XModel.XMember mobj = bllXMember.GetOneByNickName(txtNickName.Text);
            if (mobj != null)
            {
                if (mobj.WXCode != txtWxCode.Text) litMsg.Text = "微信号不匹配！";
                else if (mobj.Phone != txtPhone.Text) litMsg.Text = "手机号码不匹配！";
                else if (mobj.Question != DropDownList1.SelectedItem.Text || mobj.Answer != TextBox1.Text) litMsg.Text = "密保问题不匹配！";
                else if (txtPassword.Text.Length < 6 || txtNextPWD.Text.Length < 6) litMsg.Text = "密码长度不得小于6位";
                else if (txtPassword.Text != txtConfirmPassword.Text) litMsg.Text = "登陆两次输入不匹配";
                else if (txtNextPWD.Text != txtConfirmNextPWD.Text) litMsg.Text = "二次密码两次输入不匹配!";
                else
                {
                    mobj.LoginPWD = XCommon.Uitl.MD5(txtPassword.Text);
                    mobj.ManagePWD = XCommon.Uitl.MD5(txtNextPWD.Text);
                    mobj.UpdateTime = DateTime.Now;
                    if (bllXMember.Update(mobj))
                    {
                        litMsg.Text = "密码重置成功！";
                        Response.Redirect("./?cmd=member_login");
                    }
                    else
                    {
                        phMsg.Visible = true;
                        litMsg.Text = "重置失败，请重新操作";
                    }
                }
            }
            else
            {
                phMsg.Visible = true;
                litMsg.Text = "不存在该昵称用户";
            }
        }
    }
}