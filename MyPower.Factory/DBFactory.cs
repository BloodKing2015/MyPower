/************************************************************************
 * Create By:   daidch
 * Create Time: 2016-01-20
 * Modify By: 
 * Modify Time:
 * Version:     1.0
 * Description: 工厂类
 * Copy Right WGX ALL Right
 ***********************************************************************/

using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyPower.Factory
{
    public class DBFactory
    {
        public const string MyPowerConStrName = "MyPowerConStr";
        MyPowerConStr db = null;
        //public void CreateMyPowerConStr()
        //{
        //    db = new MyPowerConStr();
        //}

        //public static MyPowerConStr Instance()
        //{
        //    MyPowerConStr db = null;
        //    DBFactory df = HttpContext.Current.Session[DBFactory.MyPowerConStrName] as DBFactory;
        //    if (df != null)
        //    {
        //        db = df.db;
        //    }
        //    return db;
        //}

        //public void CloseMyPowerConStr()
        //{
        //    if (db != null)
        //    {
        //        db.Dispose();
        //    }
        //}
    }
}
