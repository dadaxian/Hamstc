using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hamstc.HYSystem.XWeb.XBase;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XFriend :MemberMaster
    {
        List<XModel.XMember> list;
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
                        InitTree();
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
        private void InitTree()
        {
            string tmpPCodeStr=CurrMember.PCodeStr+CurrMember.InviteCode+"|";
            list = bllXMember.GetListBySearch(string.Format("and PCodeStr like '{0}%'", CurrMember.PCodeStr + CurrMember.InviteCode + "|"));
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(string.Format("<a>{0}</a>", CurrMember.NickName));
            //递归加载子节点
            sb.Append(TreeStr(tmpPCodeStr));
            sb.Append("</li>");
            litTree.Text = sb.ToString();
        }
        private string TreeStr(string pcodeStr)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                List<XModel.XMember> tmpList = list.FindAll(delegate(XModel.XMember mobj)
                {
                    return mobj.PCodeStr == pcodeStr;
                });
                if (tmpList.Count > 0)
                {
                    sb.Append("<ul>");
                    foreach (var mobj in tmpList)
                    {
                        sb.Append("<li>");
                        sb.Append(string.Format("<a>{0}</a>", mobj.NickName));
                        //递归加载子节点
                        sb.Append(TreeStr(mobj.PCodeStr + mobj.InviteCode + "|"));
                        sb.Append("</li>");
                    }
                    sb.Append("</ul>");
                }
            }
            return sb.ToString();
        }
    }
}