using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DataLibrary
{
    /// <summary>
    /// 数据存储类
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class Repository<T> where T:class
    {
        #region 数据上下文
        public DbContext DbContext { get; set; }
        public Repository()
        {

        }
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion
        #region 查找单个实体
        /// <summary>
        /// 用ID查找实体
        /// </summary>
        /// <param name="ID">实体主键</param>
        /// <returns></returns>
        public T Find(int ID)
        {
            return DbContext.Set<T>().Find(ID);
        }
        /// <summary>
        /// 使用lambda表达式查找实体
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        public T Find(Expression<Func<T,bool>> where)
        {
            return DbContext.Set<T>().SingleOrDefault(where);
        }
        #endregion
        #region 查找多个实体
        public IQueryable<T> FindList()
        {
            return DbContext.Set<T>();
        }
        /// <summary>
        /// 使用lambda表达式查询多个实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T,bool>> where)
        {
            return DbContext.Set<T>().Where(where);
        }
        /// <summary>
        /// 使用lambda表达式来返回一定数量的实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T,bool>> where,int num)
        {
            return DbContext.Set<T>().Where(where).Take(num);
        }
        /// <summary>
        /// 使用排序方式返回一定数量的实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="num">数量</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T,bool>> where,OrderParamcs order)
        {
            return FindList(where, order,0);
        }
        /// <summary>
        /// 返回一定数量的用一定方式排序实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="order">排序方式</param>
        /// <param name="num">数量 【0-不起用】</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T,bool>> where,OrderParamcs order,int num)
        {
            OrderParamcs[] _order = null;
            if (order != null) _order = new OrderParamcs[] { order };
            return FindList(where, _order, num);
        }
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T,bool>> where,OrderParamcs[] order,int num)
        {
            var _list = DbContext.Set<T>().Where(where);
            var _order = Expression.Parameter(typeof(T), "0");
            if (order != null && order.Length > 0)
            {
                for(var i = 0; i < order.Length; i++)
                {
                    var _property = typeof(T).GetProperty(order[i].PropertyName);
                    var _propertAccess = Expression.MakeMemberAccess(_order, _property);
                    var _orderByExp = Expression.Lambda(_propertAccess, _order);
                    string _orderName = order[i].Method == OrderParamcs.OrderMethod.ASC ? "Order" : "OrderByDescending";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), _orderName, new Type[] { typeof(T), _property.PropertyType }, _list.Expression, Expression.Quote(_orderByExp));
                    _list = _list.Provider.CreateQuery<T>(resultExp);
                }
            }
            if (num > 0) _list = _list.Take(num);
            return _list;
        }

        #endregion
        #region 增加实体
        public int Add(T entity)
        {
            return Add(entity, true);
        }
        public int Add(T entity,bool isSave)
        {
            DbContext.Set<T>().Add(entity);
            return isSave ?  DbContext.SaveChanges(): 0;
        }
        #endregion
        #region 更新实体
        public int Update(T entity)
        {
            return Update(entity, true);
        }
        public int Update(T entity,bool isSave)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                HandleDetached(entity);
            }
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Modified;
            return isSave ? DbContext.SaveChanges() : 0;
        }
        /// <summary>
        /// 将当前线程中的上下文移除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool HandleDetached(T entity)
        {
            var objectContext = ((IObjectContextAdapter)DbContext).ObjectContext;
            var entitySet = objectContext.CreateObjectSet<T>();
            var entityKey = objectContext.CreateEntityKey(entitySet.EntitySet.Name, entity);
            object foundSet;
            bool exists = objectContext.TryGetObjectByKey(entityKey, out foundSet);
            if (exists)
            {
                objectContext.Detach(foundSet); //从上下文中移除
            }
            return exists;
        }
        #endregion
        #region 删除实体
        public int Delete(T entity)
        {
            return Delete(entity, true);
        }
        public int Delete(T entity,bool isSave)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Deleted;
            return isSave ? DbContext.SaveChanges() : 0;
        }
        #endregion
        #region 删除实体列表
        public int Delete(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }
        #endregion
        #region 实体数量
        public int Count()
        {
            return DbContext.Set<T>().Count();
        }
        public int Count(Expression<Func<T,bool>> where)
        {
            return DbContext.Set<T>().Count(where);
        }
        #endregion
        #region 是否存在
        public bool isContains(Expression<Func<T,bool>> where)
        {
            return Count(where) > 0;
        }
        #endregion
        #region 保存实体
        public int Save()
        {
            return DbContext.SaveChanges();
        }
        #endregion
        #region 实体列表分页
        public IQueryable<T> FindPageList(int pageSize,int pageIndex,out int totalNumber)
        {
            OrderParamcs _order = null;
            return FindPageList(pageSize, pageIndex, out totalNumber, _order);
        }
        public IQueryable<T> FindPageList(int pageSize,int pageIndex,out int totalNumber,OrderParamcs order)
        {
            OrderParamcs[] _order = null;
            if (order != null) _order = new OrderParamcs[] { order };
            return FindPageList(pageSize, pageIndex, out totalNumber, _order);
        }
        public IQueryable<T> FindPageList(int pageSize,int pageIndex,out int totalNumber,OrderParamcs[] order)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 5;
            IQueryable<T> _list = DbContext.Set<T>();
            var _order = Expression.Parameter(typeof(T), "0");
            if (order != null && order.Length > 0)
            {
                for (var i = 0; i < order.Length; i++)
                {
                    var _property = typeof(T).GetProperty(order[i].PropertyName);
                    var _propertAccess = Expression.MakeMemberAccess(_order, _property);
                    var _orderByExp = Expression.Lambda(_propertAccess, _order);
                    string _orderName = order[i].Method == OrderParamcs.OrderMethod.ASC ? "OrderBy" : "OrderByDescending";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), _orderName, new Type[] { typeof(T), _property.PropertyType }, _list.Expression, Expression.Quote(_orderByExp));
                    _list = _list.Provider.CreateQuery<T>(resultExp);
                }
            }
            totalNumber = _list.Count();
            return _list.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        #endregion
    }
}
