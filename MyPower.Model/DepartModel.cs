﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Model
{
    public class DepartModel
    {
        public int ID { get; set; }
        public string Creater { get; set; }
        public string Createtime { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IsDepart { get; set; }
        public string Manager { get; set; }
        public Nullable<int> ParentId { get; set; }

        public List<DepartModel> children = new List<DepartModel>();
    }


    public class TreeDepartModel
    {
        public int id { get; set; }
        public string text { get; set; }

        public List<TreeDepartModel> children = new List<TreeDepartModel>();

        public int ParentId { get; set; }
    }


    public class DepartMentOut
    {
        public int id
        {
            get;
            set;
        }
        public int pId
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public bool open
        {
            get;
            set;
        }
        public bool @checked
        {
            get;
            set;
        }

        public bool isParent
        {
            get;
            set;
        }
    }
}
