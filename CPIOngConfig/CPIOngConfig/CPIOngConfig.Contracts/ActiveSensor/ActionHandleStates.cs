using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigLogicLayer.DigitalInputState
{
    public enum ActionHandleStates
    {
        // todo mb: vorbeitung für martin
        //Namur = 0,
        //Reed = 1,
        //Licht = 2,
        //Reset = 3,
        //Impulse = 4

        /// <summary>
        /// The none status.
        /// </summary>
        None = 0,

        /// <summary>
        /// The read info.
        /// </summary>
        Read = 1,

        /// <summary>
        /// The namur.
        /// </summary>
        Namur = 2,

        /// <summary>
        /// The licht.
        /// </summary>
        Licht = 3,

        /// <summary>
        /// The QMIN info.
        /// </summary>
        Qmin = 4,

        /// <summary>
        /// The QMAX info.
        /// </summary>
        Qmax = 5,

        /// <summary>
        /// The analog.
        /// </summary>
        Analog = 6
    }
}
