﻿using MyPower.DB;
using MyPower.Factory;
using MyPower.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace MyPower.Buiness
{
    public class Base_RoleBLL : BaseBLL<Base_Role>
    {
        private static Base_RoleBLL m_Instance = null;
        private Base_RoleBLL()
        {
            baseDAL = container.Resolve<IBase_RoleDAL>();
        }

        private static object syncRoot = new Object();
        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static Base_RoleBLL Instance(MyPowerConStr pcon)
        {
            if (m_Instance == null)
            {
                lock (syncRoot)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new Base_RoleBLL();
                    }
                }
            }
            m_Instance.InitData(pcon);
            return m_Instance;
        }
        public int Save(Base_Role model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                //MyPowerConStr db = DBFactory.Instance();
                //Base_Role tmodel = db.Base_Role.FirstOrDefault(f => f.ID == model.ID);
                //db.Base_Role.Add(model);
                Base_Role tmodel = baseDAL.FindByID(model.ID);
                if (tmodel != null)
                {
                    result = baseDAL.Update(model, model.ID) ? 1 : 0;
                    //db.Entry<Base_Role>(tmodel).State = System.Data.Entity.EntityState.Detached;
                    //db.Entry<Base_Role>(model).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    result = baseDAL.Insert(model) ? 1 : 0;
                    //db.Entry<Base_Role>(model).State = System.Data.Entity.EntityState.Added;
                }
                //result = db.SaveChanges();
            }
            return result;
        }


        public int Delete(Base_Role model)
        {
            int result = 0;
            if (model != null)
            {
                //MyPowerConStr db = DBFactory.Instance();
                //Base_Role tmodel = db.Base_Role.FirstOrDefault(f => f.ID == model.ID);
                //if (tmodel != null)
                //{
                //    db.Entry<Base_Role>(tmodel).State = System.Data.Entity.EntityState.Deleted;
                //    result = db.SaveChanges();
                //}
                result = baseDAL.Delete(model.ID) ? 1 : 0;
            }

            return result;
        }

    }
}
