using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem.Provider
{
    /// <summary>
    /// File Provider Factory for identifying specific Provider by its Domain
    /// </summary>
    public interface IFileProviderFactory
        : IQuant
    {
        /// <summary>
        /// Get specific Provider by Domain
        /// </summary>
        /// <param name="domain">Domain</param>
        /// <returns>File provider</returns>
        IFileProvider GetFileProvider(IDomain domain);
    }
}