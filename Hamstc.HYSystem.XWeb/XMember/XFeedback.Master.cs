using Hamstc.HYSystem.XWeb.XBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XFeedback : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null)
                    RedirectToLogin();
                else
                {
                    //判断是否输过二次密码
                    string cacheKey = string.Format("murl_{0}", CurrMember.MID);
                    PageAccess paobj = GetCache(cacheKey) as PageAccess;
                    if (paobj != null && paobj.IsChecked == true && paobj.Url.Split('?')[1] == Request.Url.ToString().Split('?')[1])
                    {
                        //页面初始化
                        
                    }
                    else
                    {
                        paobj = new PageAccess()
                        {
                            IsChecked = false,
                            Url = Request.Url.ToString()
                        };
                        SetCache(cacheKey, paobj, DateTime.Now.AddMinutes(1));
                        Response.Redirect("./?cmd=member_manage");
                    }

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            XModel.XFeedback fobj = new XModel.XFeedback()
            {
                FID = CreateID(),
                MID = CurrMember.MID,
                FType = DropDownList1.SelectedItem.Text,
                Info = TextBox1.Text,
                IsReply = 0,
                SortIndex = 100,
                IsDeleted = 0,
                CreateTime = DateTime.Now,
                UpdateTime=DateTime.Now
            };
            if (bllXFeedback.Add(fobj))
            {
                phMsg.Visible = true;
                litMsg.Text = "反馈已提交";
                Response.Redirect("./?cmd=member_home");
            }
            else
            {
                phMsg.Visible = true;
                litMsg.Text = "数据异常，请重试";
            }
        }
    }
}