using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPIOngConfig.InputBinary
{
    using System.Windows.Media;

    using Prism.Mvvm;

    public class InOutState : BindableBase
    {
        private SolidColorBrush color;

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PortForUi" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public InOutState(int position, SolidColorBrush color)
        {
            this.Position = position;
            this.Color = color;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public SolidColorBrush Color
        {
            get => this.color;

            set => this.SetProperty(ref this.color, value);
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public int Position { get; set; }

        #endregion
    }
}
