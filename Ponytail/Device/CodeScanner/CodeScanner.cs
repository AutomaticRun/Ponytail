using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Ponytail.Device.CodeScanner
{
    /// <summary>
    /// 扫码枪类，通过串口通信，串口默认参数：波特率=9600，数据位=8，无奇偶校验，停止位=1。
    /// 扫描到的码为16进制的ASCII码。
    /// 扫码枪获取到码后会自动发送数据，触发扫码完成事件。
    /// </summary>
    public class CodeScanner
    {
        #region 构造器

        /// <summary>
        /// 构造器，手动设置串口参数和码的字符长度
        /// </summary>
        [Obsolete("请用 public CodeScanner(string portName, int charCount)")]
        public CodeScanner()
        {
            Port = new SerialPort();
        }

        /// <summary>
        /// 构造器，串口默认参数：波特率=9600，数据位=8，无奇偶校验，停止位=1。
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="charCount">扫描码的固定字符数</param>
        public CodeScanner(string portName, int charCount)
        {
            Port = new SerialPort()
            {
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                PortName = portName,               
            };
            CharCount = charCount;
            Port.ReceivedBytesThreshold = CharCount;

            Port.DataReceived += Port_DataReceived;
        }

        #endregion

        #region 公开属性

        /// <summary>
        /// 扫码枪通信串口
        /// </summary>
        public SerialPort Port { get; set; }

        private int charCount;
        /// <summary>
        /// 码的字符长度
        /// </summary>
        public int CharCount
        {
            get { return charCount; }
            set
            {
                charCount = value;
                Port.ReceivedBytesThreshold = value + 2;
            }
        }


        #endregion

        #region 公开方法

        /// <summary>
        /// 打开串口
        /// </summary>
        public  void Open()
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

        /// <summary>
        /// 清空接收缓冲区
        /// </summary>
        public void DiscardInBuffer()
        {
            Port?.DiscardInBuffer();
        }

        #endregion

        #region 私有方法

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = sender as SerialPort;
            var msg = new StringBuilder();
            char[] data = new char[CharCount];

            port.Read(data, 0, CharCount);
            port.DiscardInBuffer();

            for (int i = 0; i < data.Length; i++)
            {
                msg.Append(data[i]);
            }

            DataReceived?.Invoke(msg.ToString());
        }

        #endregion

        #region 事件

        /// <summary>
        /// 收到数据委托
        /// </summary>
        /// <param name="code"></param>
        public delegate void DataReceivedEventHandler(string code);

        /// <summary>
        /// 扫码事件
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        #endregion

    }
}
