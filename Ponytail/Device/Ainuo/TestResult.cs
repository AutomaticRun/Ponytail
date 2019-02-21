using System;
using System.Reflection;
using System.Text;

namespace Ponytail.Device.Ainuo
{
    /// <summary>
    /// 定子综合测试结果类
    /// </summary>
    public class TestResult
    {
        #region 公开属性

        /// <summary>
        /// 综合测试结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int IntegralTestResult { get; set; }
        /// <summary>
        /// 电阻
        /// </summary>
        public float Resistance1 { get; set; }
        /// <summary>
        /// 电阻项偏差
        /// </summary>
        public float ResistanceDeviation1 { get; set; }
        /// <summary>
        /// 电阻项挡位
        /// </summary>
        public int ResistanceGear1 { get; set; }
        /// <summary>
        /// 电阻项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int ResistanceResult1 { get; set; }
        /// <summary>
        /// 电阻
        /// </summary>
        public float Resistance2 { get; set; }
        /// <summary>
        /// 电阻项偏差
        /// </summary>
        public float ResistanceDeviation2 { get; set; }
        /// <summary>
        /// 电阻项挡位
        /// </summary>
        public int ResistanceGear2 { get; set; }
        /// <summary>
        /// 电阻项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int ResistanceResult2 { get; set; }
        /// <summary>
        /// 电阻
        /// </summary>
        public float Resistance3 { get; set; }
        /// <summary>
        /// 电阻项偏差
        /// </summary>
        public float ResistanceDeviation3 { get; set; }
        /// <summary>
        /// 电阻项挡位
        /// </summary>
        public int ResistanceGear3 { get; set; }
        /// <summary>
        /// 电阻项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int ResistanceResult3 { get; set; }
        /// <summary>
        /// 接地电阻
        /// </summary>
        public float Resistance5 { get; set; }
        /// <summary>
        /// 接地电阻项偏差
        /// </summary>
        public float ResistanceDeviation5 { get; set; }
        /// <summary>
        /// 接地电阻项挡位
        /// </summary>
        public int ResistanceGear5 { get; set; }
        /// <summary>
        /// 接地电阻项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int ResistanceResult5 { get; set; }
        /// <summary>
        /// 电感
        /// </summary>
        public float Inductance1 { get; set; }
        /// <summary>
        /// 电感偏差
        /// </summary>
        public float InductanceDeviation1 { get; set; }
        /// <summary>
        /// 电感项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InductanceResult1 { get; set; }
        /// <summary>
        /// 电感
        /// </summary>
        public float Inductance2 { get; set; }
        /// <summary>
        /// 电感偏差
        /// </summary>
        public float InductanceDeviation2 { get; set; }
        /// <summary>
        /// 电感项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InductanceResult2 { get; set; }
        /// <summary>
        /// 电感
        /// </summary>
        public float Inductance3 { get; set; }
        /// <summary>
        /// 电感偏差
        /// </summary>
        public float InductanceDeviation3 { get; set; }
        /// <summary>
        /// 电感项结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InductanceResult3 { get; set; }
        /// <summary>
        /// 绝缘电阻
        /// </summary>
        public int InsulatedResistance { get; set; }
        /// <summary>
        /// 绝缘结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InsulatedResistanceResult { get; set; }
        /// <summary>
        /// 耐压
        /// </summary>
        public float WithstandVoltage { get; set; }
        /// <summary>
        /// 耐压结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int WithstandVoltageResult { get; set; }
        /// <summary>
        /// 匝间实测面积
        /// </summary>
        public float InterturnArea1 { get; set; }
        /// <summary>
        /// 匝间实测差积
        /// </summary>
        public float InterturnDifferentialArea1 { get; set; }
        /// <summary>
        /// 匝间结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InterturnResult1 { get; set; }
        /// <summary>
        /// 匝间实测面积
        /// </summary>
        public float InterturnArea2 { get; set; }
        /// <summary>
        /// 匝间实测差积
        /// </summary>
        public float InterturnDifferentialArea2 { get; set; }
        /// <summary>
        /// 匝间结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InterturnResult2 { get; set; }
        /// <summary>
        /// 匝间实测面积
        /// </summary>
        public float InterturnArea3 { get; set; }
        /// <summary>
        /// 匝间实测差积
        /// </summary>
        public float InterturnDifferentialArea3 { get; set; }
        /// <summary>
        /// 匝间结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int InterturnResult3 { get; set; }
        /// <summary>
        /// 旋向，0=不转，1=顺转，2=逆转
        /// </summary>
        public int TurnDirection { get; set; }
        /// <summary>
        /// 旋向结果，0x5A=合格，0xA5=不合格，0x00=测试尚未结束
        /// </summary>
        public int TurnDirectionResult { get; set; }

        #endregion

        /// <summary>
        /// 获取结果数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Type type = GetType();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                sb.Append($"{pi.Name}: {pi.GetValue(this)}{Environment.NewLine}");
            }
            return sb.ToString();
        }
    }
}
