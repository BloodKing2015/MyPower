//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPower.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Base_Usr
    {
        public Base_Usr()
        {
            this.Base_Login_log = new HashSet<Base_Login_log>();
            this.Base_Department = new HashSet<Base_Department>();
            this.Base_Role = new HashSet<Base_Role>();
            this.Base_UserGroup = new HashSet<Base_UserGroup>();
        }
    
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
    
        public virtual ICollection<Base_Login_log> Base_Login_log { get; set; }
        public virtual ICollection<Base_Department> Base_Department { get; set; }
        public virtual ICollection<Base_Role> Base_Role { get; set; }
        public virtual ICollection<Base_UserGroup> Base_UserGroup { get; set; }
    }
}
