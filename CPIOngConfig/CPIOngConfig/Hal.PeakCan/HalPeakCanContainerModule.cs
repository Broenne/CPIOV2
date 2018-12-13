namespace Hal.PeakCan
{
    using System.Reflection;

    using Autofac;
    using Hal.PeakCan.Basics;
    using Hal.PeakCan.Contracts.Basics;
    using HardwareAbstaction.PCAN.Init;

    using Module = Autofac.Module;

    /// <summary>
    /// The container module.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class HalPeakCanContainerModule : Module
    {
        #region Protected Functions

        /// <summary>Override to add registrations to the container.</summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();

            builder.RegisterType<PreparePeakCan>().As<IPreparePeakCan>().SingleInstance();
            builder.RegisterType<ReadCanMessageEvent>().As<IReadCanMessageEvent>().SingleInstance();

        }

        #endregion
    }
}
