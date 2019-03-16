using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileRepository
    {
        /// <summary>
        /// Open a write stream in the repository.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="mimeType">The mime type to verify the file with.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        Task<Stream> OpenWrite(String fileName, String mimeType, Stream stream);

        Task<Stream> OpenRead(String fileName);

        Task<bool> Exists(String fileName);

        Task<bool> DirectoryExists(String path);

        Task DeleteFile(String fileName);

        /// <summary>
        /// Enumerate through the directories in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="searchOption">The search options</param>
        /// <returns></returns>
        Task<IEnumerable<String>> GetDirectories(String path, String searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Enumerate through the files in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="searchOption">The search options</param>
        /// <returns></returns>
        Task<IEnumerable<String>> GetFiles(String path, String searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
    }

    public interface IFileRepository<InjectT> : IFileRepository
    {

    }
}