using MyPower.DB;
using MyPower.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class Base_DictionaryBLL
    {
        public static int Save(Base_Dictionary model, MyPowerConStr db)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;             
                db.Base_Dictionary.Add(model);
                if (db.Base_Dictionary.Count(c => string.Equals(c.Code, model.Code)) > 0)
                {
                    db.Entry<Base_Dictionary>(model).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Entry<Base_Dictionary>(model).State = System.Data.Entity.EntityState.Added;
                }
                result = db.SaveChanges();
            }
            return result;
        }


        public static int Delete(Base_Dictionary model, MyPowerConStr db)
        {
            int result = 0;
            if (model != null)
            {                                          
                db.Base_Dictionary.Add(model);
                IEnumerable<Base_Dictionary_Value> dvs = db.Base_Dictionary_Value.Where(w => string.Equals(w.Base_Dictionary_Code, model.Code));
                db.Base_Dictionary_Value.RemoveRange(dvs);
                db.Entry<Base_Dictionary>(model).State = System.Data.Entity.EntityState.Deleted;
                result = db.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// 获取字典属性
        /// </summary>
        /// <param name="dicCode"></param>
        /// <returns></returns>
        public static List<Base_Dictionary_Value> GetDicValue(string dicCode,MyPowerConStr db)
        {
            List<Base_Dictionary_Value> dvs = new List<Base_Dictionary_Value>();
            Base_Dictionary bd = db.Base_Dictionary.FirstOrDefault(f => string.Equals(f.Code, dicCode));
            if (bd != null)
            {
                dvs = bd.Base_Dictionary_Value.ToList();
            }
            return dvs;
        }
    }
}
