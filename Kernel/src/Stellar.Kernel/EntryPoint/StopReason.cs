namespace Stellar.Kernel.EntryPoint
{
    public enum StopReason
    {
        Unknown,
        Regular,
        ModuleUnloading,
        CriticalError,
    }
}