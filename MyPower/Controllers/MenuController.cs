using MyPower.Buiness;
using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class MenuController : MyController
    {
        //
        // GET: /Menu/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            Menus mEntity = null;
            if (id != null)
            {
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    mEntity = db.Menus.FirstOrDefault(
                        f =>
                            f.ID == (id ?? 0)
                        );
                }
            }
            using (MyPowerConStr db = new MyPowerConStr())
            {
                ViewData["ParentMenus"] = db.Menus.ToList().Where(w => (w.ParentId ?? 0) == 0 && w.ID != (id ?? 0))
                            .Select(s => new SelectListItem() { Text = s.Name, Value = s.ID.ToString() }).ToList();
            }
            return View(mEntity);
        }

        //
        // POST: 保存
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Post(Menus model)
        {
            int result = MenuBLL.MenuSave(model);
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return Json(result);
        }

        //
        // POST: 删除
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Delete(Menus model)
        {
            int result = MenuBLL.Delete(model);
            return Json(result);
        }


        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult MenuList()
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
                    id = item.ID,
                    Name = item.Name,
                    url = item.URL,
                    code = item.Code,
                    children = from child in menuList.Where(w => (w.ParentId ?? 0) == item.ID).OrderBy(o => o.Code)
                               select new
                               {
                                   id = child.ID,
                                   Name = child.Name,
                                   url = child.URL,
                                   code = child.Code
                               }
                };

            return Json(menuObj);
        }
    }
}