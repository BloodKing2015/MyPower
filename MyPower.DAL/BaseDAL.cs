using MyPower.DB;
using MyPower.Factory;
using MyPower.IDAL;
using MyPower.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.DAL
{
    public class BaseDAL<TEntity> : IBaseDAL<TEntity> where TEntity : class
    {
        #region 变量及构造函数

        /// <summary>
        /// DbContext对象
        /// </summary>
        protected MyPowerConStr baseContext;

        /// <summary>
        /// 指定类型的实体对象集合
        /// </summary>
        protected DbSet<TEntity> objectSet;   

        public void InitData(MyPowerConStr pcon)
        {
            this.baseContext = pcon;
            this.objectSet = this.baseContext.Set<TEntity>();
        }

        public MyPowerConStr GetDB()
        {
            return this.baseContext;
        }

        #endregion


        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool Insert(TEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                objectSet.Add(entity);
                result = baseContext.SaveChanges();
            }
            return result > 0;
        }


        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(object id)
        {
            TEntity obj = objectSet.Find(id);
            if (obj != null)
            {
                objectSet.Remove(obj);
                return baseContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="key">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool Update(TEntity t, object key)
        {
            if (t == null)
            {
                return false;
            }

            bool result = false;
            TEntity existing = objectSet.Find(key);
            if (existing != null)
            {
                baseContext.Entry(existing).CurrentValues.SetValues(t);
                result = baseContext.SaveChanges() > 0;
            }
            return result;
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">ID主键的值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual TEntity FindByID(object id)
        {
            return objectSet.Find(id);
        }


        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> match)
        {
            return objectSet.FirstOrDefault(match);
        }


        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return this.objectSet;
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
            IQueryable<TEntity> query = this.objectSet;
            if (match != null)
            {
                query = query.Where(match);
            }
            if (isDescending == false)
            {
                return query.OrderBy(sortMatch);
            }
            else
            {
                return query.OrderByDescending(sortMatch);
            }
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns></returns>
        public virtual ICollection<TEntity> Find(Expression<Func<TEntity, bool>> match)
        {
            return this.objectSet.Where(match).ToList();
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
            ICollection<TEntity> result;
            if (isDescending == false)
            {
                result = this.objectSet.Where(match).OrderBy(orderByProperty).ToList();
            }
            else
            {
                result = this.objectSet.Where(match).OrderByDescending(orderByProperty).ToList();
            }
            return result;
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
            int pageindex = (info.CurrenetPageIndex < 1) ? 1 : info.CurrenetPageIndex;
            int pageSize = (info.PageSize <= 0) ? 20 : info.PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<TEntity> query = GetQueryable().Where(match);
            RecordCount = query.Count();

            return query.Skip(excludedRows).Take(pageSize).ToList();
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
            int pageindex = (info.CurrenetPageIndex < 1) ? 1 : info.CurrenetPageIndex;
            int pageSize = (info.PageSize <= 0) ? 20 : info.PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<TEntity> query = GetQueryable().Where(match);
            RecordCount = query.Count();
            if (isDescending == false)
            {
                query = query.OrderBy(orderByProperty);
            }
            else
            {
                query = query.OrderByDescending(orderByProperty);
            }

            return query.Skip(excludedRows).Take(pageSize).ToList();
        }
    }
}
