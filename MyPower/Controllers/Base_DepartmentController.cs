using MyPower.Buiness;
using MyPower.DB;
using MyPower.Factory;
using MyPower.Model;
using MyPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class Base_DepartmentController : BussinessController
    {
        //
        // GET: /Base_Department/
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public JsonResult DeptList()
        {
            List<DepartModel> modelList = Base_DepartmentBLL.GetDepart();
            return Json(modelList);
        }



        public ActionResult Edit(int? id)
        {
            Base_Department mEntity = null;
            MyPowerConStr db = DBFactory.Instance();
            {
                mEntity = db.Base_Department.FirstOrDefault(
                    f =>
                        f.ID == id
                    );
            }
            return View(mEntity);
        }


        // POST: 保存
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Save(Base_Department model)
        {
            int result = Base_DepartmentBLL.Save(model);
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return Json(result);
        }

        //
        // POST: 删除
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Delete(Base_Department model)
        {
            int result = Base_DepartmentBLL.Delete(model);
            return Json(result);
        }



        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public JsonResult TreeDeptList()
        {
            List<TreeDepartModel> modelList = Base_DepartmentBLL.GetTreeDepart();
            return Json(modelList);
        }
    }
}