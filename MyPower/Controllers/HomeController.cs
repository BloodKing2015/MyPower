using MyPower.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class HomeController : MyController
    {
        public ActionResult Index()
        {
            using (MyPowerConStr db = new MyPowerConStr())
            {
                int k = db.Base_Buttons.Count();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Menu()
        {
            List<Menus> menuList = new List<Menus>();
            using (MyPowerConStr db = new MyPowerConStr())
            {
                menuList = db.Menus.ToList();
            }
            var menuObj =
                from item in menuList.Where(w => (w.ParentId ?? 0) == 0).OrderBy(o => o.Code)
                select new
                {
                    Name = item.Name,
                    url = item.URL,
                    code = item.Code,
                    children = from child in menuList.Where(w => (w.ParentId ?? 0) == item.ID).OrderBy(o => o.Code)
                               select new { Name = child.Name, url = child.URL, code = child.Code }
                };

            return Json(menuObj);
        }

        /// <summary>
        /// app 接口 DEMO
        /// </summary>
        /// <returns></returns>
        public JsonResult bt()
        {
            Stream stream = Request.InputStream;

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);

            return Json(System.Text.Encoding.UTF8.GetString(bytes));
        }
    }
}