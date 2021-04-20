using Hamstc.HYSystem.DAL;
using Hamstc.HYSystem.XModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.BLL
{
    public class XNoticeBLL
    {
        private XNoticeDAL dal;
        public XNoticeBLL()
        {
            dal = new XNoticeDAL();
        }
        #region //操作
        public bool Add(XNotice model)
        {
            return dal.Add(model);
        }
        public bool Update(XNotice model)
        {
            return dal.Update(model);
        }
        public bool Delete(XNotice model)
        {
            return dal.Delete(model);
        }
        #endregion
        #region //查询
        public XNotice GetOneByID(string id)
        {
            return dal.GetOneByID(id);
        }
        public List<XNotice> GetListBySearch(string sqlWhere)
        {
            return dal.GetListBySearch(sqlWhere);
        }
        public List<XNotice> GetListBySearch(string sqlWhere, string orderBy)
        {
            return dal.GetListBySearch(sqlWhere, orderBy);
        }
        //分页
        public List<XNotice> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            return dal.GetListBySearch(sqlWhere, pageIndex, pageSize, orderBy);
        }
        public int GetCountBySearch(string sqlWhere)
        {
            return dal.GetCountBySearch(sqlWhere);
        }
        #endregion
    }
}
