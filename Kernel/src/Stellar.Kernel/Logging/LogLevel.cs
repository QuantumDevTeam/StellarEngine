namespace Stellar.Kernel.Logging
{
    /// <summary>
    /// Level of log
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Any Information
        /// </summary>
        Info,
        
        /// <summary>
        /// Debug message or information
        /// </summary>
        Debug,
        
        /// <summary>
        /// Operation succeed
        /// </summary>
        Success,
        
        /// <summary>
        /// A Warning
        /// </summary>
        Warning,
        
        /// <summary>
        /// An Error
        /// </summary>
        Error,
        
        /// <summary>
        /// An Exception
        /// </summary>
        Exception
    }
}