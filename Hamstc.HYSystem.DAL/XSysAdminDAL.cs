using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hamstc.HYSystem.XCommon;
using Hamstc.HYSystem.XModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;


namespace Hamstc.HYSystem.DAL
{
    public class XSysAdminDAL:BaseDAL
    {
        public XSysAdminDAL():base()
        {

        }
        #region//操作
        //新增管理员
        public bool Add(XSysAdmin model)
        {
            string sql = string.Format(@"insert into XSysAdmin
                                (SAID,LoginName,LoginPWD,LoginSign,Grade,IsDisable,DisableTime,SortIndex,IsDeleted,CreateTime,UpdateTime)
                                values(@SAID,@LoginName,@LoginPWD,@LoginSign,@Grade,@IsDisable,@DisableTime,@SortIndex,@IsDeleted,@CreateTime,@UpdateTime)");
            DbParameter[] parms ={
                                new SqlParameter("@SAID",SqlDbType.VarChar,50){Value=model.SAID},
                                new SqlParameter("@LoginName",SqlDbType.VarChar,50){Value=model.LoginName},
                                new SqlParameter("@LoginPWD",SqlDbType.VarChar,1000){Value=model.LoginPWD},
                                new SqlParameter("@LoginSign",SqlDbType.VarChar,50){Value=model.LoginSign},
                                new SqlParameter("@Grade",SqlDbType.Int){Value=model.Grade},
                                new SqlParameter("@IsDisable",SqlDbType.Int){Value=model.IsDisable},
                                new SqlParameter("@DisableTime",SqlDbType.DateTime){Value=model.DisableTime},
                                new SqlParameter("@SortIndex",SqlDbType.Int){Value=model.SortIndex},
                                new SqlParameter("@IsDeleted",SqlDbType.Int){Value=model.IsDeleted},
                                new SqlParameter("@CreateTime",SqlDbType.DateTime){Value=model.CreateTime},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql,parms);
        }
        //修改管理员
        public bool Update(XSysAdmin model)
        {
            string sql = string.Format(@"update XSysAdmin
                set LoginName=@LoginName,LoginPWD=@LoginPWD,LoginSign=@LoginSign,Grade=@Grade,IsDisable=@IsDisable,
                    DisableTime=@DisableTime,SortIndex=@SortIndex,IsDeleted=@IsDeleted,CreateTime=@CreateTime,
                    UpdateTime=@UpdateTime
                where SAID=@SAID");
            DbParameter[] parms ={
                                new SqlParameter("@SAID",SqlDbType.VarChar,50){Value=model.SAID},
                                new SqlParameter("@LoginName",SqlDbType.VarChar,50){Value=model.LoginName},
                                new SqlParameter("@LoginPWD",SqlDbType.VarChar,1000){Value=model.LoginPWD},
                                new SqlParameter("@LoginSign",SqlDbType.VarChar,50){Value=model.LoginSign},
                                new SqlParameter("@Grade",SqlDbType.Int){Value=model.Grade},
                                new SqlParameter("@IsDisable",SqlDbType.Int){Value=model.IsDisable},
                                new SqlParameter("@DisableTime",SqlDbType.DateTime){Value=model.DisableTime},
                                new SqlParameter("@SortIndex",SqlDbType.Int){Value=model.SortIndex},
                                new SqlParameter("@IsDeleted",SqlDbType.Int){Value=model.IsDeleted},
                                new SqlParameter("@CreateTime",SqlDbType.DateTime){Value=model.CreateTime},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        //修改登录签名
        public bool UpdateLoginSign(XSysAdmin model)
        {
            string sql = string.Format(@"update XSysAdmin
                set LoginSign=@LoginSign, 
                    UpdateTime=@UpdateTime
                where SAID=@SAID");
            DbParameter[] parms ={
                                new SqlParameter("@SAID",SqlDbType.VarChar,50){Value=model.SAID},
                                new SqlParameter("@LoginSign",SqlDbType.VarChar,50){Value=model.LoginSign},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        //删除
        public bool Delete(XSysAdmin model)
        {
            string sql = string.Format(@"update XSysAdmin set
                               IsDeleted=@IsDeleted,UpdateTime=@UpdateTime where
                                SAID=@SAID");
            DbParameter[] parms ={
                                new SqlParameter("@SAID",SqlDbType.VarChar,50){Value=model.SAID},
                                new SqlParameter("@IsDeleted",SqlDbType.Int){Value=model.IsDeleted},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        #endregion

        #region//查询
        private List<XSysAdmin> DataReaderToList(DbDataReader dr)
        {
            List<XSysAdmin> list = new List<XSysAdmin>();
            if (dr != null && dr.IsClosed==false)
            {
                while (dr.Read())
                {
                    list.Add(new XSysAdmin()
                        {
                            SAID = Convert.ToString(dr["SAID"]),
                            LoginName = Convert.IsDBNull(dr["LoginName"]) ? "" : Convert.ToString(dr["LoginName"]),
                            LoginPWD = Convert.ToString(dr["LoginPWD"]),
                            LoginSign = Convert.ToString(dr["LoginSign"]),
                            CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                            DisableTime = Convert.ToDateTime(dr["DisableTime"]),
                            Grade = Convert.ToInt32(dr["Grade"]),
                            IsDeleted = Convert.ToInt32(dr["IsDeleted"]),
                            IsDisable = Convert.ToInt32(dr["IsDisable"]),
                            SortIndex = Convert.ToInt32(dr["SortIndex"]),
                            UpdateTime = Convert.ToDateTime(dr["UpdateTime"])
                        });
                }
                dr.Close();
            }
            return list;
        }

        public XSysAdmin GetOneById(string id)
        {
            string sql = string.Format("select * from XSysAdmin where  STAID=@STAID and IsDeleted=0");//IsDeleted
            DbParameter[] parms ={
                                    new SqlParameter("@STAID",SqlDbType.VarChar,50){Value=id}                                
                                 };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XSysAdmin> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }

        public XSysAdmin GetOneByLogin(string name, string pwd)
        {
            string sql = string.Format("select * from XSysAdmin where LoginName=@LoginName and LoginPWD=@LoginPWD and IsDeleted=0");//IsDeleted
            DbParameter[] parms ={
                                    new SqlParameter("@LoginName",SqlDbType.VarChar,50){Value=name},
                                    new SqlParameter("@LoginPWD",SqlDbType.VarChar,1000){Value=pwd}
                                 };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XSysAdmin> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }


        public XSysAdmin GetOneByLogin(string sign)
        {
            string sql = string.Format("select * from XSysAdmin where LoginSign=@LoginSign and IsDeleted=0");//IsDelete=0;
            DbParameter[] parms ={
                                new SqlParameter("@LoginSign",SqlDbType.VarChar,100){Value=sign}
                                             };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XSysAdmin> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }

        public List<XSysAdmin> GetListAll()
        {
            string sql = string.Format("select * from XSysAdmin where IsDeleted=0 order by SortIndex desc,SAID asc");
            return DataReaderToList(dbHelper.QueryReader(sql,null));
        }

        #endregion
    }
}
