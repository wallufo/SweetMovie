using LinqKit;
using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SweetMoive.DAL.ModelManage
{
    public class MovieManage:BaseMannage<Movie>
    {
        #region 分页数据
        public Paging<Movie> FindPageList(Paging<Movie> pagingMovie, int? order)
        {
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "Score", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "Score", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "Score", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingMovie.Items = Repository.FindPageList(pagingMovie.PageSize, pagingMovie.PageIndex, out pagingMovie.TotalNumber, _order).ToList();
            return pagingMovie;
        }
        #endregion
        #region 动态查询分页数据
        public Paging<Movie> FindKeywordPageList(Paging<Movie> pagingMovie, int? order, string keyword, int selectval)
        {
            var _where = PredicateBuilder.True<Movie>();
            if (keyword != null) _where = _where.And(m => m.MovieName.Contains(keyword));
            if (selectval != 2) _where = _where.And(m => m.Hidden == (Movie.Hiddens)selectval);
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "ID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "ID", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "ID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingMovie.Items = Repository.FindWherePageList(pagingMovie.PageSize, pagingMovie.PageIndex, out pagingMovie.TotalNumber, _where.Expand(), _order).ToList();
            return pagingMovie;
        }
        #endregion
        #region 是否存在当前电影
        public bool HasMovieName(string moviename)
        {
            return base.Repository.isContains(u => u.MovieName == moviename);
        }
        #endregion
        #region 查找当前的最大的电影ID
        public int MovieId(Expression<Func<Movie,int>> where)
        {
            return base.Repository.FindMaxId(where);
        }
        #endregion
        #region 批量删除
        public Response Delete(List<int> ID)
        {
            Response _resp = new Response();
            int _totalDel = ID.Count;
            foreach (var i in ID)
            {
                base.Repository.Delete(new Movie() { ID = i }, false);
            }
            _resp.Data = base.Repository.Save();
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "条电影";
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }
        #endregion
        #region 电影排行榜

        #endregion
        #region 电影，演员，导演关键词搜索
        public Search<Movie> Search(Search<Movie> searchMovie,string keyword,int? order)
        {
            var _where = PredicateBuilder.True<Movie>();
            _where = _where.And(m => m.MovieName.Contains(keyword)||m.Actors.Contains(keyword)||m.Director.Contains(keyword));
            _where = _where.And(m => m.Hidden == Movie.Hiddens.显示);
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "Score", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "Score", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "Score", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            searchMovie.Items = Repository.FindWhere(_where.Expand(), _order).ToList();
            return searchMovie;
        }
        #endregion
    }
}
