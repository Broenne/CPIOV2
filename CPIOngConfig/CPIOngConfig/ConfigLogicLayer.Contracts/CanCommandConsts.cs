namespace ConfigLogicLayer.Contracts
{
    /// <summary>
    /// The can commands.
    /// </summary>
    public static class CanCommandConsts
    {
        #region Constants

        public const uint SensorModiResponse = 0xFFFFFC;//0x179;

        public const uint SetActiveSensor = 0xFFFFFA;//0x176;

        public const uint AliveOffset = 0xFFFFFE; // 0x200;

        public const uint CountOfErrorBytes = 0x04;

        public const uint Text = 0xFFFFFF; // 0x177;

        public const uint InputState = 0xFFFFFD; // 0x177;

        public const uint PulseId = 0x1;

        public const uint RequestAnalogValue = 0x175;

        public const uint FlipFlop = 0x00;

        public const uint ResetFlipFlop = 0x4;

        #endregion

        public const uint TriggerGetInputConfigurationOffset = 0xFFFFFB;
        //public static readonly uint TriggerGetInputConfigurationOffset = 0x0178;
    }
}