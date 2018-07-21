using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// 获取当前数据库上下文
        /// </summary>
        /// <returns></returns>
        public static SweetMovieContext CurrentContext()
        {
            SweetMovieContext _smContext = CallContext.GetData("SweetMovieContext") as SweetMovieContext;
            if (_smContext == null)
            {
                _smContext = new SweetMovieContext();
                CallContext.SetData("SweetMovieContext", _smContext);
            }
            return _smContext;
        }
    }
}
