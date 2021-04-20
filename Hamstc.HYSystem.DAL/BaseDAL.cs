using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hamstc.HYSystem.XCommon;
using System.Configuration;

namespace Hamstc.HYSystem.DAL
{
    public class BaseDAL
    {
        public IDbHelper dbHelper;
        public BaseDAL()
        {
            dbHelper=new SqlHelper(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        }
    }
}
