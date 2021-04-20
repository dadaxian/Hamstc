using Hamstc.HYSystem.XWeb.XBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XManage : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null)
                    RedirectToLogin();
                else
                {
                    string cacheKey = string.Format("murl_{0}", CurrMember.MID);
                    PageAccess paObj = GetCache(cacheKey) as PageAccess;
                    if (paObj != null && paObj.Url.Length > 0)
                    {
                        hidViewState.Value = "./?" + paObj.Url.Split('?')[1];
                    }
                    else
                    {
                        Response.Redirect("./?cmd=member_home");
                    }
                     //cookie session
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CurrMember == null)
                RedirectToLogin();
            else
            {
                if (CurrMember.ManagePWD ==XCommon.Uitl.MD5(txtNextPWD.Text))
                {
                    string cache = string.Format("murl_{0}", CurrMember.MID);
                    PageAccess pa = new PageAccess()
                    {
                        IsChecked = true,
                        Url = hidViewState.Value
                    };
                    SetCache(cache, pa, DateTime.Now.AddMinutes(1));
                    Response.Redirect(pa.Url);
                }
                else
                {
                    litMsg.Text = "二次密码错误";
                    phMsg.Visible = true;
                }
            }
        }
    }
}