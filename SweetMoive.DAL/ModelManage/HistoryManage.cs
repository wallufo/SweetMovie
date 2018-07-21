using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class HistoryManage:BaseMannage<History>
    {
        #region 分页数据
        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="pagingHistory"></param>
        /// <param name="order">【0】HistoryID-正序，【1】HistoryID-倒序，【2】userID-正序，【3】UserID-倒序，【4】ViewTime-正序，【5】ViewTime-倒序</param>
        /// <returns></returns>
        public Paging<History> FindPageList(Paging<History> pagingHistory, int? order)
        {
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "HistoryID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "HistoryID", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                case 2:
                    _order = new OrderParamcs() { PropertyName = "UserID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 3:
                    _order = new OrderParamcs() { PropertyName = "UserID", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                case 4:
                    _order = new OrderParamcs() { PropertyName = "ViewTime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 5:
                    _order = new OrderParamcs() { PropertyName = "ViewTime", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "HistoryID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingHistory.Items = Repository.FindPageList(pagingHistory.PageSize, pagingHistory.PageIndex, out pagingHistory.TotalNumber, _order).ToList();
            return pagingHistory;
        }
        #endregion
    }
}
