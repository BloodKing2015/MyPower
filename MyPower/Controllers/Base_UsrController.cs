using MyPower.Buiness;
using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class Base_UsrController : BussinessController
    {
        //
        // GET: /Base_Usr/
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public JsonResult UsrList()
        {
            var modelList = Base_UsrBLL.Instance(baseContext).GetUsrList();
            return Json(modelList);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [Route("Base_Usr/DepartUsrList/{deptId}")]
        public JsonResult DepartUsrList(int deptId)
        {
            int totals = 0;
            int pageSize = Convert.ToInt32(Request["rows"]);
            int pageNum = Convert.ToInt32(Request["page"]);
            List<Base_Usr> modelList = Base_UsrBLL.Instance(baseContext).GetUsrByDeptId(deptId, pageNum, pageSize, out totals);
            var model = new
            {
                total = totals,
                rows = modelList
            };
            return Json(model);
        }




        public ActionResult Edit(int? id)
        {
            Base_Usr mEntity = null;
            using (MyPowerConStr db = new MyPowerConStr())
            {
                mEntity = db.Base_Usr.FirstOrDefault(
                    f =>
                        f.ID == id
                    );
            }
            return View(mEntity);
        }


        // POST: 保存
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Save(Base_Usr model)
        {
            int result = Base_UsrBLL.Instance(baseContext).Save(model);
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return Json(result);
        }

        //
        // POST: 删除
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Delete(int id)
        {
            int result = Base_UsrBLL.Instance(baseContext).Delete(id);
            return Json(result);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetUserRole(int userId)
        {
            return Json(Base_UsrBLL.Instance(baseContext).UserRole(userId));
        }       
    }
}