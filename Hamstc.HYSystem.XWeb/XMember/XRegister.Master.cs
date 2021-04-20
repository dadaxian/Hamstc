using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M = Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XRegister : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //不需要验证登录
            if (!IsPostBack)
            {
                //没有开放注册，必须要邀请才可以注册
                if (Request.QueryString["ic"] == null)
                {
                    RedirectToLogin();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //注册步骤：
            //1、验证空值
            //2、验证昵称是否存在，因为微信昵称是作为登录名，唯一
            //3、微信号和手机号码验证，唯一
            //4、会员分支管理，2条分支，如果满了，往一下级查找空缺的位置
            //4.1  查找自己是否还有空缺
            //4.2  如果已满，往下查找空缺
            //5、如果没有了空缺位置怎么办？
            //6、会员注册完成之后，添加两个分支空缺
            string msg = "";
            #region //1、验证空值
            if (txtNickName.Text.Length == 0) msg = "微信昵称不能为空！";
            else if (bllXMember.GetCountBySearch(" and LoginName='" + txtNickName.Text + "'") > 0 ) msg = "该用户名已被注册";
            else if (txtWxCode.Text.Length == 0) msg = "微信号不能为空！";
            else if (txtPhone.Text.Length == 0) msg = "手机号码不能为空！";
            else if (TextBox1.Text.Length == 0) msg = "密保答案不能为空！";
            else if (txtPassword.Text.Length < 6) msg = "密码不得小于6位";
            else if (txtConfirmPassword.Text != txtPassword.Text) msg = "两次密码输入不一致";
            #endregion
            #region
            //2、验证昵称是否存在，因为微信昵称是作为登录名，唯一
            //3、微信号和手机号码验证，唯一
            else if (bllXMember.CheckReg(txtNickName.Text, txtWxCode.Text, txtPhone.Text) > 0)
            {
                msg = "微信昵称、微信号、手机号码已注册！";
            }
            #endregion
            #region //4、会员分支管理，2条分支，如果满了，往一下级查找空缺的位置
            else
            {
                string tmpCode = Request.QueryString["ic"];
                M.XMember mobj = bllXMember.GetOneByInviteCode(tmpCode);
                if (mobj != null)
                {
                    #region //唯一 邀请码
                    string tmpIC = "";
                    int tmpICCount = 0;
                    do
                    {
                        tmpIC = XCommon.CreateRandom.Code(6).ToLower(); //6位小写字母和数字
                        tmpICCount = bllXMember.GetCountBySearch(string.Format(" and InviteCode='{0}'", tmpIC));
                    }
                    while (tmpICCount > 0);
                    #endregion
                    #region  //4.1  查找自己是否还有空缺
                    M.XMemberInvite miObj = bllXMemberInvite.GetTopOneByMID(mobj.MID);
                    if (miObj != null)
                    {
                        #region //创建会员
                        M.XMember newOjb = new M.XMember()
                        {
                            MID = CreateID(),
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            Answer = TextBox1.Text,
                            IsDeleted = 0,
                            IsDisable = 0,
                            LoginPWD = XCommon.Uitl.MD5(txtPassword.Text),
                            ManagePWD = XCommon.Uitl.MD5("888888"),
                            CurrentCode = tmpCode,
                            Grade = 1,
                            InviteCode = tmpIC,
                            LoginIP = "",
                            LoginName = txtNickName.Text,
                            LoginSign = "",
                            LoginTime = DateTime.Now,
                            NickName = txtNickName.Text,
                            PCode = mobj.InviteCode,
                            PCodeStr = mobj.PCodeStr + mobj.InviteCode + "|", //
                            Phone = txtPhone.Text,
                            WXCode = txtWxCode.Text,
                            Question = DropDownList1.SelectedItem.Text,
                            Remark = "",
                            SortIndex = 100
                        };
                        if (bllXMember.Add(newOjb))
                        {
                            //创建成功
                            //修改自己的空缺名额，用掉了一个
                            miObj.IsAdd = 1;
                            miObj.IsReply = 1;
                            miObj.CID = newOjb.MID;
                            miObj.UpdateTime = DateTime.Now;
                            bllXMemberInvite.Update(miObj);
                            //给会员增加两个2个空缺名额
                            AddMemberInvite(newOjb);
                        }
                        else
                        {
                            msg = "数据保存失败，请重新提交。";
                        }
                        #endregion
                    }
                    #endregion
                    #region  //4.2  如果已满，往下查找空缺
                    else
                    {
                        miObj = bllXMemberInvite.GetTopOneByPCode(mobj.PCodeStr + mobj.InviteCode + "|");
                        if (miObj != null)
                        {
                            #region
                            M.XMember cobj = bllXMember.GetOneById(miObj.MID);
                            if (cobj != null)
                            {
                                #region //创建会员
                                M.XMember newOjb = new M.XMember()
                                {
                                    MID = CreateID(),
                                    CreateTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                    Answer = TextBox1.Text,
                                    IsDeleted = 0,
                                    IsDisable = 0,
                                    LoginPWD = XCommon.Uitl.MD5(txtPassword.Text),
                                    ManagePWD = XCommon.Uitl.MD5("888888"),
                                    CurrentCode = tmpCode,
                                    Grade = 1,
                                    InviteCode = tmpIC,
                                    LoginIP = "",
                                    LoginName = txtNickName.Text,
                                    LoginSign = "",
                                    LoginTime = DateTime.Now,
                                    NickName = txtNickName.Text,
                                    PCode = cobj.InviteCode, //需要修改地方
                                    PCodeStr = cobj.PCodeStr + cobj.InviteCode + "|",  //需要修改地方
                                    Phone = txtPhone.Text,
                                    WXCode = txtWxCode.Text,
                                    Question = DropDownList1.SelectedItem.Text,
                                    Remark = "",
                                    SortIndex = 100
                                };
                                if (bllXMember.Add(newOjb))
                                {
                                    //创建成功
                                    //修改自己的空缺名额，用掉了一个
                                    miObj.IsAdd = 1;
                                    miObj.IsReply = 1;
                                    miObj.CID = newOjb.MID;
                                    miObj.UpdateTime = DateTime.Now;
                                    bllXMemberInvite.Update(miObj);
                                    //给会员增加两个2个空缺名额
                                    AddMemberInvite(newOjb);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region  //5、如果没有了空缺位置怎么办？
                            M.XMember pobj = bllXMember.GetOneById("12980");
                            if (pobj != null)
                            {
                                #region //创建会员
                                M.XMember newOjb = new M.XMember()
                                {
                                    MID = CreateID(),
                                    CreateTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                    Answer = TextBox1.Text,
                                    IsDeleted = 0,
                                    IsDisable = 0,
                                    LoginPWD = XCommon.Uitl.MD5(txtPassword.Text),
                                    ManagePWD = XCommon.Uitl.MD5("888888"),
                                    CurrentCode = tmpCode,
                                    Grade = 1,
                                    InviteCode = tmpIC,
                                    LoginIP = "",
                                    LoginName = txtNickName.Text,
                                    LoginSign = "",
                                    LoginTime = DateTime.Now,
                                    NickName = txtNickName.Text,
                                    PCode = pobj.InviteCode, //需要修改地方
                                    PCodeStr = pobj.PCodeStr + pobj.InviteCode + "|",  //需要修改地方
                                    Phone = txtPhone.Text,
                                    WXCode = txtWxCode.Text,
                                    Question = DropDownList1.SelectedItem.Text,
                                    Remark = "",
                                    SortIndex = 100
                                };
                                if (bllXMember.Add(newOjb))
                                {
                                    //创建成功                                
                                    //给会员增加两个2个空缺名额
                                    AddMemberInvite(newOjb);
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    msg = "当前会员无资格邀请注册！";
                }
            }
            #endregion
            if (msg.Length == 0)
            {
                //成功
                RedirectToLogin();
            }
            else
            {
                //失败
                phMsg.Visible = true;
                litMsg.Text = msg;
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

        protected void txtWxCode_TextChanged(object sender, EventArgs e)
        {
            phMsg.Visible = false;
            litMsg.Text = "";
            string msg = "";
            if (bllXMember.GetCountBySearch(" and LoginName='" + txtNickName.Text + "'") > 0) {
                msg = "该用户名已被注册";
                phMsg.Visible = true;
                litMsg.Text = msg;
            } 
        }
    }
}