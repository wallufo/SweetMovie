using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DataLibrary
{
    public class OrderParamcs
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderMethod Method { get; set; }
        public enum OrderMethod
        {
            /// <summary>
            /// 正序
            /// </summary>
            ASC,
            /// <summary>
            /// 倒序
            /// </summary>
            DESC
        }
    }
}
