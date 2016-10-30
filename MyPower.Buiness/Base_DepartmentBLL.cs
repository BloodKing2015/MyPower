﻿using MyPower.DB;
using MyPower.Factory;
using MyPower.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MyPower.IDAL;

namespace MyPower.Buiness
{
    public class Base_DepartmentBLL : BaseBLL<Base_Department>
    {
        private static Base_DepartmentBLL m_Instance = null;

        private static object syncRoot = new Object();

        private Base_DepartmentBLL()
        {
            baseDAL = container.Resolve<IBase_DepartmentDAL>();
        }
        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static Base_DepartmentBLL Instance(MyPowerConStr pcon)
        {
            if (m_Instance == null)
            {
                lock (syncRoot)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new Base_DepartmentBLL();                         
                    }
                }
            }
            m_Instance.InitData(pcon);
            return m_Instance;
        }
        public List<DepartModel> GetDepart()
        {
            List<DepartModel> resultList = new List<DepartModel>();

            MyPowerConStr db = GetDB();
            resultList = (from item in db.Base_Department
                          join j in db.Base_Usr on item.Creater equals j.ID
                          join m in db.Base_Usr on item.Manager equals m.ID
                          select new
                          {
                              ID = item.ID,
                              Creater = j.Name,
                              Createtime = item.Createtime,
                              Remark = item.Remark,
                              Code = item.Code,
                              Name = item.Name,
                              ManageName = m.Name,
                              IsDepart = item.IsDepart,
                              ParentId = item.ParentId
                          })
                        .ToList()
                        .Select(s =>
                        new DepartModel()
                        {
                            ID = s.ID,
                            Creater = s.Creater,
                            Createtime = (s.Createtime ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm"),
                            Remark = s.Remark,
                            Code = s.Code,
                            Name = s.Name,
                            Manager = s.ManageName,
                            IsDepart = s.IsDepart,
                            ParentId = s.ParentId
                        }
                        ).ToList();

            List<DepartModel> treeList = new List<DepartModel>();
            treeList.AddRange(InsertDepart(resultList, 0));
            return treeList;
        }

        private List<DepartModel> InsertDepart(List<DepartModel> dSource, int parentId)
        {
            List<DepartModel> resultList = new List<DepartModel>();
            DepartModel tmp = null;
            foreach (var row in dSource.Where(w => w.ParentId == parentId))
            {
                tmp = row;
                tmp.children = InsertDepart(dSource, row.ID);
                resultList.Add(tmp);
            }
            return resultList;
        }



        public int Save(Base_Department model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                //MyPowerConStr db = DBFactory.Instance();
                //db.Base_Department.Add(model);
                if (baseDAL.FindSingle(f => f.ID == model.ID) != null)
                {
                    //db.Entry<Base_Department>(model).State = System.Data.Entity.EntityState.Modified;
                    result = baseDAL.Update(model, model.ID) ? 1 : 0;
                }
                else
                {
                    //db.Entry<Base_Department>(model).State = System.Data.Entity.EntityState.Added;
                    result = baseDAL.Insert(model) ? 1 : 0;
                }
                //result = db.SaveChanges();
            }
            return result;
        }


        public int Delete(Base_Department model)
        {
            int result = 0;
            if (model != null)
            {
                //MyPowerConStr db = DBFactory.Instance();
                //db.Base_Department.Add(model);
                //db.Entry<Base_Department>(model).State = System.Data.Entity.EntityState.Deleted;
                //result = db.SaveChanges();
                result = baseDAL.Delete(model.ID) ? 1 : 0;
            }

            return result;
        }





        public List<TreeDepartModel> GetTreeDepart()
        {
            List<TreeDepartModel> resultList = new List<TreeDepartModel>();

            MyPowerConStr db = GetDB();
            resultList = (from item in db.Base_Department
                          select new
                          {
                              ID = item.ID,
                              Name = item.Name,
                              ParentId = item.ParentId
                          })
                        .ToList()
                        .Select(s =>
                        new TreeDepartModel()
                        {
                            id = s.ID,
                            text = s.Name,
                            ParentId = s.ParentId ?? 0
                        }
                        ).ToList();

            List<TreeDepartModel> treeList = new List<TreeDepartModel>();
            treeList.AddRange(InsertTreeDepart(resultList, 0));
            return treeList;
        }

        private List<TreeDepartModel> InsertTreeDepart(List<TreeDepartModel> dSource, int parentId)
        {
            List<TreeDepartModel> resultList = new List<TreeDepartModel>();
            TreeDepartModel tmp = null;
            foreach (var row in dSource.Where(w => w.ParentId == parentId))
            {
                tmp = row;
                tmp.children = InsertTreeDepart(dSource, row.id);
                resultList.Add(tmp);
            }
            return resultList;
        }

        public List<DepartMentOut> GetNodeByParentId(int pId)
        {
            List<DepartMentOut> list = new List<DepartMentOut>();
            list = baseDAL.GetQueryable().Where(w => w.ParentId == pId).ToList()
                .Select(s => new DepartMentOut()
                {
                    id = s.ID,
                    name = s.Name,
                    pId = s.ParentId ?? 0,
                    open = false,
                    @checked = false,
                    isParent = baseDAL.GetQueryable().Count(c => c.ParentId == s.ID) > 0
                }).ToList();
            return list;
        }
    }
}
