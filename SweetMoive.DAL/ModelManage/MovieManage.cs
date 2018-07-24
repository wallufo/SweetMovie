using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SweetMoive.DAL.ModelManage
{
    public class MovieManage:BaseMannage<Movie>
    {
        #region 分页数据
        public Paging<Movie> FindPageList(Paging<Movie> pagingUser, int? order)
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
            pagingUser.Items = Repository.FindPageList(pagingUser.PageSize, pagingUser.PageIndex, out pagingUser.TotalNumber, _order).ToList();
            return pagingUser;
        }
        #endregion
      
    }
}
