using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class FavoriteManage:BaseMannage<Favorite>
    {
        #region 分页数据
        public Paging<Favorite> FindPageList(Paging<Favorite> pagingFavor,int? order)
        {
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "FavoriteID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "FavoriteID", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                case 2:
                    _order = new OrderParamcs() { PropertyName = "UserID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 3:
                    _order = new OrderParamcs() { PropertyName = "UserID", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "FavoriteID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingFavor.Items = Repository.FindPageList(pagingFavor.PageSize, pagingFavor.PageIndex, out pagingFavor.TotalNumber, _order).ToList();
            return pagingFavor;
        }
        #endregion
        #region 查找实体
        public Favorite Find(int? movieid,int userid)
        {
            return base.Repository.Find(p => p.MovieID == movieid && p.UserID == userid);
        }
        #endregion
    }
}
