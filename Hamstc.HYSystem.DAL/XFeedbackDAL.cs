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
    public class XFeedbackDAL : BaseDAL
    {
        public XFeedbackDAL()
            : base()
        {

        }
        #region//操作
        //新增
        public bool Add(XFeedback model)
        {
            string sql = string.Format(@"insert into XFeedback
                                (FID,MID,FType,Info,IsReply,SortIndex,IsDeleted,CreateTime,UpdateTime)
                                values(@FID,@MID,@FType,@Info,@IsReply,@SortIndex,@IsDeleted,@CreateTime,@UpdateTime)");
            DbParameter[] parms ={
                                new SqlParameter("@FID",SqlDbType.VarChar,50){Value=model.FID},
                                new SqlParameter("@MID",SqlDbType.VarChar,50){Value=model.MID},
                                new SqlParameter("@FType",SqlDbType.VarChar,50){Value=model.FType},
                                new SqlParameter("@Info",SqlDbType.Text){Value=model.Info},
                                new SqlParameter("@IsReply",SqlDbType.Int){Value=model.IsReply},
                                new SqlParameter("@SortIndex",SqlDbType.Int){Value=model.SortIndex},
                                new SqlParameter("@IsDeleted",SqlDbType.Int){Value=model.IsDeleted},
                                new SqlParameter("@CreateTime",SqlDbType.DateTime){Value=model.CreateTime},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        //修改
        public bool Update(XFeedback model)
        {
            string sql = string.Format(@"update XFeedback set
                                MID=@MID,FType=@FType,Info=@Info,IsReply=@IsReply,SortIndex=@SortIndex,IsDeleted=@IsDeleted,CreateTime=@CreateTime,UpdateTime=@UpdateTime 
                            where  FID=@FID");

            DbParameter[] parms ={
                              new SqlParameter("@FID",SqlDbType.VarChar,50){Value=model.FID},
                                new SqlParameter("@MID",SqlDbType.VarChar,50){Value=model.MID},
                                new SqlParameter("@FType",SqlDbType.VarChar,50){Value=model.FType},
                                new SqlParameter("@Info",SqlDbType.Text){Value=model.Info},
                                new SqlParameter("@IsReply",SqlDbType.Int){Value=model.IsReply},
                                new SqlParameter("@SortIndex",SqlDbType.Int){Value=model.SortIndex},
                                new SqlParameter("@IsDeleted",SqlDbType.Int){Value=model.IsDeleted},
                                new SqlParameter("@CreateTime",SqlDbType.DateTime){Value=model.CreateTime},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }


        //修改登录签名




        //删除
        public bool Delete(XFeedback model)
        {
            string sql = string.Format(@"update XFeedback set
                               IsDeleted=@IsDeleted,UpdateTime=@UpdateTime where
                                FID=@FID");
            DbParameter[] parms ={
                                new SqlParameter("@FID",SqlDbType.VarChar,50){Value=model.FID},
                                new SqlParameter("@IsDeleted",SqlDbType.Int){Value=model.IsDeleted},
                                new SqlParameter("@UpdateTime",SqlDbType.DateTime){Value=model.UpdateTime}
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        #endregion

        #region//查询
        private List<XFeedback> DataReaderToList(DbDataReader dr)
        {
            List<XFeedback> list = new List<XFeedback>();
            if (dr != null && dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    list.Add(new XFeedback()
                    {
                        FID = Convert.ToString(dr["FID"]),
                        MID = Convert.ToString(dr["MID"]),
                        FType = Convert.ToString(dr["FType"]),
                        Info = Convert.ToString(dr["Info"]),
                        IsReply = Convert.ToInt32(dr["IsReply"]),
                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        IsDeleted = Convert.ToInt32(dr["IsDeleted"]),
                        SortIndex = Convert.ToInt32(dr["SortIndex"]),
                        UpdateTime = Convert.ToDateTime(dr["UpdateTime"])
                    });
                }
                dr.Close();
            }
            return list;
        }

        public List<XFeedback> GetListAll()
        {
            string sql = string.Format("select * from XFeedback where IsDeleted=0 order by SortIndex desc,FID asc");
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }

        public XFeedback GetOneByID(string id)
        {
            string sql = string.Format("select * from XFeedback where IsDeleted=0 and FID=@FID");
            DbParameter[] parms = {
                new SqlParameter("@FID",SqlDbType.VarChar,50) {Value =id }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XFeedback> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        //返回数量
        public int GetCountBySearch(string sqlWhere)
        {
            string sql = string.Format("select count(*) from XFeedback where IsDeleted=0 {0}", sqlWhere);
            return int.Parse(dbHelper.ExecuteScalar(sql, null).ToString());
        }
        public List<XFeedback> GetListBySearch(string sqlWhere)
        {
            string sql = string.Format("select * from XFeedback where IsDeleted=0 {0}", sqlWhere);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        public List<XFeedback> GetListBySearch(string sqlWhere, string orderBy)
        {
            string sql = string.Format("select * from XFeedback where IsDeleted=0 {0} order by {1}", sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        //分页
        public List<XFeedback> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            string sql = string.Format(@"select top {0} * from XFeedback 
                                         where IsDeleted=0 {2}  
                                         and FID not in(select top {1} FID from XFeedback where IsDeleted=0 {2} order by {3} ) order by {3}",
                                         pageSize, (pageIndex - 1) * pageSize, sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }

        #endregion
    }
}
