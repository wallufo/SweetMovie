using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class CommentManage:BaseMannage<MovieComment>
    {
        #region 分页数据
        public Paging<MovieComment> FindPageList(Paging<MovieComment> pagingComment, int? order)
        {
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
            pagingComment.Items = Repository.FindPageList(pagingComment.PageSize, pagingComment.PageIndex, out pagingComment.TotalNumber, _order).ToList();
            
            return pagingComment;
        }
        #endregion
        #region 验证数据
        /// <summary>
        /// 一个用户对一个电影只能产生一条评论
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool HasUserComment(int userid,int movieid)
        {
            return base.Repository.isContains(p => p.UserID == userid && p.MovieID == movieid);
        }
        #endregion
        #region 批量删除
        public Response Delete(List<int> ID)
        {
            Response _resp = new Response();
            int _totalDel = ID.Count;
            foreach(var i in ID)
            {
                base.Repository.Delete(new MovieComment() { ID = i }, false);
            }
            _resp.Data = base.Repository.Save();
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "评论";
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }
        #endregion
        #region 添加评论
        public override Response Add(MovieComment movieComment)
        {
            Response _resp = new Response();
            if (!string.IsNullOrEmpty(movieComment.UserID.ToString()) && !string.IsNullOrEmpty(movieComment.MovieID.ToString()))
            {
                _resp.Code = 2;
                _resp.Message = "此用户已评论过该电影";
            }
            if (_resp.Code == 0) _resp = base.Add(movieComment);
            return _resp;
        }
        #endregion
    }
}
