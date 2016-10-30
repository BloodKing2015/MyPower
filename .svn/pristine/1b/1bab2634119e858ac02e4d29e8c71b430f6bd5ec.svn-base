/************************************************************************
 * Create By:   daidch
 * Create Time: 2016-01-19
 * Modify By: 
 * Modify Time:
 * Version:     1.0
 * Description: 日志
 * Copy Right WGX ALL Right
 ***********************************************************************/

using MyPower.DB;
using MyPower.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class Base_logBLL
    {
        public static void WriteException(Exception ex, int? userId)
        {
            MyPowerConStr db = DBFactory.Instance();
            Base_log log = db.Base_log.Create();
            log.Code = ex.HResult.ToString();
            log.errorMsg = ex.Message;
            log.ClientIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            log.UsrId = userId;
        }
    }
}
