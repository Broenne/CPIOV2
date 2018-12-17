namespace CPIOngConfig.InputBinary
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using ConfigLogicLayer.Contracts.DigitalInputState;
    using ConfigLogicLayer.DigitalInputState;

    using CPIOngConfig.Contracts.InputBinary;

    using Global.UiHelper;

    using Helper.Contracts.Logger;

    using Prism.Mvvm;

    /// <summary>
    /// The input binary view.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="CPIOngConfig.InputBinary.IInputBinaryViewModel" />
    public class InputBinaryViewModel : BindableBase, IInputBinaryViewModel
    {
        private readonly SolidColorBrush gray;

        private readonly SolidColorBrush green;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBinaryViewModel"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="inputBinaryEventHandler">The input binary event handler.</param>
        /// <param name="stimulate">The stimulate.</param>
        /// <param name="handleInputs">The handle inputs.</param>
        public InputBinaryViewModel(ILogger logger, IInputBinaryEventHandler inputBinaryEventHandler, IStimulate stimulate, IHandleInputs handleInputs)
        {
            this.Logger = logger;
            this.Stimulate = stimulate;
            this.GetCommand = new RelayCommand(this.GetCommandAction);

            this.gray = new SolidColorBrush(Colors.Gray);
            this.green = new SolidColorBrush(Colors.Green);

            this.Input = new List<InOutState>();
            for (var i = 0; i < 16; ++i)
            {
                this.Input.Add(new InOutState(0, this.gray));
            }

            inputBinaryEventHandler.EventIsReached += this.InputBinaryEventHandler_EventIsReached;
            handleInputs.Start();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the get command.
        /// </summary>
        /// <value>
        /// The get command.
        /// </value>
        public ICommand GetCommand { get; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public List<InOutState> Input { get; set; }

        private ILogger Logger { get; }

        private IStimulate Stimulate { get; }

        #endregion

        #region Private Methods

        private void InputBinaryEventHandler_EventIsReached(object sender, InputBinaryEventArgs e)
        {
            try
            {
                var i = 0;
                foreach (var item in e.Store)
                {
                    this.Input[i].Color = item.Value ? this.green : this.gray;
                    ++i;
                }
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCommandAction(object obj)
        {
            try
            {
                this.Stimulate.RequestById(4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        #endregion
    }
}