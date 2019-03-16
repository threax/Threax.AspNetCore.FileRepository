using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileRepository
    {
        /// <summary>
        /// Save a stream to the repository. It will be verified first.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="mimeType">The mime type to verify the file with.</param>
        /// <param name="stream">The stream to write.</param>
        /// <returns></returns>
        Task Write(String fileName, String mimeType, Stream stream);

        /// <summary>
        /// Open a stream to read a file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns></returns>
        Task<Stream> OpenRead(String fileName);

        /// <summary>
        /// Test to see if a file exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<bool> FileExists(String fileName);

        /// <summary>
        /// Test to see if a directory exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> DirectoryExists(String path);

        /// <summary>
        /// Delete a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task DeleteFile(String fileName);

        /// <summary>
        /// Delete a directory.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        Task DeleteDirectory(String path, bool recursive = false);

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