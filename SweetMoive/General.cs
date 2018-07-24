using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive
{
    public class General
    {
        /// <summary>
        /// 获取模型错误
        /// </summary>
        /// <param name="modelState">模型状态</param>
        /// <returns></returns>
        public static string GetModelErrorString(ModelStateDictionary modelState)
        {
            StringBuilder _sb = new StringBuilder();
            var _ErrorModelState = modelState.Where(m => m.Value.Errors.Count() > 0);
            foreach (var item in _ErrorModelState)
            {
                foreach (var modelError in item.Value.Errors)
                {
                    _sb.AppendLine(modelError.ErrorMessage);
                }
            }
            return _sb.ToString();
        }
    }
}