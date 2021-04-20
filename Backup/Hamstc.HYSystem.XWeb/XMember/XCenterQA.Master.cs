using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XCenterQA : MemberMaster 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null) RedirectToLogin();
                else
                {
                    //密保问题初始化
                    for (int i = 0; i < DropDownList1.Items.Count; i++)
                    {
                        if (DropDownList1.Items[i].Text==CurrMember.Question)
                        {
                            DropDownList1.SelectedIndex = i;
                            break;
                        }
                    }
                    //密保答案初始化
                    TextBox1.Text = CurrMember.Answer;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (TextBox1.Text.Length == 0) msg = "请输入密保答案";
            else
            {
                CurrMember.Answer = TextBox1.Text;
                CurrMember.Question = DropDownList1.SelectedItem.Text;
                CurrMember.UpdateTime = DateTime.Now;
                if (bllXMember.Update(CurrMember) == false)
                {
                    msg = "数据保存失败，请重新提交";
                }
                if (msg.Length > 0)
                {
                    litMsg.Text = msg;
                    phMsg.Visible = true;
                }
                else
                {
                    Response.Redirect("./?cmd=member_center");
                }

            }
        }
        
    }
}