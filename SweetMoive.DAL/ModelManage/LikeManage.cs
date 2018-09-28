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
    public class LikeManage:BaseMannage<Like>
    {
        #region 返回评论的点赞总数
        public int LikeCounts(Expression<Func<Like, bool>> where)
        {
            return base.Repository.Count(where);
        }
        #endregion
        #region 分页数据
        public Paging<Like> FindPageList(Paging<Like> pagingLike, int? order)
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
            pagingLike.Items = Repository.FindPageList(pagingLike.PageSize, pagingLike.PageIndex, out pagingLike.TotalNumber, _order).ToList();
            return pagingLike;
        }
        #endregion
        #region 查找实体
        public Like Find(int userID, int commentID)
        {
            return base.Repository.Find(p => p.UserID == userID && p.MovieCommentID == commentID);
        }
        #endregion
    }
}
