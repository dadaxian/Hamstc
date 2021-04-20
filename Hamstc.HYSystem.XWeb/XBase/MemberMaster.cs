using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hamstc.HYSystem.BLL;
using M = Hamstc.HYSystem.XModel;
using System.Collections;

namespace Hamstc.HYSystem.XWeb
{
    public class MemberMaster : System.Web.UI.MasterPage
    {
        public XMemberBLL bllXMember;
        public XNoticeBLL bllXNotice;
        public XMemberInviteBLL bllXMemberInvite;
        public XMemberUpgradeBLL bllXMemberUpgrade;
        public XFeedbackBLL bllXFeedback;

        public MemberMaster()
        {
            bllXMember = new XMemberBLL();
            bllXNotice = new XNoticeBLL();
            bllXMemberInvite = new XMemberInviteBLL();
            bllXMemberUpgrade = new XMemberUpgradeBLL();
            bllXFeedback = new XFeedbackBLL();
        }

        #region //当前会员
        private M.XMember _currMember;
        public M.XMember CurrMember
        {
            get
            {
                if (_currMember == null)
                {
                    HttpCookie hcookie = Request.Cookies["MLOGIN"];
                    if (hcookie != null)
                    {
                        //根据登录签名获取对应用户对象
                        _currMember = bllXMember.GetOneByLogin(hcookie["MSIGN"]);
                    }
                }
                return _currMember;
            }
        }
        #endregion

        #region //跳转到登录页
        public void RedirectToLogin()
        {
            Response.Redirect("./?cmd=member_login");
        }
        #endregion

        public string CreateID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        #region 设置缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public object GetCache(string cacheKey) {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }
        /**//// <summary>  
            /// 设置数据缓存  
            /// </summary>  
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        ///**//// <summary>  
        //    /// 设置数据缓存  
        //    /// </summary>  
        //public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        //{
        //    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        //    objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        //}

        /**//// <summary>  
            /// 设置数据缓存  
            /// </summary>  
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /**//// <summary>  
            /// 移除指定数据缓存  
            /// </summary>  
        public static void RemoveAllCache(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }

        /**//// <summary>  
            /// 移除全部缓存  
            /// </summary>  
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// 设置绝对过期缓存 到了给定时间就被清除
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="dt"></param>
        public static void SetCache(string CacheKey, object objObject,DateTime dt)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null,dt, System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 滑动过期缓存：最后一次使用之后给定时间删除
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="slidingExpiration"></param>
        public static void SetCache(string CacheKey, object objObject, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);
        }


        #endregion
    }
}