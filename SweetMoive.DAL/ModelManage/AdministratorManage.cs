using SweetMoive.DAL.Models;
using SweetMoive.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.ModelManage
{
    public class AdministratorManage : BaseMannage<Administrator>
    {
        #region 分页数据
        public Paging<Administrator> FindPageList(Paging<Administrator> pagingAdministrator, int? order)
        {
            OrderParamcs _order;
            switch (order)
            {
                case 0:
                    _order = new OrderParamcs() { PropertyName = "AdministratorID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
                case 1:
                    _order = new OrderParamcs() { PropertyName = "AdministratorID", Method = OrderParamcs.OrderMethod.DESC };
                    break;
                default:
                    _order = new OrderParamcs() { PropertyName = "AdministratorID", Method = OrderParamcs.OrderMethod.ASC };
                    break;
            }
            pagingAdministrator.Items = Repository.FindPageList(pagingAdministrator.PageSize, pagingAdministrator.PageIndex, out pagingAdministrator.TotalNumber, _order).ToList();
            return pagingAdministrator;
        }
        #endregion
        #region 是否存在账号
        public bool HasAccounts(string accounts)
        {
            return base.Repository.isContains(x => x.Accounts.ToUpper() == accounts.ToUpper());
        }
        #endregion
        #region 查找实体
        public Administrator Find(string accounts)
        {
            return base.Repository.Find(x => x.Accounts.ToUpper() == accounts.ToUpper());
        }
        #endregion
        #region 添加管理员
        public override Response Add(Administrator entity)
        {
            Response _res = new Response();
            if (HasAccounts(entity.Accounts))
            {
                _res.Code = 0;
                _res.Message = "管理员已经存在";
            }
            else
            {
                _res = base.Add(entity);
            }
            return _res;
        }
        #endregion
        #region 数据删除
        public override Response Delete(int ID)
        {
            Response _res = new Response();
            var entity = base.Find(ID);
            if (Count() == 1)
            {
                _res.Code = 0;
                _res.Message = "不能删除唯一的管理员";
            }
            else _res = base.Delete(ID);
            return _res;
        }
        #endregion
        #region 批量删除数据
        public Response Delete(List<int> ID)
        {
            Response _res = new Response();
            int _totalDel = ID.Count;
            int _totalAdmin = Count();
            foreach(var i in ID)
            {
                if (_totalAdmin > 1)
                {
                    base.Repository.Delete(new Administrator() { AdministratorID = i }, false);
                    _totalAdmin--;
                }
                else _res.Message = "至少保留一名管理员";
            }
            _res.Data = base.Repository.Save();
            if (_res.Data == _totalDel)
            {
                _res.Code = 1;
                _res.Message = "成功删除" + _res.Data + "管理员";
            }
            else if (_res.Data > 0)
            {
                _res.Code = 2;
                _res.Message = "成功删除" + _res.Data + "管理员";
            }
            else
            {
                _res.Code = 0;
                _res.Message = "删除失败";
            }
            return _res;

        }
        #endregion
        #region 验证账号密码
        /// <summary>
        /// 验证账号密码
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="password"></param>
        /// <returns>【1】管理员不存在,【2】验证通过,【3】账号密码不正确</returns>
        public Response Verity(string accounts,string password)
        {
            Response _res = new Response();
            var _admin = Find(accounts);
            if (_admin==null)
            {
                _res.Code = 1;
                _res.Message = "账号为：【"+accounts+"】的管理员不存在";
            }
            else if (_admin.Password == password)
            {
                _res.Code = 2;
                _res.Message = "验证通过";
            }
            else
            {
                _res.Code = 3;
                _res.Message = "账号或密码错误";
            }
            return _res;
        }
        #endregion
    }
}
