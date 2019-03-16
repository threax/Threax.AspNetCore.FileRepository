using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileRepository
    {
        /// <summary>
        /// Save a stream to the repository. It will be validated first. This is the preferred way to write streams
        /// unless you need more control, then see Validate and OpenWriteNoValidate.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="mimeType">The mime type to verify the file with.</param>
        /// <param name="stream">The stream to write.</param>
        /// <returns></returns>
        Task WriteFile(String fileName, String mimeType, Stream stream);

        /// <summary>
        /// Validate that the stream is valid. Will throw an exception if there is a problem.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="mimeType">The mime type to verify the file with.</param>
        /// <param name="stream">The stream to validate.</param>
        /// <returns></returns>
        Task Validate(string fileName, string mimeType, Stream stream);

        /// <summary>
        /// Open a write stream, if the file exists it will be overwritten, this will not validate the stream first. This can be used if you need
        /// more control over the stream, but be careful since none of the validation you setup will be executed.
        /// Ideally use WriteFile so the validation will run or call Validate before opening this stream. If you trust
        /// the source you can also just open this stream and write. The caller must dispose the returned stream.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns></returns>
        Task<Stream> OpenWriteNoValidate(string fileName);

        /// <summary>
        /// Open a stream to read a file. The caller must dispose the returned stream.
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