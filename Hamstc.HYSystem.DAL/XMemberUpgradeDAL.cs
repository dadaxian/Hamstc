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
    public  class XMemberUpgradeDAL : BaseDAL
    {
        public XMemberUpgradeDAL()
            : base()
        {

        }
        #region//操作


        public bool Add(XMemberUpgrade model)
        {
            string sql = string.Format(@"insert into XMemberUpgrade(MUID,SID,Grade1,Grade2,CID,IsReply,SortIndex,IsDeleted,CreateTime,UpdateTime) 
       values(@MUID,@SID,@Grade1,@Grade2,@CID,@IsReply,@SortIndex,@IsDeleted,@CreateTime,@UpdateTime)");
            DbParameter[] parms ={
                                new SqlParameter ("@MUID",SqlDbType.VarChar ,50 ){Value =model.MUID},
                                new SqlParameter ("@SID",SqlDbType.VarChar ,50 ){Value =model.SID},
                                new SqlParameter ("@Grade1",SqlDbType.Int  ){Value =model.Grade1},
                                new SqlParameter ("@Grade2",SqlDbType.Int ){Value =model.Grade2},
                                new SqlParameter ("@CID",SqlDbType.VarChar ,50){Value =model.CID  },
                                new SqlParameter ("@IsReply",SqlDbType.Int ){Value =model.IsReply },
                                new SqlParameter ("@SortIndex",SqlDbType.Int  ){Value =model.SortIndex},
                                new SqlParameter ("@IsDeleted",SqlDbType.Int ){Value =model.IsDeleted  },
                                new SqlParameter ("@CreateTime",SqlDbType.DateTime ){Value =model.CreateTime },
                                new SqlParameter ("@UpdateTime",SqlDbType.DateTime  ){Value =model.UpdateTime  }
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }

        public bool Update(XMemberUpgrade model)
        {
            string sql = string.Format(@"update XMemberUpgrade set SID=@SID,Grade1=@Grade1,Grade2=@Grade2,CID=@CID,IsReply=@IsReply,
SortIndex=@SortIndex,IsDeleted=@IsDeleted,CreateTime=@CreateTime,UpdateTime=@UpdateTime where MUID=@MUID");
            DbParameter[] parms ={
                                new SqlParameter ("@MUID",SqlDbType.VarChar ,50 ){Value =model.MUID},
                                new SqlParameter ("@SID",SqlDbType.VarChar ,50 ){Value =model.SID},
                                new SqlParameter ("@Grade1",SqlDbType.Int  ){Value =model.Grade1},
                                new SqlParameter ("@Grade2",SqlDbType.Int ){Value =model.Grade2},
                                new SqlParameter ("@CID",SqlDbType.VarChar ,50){Value =model.CID  },
                                new SqlParameter ("@IsReply",SqlDbType.Int ){Value =model.IsReply },
                                new SqlParameter ("@SortIndex",SqlDbType.Int  ){Value =model.SortIndex},
                                new SqlParameter ("@IsDeleted",SqlDbType.Int ){Value =model.IsDeleted  },
                                new SqlParameter ("@CreateTime",SqlDbType.DateTime ){Value =model.CreateTime },
                                new SqlParameter ("@UpdateTime",SqlDbType.DateTime  ){Value =model.UpdateTime  }
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }

        public bool Delete(XMemberUpgrade model)
        {
            string sql = string.Format(@"delete XMemberUpgrade set IsDeleted=@IsDeleted,UpdateTime=@UpdateTime where MUID=@MUID");
            DbParameter[] parms ={
                                new SqlParameter ("@MUID",SqlDbType.VarChar ,50 ){Value =model.MUID},
                                
                                new SqlParameter ("@IsDeleted",SqlDbType.Int ){Value =model.IsDeleted  },
                                
                                new SqlParameter ("@UpdateTime",SqlDbType.DateTime  ){Value =model.UpdateTime  }
                                };
            return dbHelper.ExecuteNonQuery(sql, parms);
        }
        #endregion


        #region//查询
        private List<XMemberUpgrade> DataReaderToList(DbDataReader dr)
        {
            List<XMemberUpgrade> list = new List<XMemberUpgrade>();
            if (dr != null && dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    list.Add(new XMemberUpgrade()
                    {
                        MUID = Convert.ToString(dr["MUID"]),
                        SID = Convert.ToString(dr["SID"]),
                        Grade1 = Convert.ToInt32(dr["Grade1"]),
                        Grade2 = Convert.ToInt32(dr["Grade2"]),
                        CID = Convert.ToString(dr["CID"]),
                        IsReply = Convert.ToInt32(dr["IsReply"]),
                        SortIndex = Convert.ToInt32(dr["SortIndex"]),
                        IsDeleted = Convert.ToInt32(dr["IsDeleted"]),
                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        UpdateTime = Convert.ToDateTime(dr["UpdateTime"])
                    });
                }
                dr.Close();
            }
            return list;
        }

        public XMemberUpgrade GetOneById(string id)
        {
            string sql = string.Format("select * from XMemberUpgrade where MUID=@MUID");//IsDeleted=0
            DbParameter[] parms ={
                                new SqlParameter ("@MUID",SqlDbType.VarChar ,50 ){Value =id}
                                 };
            DbDataReader dr = dbHelper.QueryReader(sql, parms);
            List<XMemberUpgrade> list = DataReaderToList(dr);
            if (list.Count > 0) return list[0];
            return null;
        }

        public List<XMemberUpgrade> GetListBySearch(string sqlWhere)
        {
            string sql = string.Format("select * from XMemberUpgrade where IsDeleted=0 {0}", sqlWhere);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        public List<XMemberUpgrade> GetListBySearch(string sqlWhere, string orderBy)
        {
            string sql = string.Format("select * from XMemberUpgrade where IsDeleted=0 {0} order by {1}", sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }
        //分页
        public List<XMemberUpgrade> GetListBySearch(string sqlWhere, int pageIndex, int pageSize, string orderBy)
        {
            string sql = string.Format(@"select top {0} * from XMemberUpgrade 
                                         where IsDeleted=0 {2}  
                                         and MUID not in(select top {1} MUID from XMemberUpgrade where IsDeleted=0 {2} order by {3} ) order by {3}",
                                         pageSize, (pageIndex - 1) * pageSize, sqlWhere, orderBy);
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }

        public List<XMemberUpgrade> GetListAll()
        {
            string sql = string.Format("select * from XMemberUpgrade  where IsDeleted=0 order by SortIndex desc,MUID asc");
            return DataReaderToList(dbHelper.QueryReader(sql, null));
        }

        public int GetCountBySearch(string sqlWhere)
        {
            string sql = string.Format("select count(*) from XMemberUpgrade where IsDeleted=0 {0}", sqlWhere);
            return int.Parse(dbHelper.ExecuteScalar(sql, null).ToString());
        }
        #endregion

    }
}
