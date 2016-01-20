﻿/************************************************************************
 * Create By:   daidch
 * Create Time: 2016-01-07
 * Modify By: 
 * Modify Time:
 * Version:     1.0
 * Description: 控制器类
 * Copy Right WGX ALL Right
 ***********************************************************************/

using MyPower.Buiness;
using MyPower.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MyPower
{
    public class MyController : Controller
    {
        private const string UserSessionKey = "usr";
        protected const string AccountURL = "/Login/Index";
        protected const string HomeURL = "/Home/Index";
        /// <summary>
        /// 构造函数 判断 用户是否登录 未登录 到登录页面登录
        /// </summary>
        public MyController()
        {

        }

       

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            int userId = 0;
            if (CurrentSession!=null)
            {
                userId = CurrentSession.C_User.ID;
            }
            Base_logBLL.WriteException(filterContext.Exception, userId);
        }

        #region function
        protected void SetSessionUser(SessionUser SUser)
        {
            Session[UserSessionKey] = SUser;
        }

        protected void RemoveUserSession()
        {
            if (Session[UserSessionKey] != null)
            {
                Session.Remove(UserSessionKey);
            }
        }

        SessionUser _CSession = null;
        /// <summary>
        /// 或登录者信息
        /// </summary>
        protected SessionUser CurrentSession
        {
            get
            {
                if (Session[UserSessionKey] != null)
                {
                    _CSession = Session[UserSessionKey] as SessionUser;
                }
                return _CSession;
            }
        }
        #endregion
    }

}