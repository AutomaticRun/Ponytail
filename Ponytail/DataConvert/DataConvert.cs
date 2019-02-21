using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail
{
    namespace DataConvert
    {
        /// <summary>
        /// 数据转换
        /// </summary>
        public static class DataConvert
        {
            /// <summary>
            /// 整型数组转浮点数，数据长度为2，低位在前，高位在后
            /// 例如：int[] p={ 0xCCCD, 0x4144}， 合并后为 0x4144CCCD，
            /// 转换为浮点数为12.3
            /// </summary>
            /// <param name="p">待转换的整型数据</param>
            /// <returns></returns>
            public static float TwoIntToFloat(int[] p)
            {
                if (p == null || p.Length != 2)
                {
                    throw new ArgumentNullException("参数不能为null,且长度必须为2");
                }

                int tempInt = p[1] << 16 | p[0];

                byte[] tempBytes = BitConverter.GetBytes(tempInt);

                var result = BitConverter.ToSingle(tempBytes, 0);

                return result;
            }

            /// <summary>
            /// Get bit from byte, convert it to bool.
            /// </summary>
            /// <param name="b">Source byte</param>
            /// <param name="index">the index of bit in the source byte</param>
            /// <returns></returns>
            public static bool GetBitFromByte(byte b,int index)
            {
                byte temp =(byte) (b << (7-index));
                temp = (byte)(temp >> 7);
                return temp == 0x01;
            }
        }
    }
}
