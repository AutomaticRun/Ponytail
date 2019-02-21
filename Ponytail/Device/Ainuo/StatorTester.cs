using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ponytail.Device.Ainuo
{
    /// <summary>
    /// 定子综合测试仪，测试结果收取类
    /// </summary>
    public class StatorTester
    {
        #region 构造器

        /// <summary>
        /// 构造器，手动设置串口参数和码的字符长度
        /// </summary>
        public StatorTester()
        {
            Port = new SerialPort();
        }

        /// <summary>
        /// 构造器，串口默认参数：波特率=9600，数据位=8，无奇偶校验，停止位=1。
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="receiveTimeout">接收数据超时，单位毫秒</param>
        public StatorTester(string portName,int receiveTimeout)
        {
            Port = new SerialPort()
            {
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                PortName = portName,
                ReceivedBytesThreshold = 1,
            };
            ReceiveTimeout = receiveTimeout;
            Port.DataReceived += Port_DataReceived;
        }

        #endregion

        #region 公开属性

        /// <summary>
        /// 通信串口
        /// </summary>
        public SerialPort Port { get; set; }

        /// <summary>
        /// 接收数据超时，单位毫秒
        /// </summary>
        public int ReceiveTimeout { get; set; }

        #endregion

        #region 公开方法

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            if (!Port.IsOpen)
            {
                Port.Open();
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (Port.IsOpen)
            {
                Port.Close();
            }
        }

        #endregion

        #region 私有变量

        private DateTime _oldTime=new DateTime();

        #endregion

        #region 私有方法

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> data = new List<byte>();

            var startByte = Port.ReadByte();
            _oldTime = DateTime.Now;
            if (startByte == 0x7B)  // 如果检测到帧头
            {
                while (true)
                {
                    var time = (DateTime.Now - _oldTime).TotalMilliseconds;
                    if (time > ReceiveTimeout) break;

                    var temp = Port.ReadByte();
                    if (temp != 0x7D)
                    {
                        data.Add((byte)temp);
                    }
                    else  // 如果检测到帧尾
                    {
                        Decode(data);
                        break;
                    }
                }
            }

            Port.DiscardInBuffer();
        }

        // 解码
        private void Decode(List<byte> data)
        {
            if (CheckSum(data))  // 如果校验成功
            {
                var dataBytes = data.ToArray();
                TestResult result = new TestResult();

                int interTurnCount = 1;   // 匝间计数
                int resistanceCount = 1;  // 电阻计数
                int inductanceCount = 1;  // 电感计数

                result.IntegralTestResult = dataBytes[2];  // 总结果
                for (int i = 3; i < dataBytes .Length- 1;i++)
                {
                    Item item = (Item)data[i];
                    switch (item)
                    {
                        case Item.WithstandVoltage:  // 耐压
                            i += 1;
                            result.WithstandVoltage =(float) GetShort(dataBytes, i)/100;
                            i += 2;
                            result.WithstandVoltageResult = dataBytes[i];
                            break;
                        case Item.Insulated:  // 绝缘
                            i += 1;
                            result.InsulatedResistance = GetShort(dataBytes, i);
                            i += 2;
                            result.InsulatedResistanceResult= dataBytes[i];
                            break;
                        case Item.Interturn:  // 匝间
                            switch (interTurnCount)
                            {
                                case 1:
                                    i += 1;
                                    result.InterturnArea1 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InterturnDifferentialArea1 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InterturnResult1 = dataBytes[i];
                                    interTurnCount++;
                                    break;
                                case 2:
                                    i += 1;
                                    result.InterturnArea2 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InterturnDifferentialArea2 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InterturnResult2 = dataBytes[i];
                                    interTurnCount++;
                                    break;
                                case 3:
                                    i += 1;
                                    result.InterturnArea3 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InterturnDifferentialArea3 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InterturnResult3 = dataBytes[i];
                                    interTurnCount++;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Item.Resistance:  // 电阻
                            switch (resistanceCount)
                            {
                                case 1:
                                    i += 1;             
                                    result.Resistance1 =(float)GetInt(dataBytes, i)/1000;
                                    i += 4;
                                    result.ResistanceDeviation1 = (float)GetShort(dataBytes, i)/10;
                                    i += 2;
                                    result.ResistanceGear1= dataBytes[i];
                                    i += 1;
                                    result.ResistanceResult1 = dataBytes[i];
                                    resistanceCount++;
                                    break;
                                case 2:
                                    i += 1;
                                    result.Resistance2 = (float)GetInt(dataBytes, i) / 1000;
                                    i += 4;
                                    result.ResistanceDeviation2 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.ResistanceGear2 = dataBytes[i];
                                    i += 1;
                                    result.ResistanceResult2 = dataBytes[i];
                                    resistanceCount++;
                                    break;
                                case 3:
                                    i += 1;
                                    result.Resistance3 = (float)GetInt(dataBytes, i) / 1000;
                                    i += 4;
                                    result.ResistanceDeviation3 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.ResistanceGear3 = dataBytes[i];
                                    i += 1;
                                    result.ResistanceResult3 = dataBytes[i];
                                    resistanceCount++;
                                    break;
                                case 4:
                                    i += 1;                                 
                                    i += 4;                                   
                                    i += 2;
                                    i += 1;
                                    resistanceCount++;
                                    break;
                                case 5:
                                    i += 1;
                                    result.Resistance5 = (float)GetInt(dataBytes, i) / 100;
                                    i += 4;
                                    result.ResistanceDeviation5 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.ResistanceGear5 = dataBytes[i];
                                    i += 1;
                                    result.ResistanceResult5 = dataBytes[i];
                                    resistanceCount++;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Item.Inductance:  // 电感
                            switch (inductanceCount)
                            {
                                case 1:
                                    i += 1;
                                    result.Inductance1= (float)GetInt(dataBytes, i) / 1000;
                                    i += 4;
                                    result.InductanceDeviation1 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InductanceResult1 = dataBytes[i];
                                    inductanceCount++;
                                    break;
                                case 2:
                                    i += 1;
                                    result.Inductance2 = (float)GetInt(dataBytes, i) / 1000;
                                    i += 4;
                                    result.InductanceDeviation2 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InductanceResult2 = dataBytes[i];
                                    inductanceCount++;
                                    break;
                                case 3:
                                    i += 1;
                                    result.Inductance3 = (float)GetInt(dataBytes, i) / 1000;
                                    i += 4;
                                    result.InductanceDeviation3 = (float)GetShort(dataBytes, i) / 10;
                                    i += 2;
                                    result.InductanceResult3 = dataBytes[i];
                                    inductanceCount++;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                DataReceived?.Invoke(result, State.Success);
            }
            else  // 如果校验失败
            {
                DataReceived?.Invoke(null, State.CheckFailed);
            }
        }

        // 校验
        private bool CheckSum(List<byte> data)
        {
            // 数据帧出去帧头帧尾和校验和求和
            int sum = 0;
            for (int i = 0; i < data.Count - 1; i++)
            {
                sum += data[i];
            }

            // 取校验和的低字节
            byte[] bytes = BitConverter.GetBytes(sum);

            return bytes[0] == data[data.Count-1];
        }

        /// <summary>
        /// 从字节数组中获取int，低索引存放高字节
        /// </summary>
        /// <param name="datas">字节数据</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns></returns>
        private int GetInt(byte[] datas, int startIndex)
        {
            int data = 0;
            data = data + datas[startIndex] << 24;
            data = data + datas[startIndex+1] << 16;
            data = data + datas[startIndex+2] << 8;
            data = data + datas[startIndex+3];
            return data;
        }

        /// <summary>
        /// 从字节数组中获取short，低索引存放高字节
        /// </summary>
        /// <param name="datas">字节数据</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns></returns>
        private int GetShort(byte[] datas, int startIndex)
        {
            int data = 0;
            data = data + datas[startIndex] << 8;
            data = data + datas[startIndex + 1];
            return data;
        }

        #endregion

        #region 事件
        
        /// <summary>
        /// 收到数据委托
        /// </summary>
        /// <param name="result"></param>
        /// <param name="state"></param>
        public delegate void DataReceivedEventHandler(TestResult result, State state);

        /// <summary>
        /// 收到数据事件
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        #endregion

    }
}
