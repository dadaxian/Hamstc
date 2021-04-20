using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hamstc.HYSystem.DAL;
using Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.BLL
{
    public class XMemberUpgradeBLL
    {
        XMemberUpgradeDAL dalXMemberUpgrade;
        public XMemberUpgradeBLL()
        {
            dalXMemberUpgrade = new XMemberUpgradeDAL();
        }
        #region//操作


        public bool Add(XMemberUpgrade model)
        {
            return dalXMemberUpgrade.Add(model);
        }

        public bool Update(XMemberUpgrade model)
        {
            return dalXMemberUpgrade.Update(model);
        }

        public bool Delete(XMemberUpgrade model)
        {
            return dalXMemberUpgrade.Delete(model);
        }
        #endregion


        #region//查询

        public XMemberUpgrade GetOneById(string id)
        {
            return dalXMemberUpgrade.GetOneById(id);
        }

        public List<XMemberUpgrade> GetListBySearch(string sqlWhere)
        {
            return dalXMemberUpgrade.GetListBySearch(sqlWhere);
        }
        public List<XMemberUpgrade> GetListBySearch(string sqlWhere, string orderBy)
        {
            return dalXMemberUpgrade.GetListBySearch(sqlWhere, orderBy);
        }
        //分页
        public List<XMemberUpgrade> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            return GetListBySearch(sqlWhere, pageIndex, pageSize, orderBy);
        }

        public List<XMemberUpgrade> GetListAll()
        {
            return dalXMemberUpgrade.GetListAll();
        }

        public int GetCountBySearch(string sqlWhere)
        {
            return dalXMemberUpgrade.GetCountBySearch(sqlWhere);
        }
        #endregion
    }
}
