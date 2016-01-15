using MyPower.DB;
using MyPower.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class Base_DepartmentBLL
    {
        public static List<DepartModel> GetDepart()
        {
            List<DepartModel> resultList = new List<DepartModel>();

            using (MyPowerConStr db = new MyPowerConStr())
            {
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
            }

            List<DepartModel> treeList = new List<DepartModel>();
            treeList.AddRange(InsertDepart(resultList, 0));
            return treeList;
        }

        private static List<DepartModel> InsertDepart(List<DepartModel> dSource, int parentId)
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



        public static int Save(Base_Department model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Department.Add(model);
                    if (model.ID > 0)
                    {
                        db.Entry<Base_Department>(model).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry<Base_Department>(model).State = System.Data.Entity.EntityState.Added;
                    }
                    result = db.SaveChanges();
                }
            }
            return result;
        }


        public static int Delete(Base_Department model)
        {
            int result = 0;
            if (model != null)
            {
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Department.Add(model);
                    db.Entry<Base_Department>(model).State = System.Data.Entity.EntityState.Deleted;
                    result = db.SaveChanges();
                }
            }

            return result;
        }





        public static List<TreeDepartModel> GetTreeDepart()
        {
            List<TreeDepartModel> resultList = new List<TreeDepartModel>();

            using (MyPowerConStr db = new MyPowerConStr())
            {
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
            }

            List<TreeDepartModel> treeList = new List<TreeDepartModel>();
            treeList.AddRange(InsertTreeDepart(resultList, 0));
            return treeList;
        }

        private static List<TreeDepartModel> InsertTreeDepart(List<TreeDepartModel> dSource, int parentId)
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
    }
}
