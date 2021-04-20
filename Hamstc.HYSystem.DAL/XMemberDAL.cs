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
    public class XMemberDAL : BaseDAL
    {
        public XMemberDAL()
            : base()
        {

        }
        #region //操作
        public bool Add(XMember model)
        {
            string sql = string.Format(@"insert into XMember
                (MID,PCode,PCodeStr,CurrentCode,NickName,WXCode,Phone,InviteCode,Question,Answer,Remark,LoginName,
                LoginPWD,LoginSign,LoginIP,LoginTime,ManagePWD,IsDisable,Grade,SortIndex,IsDeleted,CreateTime,UpdateTime) 
                values(@MID,@PCode,@PCodeStr,@CurrentCode,@NickName,@WXCode,@Phone,@InviteCode,@Question,@Answer,@Remark,@LoginName,
                @LoginPWD,@LoginSign,@LoginIP,@LoginTime,@ManagePWD,@IsDisable,@Grade,@SortIndex,@IsDeleted,@CreateTime,@UpdateTime) ");
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value = model.MID },
                new SqlParameter("@PCode",SqlDbType.VarChar,50) {Value = model.PCode },
                new SqlParameter("@PCodeStr",SqlDbType.VarChar,2000) {Value = model.PCodeStr },
                new SqlParameter("@CurrentCode",SqlDbType.VarChar,50) {Value = model.CurrentCode },
                new SqlParameter("@NickName",SqlDbType.VarChar,500) {Value = model.NickName },

                new SqlParameter("@WXCode",SqlDbType.VarChar,50) {Value = model.WXCode },
                new SqlParameter("@Phone",SqlDbType.VarChar,50) {Value = model.Phone },
                new SqlParameter("@InviteCode",SqlDbType.VarChar,50) {Value = model.InviteCode },
                new SqlParameter("@Question",SqlDbType.VarChar,500) {Value = model.Question },
                new SqlParameter("@Answer",SqlDbType.VarChar,500) {Value = model.Answer },  

                new SqlParameter("@Remark",SqlDbType.Text) {Value = model.Remark },                
                new SqlParameter("@LoginName",SqlDbType.VarChar,50) {Value = model.LoginName },
                new SqlParameter("@LoginPWD",SqlDbType.VarChar,1000) {Value = model.LoginPWD },
                new SqlParameter("@LoginSign",SqlDbType.VarChar,50) {Value = model.LoginSign },
                new SqlParameter("@LoginIP",SqlDbType.VarChar,50) {Value = model.LoginIP },

                new SqlParameter("@LoginTime",SqlDbType.DateTime) {Value = model.LoginTime },
                new SqlParameter("@ManagePWD",SqlDbType.VarChar,50) {Value = model.ManagePWD },
                new SqlParameter("@IsDisable",SqlDbType.Int) {Value = model.IsDisable }, 
                new SqlParameter("@Grade",SqlDbType.Int) {Value = model.Grade },
                new SqlParameter("@SortIndex",SqlDbType.Int) {Value = model.SortIndex },

                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value = model.CreateTime },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        public bool Update(XMember model)
        {
            string sql = string.Format(@"update XMember
                set PCode=@PCode,PCodeStr=@PCodeStr,CurrentCode=@CurrentCode,NickName=@NickName,WXCode=@WXCode,
                    Phone=@Phone,InviteCode=@InviteCode,Question=@Question,Answer=@Answer,Remark=@Remark,
                    LoginName=@LoginName,LoginPWD=@LoginPWD,LoginSign=@LoginSign,LoginIP=@LoginIP,
                    LoginTime=@LoginTime,ManagePWD=@ManagePWD,IsDisable=@IsDisable,Grade=@Grade,
                    SortIndex=@SortIndex,IsDeleted=@IsDeleted,CreateTime=@CreateTime,
                    UpdateTime=@UpdateTime
                where MID=@MID");
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value = model.MID },
                new SqlParameter("@PCode",SqlDbType.VarChar,50) {Value = model.PCode },
                new SqlParameter("@PCodeStr",SqlDbType.VarChar,2000) {Value = model.PCodeStr },
                new SqlParameter("@CurrentCode",SqlDbType.VarChar,50) {Value = model.CurrentCode },
                new SqlParameter("@NickName",SqlDbType.VarChar,500) {Value = model.NickName },

                new SqlParameter("@WXCode",SqlDbType.VarChar,50) {Value = model.WXCode },
                new SqlParameter("@Phone",SqlDbType.VarChar,50) {Value = model.Phone },
                new SqlParameter("@InviteCode",SqlDbType.VarChar,50) {Value = model.InviteCode },
                new SqlParameter("@Question",SqlDbType.VarChar,500) {Value = model.Question },
                new SqlParameter("@Answer",SqlDbType.VarChar,500) {Value = model.Answer },

                new SqlParameter("@Remark",SqlDbType.Text) {Value = model.Remark },
                new SqlParameter("@LoginName",SqlDbType.VarChar,50) {Value = model.LoginName },
                new SqlParameter("@LoginPWD",SqlDbType.VarChar,1000) {Value = model.LoginPWD },
                new SqlParameter("@LoginSign",SqlDbType.VarChar,50) {Value = model.LoginSign },
                new SqlParameter("@LoginIP",SqlDbType.VarChar,50) {Value = model.LoginIP },

                new SqlParameter("@LoginTime",SqlDbType.DateTime) {Value = model.LoginTime },
                new SqlParameter("@ManagePWD",SqlDbType.VarChar,50) {Value = model.ManagePWD },
                new SqlParameter("@IsDisable",SqlDbType.Int) {Value = model.IsDisable },
                new SqlParameter("@Grade",SqlDbType.Int) {Value = model.Grade },
                new SqlParameter("@SortIndex",SqlDbType.Int) {Value = model.SortIndex },

                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value = model.CreateTime },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        public bool Delete(XMember model)
        {
            string sql = string.Format(@"update XMember
                set IsDeleted=@IsDeleted,UpdateTime=@UpdateTime
                where MID=@MID");
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value = model.MID },
                new SqlParameter("@IsDeleted",SqlDbType.Int) {Value = model.IsDeleted },
                new SqlParameter("@UpdateTime",SqlDbType.DateTime) {Value = model.UpdateTime }
            };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        #endregion
        #region //查询
        private List<XMember> DataReaderToList(DbDataReader dr)
        {
            List<XMember> list = new List<XMember>();
            if (dr != null && dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    list.Add(new XMember()
                    {
                        MID = Convert.ToString(dr["MID"]),
                        Answer = Convert.ToString(dr["Answer"]),
                        CurrentCode = Convert.ToString(dr["CurrentCode"]),
                        InviteCode = Convert.ToString(dr["InviteCode"]),
                        LoginIP = Convert.ToString(dr["LoginIP"]),
                        NickName = Convert.ToString(dr["NickName"]),
                        ManagePWD = Convert.ToString(dr["ManagePWD"]),
                        PCode = Convert.ToString(dr["PCode"]),
                        PCodeStr = Convert.ToString(dr["PCodeStr"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Question = Convert.ToString(dr["Question"]),
                        Remark = Convert.ToString(dr["Remark"]),
                        WXCode = Convert.ToString(dr["WXCode"]),
                        LoginName = Convert.IsDBNull(dr["LoginName"]) ? "" : Convert.ToString(dr["LoginName"]),
                        LoginPWD = Convert.ToString(dr["LoginPWD"]),
                        LoginSign = Convert.ToString(dr["LoginSign"]),
                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        LoginTime = Convert.ToDateTime(dr["LoginTime"]),
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
        public XMember GetOneById(string id)
        {
            string sql = string.Format("select * from XMember where MID=@MID "); //IsDeleted=0
            DbParameter[] parms = {
                new SqlParameter("@MID",SqlDbType.VarChar,50) {Value =id }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMember> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        public XMember GetOneByLogin(string name, string pwd)
        {
            string sql = string.Format("select * from XMember where LoginName=@LoginName and LoginPWD=@LoginPWD and IsDeleted=0 ");
            DbParameter[] parms = {
                new SqlParameter("@LoginName",SqlDbType.VarChar,50) {Value = name },
                new SqlParameter("@LoginPWD",SqlDbType.VarChar,1000) {Value = pwd }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMember> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        public XMember GetOneByLogin(string sign)
        {
            string sql = string.Format("select * from XMember where LoginSign=@LoginSign and IsDeleted=0 ");
            DbParameter[] parms = {
                new SqlParameter("@LoginSign",SqlDbType.VarChar,50) {Value = sign }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMember> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        public XMember GetOneByInviteCode(string code)
        {
            string sql = string.Format("select * from XMember where InviteCode=@InviteCode and IsDeleted=0");
            DbParameter[] parms = {
                new SqlParameter("@InviteCode",SqlDbType.VarChar,50) {Value =code }
            };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMember> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }
        public int GetCountBySearch(string sqlWhere)
        {
            string sql = string.Format("select count(*) from XMember where IsDeleted=0 {0}", sqlWhere);
            return int.Parse(dbHelper.ExecuteScalar(sql, null).ToString());
        }
        public int CheckReg(string nickname, string wxcode, string phone)
        {
            string sql = string.Format(@"select count(*) from XMember 
                                        where IsDeleted=0 
                                        and (NickName=@NickName or WXCode=@WXCode or Phone=@Phone)");
            DbParameter[] parms = {
                new SqlParameter("@NickName",SqlDbType.VarChar,50) {Value = nickname },
                new SqlParameter("@WXCode",SqlDbType.VarChar,50) {Value = wxcode },
                new SqlParameter("@Phone",SqlDbType.VarChar,50) {Value = phone }
            };
            return int.Parse(dbHelper.ExecuteScalar(sql, parms).ToString());
        }
        public List<XMember> GetListBySearch(string sqlWhere)
        {
            string sql = string.Format("select * from XMember where IsDeleted=0 {0}", sqlWhere);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        public List<XMember> GetListBySearch(string sqlWhere, string orderBy)
        {
            string sql = string.Format("select * from XMember where IsDeleted=0 {0} order by {1}", sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        //分页
        public List<XMember> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            string sql = string.Format(@"select top {0} * from XMember 
                                         where IsDeleted=0 {2}  
                                         and MID not in(select top {1} MID from XMember where IsDeleted=0 {2} order by {3} ) order by {3}",
                                         pageSize, (pageIndex - 1) * pageSize, sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }

        #endregion
    }
}
