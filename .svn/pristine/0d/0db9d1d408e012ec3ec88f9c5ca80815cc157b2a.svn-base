using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Model
{
    [Serializable]
    public class SessionUser
    {
        public CurrentUser C_User
        {
            get;
            set;
        }

        public List<CurrentDepartment> C_Dept
        {
            get;
            set;
        }

        public List<CurrentRole> C_Role
        {
            get;
            set;
        }

        public List<CurrentMenus> C_Menus
        {
            get;
            set;
        }

        public List<CurrentUserGroup> C_UGroup
        {
            get;
            set;
        }
    }
    [Serializable]
    public class CurrentUser
    {
        public int ID { get; set; }
        public Nullable<int> Creater { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public string Remark { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string IdCard { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string JobCode { get; set; }
        public string HomeAddress { get; set; }
    }
    [Serializable]
    public class CurrentDepartment
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public Nullable<int> ParentId { get; set; }
    }
    [Serializable]
    public class CurrentMenus
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string logo { get; set; }
        public Nullable<int> ParentId { get; set; }

        public List<CurrentButtons> Btns
        {
            get;
            set;
        }
    }

    [Serializable]
    public class CurrentRole
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class CurrentUserGroup
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class CurrentButtons
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
    }
}
