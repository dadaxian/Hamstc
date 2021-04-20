using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XReplyInfo :MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null)
                    RedirectToLogin();
                else
                {
                    phMsg.Visible = false;
                    PlaceHolder1.Visible = false;
                    if (Request.QueryString["id"] != null)
                    {
                        XModel.XMemberUpgrade muobj = bllXMemberUpgrade.GetOneById(Request.QueryString["id"]);
                        if (muobj != null)
                        {
                            XModel.XMember mobj = bllXMember.GetOneById(muobj.SID);
                            if (mobj != null)
                            {
                                txtSJHM.Text = mobj.Phone;
                                txtWXH.Text = mobj.WXCode;
                                txtWXNC.Text = mobj.NickName;
                                txtGrade1.Text = muobj.Grade1.ToString();
                                txtGrade2.Text = muobj.Grade2.ToString();
                                if (muobj.IsReply == 0)
                                {
                                    PlaceHolder1.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btn_comfirm_Click(object sender, EventArgs e)
        {
            if (checkbox1.Checked)
            {
                string msg = "";
                string tmpID = "";
                if (Request.QueryString["id"] != null)
                {
                    tmpID = Request.QueryString["id"];
                    XModel.XMemberUpgrade muobj = bllXMemberUpgrade.GetOneById(tmpID);
                    if (muobj != null)
                    {
                        XModel.XMember mobj = bllXMember.GetOneById(muobj.SID);
                        if (mobj.Grade >= muobj.Grade2)
                        {
                            msg = "当前级别已经达到目标级别，无需确认！";

                        }
                        else
                        {
                            muobj.IsReply = 1;
                            muobj.UpdateTime = DateTime.Now;
                            if (bllXMemberUpgrade.Update(muobj))
                            {
                                mobj.Grade = muobj.Grade2;
                                mobj.UpdateTime = DateTime.Now;
                                if (bllXMember.Update(mobj))
                                {
                                    Response.Redirect("./?cmd=member_reply");
                                }
                                else
                                    msg = "数据操作异常，请重新提交";
                            }
                            else
                                msg = "数据操作异常，请重新提交";
                        }
                    }
                    else
                    {
                        Response.Redirect("./?cmd=member_reply");
                    }

                }
            }
            else
                Response.Redirect("./?cmd=member_reply");
        }
    }
}