using LinqKit;
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
            return pagingUser;
        }
        #endregion
        #region 动态查询分页数据
        public Paging<User> FindKeywordPageList(Paging<User> pagingUser, int? order,string keyword,int selectval)
        {
            var _where = PredicateBuilder.True<User>();
            if(keyword!=null) _where = _where.And(m=>m.Username.Contains(keyword));
            if (selectval != 2) _where = _where.And(m => m.Userstatus == (User.UserStatus)selectval);
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
            pagingUser.Items = Repository.FindWherePageList(pagingUser.PageSize, pagingUser.PageIndex, out pagingUser.TotalNumber, _where.Expand(), _order).ToList();
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
        #region 批量删除
        public Response Delete(List<int> ID)
        {
            Response _resp = new Response();
            int _totalDel = ID.Count;
            foreach (var i in ID)
            {
                base.Repository.Delete(new User() { ID = i }, false);
            }
            _resp.Data = base.Repository.Save();
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "位用户";
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }
        #endregion
        #region 查找实体
        public User Find(string username)
        {
            return base.Repository.Find(x => x.Username.ToUpper() == username.ToUpper());
        }
        #endregion
        #region 按条件查找实体
        public User Find(Expression<Func<User,bool>> where)
        {
            return base.Repository.Find(where);
        }
        #endregion
        #region 验证账号密码
        public Response Verity(string username,string password)
        {
            Response _res = new Response();
            var _user = Find(username);
            if (_user == null)
            {
                _res.Code = 1;
                _res.Message = "账号或密码错误";
            }
            else if (_user.Password == password)
            {
                _res.Code = 2;
                _res.Message = "验证通过";
            }
            else
            {
                _res.Code = 3;
                _res.Message= "账号或密码错误";
            }
            return _res;
        }
        #endregion
    }
}
