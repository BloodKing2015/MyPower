using MyPower.DB;
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
    public class MenuBLL : BaseBLL<Menus>
    {
        private static MenuBLL m_Instance = null;
        private MenuBLL()
        {
            baseDAL = container.Resolve<IMenuDAL>();
        }

        private static object syncRoot = new Object();
        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static MenuBLL Instance(MyPowerConStr pcon)
        {
            if (m_Instance == null)
            {
                lock (syncRoot)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new MenuBLL();
                    }
                }
            }
            m_Instance.InitData(pcon);
            return m_Instance;
        }

        public int MenuSave(Menus model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                MyPowerConStr db = GetDB();

                Menus tmodel = FindByID(model.ID);
                if (tmodel != null)
                {
                    result = Update(model, model.ID) ? 1 : 0;
                }
                else
                {
                    result = Insert(model) ? 1 : 0;
                }
            }
            return result;
        }
    }
}
