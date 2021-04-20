using Hamstc.HYSystem.DAL;
using Hamstc.HYSystem.XModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.BLL
{
    public class XMemberBLL
    {
        private XMemberDAL dal;
        public XMemberBLL()
        {
            dal = new XMemberDAL();
        }
        public bool Add(XMember model)
        {
            return dal.Add(model);
        }
        public bool Update(XMember model)
        {
            return dal.Update(model);
        }

        public bool Delete(XMember model)
        {
            return dal.Delete(model);
        }
        public XMember GetOneById(string id)
        {
            return dal.GetOneById(id);
        }
        public XMember GetOneByLogin(string name, string pwd)
        {
            return dal.GetOneByLogin(name, pwd);
        }
        public XMember GetOneByLogin(string sign)
        {
            return dal.GetOneByLogin(sign);
        }
        public int GetCountBySearch(string sqlWhere)
        {
            return dal.GetCountBySearch(sqlWhere);
        }
        public int GetFriendCount(string pcodeStr)
        {
            string sqlWhere = string.Format(" and PCodeStr like '{0}%'", pcodeStr);
            return dal.GetCountBySearch(sqlWhere);
        }
        public int CheckReg(string nickname, string wxcode, string phone)
        {
            return dal.CheckReg(nickname, wxcode, phone);
        }
        public XMember GetOneByInviteCode(string code)
        {
            return dal.GetOneByInviteCode(code);
        }

        public XModel.XMember GetOneByNickName(string NickName)
        {
            string sqlwhere = string.Format(" and NickName='{0}'",NickName);
            List<XModel.XMember> list = dal.GetListBySearch(sqlwhere);
            if (list.Count > 0) return list[0];
            return null;
        }
        public List<XMember> GetListBySearch(string sqlWhere)
        {
            return dal.GetListBySearch(sqlWhere);
        }
        public List<XMember> GetListBySearch(string sqlWhere, string orderBy)
        {
            return dal.GetListBySearch(sqlWhere, orderBy);
        }
        //分页
        public List<XMember> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            return dal.GetListBySearch(sqlWhere, pageIndex, pageSize, orderBy);
        }
    }
}
