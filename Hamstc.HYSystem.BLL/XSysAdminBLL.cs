using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hamstc.HYSystem.XModel;
using Hamstc.HYSystem.DAL;

namespace Hamstc.HYSystem.BLL
{
    public class XSysAdminBLL
    {
        private XSysAdminDAL dal;
        public XSysAdminBLL()
        {
            dal = new XSysAdminDAL();
        }

        public bool Add(XSysAdmin model)
        {
            return dal.Add(model);
        }

        public bool Update(XSysAdmin model)
        {
            return dal.Update(model);
        }

        public bool UpdateLoginSign(XSysAdmin model)
        {
            return dal.UpdateLoginSign(model);
        }

        public bool Delete(XSysAdmin model)
        {
            return dal.Delete(model);
        }

        public XSysAdmin GetOneById(string id)
        {
            return dal.GetOneById(id);
        }

        public XSysAdmin GetOneByLogin(string name, string pwd)
        {
            return dal.GetOneByLogin(name,pwd);
        }

        public XSysAdmin GetOneByLogin(string sign)
        {
            return dal.GetOneByLogin(sign);
        }

        public List<XSysAdmin> GetListAll()
        {
            return dal.GetListAll();
        }
    }
}
