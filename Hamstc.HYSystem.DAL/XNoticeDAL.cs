using Hamstc.HYSystem.XModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.DAL
{
    public class XNoticeDAL : BaseDAL
    {
        public XNoticeDAL()
            : base()
        {

        }
        #region //操作
        public bool Add(XNotice model)
        {
            string sql = string.Format(@"insert into XNotice
                (NID,NType,Info,IsPublish,Title,SortIndex,IsDeleted,CreateTime,UpdateTime) 
                values(@NID,@NType,@Info,@IsPublish,@Title,@SortIndex,@IsDeleted,@CreateTime,@UpdateTime) ");
            DbParameter[] parms = {
                new SqlParameter("@NID",SqlDbType.VarChar,50) {Value = model.NID },
                new SqlParameter("@NType",SqlDbType.VarChar,50) {Value = model.NType },
                new SqlParameter("@Info",SqlDbType.Text) {Value = model.Info },
                new SqlParameter("@IsPublish",SqlDbType.Int) {Value = model.IsPublish },
                new SqlParameter("@Title",SqlDbType.VarChar,500) {Value = model.Title }, 
                new SqlParameter("@SortIndex",SqlDbType.Int) {Value = model.SortIndex },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value = model.CreateTime },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        public bool Update(XNotice model)
        {
            string sql = string.Format(@"update XNotice
                set NType=@NType,Info=@Info,IsPublish=@IsPublish,Title=@Title,
                    SortIndex=@SortIndex,IsDeleted=@IsDeleted,CreateTime=@CreateTime,
                    UpdateTime=@UpdateTime
                where NID=@NID");
            DbParameter[] parms = {
                new SqlParameter("@NID",SqlDbType.VarChar,50) {Value = model.NID },
                new SqlParameter("@NType",SqlDbType.VarChar,50) {Value = model.NType },
                new SqlParameter("@Info",SqlDbType.Text) {Value = model.Info },
                new SqlParameter("@IsPublish",SqlDbType.Int) {Value = model.IsPublish },
                new SqlParameter("@Title",SqlDbType.VarChar,500) {Value = model.Title },
                new SqlParameter("@SortIndex",SqlDbType.Int) {Value = model.SortIndex },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value = model.CreateTime },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        public bool Delete(XNotice model)
        {
            string sql = string.Format(@"update XNotice
                set IsDeleted=@IsDeleted,UpdateTime=@UpdateTime
                where NID=@NID");
            DbParameter[] parms = {
                new SqlParameter("@NID",SqlDbType.VarChar,50) {Value = model.NID },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        #endregion
        #region //查询
        private List<XNotice> DataReaderToList(DbDataReader dr)
        {
            List<XNotice> list = new List<XNotice>();
            if (dr != null && dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    list.Add(new XNotice()
                    {
                        NID = Convert.ToString(dr["NID"]),
                        Info = Convert.IsDBNull(dr["Info"]) ? "" : Convert.ToString(dr["Info"]),
                        NType = Convert.ToString(dr["NType"]),
                        Title = Convert.ToString(dr["Title"]),
                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        IsPublish = Convert.ToInt32(dr["IsPublish"]),
                        IsDeleted = Convert.ToInt32(dr["IsDeleted"]),
                        SortIndex = Convert.ToInt32(dr["SortIndex"]),
                        UpdateTime = Convert.ToDateTime(dr["UpdateTime"])
                    });
                }
                dr.Close();
            }
            return list;
        }
        public XNotice GetOneByID(string id)
        {
            string sql = string.Format("select * from XNotice where IsDeleted=0 and NID=@NID");
            DbParameter[] parms = {
                new SqlParameter("@NID",SqlDbType.VarChar,50) {Value =id }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XNotice> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        //返回数量
        public int GetCountBySearch(string sqlWhere)
        {
            string sql = string.Format("select count(*) from XNotice where IsDeleted=0 {0}", sqlWhere);
            return int.Parse(dbHelper.ExecuteScalar(sql, null).ToString());
        }
        public List<XNotice> GetListBySearch(string sqlWhere)
        {
            string sql = string.Format("select * from XNotice where IsDeleted=0 {0}", sqlWhere);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        public List<XNotice> GetListBySearch(string sqlWhere, string orderBy)
        {
            string sql = string.Format("select * from XNotice where IsDeleted=0 {0} order by {1}", sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        //分页
        public List<XNotice> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            string sql = string.Format(@"select top {0} * from XNotice 
                                         where IsDeleted=0 {2}  
                                         and NID not in(select top {1} NID from XNotice where IsDeleted=0 {2} order by {3} ) order by {3}",
                                         pageSize, (pageIndex - 1) * pageSize, sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        #endregion
    }
}
