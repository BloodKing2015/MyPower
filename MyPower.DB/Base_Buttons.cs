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
    
    public partial class Base_Buttons
    {
        public Base_Buttons()
        {
            this.Menus = new HashSet<Menus>();
            this.Base_Role = new HashSet<Base_Role>();
        }
    
        public int ID { get; set; }
        public Nullable<int> Creater { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
    
        public virtual ICollection<Menus> Menus { get; set; }
        public virtual ICollection<Base_Role> Base_Role { get; set; }
    }
}
