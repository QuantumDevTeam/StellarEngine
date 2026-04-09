namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Defines the possible reasons why an entry point or module stopped execution.
    /// </summary>
    public enum StopReason
    {
        /// <summary>
        /// The stop reason cannot be determined.
        /// </summary>
        Unknown,

        /// <summary>
        /// Normal, expected shutdown (e.g., application exit).
        /// </summary>
        Regular,

        /// <summary>
        /// Shutdown caused by unloading of a module.
        /// </summary>
        ModuleUnloading,

        /// <summary>
        /// Shutdown due to an unrecoverable critical error.
        /// </summary>
        CriticalError,
    }
}