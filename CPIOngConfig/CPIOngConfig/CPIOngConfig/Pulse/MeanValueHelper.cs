//namespace CPIOngConfig.Pulse
//{
//    using System.Collections.Generic;
//    using System.Linq;

//    /// <summary>
//    /// The mean value helper.
//    /// </summary>
//    public class MeanValueHelper
//    {
//        #region Constructor

//        /// <summary>
//        /// Initializes a new instance of the <see cref="MeanValueHelper"/> class.
//        /// </summary>
//        public MeanValueHelper()
//        {
//            // todoo mb: in eigen service
//            this.StorageForMeanValue = new Dictionary<byte, List<double>>();
//            for (byte i = 0; i < 16; i++)
//            {
//                this.StorageForMeanValue.Add(i, new List<double>());
//            }
//        }

//        #endregion

//        #region Properties

//        private Dictionary<byte, List<double>> StorageForMeanValue { get; }

//        #endregion

//        #region Internal Methods


//        private const int MeanValueDeepth = 100;

//        /// <summary>
//        /// Adds to mean value storage.
//        /// </summary>
//        /// <param name="channel">The channel.</param>
//        /// <param name="value">The value.</param>
//        /// <returns>Return the avevarge.</returns>
//        internal double AddToMeanValueStorage(byte channel, double value)
//        {
//            var lis = this.StorageForMeanValue[channel];

//            if (lis.Count >= MeanValueDeepth)
//            {
//                lis.Remove(0);  
//            }

//            // also hier nicht länger als 99 (größe -1 )

//            // mittelwert, mittelwert tiefe nachher beschränken
//            lis.Add(value);

//            // hier wieder größe
//            double avr = lis.Average();

//            return avr;
//        }

//        #endregion
//    }
//}