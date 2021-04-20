using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Hamstc.HYSystem.XCommon
{
    public  class SqlHelper:IDbHelper
    {
        //连接字符串
        private string ConnStr = "";
        public SqlHelper(string connstr)
        {
            this.ConnStr = connstr;
        }

        //执行
        public bool ExecuteNonQuery(string sql, DbParameter[] parms)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                if (parms != null)
                {
                    comm.Parameters.AddRange(parms);
                }
                result = comm.ExecuteNonQuery()>0;
                comm.Parameters.Clear();
                conn.Close();
            }
            return result;
        }
        //查询
        public object ExecuteScalar(string sql, DbParameter[] parms)
        {
            object result = false;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                if (parms != null)
                {
                    comm.Parameters.AddRange(parms);
                }
                result = comm.ExecuteScalar();
                comm.Parameters.Clear();
                conn.Close();
            }
            return result;
        }

        public DataSet Query(string sql, DbParameter[] parms)
        {
            DataSet ds=null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                if (parms != null)
                {
                    comm.Parameters.AddRange(parms);
                }
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = comm;
                sda.Fill(ds);
                comm.Parameters.Clear();
                conn.Close();
            }
            return ds;
        }

        public DbDataReader QueryReader(string sql, DbParameter[] parms)
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            if (parms != null)
            {
                comm.Parameters.AddRange(parms);
            }
            SqlDataReader sdr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            comm.Parameters.Clear();
            return sdr;
        }
    }
}
