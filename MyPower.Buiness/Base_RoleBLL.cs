using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class Base_RoleBLL
    {
        public static int Save(Base_Role model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Role.Add(model);
                    if (model.ID > 0)
                    {
                        db.Entry<Base_Role>(model).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry<Base_Role>(model).State = System.Data.Entity.EntityState.Added;
                    }
                    result = db.SaveChanges();
                }
            }
            return result;
        }


        public static int Delete(Base_Role model)
        {
            int result = 0;
            if (model != null)
            {
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Role.Add(model);
                    db.Entry<Base_Role>(model).State = System.Data.Entity.EntityState.Deleted;
                    result = db.SaveChanges();
                }
            }

            return result;
        }
    }
}
