using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class Base_UsrBLL
    {
        public static List<Base_Usr> GetUsrList()
        {
            List<Base_Usr> entityList = new List<Base_Usr>();
            using (MyPowerConStr db = new MyPowerConStr())
            {
                entityList = (from item in db.Base_Usr.ToList()
                              select new Base_Usr()
                              {
                                  ID = item.ID,
                                  Creater = item.Creater,
                                  Createtime = item.Createtime,
                                  Remark = item.Remark,
                                  Account = item.Account,
                                  Pwd = item.Pwd,
                                  Name = item.Name,
                                  Sex = item.Sex,
                                  IdCard = item.IdCard,
                                  Mobile = item.Mobile,
                                  Email = item.Email,
                                  JobCode = item.JobCode,
                                  HomeAddress = item.HomeAddress
                              }).ToList();
            }
            return entityList;
        }


        public static List<Base_Usr> GetUsrByDeptId(int deptId, int pageNum, int pageSize, out int totals)
        {
            totals = 0;
            List<Base_Usr> result = new List<Base_Usr>();
            using (MyPowerConStr db = new MyPowerConStr())
            {
                Base_Department dept = db.Base_Department.FirstOrDefault(f => f.ID == deptId);
                if (dept != null)
                {
                    totals = dept.Base_Usr.Count();
                    result = (from item in dept.Base_Usr
                              select new Base_Usr()
                              {
                                  ID = item.ID,
                                  Creater = item.Creater,
                                  Createtime = item.Createtime,
                                  Remark = item.Remark,
                                  Account = item.Account,
                                  Pwd = item.Pwd,
                                  Name = item.Name,
                                  Sex = item.Sex,
                                  IdCard = item.IdCard,
                                  Mobile = item.Mobile,
                                  Email = item.Email,
                                  JobCode = item.JobCode,
                                  HomeAddress = item.HomeAddress
                              })
                           .OrderBy(o => o.JobCode)
                           .Skip((pageNum - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();

                }
            }
            return result;
        }


        public static int Save(Base_Usr model)
        {
            int result = 0;
            if (model != null)
            {
                model.Createtime = DateTime.Now;
                model.Creater = 1;
                using (MyPowerConStr db = new MyPowerConStr())
                {
                    db.Base_Usr.Add(model);
                    if (model.ID > 0)
                    {
                        db.Entry<Base_Usr>(model).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry<Base_Usr>(model).State = System.Data.Entity.EntityState.Added;
                    }
                    result = db.SaveChanges();
                }
            }
            return result;
        }


        public static int Delete(int id)
        {
            int result = 0;
            using (MyPowerConStr db = new MyPowerConStr())
            {
                Base_Usr model = db.Base_Usr.FirstOrDefault(f => f.ID == id);
                if (model != null)
                {
                    db.Base_Usr.Add(model);
                    db.Entry<Base_Usr>(model).State = System.Data.Entity.EntityState.Deleted;
                    result = db.SaveChanges();
                }
            }

            return result;
        }


        public static Base_Usr GetByAccountPwd(string account,string pwd)
        {
            Base_Usr model = null;
            using (MyPowerConStr db = new MyPowerConStr())
            {
                model = db.Base_Usr.FirstOrDefault(f => string.Equals(f.Account, account) && string.Equals(f.Pwd, pwd));
            }
            return model;
        }
    }
}
