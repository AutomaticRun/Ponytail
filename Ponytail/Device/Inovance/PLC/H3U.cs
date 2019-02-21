using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail.Device.Inovance.PLC
{
    /// <summary>
    /// 汇川H3U PLC 地址转换类（Modbus TCP 通信）
    /// </summary>
    public class H3U
    {
        #region H3U系列软元件起始地址

        #region 线圈

        // M0-M7679
        private const ushort Coil_M_StartAddress1 = 0x00;
        private const ushort Coil_M_Count1 = 7680;
        // M8000-M8511
        private const ushort Coil_M_StartAddress2 = 0x1F40;
        private const ushort Coil_M_StartNo2 = 8000;
        private const ushort Coil_M_Count2 = 512;
        // SM0_SM1023
        private const ushort Coil_SM_StartAddress = 0x2400;
        private const ushort Coil_SM_Count = 1024;
        // S0-S4095
        private const ushort Coil_S_StartAddress = 0xE000;
        private const ushort Coil_S_Count = 4096;
        // T0-T511
        private const ushort Coil_T_StartAddress = 0xF000;
        private const ushort Coil_T_Count = 512;
        // C0-C255
        private const ushort Coil_C_StartAddress = 0xF400;
        private const ushort Coil_C_Count = 512;
        // X0-X377
        private const ushort Coil_X_StartAddress = 0xF800;
        private const ushort Coil_X_Count = 256;
        // Y0-Y377
        private const ushort Coil_Y_StartAddress = 0xFC00;
        private const ushort Coil_Y_Count = 256;

        #endregion

        #region 寄存器，除特殊标注均为16位

        // D0-D8511
        private const ushort Register_D_StartAddress = 0x00;
        private const ushort Register_D_Count = 8512;
        // SD0-SD1023
        private const ushort Register_SD_StartAddress = 0x2400;
        private const ushort Register_SD_Count = 1024;
        // R0-R32767
        private const ushort Register_R_StartAddress = 0x3000;
        private const ushort Register_R_Count = 32768;
        // T0-T511
        private const ushort Register_T_StartAddress = 0xF000;
        private const ushort Register_T_Count = 512;
        // C0-C199 起始地址
        private const ushort Register_C_StartAddress1 = 0xF400;
        private const ushort Register_C_Count1 = 200;
        // C200-C255 起始地址，寄存器为32位
        private const ushort Register_C_StartAddress2 = 0xF700;
        private const ushort Register_C_StartNo2 = 200;
        private const ushort Register_C_Count2 = 56;

        #endregion

        #endregion

        /// <summary>
        /// M0-M7679和M8000-M8511
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort M(ushort number)
        {
            if (number>= 0 && number< Coil_M_Count1)
            {
                return (ushort)(number+Coil_M_StartAddress1);
            }
            if (number >= Coil_M_StartNo2 && number <Coil_M_StartNo2+ Coil_M_Count2)
            {
                return (ushort)(number-Coil_M_StartNo2 + Coil_M_StartAddress2);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// SM0_SM1023
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort SM(ushort number)
        {
            if (number>=0 && number<Coil_SM_Count)
            {
                return (ushort)(number + Coil_SM_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// S0-S4095
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort S(ushort number)
        {
            if (number >= 0 && number < Coil_S_Count)
            {
                return (ushort)(number + Coil_S_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// T0-T511，线圈
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort CoilT(ushort number)
        {
            if (number >= 0 && number < Coil_T_Count)
            {
                return (ushort)(number + Coil_T_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// C0-C255，线圈
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort CoilC(ushort number)
        {
            if (number >= 0 && number < Coil_C_Count)
            {
                return (ushort)(number + Coil_C_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// X0-X377
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort X(ushort number)
        {
            if (number >= 0 && number < Coil_X_Count)
            {
                return (ushort)(number + Coil_X_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// Y0-Y377
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort Y(ushort number)
        {
            if (number >= 0 && number < Coil_Y_Count)
            {
                return (ushort)(number + Coil_Y_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// D0-D8511
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort D(ushort number)
        {
            if (number >= 0 && number < Register_D_Count)
            {
                return (ushort)(number + Register_D_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// SD0-SD1023
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort SD(ushort number)
        {
            if (number >= 0 && number < Register_SD_Count)
            {
                return (ushort)(number + Register_SD_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// R0-R32767
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort R(ushort number)
        {
            if (number >= 0 && number < Register_R_Count)
            {
                return (ushort)(number + Register_R_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// R0-R32767，寄存器
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort RegisterT(ushort number)
        {
            if (number >= 0 && number < Register_T_Count)
            {
                return (ushort)(number + Register_T_StartAddress);
            }
            throw new Exception("参数超出范围！");
        }

        /// <summary>
        /// C0-M199和C200-C255
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static ushort RegisterC(ushort number)
        {
            if (number >= 0 && number < Register_C_Count1)
            {
                return (ushort)(number + Register_C_StartAddress1);
            }
            if (number >= Register_C_StartNo2 && number < Register_C_StartNo2 + Register_C_Count2)
            {
                return (ushort)(number - Register_C_StartNo2 + Register_C_StartAddress2);
            }
            throw new Exception("参数超出范围！");
        }
    }
}
