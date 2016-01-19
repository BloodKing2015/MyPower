/************************************************************************
 * Create By:   daidch
 * Create Time: 2016-01-07
 * Modify By: 
 * Modify Time:
 * Version:     1.0
 * Description: 控制器类
 * Copy Right WGX ALL Right
 ***********************************************************************/

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
        /// <summary>
        /// 构造函数 判断 用户是否登录 未登录 到登录页面登录
        /// </summary>
        public MyController()
        {

        }

        #region Authorization filters – 需要实现IAuthorizationFilter接口，用于验证处理验证相关的操作
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            ///TODO            
        }
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            ///TODO
        }
        #endregion

        // 摘要: 
        //     在执行操作方法后调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!(string.Equals(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                "Home"
                ) && string.Equals(filterContext.ActionDescriptor.ActionName, "Login"))
                && CurrentSession == null
                )
            {
                Response.Redirect("/Home/Login");
            }
        }
        //
        // 摘要: 
        //     在执行操作方法之前调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }


        // 摘要: 
        //     在操作结果执行后调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
        //
        // 摘要: 
        //     在操作结果执行之前调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        protected override void OnException(ExceptionContext filterContext)
        {

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