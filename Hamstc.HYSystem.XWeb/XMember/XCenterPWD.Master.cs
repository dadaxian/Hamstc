using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XCenterPWD :MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null) RedirectToLogin();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool isUpdate = false;
            #region 登录密码
            if (TextBox1.Text.Length>0 || TextBox2.Text.Length>0 || TextBox3.Text.Length>0)
            {
                isUpdate = true;
                if (TextBox1.Text.Length == 0) msg = "请输入原来密码";
                else if (TextBox2.Text.Length == 0) msg = "请输入新的密码";
                else if (TextBox2.Text!=TextBox3.Text) msg = "确认密码有误";
                else if (XCommon.Uitl.MD5(TextBox1.Text)!=CurrMember.LoginPWD) msg="原来密码有误";
                else
                {
                    CurrMember.LoginPWD=XCommon.Uitl.MD5(TextBox2.Text);
                    CurrMember.UpdateTime=DateTime.Now;
                    
                }
                
            }
            #endregion

            #region 二次密码
            if (TextBox4.Text.Length > 0 || TextBox5.Text.Length > 0 || TextBox6.Text.Length > 0)
            {
                isUpdate = true;
                if (TextBox4.Text.Length == 0) msg = "请输入原来密码";
                else if (TextBox5.Text.Length == 0) msg = "请输入新的密码";
                else if (TextBox5.Text != TextBox6.Text) msg = "两次输入不一致";
                else if (XCommon.Uitl.MD5(TextBox4.Text) != CurrMember.ManagePWD) msg = "原来密码有误";
                else
                {
                    CurrMember.ManagePWD =XCommon.Uitl.MD5(TextBox5.Text);
                    CurrMember.UpdateTime = DateTime.Now;
                    
                }

            }
            #endregion
            if (isUpdate)
            {
                if (msg.Length==0 )
                {
                    if (bllXMember.Update(CurrMember)) msg = "";
                    else
                    {
                        msg = "数据保存失败，请重新保存。";
                    }
                }
                
                if (msg.Length == 0) RedirectToLogin();
                else
                {
                    litMsg.Text = msg;
                    phMsg.Visible = true;
                }
            }
        }
    }
}