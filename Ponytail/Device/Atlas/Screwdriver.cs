using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Threading;

namespace Ponytail.Device.Atlas
{
    /// <summary>
    /// 阿特拉斯螺丝枪驱动器通信类
    /// </summary>
    public class Screwdriver
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">螺丝枪驱动器IP</param>
        /// <param name="port">螺丝枪驱动器端口</param>
        public Screwdriver(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        #region 公开属性

        /// <summary>
        /// 通信客户端
        /// </summary>
        public Socket Client { get; set; }


        #endregion

        #region 私有变量

        private bool _receiving;
        private const int BufferSize = 1024;  // Socket 接收数据缓冲区大小
        private byte[] _buffer = new byte[BufferSize];  // Socket 接收数据缓冲区
        private ConcurrentQueue<byte> _cache = new ConcurrentQueue<byte>();  // 数据缓存区
        private IList<byte> _data = new List<byte>();  // 数据帧字节数组

        #endregion

        #region 私有方法

        /// <summary>
        /// 从服务端接收数据
        /// </summary>
        private void Receive()
        {
            while (_receiving)
            {
                try
                {
                    var byteCount = Client.Receive(_buffer);
                    for (int i = 0; i < byteCount; i++)
                    {
                        _cache.Enqueue(_buffer[i]);
                    }
                }
                catch (SocketException)
                {
                    //throw ex;
                }
            }
        }

        /// <summary>
        /// 提取数据，并调用解析程序
        /// </summary>
        private void ExtractData()
        {
            while (_receiving)
            {
                if (_cache.Count > 0)  // 如果缓存中有字节
                {
                    byte temp;
                    if (_cache.TryDequeue(out temp))  // 如果成功取到字节
                    {
                        if (temp == 0xF3) // 如果发现帧头
                        {
                            if (_data.Count > 0)
                            {
                                _data.Clear();
                            }
                            _data.Add(temp);

                            while (_receiving)  // 直到取满一帧数据，方可结束
                            {
                                if (_cache.TryDequeue(out temp))
                                {
                                    _data.Add(temp);
                                    if (temp == 0xEE)  // 如果发现帧尾
                                    {
                                        Decode();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    Thread.Sleep(5);
                }
            }
        }

        /// <summary>
        /// 解析数据，并调用事件处理程序
        /// </summary>
        private void Decode()
        {
            var data = new Result();

            // 计算扭矩
            var digitCount = 7;
            float scale;
            for (int i = 0; i < digitCount; i++)
            {
                int temp;
                int.TryParse(((char)_data[i + 6]).ToString(), out temp);
                scale = (float)Math.Pow(10, digitCount - i - 1);
                data.Torque += temp * scale;
            }
            scale = (float)Math.Pow(0.1, 4);
            data.Torque *= scale;

            // 计算角度
            digitCount = 4;
            for (int i = 0; i < digitCount; i++)
            {
                int temp;
                int.TryParse(((char)_data[i + 13]).ToString(), out temp);
                scale = (float)Math.Pow(10, digitCount - i - 1);
                data.Angle += (int)(temp * scale);
            }

            // 计算工作结果
            data.Qualified = _data[17] == 0x42;

            // 调用事件处理程序
            Screw?.Invoke(data);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 螺丝枪驱动器IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 螺丝枪驱动器端口
        /// </summary>
        public int Port { get; set; }

        #endregion

        #region 公开方法

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ip = IPAddress.Parse(IP);
            Client.Connect(new IPEndPoint(ip, Port));
            _receiving = true;

            // 开始接收数据线程
            Task.Factory.StartNew(Receive, TaskCreationOptions.LongRunning);
            // 开始提取数据并解析线程
            Task.Factory.StartNew(ExtractData, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            _receiving = false;

            if (Client.Connected)
            {
                Client.Shutdown(SocketShutdown.Both);
                Client.Close();
            }

        }

        #endregion

        #region 事件

        /// <summary>
        /// 接收到数据委托
        /// </summary>
        /// <param name="result"></param>
        public delegate void ScrewEventHandler(Result result);

        /// <summary>
        /// 拧螺丝事件
        /// </summary>
        public event ScrewEventHandler Screw;

        #endregion

        /// <summary>
        /// 结果
        /// </summary>
        public struct Result
        {
            /// <summary>
            /// 扭矩
            /// </summary>
            public float Torque;
            /// <summary>
            /// 转角
            /// </summary>
            public int Angle;
            /// <summary>
            /// 结果
            /// </summary>
            public bool Qualified;

            /// <summary>
            /// 获取结果字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var result = Qualified == true ? "合格" : "不合格";
                return $"扭矩：{Torque}  角度：{Angle}  结果：{result}";
            }
        }
    }
}
