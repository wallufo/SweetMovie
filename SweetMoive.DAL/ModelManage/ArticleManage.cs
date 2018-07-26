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
    }
}
