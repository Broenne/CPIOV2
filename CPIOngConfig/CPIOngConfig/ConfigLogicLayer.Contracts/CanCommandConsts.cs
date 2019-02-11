namespace ConfigLogicLayer.Contracts
{
    public static class CanCommandConsts
    {
        #region Constants

        public const uint SensorModiResponse = 0x179;

        public const uint SetActiveSensor = 0x176;

        public const uint AliveOffset = 0x200;

        public const uint CountOfErrorBytes = 0x04;

        #endregion

        public static readonly uint TriggerGetInputConfigurationOffset = 0x0178;
    }
}