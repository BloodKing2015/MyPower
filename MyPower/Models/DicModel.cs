using MyPower.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPower.Models
{
    public class DicModel
    {
        public int total = 0;
        public List<Base_Dictionary> rows = new List<Base_Dictionary>();
    }

    public class DicValueModel
    {
        public int total = 0;
        public List<DicValue> rows = new List<DicValue>();
    }

    public class DicValue
    {
        public string name { get; set; }
        public string value { get; set; }
        public string group { get; set; }
        public string editor = "text";
    }
}