﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.CommonFuc
{
    /// <summary>
    /// 定义相等比较器 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class selector<T> : IEqualityComparer<T> where T : class
    {
        Func<T, T, bool> pred;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pred">对象相等比较方法</param>
        /// <param name="_GetHashCode">对象哈希值获取</param>
        public selector(Func<T, T, bool> _pred)
        {
            pred = _pred;
        }

        public bool Equals(T x, T y)
        {
            return pred(x, y);
        }

        /// <summary>
        /// 该方法如果结果不同 不会执行 Equals 方法 所以在这里 直接 返回0 这样 就一定会执行Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
