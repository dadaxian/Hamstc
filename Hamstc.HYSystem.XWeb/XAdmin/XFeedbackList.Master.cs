using Hamstc.HYSystem.XWeb.XBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XFeedbackList : AdminMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrAdmin != null)
                {
                    #region //分页信息
                    if (Request.QueryString["page"] != null)
                    {
                        int.TryParse(Request.QueryString["page"].ToString(), out mPageIndex);
                        if (mPageIndex < 1) mPageIndex = 1;
                    }
                    if (Request.QueryString["pagesize"] != null)
                    {
                        int.TryParse(Request.QueryString["pagesize"].ToString(), out mPageSize);
                        if (mPageSize < 1) mPageSize = 20;
                        txtPageNum.Text = mPageSize.ToString();
                    }
                    #endregion
                    BindRpt("");
                }
                else RedirectToLogin();
            }
        }

        private void BindRpt(string sqlwhere)
        {
            int.TryParse(txtPageNum.Text, out mPageSize);
            if (mPageSize < 1) mPageSize = 20;

            StringBuilder sb = new StringBuilder();

            if (txtKeywords.Text.Length > 0)
                sb.Append(string.Format(" and Info like '%{0}%'  ", txtKeywords.Text));
            sb.Append(sqlwhere);
            int total = bllXFeedback.GetCountBySearch(sb.ToString());
            List<XModel.XFeedback> list = bllXFeedback.GetListBySearch(sb.ToString(), mPageIndex, mPageSize, " SortIndex desc,FID desc");

            rptList1.DataSource = list;
            rptList1.DataBind();

            txtTotal.Text = total.ToString();
            txtPageNum.Text = mPageSize.ToString();
            int pageCount = total / mPageSize;
            if (total % mPageSize > 0) pageCount += 1;
            List<MyPageItem> pageList = new List<MyPageItem>();
            for (int i = 1; i <= pageCount; i++)
            {
                pageList.Add(new MyPageItem()
                {
                    Index = i,
                    Selected = i == mPageIndex
                });
            }
            rptPage.DataSource = pageList;
            rptPage.DataBind();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            mPageIndex = 1;
            BindRpt("");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptList1.Items.Count; i++)
            {
                string id = ((HiddenField)rptList1.Items[i].FindControl("hidId")).Value;
                CheckBox cb = (CheckBox)rptList1.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    #region //日志

                    #endregion
                    bllXFeedback.Delete(new XModel.XFeedback()
                    {
                        FID = id,
                        UpdateTime = DateTime.Now,
                        IsDeleted = 1
                    });
                }
            }
            JscriptMsg("删除数据成功！", Request.Url.ToString());
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(txtPageNum.Text, out mPageSize);
            if (mPageSize < 1) mPageSize = 20;
            mPageIndex = 1;
            BindRpt("");
        }

        protected void rptPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnPage = e.Item.FindControl("lbtnPage") as LinkButton;
            Label labPage = e.Item.FindControl("labPage") as Label;
            HiddenField hidPage = e.Item.FindControl("hidPage") as HiddenField;
            if (lbtnPage != null && labPage != null && hidPage != null)
            {
                if (hidPage.Value.ToLower() == "true")
                {
                    labPage.Visible = true;
                    lbtnPage.Visible = false;
                }
                else
                {
                    labPage.Visible = false;
                    lbtnPage.Visible = true;
                }
            }
        }

        protected void rptList1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "lbtnConfirm")
            {
               XModel.XFeedback fobj=bllXFeedback.GetOneByID(e.CommandArgument.ToString());
               fobj.IsReply = 1;
                if (bllXFeedback.Update(fobj))
                {
                    Response.Redirect("./?cmd=admin_feedback");
                }
                else
                {
                    JscriptMsg("确认失败，请重试","");
                }
            }
        }

        protected void rptPage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "lbtnPage")
            {
                int.TryParse(e.CommandArgument.ToString(), out mPageIndex);
                if (mPageIndex < 1) mPageIndex = 1;
                BindRpt("");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlstr = " and FType='" + DropDownList1.SelectedItem.Text.ToString()+"' ";
            BindRpt(sqlstr);
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            string sqlstr = " and FType='" + DropDownList1.SelectedItem.Text.ToString() + "' ";
            BindRpt(sqlstr);
        }

        protected void rptList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Literal litFID = e.Item.FindControl("litFID") as Literal;
            if (litFID != null)
            {
                litFID.Text = bllXMember.GetOneById(litFID.Text).NickName;
            }
        }
    }

     
}
