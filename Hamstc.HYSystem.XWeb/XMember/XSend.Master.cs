using Hamstc.HYSystem.XWeb.XBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XSend :MemberMaster
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
                        litLoginName.Text = CurrMember.NickName;
                        litGrade.Text = CurrMember.Grade.ToString();
                        BindRpt();
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
        private void BindRpt()
        {
            //以当前会员作为申请者查询对应业务
            List<XModel.XMemberUpgrade> list = bllXMemberUpgrade.GetListBySearch(string.Format("and SID='{0}'", CurrMember.MID));
            repeater1.DataSource = list;
            repeater1.DataBind();
        }

        protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //联系对象
            Literal litCID = e.Item.FindControl("litCID") as Literal;
            if (litCID != null)
            {
                XModel.XMember mobj = bllXMember.GetOneById(litCID.Text);
                if (mobj != null)
                {
                    litCID.Text = mobj.NickName;

                }
            }
            //状态
            Label litReply = e.Item.FindControl("litReply") as Label;
            if (litReply != null)
            {
                if (litReply.Text == "0")
                {
                    litReply.Text = "点这里";
                    litReply.CssClass = "label red circle";
                }
                else
                {
                    litReply.Text = "已确认";
                }
            }
        }

        protected void linkbtn1_Click(object sender, EventArgs e)
        {
            //申请显示转到其他页面完成
            Response.Redirect("./?cmd=member_send_info&to=add");
        }
    }
}