using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail.UI.WPF.MvvmLightEx.Message
{
    /// <summary>
    /// 带一个参数的消息
    /// </summary>
    public class PackageMessage : NotificationMessage
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="msg">消息</param>
        public PackageMessage(string msg) : base(msg)
        {

        }

        /// <summary>
        /// 携带的参数或返回值
        /// </summary>
        public object Package { get; set; }
    }
}
