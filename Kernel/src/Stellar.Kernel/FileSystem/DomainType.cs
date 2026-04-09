namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Specifies the kind of file system domain.
    /// </summary>
    public enum DomainType
    {
        /// <summary>
        /// A regular directory on the operating system's file system.
        /// </summary>
        Directory,

        /// <summary>
        /// An assembly file (e.g., .dll or .exe) that may contain embedded resources.
        /// </summary>
        Assembly,
    }
}