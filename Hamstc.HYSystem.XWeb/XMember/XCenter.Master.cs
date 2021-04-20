using Hamstc.HYSystem.XWeb.XBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XCenter : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null) RedirectToLogin();
                else
                {
                    //判断是否输过二次密码
                    string cacheKey = string.Format("murl_{0}", CurrMember.MID);
                    PageAccess paobj = GetCache(cacheKey) as PageAccess;
                    if (paobj != null && paobj.IsChecked == true && paobj.Url == Request.Url.ToString())
                    {
                        //页面初始化
                        litLoginName.Text = CurrMember.NickName;
                        litGrade.Text = CurrMember.Grade.ToString();
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

        public string getFriendCount()
        {
            string tmp = "100";
            //获取好友数量
            string tmpPCodeStr = CurrMember.PCodeStr + CurrMember.InviteCode + "|";
            tmp = bllXMember.GetFriendCount(tmpPCodeStr).ToString();
            return tmp;
        }
    }
}