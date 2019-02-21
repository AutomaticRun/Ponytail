using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Ponytail.UI.WPF.Animation
{
    /// <summary>
    /// 随机跳动动画
    /// </summary>
    public class RandomJitterEase : EasingFunctionBase
    {
        // Store a random number generator.
        private Random _rand = new Random();

        /// <summary>
        /// The amount of jitter 0~2000.
        /// </summary>
        public int Jitter
        {
            get { return (int)GetValue(JitterProperty); }
            set { SetValue(JitterProperty, value); }
        }

        /// <summary>
        /// The amount of jitter
        /// </summary>
        public static readonly DependencyProperty JitterProperty =
            DependencyProperty.Register("Jitter", typeof(int), typeof(RandomJitterEase), new UIPropertyMetadata(1000), new ValidateValueCallback(ValidateJitter));

        private static bool ValidateJitter(object value)
        {
            int jitterValue = (int)value;
            return ((jitterValue <= 2000) && (jitterValue >= 0));
        }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new RandomJitterEase();
        }

        /// <summary>
        /// Perform easing
        /// </summary>
        /// <param name="normalizedTime">Normalized time</param>
        /// <returns></returns>
        protected override double EaseInCore(double normalizedTime)
        {
            double result;
            // Make sure there's no jitter in the final value.
            if (normalizedTime == 1)
            {
                result = 1;
            }

            // Offset the value by a random amount.
            result = Math.Abs(normalizedTime - (double)_rand.Next(0, 10) / (2010 - Jitter));
            Debug.WriteLine(result);
            return result;
        }
    }
}
