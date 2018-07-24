using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class UserManage:BaseMannage<User>
    {
        #region 分页数据
        public Paging<User> FindPageList(Paging<User> pagingUser, int? order)
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
            //foreach(var item in pagingUser.Items)
            //{
            //    item.Role = Models.User.Roles.普通用户;
            //}
            return pagingUser;
        }
        #endregion
        #region 验证数据
        public bool HasUserName(string username)
        {
            return base.Repository.isContains(u => u.Username.ToUpper() == username.ToUpper());
        }
        public bool HasUserEmail(string email)
        {
            return base.Repository.isContains(u => u.EmailAdress.ToUpper() == email.ToUpper());
        }
        #endregion
        #region 添加用户
        public override Response Add(User user)
        {
            Response _resp = new Response();
            if (!string.IsNullOrEmpty(user.Username) && HasUserName(user.Username))
            {
                _resp.Code = 2;
                _resp.Message = "用户名已存在";
            }
            if (!string.IsNullOrEmpty(user.EmailAdress) && HasUserEmail(user.EmailAdress))
            {
                _resp.Code = 3;
                _resp.Message = "邮箱已存在";
            }
            if (_resp.Code == 0) _resp = base.Add(user);
            return _resp;
        }
        #endregion
    }
}
