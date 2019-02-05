namespace CPIOngConfig
{
    using System.Reflection;

    using Autofac;

    using CPIOngConfig.Adapter;
    using CPIOngConfig.Alive;
    using CPIOngConfig.Contracts.Adapter;
    using CPIOngConfig.Contracts.Alive;
    using CPIOngConfig.Contracts.FactorPulse;
    using CPIOngConfig.Contracts.FlipFlop;
    using CPIOngConfig.Contracts.InputBinary;
    using CPIOngConfig.Contracts.Pulse;
    using CPIOngConfig.FactorPulse;
    using CPIOngConfig.FlipFlop;
    using CPIOngConfig.InputBinary;
    using CPIOngConfig.Pulse;

    using Module = Autofac.Module;

    /// <summary>
    ///     The container module.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class CPIOngConfigContainerModule : Module
    {
        #region Protected Methods

        /// <summary>Override to add registrations to the container.</summary>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();

            builder.RegisterType<PulseEventHandler>().As<IPulseEventHandler>().SingleInstance();
            builder.RegisterType<InputBinaryEventHandler>().As<IInputBinaryEventHandler>().SingleInstance();
            builder.RegisterType<AliveEventHandler>().As<IAliveEventHandler>().SingleInstance();
            builder.RegisterType<CanIsConnectedEventHandler>().As<ICanIsConnectedEventHandler>().SingleInstance();
            builder.RegisterType<FlipFlopEventHandler>().As<IFlipFlopEventHandler>().SingleInstance();
            builder.RegisterType<FactorPulseEventHandler>().As<IFactorPulseEventHandler>().SingleInstance();
        }

        #endregion
    }
}