using System.IO;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem.Provider
{
    public interface IFileProvider : IQuant
    {
        /// <summary>
        /// Checks whether FileProvider can operate with Domain
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        bool CanHandle(IDomain domain);

        /// <summary>
        /// Checks if File exists in given Location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        bool Exists(ILocation location);

        /// <summary>
        /// Get file information
        /// </summary>
        /// <returns>File information</returns>
        IFileInfo GetFileInfo(ILocation location);

        /// <summary>
        /// Open file for reading
        /// </summary>
        /// <returns>FileStream allowed to read</returns>
        Stream OpenRead(ILocation location);

        /// <summary>
        /// Open file for reading
        /// </summary>
        /// <returns>FileStream allowed to write</returns>
        Stream OpenWrite(ILocation location);

        /// <summary>
        /// Open file for reading
        /// </summary>
        /// <returns>FileStream allowed to read and write</returns>
        Stream OpenReadWrite(ILocation location);

        /// <summary>
        /// General method for access open file
        /// </summary>
        /// <param name="location">File location</param>
        /// <param name="access">FileAccess</param>
        /// <returns></returns>
        Stream Open(ILocation location, FileAccess access);
    }
}