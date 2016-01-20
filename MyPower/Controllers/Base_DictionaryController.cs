using MyPower.Buiness;
using MyPower.DB;
using MyPower.Factory;
using MyPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class Base_DictionaryController : BussinessController
    {
        //
        // GET: /Base_Dictionary/
        public ActionResult Index()
        {
            return View();
        }

        [Route("Base_Dictionary/MainDictionary/{code}")]
        public ActionResult MainDictionary(string code)
        {
            Base_Dictionary mEntity = null;
            MyPowerConStr db = DBFactory.Instance();
            {
                mEntity = db.Base_Dictionary.FirstOrDefault(
                    f =>
                        f.Code == code
                    );
            }
            return View(mEntity);
        }

        [Route("Base_Dictionary/MainDictionary")]
        public ActionResult MainDictionary()
        {
            return View();
        }


        //
        // POST: 保存
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Save(Base_Dictionary model)
        {
            int result = Base_DictionaryBLL.Save(model);
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return Json(result);
        }

        //
        // POST: 删除
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Delete(Base_Dictionary model)
        {
            int result = Base_DictionaryBLL.Delete(model);
            return Json(result);
        }

        public JsonResult DicList()
        {
            int pageSize = Convert.ToInt32(Request["rows"]);
            int pageNum = Convert.ToInt32(Request["page"]);
            DicModel model = new DicModel();
            MyPowerConStr db = DBFactory.Instance();
            {
                model.total = db.Base_Dictionary.Count();
                model.rows = (from item in db.Base_Dictionary.ToList()
                              select new Base_Dictionary()
                              {
                                  Code = item.Code,
                                  Name = item.Name,
                                  Remark = item.Remark,
                                  Creater = item.Creater,
                                  Createtime = item.Createtime
                              })
                              .OrderBy(o => o.Code)
                              .Skip((pageNum - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();
            }
            return Json(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("Base_Dictionary/GetDicValue/{code}")]
        public JsonResult GetDicValue(string code)
        {
            List<Base_Dictionary_Value> list = Base_DictionaryBLL.GetDicValue(code);
            var result = list.Select(s => new { code = s.Code, Name = s.Name }).OrderBy(o => o.code).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}