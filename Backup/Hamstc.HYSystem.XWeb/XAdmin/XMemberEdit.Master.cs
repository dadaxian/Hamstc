using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M= Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XMemberEdit :AdminMaster
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
                        XModel.XMember nobj = bllXMember.GetOneById(Request.QueryString["id"]);
                        if (nobj != null)
                        {
                            txtNickName.Text = nobj.NickName;
                            txtNickName.Enabled = false;
                            txtWxCode.Text = nobj.WXCode;
                            txtPhone.Text = nobj.Phone;
                            TextBox1.Text = nobj.Answer;
                            DropDownList1.Text = nobj.Question;
                            ViewState["hid"] = nobj.MID;
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
                XModel.XMember nobj = bllXMember.GetOneById(tmpID);
                if (nobj != null)
                {
                    nobj.NickName = txtNickName.Text;
                    nobj.WXCode = txtWxCode.Text;
                    nobj.Phone = txtPhone.Text;
                    nobj.Answer = TextBox1.Text;
                    nobj.Question = DropDownList1.Text;
                    nobj.UpdateTime = DateTime.Now;
                    if (bllXMember.Update(nobj))
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
                if (bllXMember.CheckReg(txtNickName.Text, txtWxCode.Text, txtPhone.Text) > 0)
                {
                    string msg = "微信昵称、微信号、手机号码已注册！";
                    JscriptMsg(msg, "");
                }
                #region 唯一 邀请码
                string tmpIC = "";
                int tmpICCount = 0;
                do
                {
                    tmpIC = XCommon.CreateRandom.Code(6).ToLower(); //6位小写字母和数字
                    tmpICCount = bllXMember.GetCountBySearch(string.Format(" and InviteCode='{0}'", tmpIC));
                }
                while (tmpICCount > 0);
                #endregion
                //新增
                XModel.XMember pobj;
                if (txtInviteID.Text == "")
                {
                    pobj = new XModel.XMember()
                    {
                        MID = "origin",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Answer = "origin",
                        IsDeleted = 0,
                        IsDisable = 0,
                        LoginPWD = "123456",
                        ManagePWD = "888888",
                        CurrentCode = "1",
                        Grade = 1,
                        InviteCode = tmpIC,
                        LoginIP = "",
                        LoginName = txtNickName.Text,
                        LoginSign = "",
                        LoginTime = DateTime.Now,
                        NickName = txtNickName.Text,
                        PCode = "origin",
                        PCodeStr = "origin",
                        Phone = "origin",
                        WXCode = "origin",
                        Question = "origin",
                        Remark = "",
                        SortIndex = 1000
                    };
                }
                else
                 pobj= bllXMember.GetOneById(txtInviteID.Text);
                XModel.XMember nobj = new XModel.XMember()
                {
                    MID = CreateID(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Answer = TextBox1.Text,
                    IsDeleted = 0,
                    IsDisable = 0,
                    LoginPWD = "123456",
                    ManagePWD = "888888",
                    CurrentCode = pobj.CurrentCode,
                    Grade = 1,
                    InviteCode = tmpIC,
                    LoginIP = "",
                    LoginName = txtNickName.Text,
                    LoginSign = "",
                    LoginTime = DateTime.Now,
                    NickName = txtNickName.Text,
                    PCode = pobj.InviteCode,
                    PCodeStr = pobj.PCodeStr + pobj.InviteCode + "|", 
                    Phone = txtPhone.Text,
                    WXCode = txtWxCode.Text,
                    Question = DropDownList1.SelectedItem.Text,
                    Remark = "",
                    SortIndex = 100
                };
                if (bllXMember.Add(nobj))
                {
                    AddMemberInvite(nobj);
                    JscriptMsg("已成功提交保存！", backUrl);
                }
                else
                {

                    JscriptMsg("提交保存失败，请重新提交！", "");
                }
            }
        }
        
        private void AddMemberInvite(M.XMember mobj)
        {
            for (int i = 0; i < 2; i++)
            {
                M.XMemberInvite miobj = new M.XMemberInvite()
                {
                    MIID = CreateID() + i, //防止重复
                    MID = mobj.MID,
                    CID = "",
                    CreateTime = DateTime.Now,
                    Grade1 = 1,
                    Grade2 = 2,
                    IsAdd = 0,
                    IsDeleted = 0,
                    IsReply = 0,
                    SortIndex = 100,
                    UpdateTime = DateTime.Now
                };
                bllXMemberInvite.Add(miobj);
            }
        }
    }
}