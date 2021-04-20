using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M = Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XNotice : MemberMaster
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
                    BindRpt();
                }
            }
        }

        //绑定通知列表
        private void BindRpt()
        {
            //按照排序索引（越大越靠前），发布时间（最新发布的靠前）
            List<M.XNotice> list = bllXNotice.GetListBySearch("", "SortIndex desc,UpdateTime desc");
            Repeater1.DataSource = list;
            Repeater1.DataBind();
        }
    }
}