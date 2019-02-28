namespace ConfigLogicLayer.Contracts
{
    /// <summary>
    /// The can commands.
    /// </summary>
    public static class CanCommandConsts
    {
        #region Constants

        public const uint SensorModiResponse = 0x179;

        public const uint SetActiveSensor = 0x176;

        public const uint AliveOffset = 0x200;

        public const uint CountOfErrorBytes = 0x04;

        public const uint Text = 0xFFFFFF;//0x177;

        public const uint PulseId = 0x180;

        public const uint RequestAnalogValue = 0x175;

        #endregion

        public static readonly uint TriggerGetInputConfigurationOffset = 0x0178;
    }
}