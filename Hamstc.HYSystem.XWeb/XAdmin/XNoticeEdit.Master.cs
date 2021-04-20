using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XNoticeEdit : AdminMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrAdmin == null) RedirectToLogin();
                else
                {
                    if (Request.QueryString["t"] != null && Request.QueryString["t"] == "edit")
                    {
                        //修改
                        XNotice nobj = bllXNotice.GetOneByID(Request.QueryString["id"]);
                        if (nobj != null)
                        {
                            txtTitle.Text = nobj.Title;
                            txtInfo.Text = nobj.Info;
                            txtIndex.Text = nobj.SortIndex.ToString();
                            ViewState["hid"] = nobj.NID;
                            ViewState["backurl"] = Request.UrlReferrer.ToString();
                        }
                    }
                    else
                    {
                        //新增
                        ViewState["hid"] = "";
                        ViewState["backurl"] = Request.UrlReferrer.ToString();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string tmpID = "";
            string backUrl = "";
            if (ViewState["hid"] != null) tmpID = ViewState["hid"].ToString();
            if (ViewState["backurl"] != null) backUrl = ViewState["backurl"].ToString();
            if (tmpID.Length > 0)
            {
                //修改
                XNotice nobj = bllXNotice.GetOneByID(tmpID);
                if (nobj != null)
                {
                    nobj.Title = txtTitle.Text;
                    nobj.Info = txtInfo.Text;
                    nobj.UpdateTime = DateTime.Now;
                    if (bllXNotice.Update(nobj))
                    {
                        JscriptMsg("已成功提交保存！", backUrl);
                    }
                    else
                    {

                        JscriptMsg("提交保存失败，请重新提交！", "");
                    }
                }
            }
            else 
            { 
            //新增
                XNotice nobj = new XNotice()
                {
                    NID = CreateID(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Info = txtInfo.Text,
                    IsDeleted = 0,
                    IsPublish = 1,
                    NType = "公告通知",
                    SortIndex = Convert.ToInt32(txtIndex.Text),
                    Title = txtTitle.Text

                };
                if (bllXNotice.Add(nobj))
                {
                    JscriptMsg("已成功提交保存！", backUrl);
                }
                else
                {

                    JscriptMsg("提交保存失败，请重新提交！", "");
                }
            }

        }
    }
}