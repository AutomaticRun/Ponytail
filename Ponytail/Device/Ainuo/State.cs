using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail.Device.Ainuo
{
    /// <summary>
    /// 获取数据状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 校验失败
        /// </summary>
        CheckFailed,
        /// <summary>
        /// 解码失败
        /// </summary>
        DecodeFailed,
    }
}
