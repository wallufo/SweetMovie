using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL
{
    /// <summary>
    /// 管理基类
    /// </summary>
    /// <typeparam name="T">模型类</typeparam>
    public class BaseMannage<T> where T:class
    {
        #region 数据上下文注入
        /// <summary>
        /// 数据存储类
        /// </summary>
        protected Repository<T> Repository;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseMannage() : this(ContextFactory.CurrentContext())
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        public BaseMannage(DbContext dbContext)
        {
            Repository = new Repository<T>(dbContext);
        }
        #endregion
        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public virtual Response Add(T entity)
        {
            Response _res = new Response();
            if (Repository.Add(entity) > 0)
            {
                _res.Code = 1;
                _res.Message = "添加数据成功";
                _res.Data = entity;
            }
            else
            {
                _res.Code = 0;
                _res.Message = "添加数据失败";
            }
            return _res;
        }
        #endregion
        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Response Update(T entity)
        {
            Response _res = new Response();
            if (Repository.Update(entity) > 0)
            {
                _res.Code = 1;
                _res.Message = "更新数据成功";
                _res.Data = entity;
            }
            else
            {
                _res.Code = 0;
                _res.Message = "更新数据失败";
            }
            return _res;
        }
        #endregion
        #region 删除数据
        public virtual Response Delete(int ID)
        {
            Response _res = new Response();
            var entity = Find(ID);
            if (entity == null)
            {
                _res.Code = 10;
                _res.Message = "数据不存在";
            }
            else
            {
                if (Repository.Delete(entity) > 0)
                {
                    _res.Code = 1;
                    _res.Message = "删除数据成功";
                    _res.Data = entity;
                }
                else
                {
                    _res.Code = 0;
                    _res.Message = "删除数据失败";
                }
            }         
            return _res;
        }
        #endregion
        #region 查找数据
        public T Find(int? ID)
        {
            return Repository.Find(ID);
        }
        #endregion
        #region 查找数据列表
        public IQueryable<T> FindList()
        {
            return Repository.FindList();
        }
        #endregion
        #region 数据数量
        public int Count()
        {
            return Repository.Count();
        }
        public int Count(Expression<Func<T,bool>> where)
        {
            return Repository.Count(where);
        }
        #endregion
        #region 寻找最大ID
        #endregion
        #region 数据分页
        public Paging<T> FindPageList(Paging<T> paging)
        {
            paging.Items = Repository.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber).ToList();
            return paging;
        }
        #endregion
    }
}
