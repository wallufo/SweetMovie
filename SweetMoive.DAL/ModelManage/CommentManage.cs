﻿using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    _order = new OrderParamcs() { PropertyName = "CommentTime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "CommentTime", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "CommentTime", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingComment.Items = Repository.FindPageList(pagingComment.PageSize, pagingComment.PageIndex, out pagingComment.TotalNumber, _order).ToList();
            return pagingComment;
        }
        #endregion
    }
}