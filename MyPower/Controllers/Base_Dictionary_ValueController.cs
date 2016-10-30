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
    public class Base_Dictionary_ValueController : BussinessController
    {
        [Route("Base_Dictionary_Value/DicValue/{dicCode}")]
        public JsonResult DicValue(string dicCode)
        {
            DicValueModel model = new DicValueModel();
            model.rows = new List<DicValue>();
            List<Base_Dictionary_Value> dvList = new List<Base_Dictionary_Value>();
            MyPowerConStr db = baseContext;
            {
                dvList = (from item in db.Base_Dictionary_Value
                          where string.Equals(item.Base_Dictionary_Code, dicCode)
                          select item).ToList();
            }

            dvList.ForEach(
                f =>
                {
                    model.rows.Add(new DicValue() { name = "Code", value = f.Code, group = f.Code });
                    model.rows.Add(new DicValue() { name = "Name", value = f.Name, group = f.Code });
                    model.rows.Add(new DicValue() { name = "Remark", value = f.Remark, group = f.Code });
                }
                );
            model.total = model.rows.Count;
            return Json(model);
        }

        [Route("Base_Dictionary_Value/Index/{dicCode}")]
        public ActionResult Index(string dicCode)
        {
            Base_Dictionary_Value dv = new Base_Dictionary_Value();
            dv.Base_Dictionary_Code = dicCode;
            return View(dv);
        }

        [Route("Base_Dictionary_Value/Edit/{dicValueCode}/{dicCode}")]
        public ActionResult Edit(string dicValueCode, string dicCode)
        {
            Base_Dictionary_Value dv = new Base_Dictionary_Value();
            MyPowerConStr db = baseContext;
            {
                dv = db.Base_Dictionary_Value.FirstOrDefault(f => string.Equals(f.Code, dicValueCode) && string.Equals(f.Base_Dictionary_Code, dicCode));
            }
            return View("Index", dv);
        }


        //
        // POST: 保存
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Save(Base_Dictionary_Value model)
        {
            int result = Base_Dictionary_ValueBLL.Save(model, baseContext);
            return Json(result);
        }

        //
        // POST: 删除
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Delete(Base_Dictionary_Value model)
        {
            int result = Base_Dictionary_ValueBLL.Delete(model, baseContext);
            return Json(result);
        }
    }
}