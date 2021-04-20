using Hamstc.HYSystem.DAL;
using Hamstc.HYSystem.XModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.BLL
{
    public class XMemberInviteBLL
    {
        private XMemberInviteDAL dal;
        public XMemberInviteBLL()
        {
            dal = new XMemberInviteDAL();
        }
        public bool Add(XMemberInvite model)
        {
            return dal.Add(model);
        }
        public bool Update(XMemberInvite model)
        {
            return dal.Update(model);
        }

        public bool Delete(XMemberInvite model)
        {
            return dal.Delete(model);
        }
        public XMemberInvite GetOneById(string id)
        {
            return dal.GetOneById(id);
        }
        public XMemberInvite GetTopOneByMID(string mid)
        {
            return dal.GetTopOneByMID(mid);
        }
        public XMemberInvite GetTopOneByPCode(string pcodeStr)
        {
            return dal.GetTopOneByPCode(pcodeStr);
        }


    }
}
