﻿using Microsoft.Practices.Unity;
using MyPower.DB;
using MyPower.Factory;
using MyPower.IBLL;
using MyPower.IDAL;
using MyPower.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Buiness
{
    public class BaseBLL<TEntity> : IBaseBLL<TEntity> where TEntity : class
    {
        #region 构造
        private static readonly object syncRoot = new Object();

        protected IBaseDAL<TEntity> baseDAL { get; set; }
        protected IUnityContainer container { get; set; }

        /// <summary>
        /// 默认构造函数。
        /// 默认获取缓存的容器，如果没有则创建容器，并注册所需的接口实现。
        /// </summary>
        public BaseBLL()
        {
            lock (syncRoot)
            {
                container = DALFactory.Instance.Container;
                if (container == null)
                {
                    throw new ArgumentNullException("container", "container没有初始化");
                }
            }
        }
        #endregion


        public void InitData(MyPowerConStr pcon)
        {
            baseDAL.InitData(pcon);
        }

        public MyPowerConStr GetDB()
        {
            return baseDAL.GetDB();
        }
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool Insert(TEntity entity)
        {
            return baseDAL.Insert(entity);
        }


        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(object id)
        {
            return baseDAL.Delete(id);
        }


        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="key">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool Update(TEntity t, object key)
        {
            return baseDAL.Update(t, key);
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">ID主键的值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual TEntity FindByID(object id)
        {
            return baseDAL.FindByID(id);
        }


        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> match)
        {
            return baseDAL.FindSingle(match);
        }


        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return baseDAL.GetQueryable();
        }
        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="match">查询条件</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable<TKey>(Expression<Func<TEntity, bool>> match,
            Expression<Func<TEntity, TKey>> sortMatch, bool isDescending = true)
        {
            return baseDAL.GetQueryable<TKey>(match, sortMatch, isDescending);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns></returns>
        public virtual ICollection<TEntity> Find(Expression<Func<TEntity, bool>> match)
        {
            return baseDAL.Find(match);
        }


        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual ICollection<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> match,
            Expression<Func<TEntity, TKey>> orderByProperty,
            bool isDescending = true)
        {
            return baseDAL.Find<TKey>(match, orderByProperty, isDescending);
        }


        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="info">分页实体</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns>指定对象的集合</returns>
        public virtual ICollection<TEntity> FindWithPager(Expression<Func<TEntity, bool>> match,
            PagerInfo info, out int RecordCount)
        {
            return baseDAL.FindWithPager(match, info, out RecordCount);
        }


        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="info">分页实体</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns>指定对象的集合</returns>
        public virtual ICollection<TEntity> FindWithPager<TKey>(Expression<Func<TEntity, bool>> match, PagerInfo info,
            Expression<Func<TEntity, TKey>> orderByProperty, out int RecordCount, bool isDescending = true)
        {
            return baseDAL.FindWithPager<TKey>(match, info, orderByProperty, out RecordCount, isDescending);
        }
    }
}
