using MyPower.DB;
using MyPower.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class MenuBLL
    {
        public static int MenuSave(Menus model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                MyPowerConStr db = DBFactory.Instance();
                db.Menus.Add(model);
                if (model.ID > 0)
                {
                    db.Entry<Menus>(model).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Entry<Menus>(model).State = System.Data.Entity.EntityState.Added;
                }
                result = db.SaveChanges();
            }
            return result;
        }


        public static int Delete(Menus model)
        {
            int result = 0;
            if (model != null)
            {
                MyPowerConStr db = DBFactory.Instance();
                db.Menus.Add(model);
                db.Entry<Menus>(model).State = System.Data.Entity.EntityState.Deleted;
                result = db.SaveChanges();
            }

            return result;
        }
    }
}
