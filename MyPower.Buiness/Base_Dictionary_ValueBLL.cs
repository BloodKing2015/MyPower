using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class Base_Dictionary_ValueBLL
    {
        public static int Save(Base_Dictionary_Value model)
        {
            int result = 0;
            if (model != null)
            {
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Dictionary_Value.Add(model);
                    if (db.Base_Dictionary_Value.Count(c => string.Equals(c.Code, model.Code) && string.Equals(c.Base_Dictionary_Code, model.Base_Dictionary_Code)) > 0)
                    {
                        db.Entry<Base_Dictionary_Value>(model).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry<Base_Dictionary_Value>(model).State = System.Data.Entity.EntityState.Added;
                    }
                    result = db.SaveChanges();
                }
            }
            return result;
        }


        public static int Delete(Base_Dictionary_Value model)
        {
            int result = 0;
            if (model != null)
            {
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Dictionary_Value.Add(model);
                    db.Entry<Base_Dictionary_Value>(model).State = System.Data.Entity.EntityState.Deleted;
                    result = db.SaveChanges();
                }
            }

            return result;
        }
    }
}
