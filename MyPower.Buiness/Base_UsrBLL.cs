using MyPower.CommonFuc;
using MyPower.DB;
using MyPower.Factory;
using MyPower.Model;
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
            MyPowerConStr db = DBFactory.Instance();
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
            return entityList;
        }


        public static List<Base_Usr> GetUsrByDeptId(int deptId, int pageNum, int pageSize, out int totals)
        {
            totals = 0;
            List<Base_Usr> result = new List<Base_Usr>();
            List<Base_Department> deptList = GetDepartAndSelf(deptId);
            MyPowerConStr db = DBFactory.Instance();
            List<Base_Usr> buList = new List<Base_Usr>();
            deptList.ForEach(
                f =>
                {
                    buList.AddRange(f.Base_Usr);
                }
                );
            buList = buList.Distinct(new selector<Base_Usr>((d1, d2) =>
            {
                return d1.ID == d2.ID;
            })).ToList();
            totals = buList.Count();
            result = (from item in buList
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
            return result;
        }

        /// <summary>
        /// 获取部门及其子部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static List<Base_Department> GetDepartAndSelf(int deptId)
        {
            List<Base_Department> result = new List<Base_Department>();
            MyPowerConStr db = DBFactory.Instance();
            Base_Department dept = db.Base_Department.FirstOrDefault(f => f.ID == deptId);
            if (dept != null)
            {
                result.Add(dept);
            }

            result.AddRange(GetDepart(deptId));
            return result;
        }
        /// <summary>
        /// 获取子部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private static List<Base_Department> GetDepart(int deptId)
        {
            List<Base_Department> result = new List<Base_Department>();
            MyPowerConStr db = DBFactory.Instance();
            foreach (Base_Department d in db.Base_Department.Where(w => (w.ParentId ?? 0) == deptId))
            {
                result.Add(d);
                result.AddRange(GetDepart(d.ID));
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
                MyPowerConStr db = DBFactory.Instance();
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
            return result;
        }


        public static int Delete(int id)
        {
            int result = 0;
            MyPowerConStr db = DBFactory.Instance();
            Base_Usr model = db.Base_Usr.FirstOrDefault(f => f.ID == id);
            if (model != null)
            {
                db.Base_Usr.Add(model);
                db.Entry<Base_Usr>(model).State = System.Data.Entity.EntityState.Deleted;
                result = db.SaveChanges();
            }

            return result;
        }

        #region userLogin
        public static SessionUser GetByAccountPwd(string account, string pwd)
        {
            SessionUser model = null;
            MyPowerConStr db = DBFactory.Instance();
            Base_Usr bUser = db.Base_Usr.FirstOrDefault(f => string.Equals(f.Account, account) && string.Equals(f.Pwd, pwd));
            if (bUser != null)
            {
                model = new SessionUser();
                model.C_User = ConvertUserToCurrentUser(bUser);
                model.C_Dept =
                    (from item in bUser.Base_Department
                     select new CurrentDepartment()
                     {
                         ID = item.ID,
                         Code = item.Code,
                         ParentId = item.ParentId
                     }).ToList();
                model.C_Role =
                    (
                    from item in bUser.Base_Role
                    select new CurrentRole()
                    {
                        ID = item.ID,
                        Code = item.Code,
                        Name = item.Name
                    }
                    ).ToList();
                model.C_UGroup =
                    (
                    from item in bUser.Base_UserGroup
                    select new CurrentUserGroup()
                    {
                        ID = item.ID,
                        Code = item.Code,
                        Name = item.Name
                    }
                    ).ToList();
                model.C_Menus =
                    (
                    from item in bUser.Base_Role
                    from r in item.Menus
                    select new CurrentMenus()
                    {
                        ID = r.ID,
                        Code = r.Code,
                        Name = r.Name,
                        URL = r.URL,
                        logo = r.logo,
                        ParentId = r.ParentId,
                        Btns =
                        (
                        from b in r.Base_Buttons
                        select new CurrentButtons()
                        {
                            ID = b.ID,
                            Code = b.Code,
                            Name = b.Name,
                            Action = b.Action
                        }
                        ).ToList()
                    }
                    ).ToList();
            }
            return model;
        }

        private static CurrentUser ConvertUserToCurrentUser(Base_Usr usr)
        {
            CurrentUser result = new CurrentUser();
            result.ID = usr.ID;
            result.Creater = usr.Creater;
            result.Createtime = usr.Createtime;
            result.Remark = usr.Remark;
            result.Account = usr.Account;
            result.Pwd = usr.Pwd;
            result.Name = usr.Name;
            result.Sex = usr.Sex;
            result.IdCard = usr.IdCard;
            result.Mobile = usr.Mobile;
            result.Email = usr.Email;
            result.JobCode = usr.JobCode;
            result.HomeAddress = usr.HomeAddress;
            return result;
        }
        #endregion

    }
}
