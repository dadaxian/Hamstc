using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XMember
{
    public partial class XSendInfo : MemberMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrMember == null)
                    RedirectToLogin();
                else
                {
                    if (Request.QueryString["to"] != null)
                    {
                        //添加
                        //1.是否满级，最高十级
                        // 2.判断是否需要新增记录，不允许重复申请，不允许跳级
                        // 3.如果需要新增，判断层级关系，申请的级别与上级层次有关，联系上一级
                        // 3.1层级关系层次不够，直接找根节点
                        // 3.2对应级的级别不够，往上追溯
                        if (CurrMember.Grade < 10)
                        {
                            AddSend();
                        }
                        else
                        {
                            litMsg.Text = "你已经是最高级别，无需升级！";
                        }
                    }
                    else
                    {
                        #region 显示
                        if (Request.QueryString["id"] != null)
                        {
                            XModel.XMemberUpgrade muobj = bllXMemberUpgrade.GetOneById(Request.QueryString["id"]);
                            if (muobj != null)
                            {
                                XModel.XMember mobj = bllXMember.GetOneById(muobj.CID);
                                if (mobj != null)
                                {
                                    txtSJHM.Text = mobj.Phone;
                                    txtWXH.Text = mobj.WXCode;
                                    txtWXNC.Text = mobj.NickName;
                                    if (muobj.IsReply == 0)
                                        litMsg.Text = "申请已发送，请联系对方进行升级操作！";
                                    else
                                        litMsg.Text = "升级操作完成！";
                                }
                                else
                                    litMsg.Text = "参数异常，未获取到数据";
                            }
                            else
                                litMsg.Text = "参数异常，未获取到数据";
                        }
                        else
                            litMsg.Text = "参数异常，未获取到数据";
                        #endregion
                    }

                }
            }
        }
        private void AddSend()
        {
            int count = bllXMemberUpgrade.GetCountBySearch(string.Format(" and IsReply=0 and SID='{0}'", CurrMember.MID));
            if (count > 0)
            {
                litMsg.Text = "申请已发送，请联系相关人员升级！";
                List<XModel.XMember> tmpList = bllXMember.GetListBySearch(
                    string.Format(" and MID in(select CID from XMemberUpgrade IsDeleted=0 and IsReply=0 and SID='{0}')", CurrMember.MID)
                    );
                if (tmpList.Count > 0)
                {
                    XModel.XMember mobj = tmpList[0];
                    txtSJHM.Text = mobj.Phone;
                    txtWXH.Text = mobj.WXCode;
                    txtWXNC.Text = mobj.NickName;
                }
            }
            else
            {
                #region 需要新增记录
                string[] _pcodeArr = CurrMember.PCodeStr.Split('|');
                string[] pcodeArr = _pcodeArr.Reverse().ToArray();
                if (CurrMember.Grade >= pcodeArr.Length - 1)
                {
                    //层级不够
                    XModel.XMember pobj = bllXMember.GetOneByInviteCode(pcodeArr[pcodeArr.Length - 1]);
                    if (pobj != null && pobj.Grade > CurrMember.Grade && pobj.IsDisable == 0)
                    {
                        #region 新增申请
                        XModel.XMemberUpgrade muobj = new XModel.XMemberUpgrade()
                        {
                            MUID = CreateID(),
                            CID = pobj.MID,
                            SID = CurrMember.MID,
                            CreateTime = DateTime.Now,
                            Grade1 = CurrMember.Grade,
                            Grade2 = CurrMember.Grade + 1,
                            IsDeleted = 0,
                            IsReply = 0,
                            SortIndex = 100,
                            UpdateTime = DateTime.Now
                        };
                        #endregion
                        if (bllXMemberUpgrade.Add(muobj))
                        {
                            txtSJHM.Text = pobj.Phone;
                            txtWXH.Text = pobj.WXCode;
                            txtWXNC.Text = pobj.NickName;
                            litMsg.Text = "申请已发送，请联系相关人员升级！";
                        }
                        else
                            litMsg.Text = "申请发送异常！，请重新申请";
                    }
                    else
                    {
                        litMsg.Text = "未匹配到合适人员为您升级，请联系管理员！";
                    }
                }
                else
                {
                    //层次关系够用
                    //3.2对应层级级别不够，往上追溯
                    for (int i = CurrMember.Grade + 1; i < pcodeArr.Length; i++)
                    {
                        XModel.XMember pobj = bllXMember.GetOneByInviteCode(pcodeArr[i]);
                        if (pobj != null && pobj.Grade > CurrMember.Grade && pobj.IsDisable == 0)
                        {
                            #region 新增申请
                            XModel.XMemberUpgrade muobj = new XModel.XMemberUpgrade()
                            {
                                MUID = CreateID(),
                                CID = pobj.MID,
                                SID = CurrMember.MID,
                                CreateTime = DateTime.Now,
                                Grade1 = CurrMember.Grade,
                                Grade2 = CurrMember.Grade + 1,
                                IsDeleted = 0,
                                IsReply = 0,
                                SortIndex = 100,
                                UpdateTime = DateTime.Now
                            };
                            #endregion
                            if (bllXMemberUpgrade.Add(muobj))
                            {
                                txtSJHM.Text = pobj.Phone;
                                txtWXH.Text = pobj.WXCode;
                                txtWXNC.Text = pobj.NickName;
                                litMsg.Text = "申请已发送，请联系相关人员升级！";
                            }
                            else
                                litMsg.Text = "申请发送异常！，请重新申请";
                            break;
                        }
                    }
                }
                #endregion
            }
        }
    }
}