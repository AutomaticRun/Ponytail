using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail.Core
{
    /// <summary>
    /// Signal class, it can detect rising edge and falling edge.
    /// </summary>
    public class Signal
    {
        /// <summary>
        /// The old state of signal.
        /// </summary>
        public bool OldState { get; set; }

        /// <summary>
        /// The new state of signal.
        /// </summary>
        public bool NewState { get; set; }

        /// <summary>
        /// Update the state of signal.
        /// </summary>
        /// <param name="state"></param>
        public void Update(bool state)
        {
            OldState = NewState;
            NewState = state;

            if (OldState && NewState)
            {
                State= Edge.True;
            }
            if (!OldState && !NewState)
            {
                State= Edge.False;
            }
            if (OldState && !NewState)
            {
                State = Edge.Falling;
            }
            if (!OldState && NewState)
            {
                State = Edge.Rising;
            }
        }

        /// <summary>
        /// Signal state.
        /// </summary>
        public Edge State { get; private set; }

    }

    /// <summary>
    /// 沿
    /// </summary>
    public enum Edge
    {
        /// <summary>
        /// 上升沿
        /// </summary>
        Rising,
        /// <summary>
        /// 下降沿
        /// </summary>
        Falling,
        /// <summary>
        /// 真
        /// </summary>
        True,
        /// <summary>
        /// 假
        /// </summary>
        False
    }
}
