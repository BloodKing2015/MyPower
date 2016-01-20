using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MyPower
{
    public class BussinessController : MyController
    {
        #region Authorization filters – 需要实现IAuthorizationFilter接口，用于验证处理验证相关的操作
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            ///TODO    
            if (CurrentSession == null)
            {
                Response.Redirect(AccountURL);
            }
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
    }
}