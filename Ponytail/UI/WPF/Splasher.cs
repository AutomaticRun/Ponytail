using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Ponytail.UI.WPF
{
    /// <summary>
    /// 显示和关闭窗体的辅助类
    /// </summary>
    public  class Splasher<T> where T:Window,new()
    {
        private bool _isAlive;

        /// <summary>
        /// 打开启动窗体
        /// </summary>
        public  void ShowSplash()
        {           
            Thread t=new Thread(()=> 
            {
                _isAlive = true;
                var screen = new T();
                while (_isAlive)
                {
                    screen.Show();
                    screen.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Background);
                    Thread.Sleep(10);
                }
                screen = null;
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary>
        /// 关闭启动窗体
        /// </summary>
        public  void CloseSplash()
        {
            _isAlive = false;
        }
    }
}
