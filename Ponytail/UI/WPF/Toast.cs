using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Ponytail.UI.WPF
{
    /// <summary>
    /// 提示小窗体，自动消失
    /// </summary>
    public class Toast : Window
    {
        private readonly string _msg;
        private readonly int _length;
        private readonly Brush _lightGray= new SolidColorBrush(Colors.LightGray);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="length">显示时间</param>
        /// <param name="fanilyName">字体</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="brush">画刷</param>
        public Toast(string msg, Length length=Length.Short,string fanilyName="FangSong",int fontSize=20,Brush brush= null)
        {
            TextBlock text = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = double.NaN,
                Height = double.NaN,
                Text = msg,
                Margin = new Thickness(10, 5, 10, 5),
                FontSize= fontSize,
                FontFamily=new FontFamily(fanilyName)
            };

            Grid grid = new Grid
            {
                Width = double.NaN,
                Height = double.NaN,
            };

            Content = grid;
            grid.Children.Add(text);
            _msg = msg;
            _length = (int)length;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.None;
            Topmost = true;
            Background = brush ?? _lightGray;
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        /// <summary>
        /// 显示提示窗体
        /// </summary>
        public new void Show()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, _length);
            base.Show();
            timer.Start();          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 显示时间长度
        /// </summary>
        public enum Length
        {
            /// <summary>
            /// 500ms
            /// </summary>
            Short = 700,
            /// <summary>
            /// 1000ms
            /// </summary>
            Normal = 1000,
            /// <summary>
            /// 2000ms
            /// </summary>
            Long = 2000
        }
    }
}
