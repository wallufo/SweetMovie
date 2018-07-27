using LinqKit;
using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class ArticleManage:BaseMannage<Article>
    {
        #region 分页数据
        public Paging<Article> FindPageList(Paging<Article> pagingArticle, int? order)
        {
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "Releasetime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "Releasetime", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "Releasetime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingArticle.Items = Repository.FindPageList(pagingArticle.PageSize, pagingArticle.PageIndex, out pagingArticle.TotalNumber, _order).ToList();
            return pagingArticle;
        }
        #endregion
        #region 待审核文章分页数据
        public Paging<Article> FindAuditPageList(Paging<Article> pagingArticle, int? order)
        {
            var _where = PredicateBuilder.True<Article>();
            _where = _where.And(u => u.Auditstatus == Article.Status.待审核);
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "Releasetime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "Releasetime", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "Releasetime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingArticle.Items = Repository.FindWherePageList(pagingArticle.PageSize, pagingArticle.PageIndex, out pagingArticle.TotalNumber,_where.Expand(), _order).ToList();
            return pagingArticle;
        }
        #endregion
        #region 改变审核状态
        public Response ChangeStatus(int id,int status)
        {
            Response _resp = new Response();
            var _article = Find(id);
            if (_article == null)
            {
                _resp.Code = 0;
                _resp.Message = "当前文章不存在";
            }
            else
            {
                if (status == 0)
                {
                    _article.Auditstatus = Article.Status.审核不通过;
                    _resp = Update(_article);
                    _resp.Code = 3;
                }
                else
                {
                    _article.Auditstatus = Article.Status.审核通过;
                    _resp = Update(_article);
                    _resp.Code = 4;
                }
            }
            return _resp;
        }
        #endregion
        #region 未审核文章数量
        public int NoAuditNum()
        {
            return base.Repository.Count(p => p.Auditstatus == Article.Status.待审核);
        }
        #endregion
        #region 批量删除
        public Response Delete(List<int> ID)
        {
            Response _resp = new Response();
            int _totalDel = ID.Count;
            foreach (var i in ID)
            {
                base.Repository.Delete(new Article() { ID = i }, false);
            }
            _resp.Data = base.Repository.Save();
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "文章s";
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }
        #endregion
    }
}
