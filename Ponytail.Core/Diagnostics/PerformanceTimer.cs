using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ponytail.Core.Diagnostics
{
    /// <summary>
    /// High accuracy timer
    /// </summary>
   public class PerformanceTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
          out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
          out long lpFrequency);

        private long _startTime;
        private long _freq;


        /// <summary>
        /// Constructor
        /// </summary>
        public PerformanceTimer()
        {
            _startTime = 0;

            if (QueryPerformanceFrequency(out _freq) == false)
            {
                throw new Exception("High performance timer not supported.");
            }
        }

        /// <summary>
        /// Start count
        /// </summary>
        public void Start()
        {
            QueryPerformanceCounter(out _startTime);
        }

        /// <summary>
        /// Elapsed time
        /// </summary>
        public long Elapsed
        {
            get
            {
                QueryPerformanceCounter(out long temp);
                return temp - _startTime;
            }
        }

        /// <summary>
        /// Elapsed seconds
        /// </summary>
        public double ElapsedSeconds
        {
            get
            {
                QueryPerformanceCounter(out long temp);
                return (temp - _startTime)/ (double)_freq;
            }
        }

        /// <summary>
        /// Elapsed milliseconds
        /// </summary>
        public double ElapsedMilliseconds
        {
            get
            {
                return ElapsedSeconds * 1000;
            }
        }
    }
}
