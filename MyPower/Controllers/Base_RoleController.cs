﻿using MyPower.Buiness;
using MyPower.DB;
using MyPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class Base_RoleController : BussinessController
    {
        //
        // GET: /Base_Role/
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult RoleList()
        {
            RoleModel model = new RoleModel();
            int pageSize = Convert.ToInt32(Request["rows"]);
            int pageNum = Convert.ToInt32(Request["page"]);
            using (MyPowerConStr db = new MyPowerConStr())
            {
                model.total = db.Base_Role.Count();
                model.rows = (from item in db.Base_Role
                              join j in db.Base_Usr on item.Creater equals j.ID
                              select new
                              {
                                  ID = item.ID,
                                  Code = item.Code,
                                  Name = item.Name,
                                  Remark = item.Remark,
                                  Creater = j.Name,
                                  Createtime = item.Createtime
                              }
                              )
                              .OrderBy(o => o.Code)
                              .Skip((pageNum - 1) * pageSize)
                              .Take(pageSize)
                              .ToList()
                              .Select(s => new Base_RoleValue()
                              {
                                  ID = s.ID,
                                  Code = s.Code,
                                  Name = s.Name,
                                  Remark = s.Remark,
                                  Creater = s.Name,
                                  Createtime = s.Createtime != null ? (s.Createtime ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss") : string.Empty
                              })
                              .ToList();
            }
            return Json(model);
        }


        public ActionResult Edit(int? id)
        {
            Base_Role mEntity = null;
            using (MyPowerConStr db = new MyPowerConStr())
            {
                mEntity = db.Base_Role.FirstOrDefault(
                    f =>
                        f.ID == id
                    );
            }
            return View(mEntity);
        }


        // POST: 保存
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Save(Base_Role model)
        {
            int result = Base_RoleBLL.Save(model);
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return Json(result);
        }

        //
        // POST: 删除
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Delete(Base_Role model)
        {
            int result = Base_RoleBLL.Delete(model);
            return Json(result);
        }
    }
}