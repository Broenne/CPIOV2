namespace CPIOngConfig.FlipFlop
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using CPIOngConfig.Contracts.FlipFlop;

    using Prism.Mvvm;

    public class FlipFlopViewModel : BindableBase, IFlipFlopViewModel
    {
        private ObservableCollection<bool> flipFlopState;

        #region Constructor

        public FlipFlopViewModel()
        {
            var defaultList = new List<bool>();
            for (var i = 0; i < 16; i++)
            {
                defaultList.Add(i % 2 == 1);
            }

            this.FlipFlopState = new ObservableCollection<bool>(defaultList);
        }

        #endregion

        #region Properties

        public ObservableCollection<bool> FlipFlopState
        {
            get => this.flipFlopState;
            set => this.SetProperty(ref this.flipFlopState, value);
        }

        #endregion
    }
}