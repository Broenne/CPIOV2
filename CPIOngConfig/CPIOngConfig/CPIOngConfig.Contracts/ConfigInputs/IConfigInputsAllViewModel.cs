using System.Collections.Generic;

namespace CPIOngConfig.ConfigInputs
{
    public interface IConfigInputsAllViewModel
    {
        List<IConfigureInputsView> ConfigureInputsViewList { get; }
        IConfigureInputsView Test { get; }
    }
}