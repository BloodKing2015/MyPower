/************************************************************************
 * Create By:   daidch
 * Create Time: 2016-01-20
 * Modify By: 
 * Modify Time:
 * Version:     1.0
 * Description: 
 * Copy Right WGX ALL Right
 ***********************************************************************/

using MyPower.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPower.HttpModule
{
    public class DbHttpModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            //DBFactory df = new DBFactory();
            //HttpContext.Current.Session[DBFactory.MyPowerConStrName] = df;
            //df.CreateMyPowerConStr();
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            //DBFactory df = HttpContext.Current.Session[DBFactory.MyPowerConStrName] as DBFactory;
            //if (df != null)
            //{
            //    df.CreateMyPowerConStr();
            //}
        }
        #endregion
    }
}