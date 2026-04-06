namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Reason for stoping execution
    /// </summary>
    public enum StopReason
    {
        /// <summary>
        /// Unknown reason
        /// </summary>
        Unknown,
        
        /// <summary>
        /// regular stoping
        /// </summary>
        Regular,
        
        /// <summary>
        /// if any module just in unload operation
        /// </summary>
        ModuleUnloading,
        
        /// <summary>
        /// if execution is stopping with critical error
        /// </summary>
        CriticalError,
    }
}