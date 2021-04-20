using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hamstc.HYSystem.DAL;
using Hamstc.HYSystem.XModel;

namespace Hamstc.HYSystem.BLL
{
    public class XFeedbackBLL
    {
        XFeedbackDAL dalXFeedback;
        public XFeedbackBLL() {
            dalXFeedback = new XFeedbackDAL();
        }
        #region//操作
        //新增
        public bool Add(XFeedback model)
        {
            return dalXFeedback.Add(model);
        }
        //修改
        public bool Update(XFeedback model)
        {
            return dalXFeedback.Update(model);
        }

        //删除
        public bool Delete(XFeedback model)
        {
            return dalXFeedback.Delete(model);   
        }
        #endregion

        #region//查询
        // GetOneById 没写


        public List<XFeedback> GetListAll()
        {
            return dalXFeedback.GetListAll();
        }

        public XFeedback GetOneByID(string id)
        {
            return dalXFeedback.GetOneByID(id);
        }
        //返回数量
        public int GetCountBySearch(string sqlWhere)
        {
            return dalXFeedback.GetCountBySearch(sqlWhere);
        }
        public List<XFeedback> GetListBySearch(string sqlWhere)
        {
            return dalXFeedback.GetListBySearch(sqlWhere);
        }
        public List<XFeedback> GetListBySearch(string sqlWhere, string orderBy)
        {
            return dalXFeedback.GetListBySearch(sqlWhere, orderBy);
        }
        //分页
        public List<XFeedback> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            return dalXFeedback.GetListBySearch(sqlWhere, pageIndex, pageSize, orderBy);
        }

        #endregion
    }
}
