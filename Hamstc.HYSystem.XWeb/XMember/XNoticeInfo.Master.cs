using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M = Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XNoticeInfo : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null)
                {
                    RedirectToLogin();
                }
                else
                {
                    M.XNotice nobj = null;
                    if (Request.QueryString["id"]!=null)
                    {
                        nobj = bllXNotice.GetOneByID(Request.QueryString["id"]);

                    }
                    if (nobj != null)
                    {
                        //初始化内容
                        litTitle.Text = nobj.Title;
                        litInfo.Text = nobj.Info;
                    }
                    else
                    {
                        Response.Redirect("./?cmd=member_notice");
                    }
                }
            }
        }
    }
}