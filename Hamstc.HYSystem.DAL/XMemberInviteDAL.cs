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
    //邀请控制
    public class XMemberInviteDAL : BaseDAL
    {
        public XMemberInviteDAL()
            : base()
        {

        }
        #region //操作
        public bool Add(XMemberInvite model)
        {
            string sql = string.Format(@"insert into XMemberInvite
                (MIID,MID,CID,Grade1,Grade2,IsAdd,IsReply,SortIndex,IsDeleted,CreateTime,UpdateTime) 
                values(@MIID,@MID,@CID,@Grade1,@Grade2,@IsAdd,@IsReply,@SortIndex,@IsDeleted,@CreateTime,@UpdateTime) ");
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value = model.MID },
                new SqlParameter("@MIID",SqlDbType.VarChar,50) {Value = model.MIID },
                new SqlParameter("@CID",SqlDbType.VarChar,50) {Value = model.CID },

               new SqlParameter("@Grade1",SqlDbType.Int) {Value = model.Grade1 },
               new SqlParameter("@Grade2",SqlDbType.Int) {Value = model.Grade2 },
               new SqlParameter("@IsAdd",SqlDbType.Int) {Value = model.IsAdd },
               new SqlParameter("@IsReply",SqlDbType.Int) {Value = model.IsReply }, 
               
                new SqlParameter("@SortIndex",SqlDbType.Int) {Value = model.SortIndex },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value = model.CreateTime },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        public bool Update(XMemberInvite model)
        {
            string sql = string.Format(@"update XMemberInvite
                set MID=@MID,CID=@CID,Grade1=@Grade1,Grade2=@Grade2,IsAdd=@IsAdd,IsReply=@IsReply,
                    SortIndex=@SortIndex,IsDeleted=@IsDeleted,CreateTime=@CreateTime,
                    UpdateTime=@UpdateTime
                where MIID=@MIID");
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value = model.MID },
                new SqlParameter("@MIID",SqlDbType.VarChar,50) {Value = model.MIID },
                new SqlParameter("@CID",SqlDbType.VarChar,50) {Value = model.CID },

               new SqlParameter("@Grade1",SqlDbType.Int) {Value = model.Grade1 },
               new SqlParameter("@Grade2",SqlDbType.Int) {Value = model.Grade2 },
               new SqlParameter("@IsAdd",SqlDbType.Int) {Value = model.IsAdd },
               new SqlParameter("@IsReply",SqlDbType.Int) {Value = model.IsReply },

                new SqlParameter("@SortIndex",SqlDbType.Int) {Value = model.SortIndex },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value = model.CreateTime },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        public bool Delete(XMemberInvite model)
        {
            string sql = string.Format(@"update XMemberInvite
                set IsDeleted=@IsDeleted,UpdateTime=@UpdateTime
                where MIID=@MIID");
            DbParameter[] parms = {
                new SqlParameter("@MIID",SqlDbType.VarChar,50) {Value = model.MIID },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        #endregion
        #region //查询
        private List<XMemberInvite> DataReaderToList(DbDataReader dr)
        {
            List<XMemberInvite> list = new List<XMemberInvite>();
            if (dr != null && dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    list.Add(new XMemberInvite()
                    {
                        MID = Convert.ToString(dr["MID"]),
                        MIID = Convert.ToString(dr["MIID"]),
                        CID = Convert.ToString(dr["CID"]),
                        Grade2 = Convert.ToInt32(dr["Grade2"]),
                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        Grade1 = Convert.ToInt32(dr["Grade1"]),
                        IsDeleted = Convert.ToInt32(dr["IsDeleted"]),
                        IsAdd = Convert.ToInt32(dr["IsAdd"]),
                        IsReply = Convert.ToInt32(dr["IsReply"]),
                        SortIndex = Convert.ToInt32(dr["SortIndex"]),
                        UpdateTime = Convert.ToDateTime(dr["UpdateTime"])
                    });
                }
                dr.Close();
            }
            return list;
        }
        public XMemberInvite GetOneById(string id)
        {
            string sql = string.Format("select * from XMemberInvite where MIID=@MIID "); //IsDeleted=0
            DbParameter[] parms = {
                new SqlParameter("@MIID",SqlDbType.VarChar,50) {Value =id }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMemberInvite> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        public XMemberInvite GetTopOneByMID(string mid)
        {
            //查找当前会员的空缺位置，最先添加的优先
            string sql = string.Format("select top 1 * from XMemberInvite where IsDeleted=0 and MID=@MID and IsAdd=0 order by MIID asc ");
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value =mid }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMemberInvite> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        public XMemberInvite GetTopOneByPCode(string pcodeStr)
        {
            //查找当前会员下级的空缺位置，层级（级别）越小越靠前
            string sql = string.Format(@"select top 1 mi.*,len(m.PCodeStr) as xlength from XMemberInvite as mi, XMember as m
                                         where mi.IsDeleted=0 and m.IsDeleted=0 and mi.MID=m.MID and mi.IsAdd=0 
                                         and mi.MID in (select MID from XMember where IsDeleted=0 and PCodeStr like '{0}%' )
                                         order by xlength asc,mi.MIID asc ", pcodeStr);
            DbDataReader dr = dbHelper.QueryReader(sql, null);
            List<XMemberInvite> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        #endregion
    }
}
