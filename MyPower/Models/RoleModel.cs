﻿using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPower.Models
{
    public class RoleModel
    {
        public int total = 0;
        public List<Base_RoleValue> rows = new List<Base_RoleValue>();
    }

    public class Base_RoleValue
    {
        public int ID { get; set; }
        public string Creater { get; set; }
        public string Createtime { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }



    public class UserRoleIn
    {
        public int usrId
        {
            get;
            set;
        }

        public string roleIds
        {
            get;
            set;
        }
    }
}