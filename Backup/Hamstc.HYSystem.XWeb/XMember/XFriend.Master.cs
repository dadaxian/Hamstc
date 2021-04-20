using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

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
                    InitTree();
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