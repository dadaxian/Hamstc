using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Hamstc.HYSystem.XCommon
{
    public  interface IDbHelper
    {
        bool ExecuteNonQuery(string sql, DbParameter[] parms);

        object ExecuteScalar(string sql, DbParameter[] parms);

        DataSet Query(string sql, DbParameter[] parms);

        DbDataReader QueryReader(string sql, DbParameter[] parms);

    }
}
