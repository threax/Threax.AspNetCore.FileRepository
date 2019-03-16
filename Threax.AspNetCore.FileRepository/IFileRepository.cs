using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileRepository
    {
        void DeleteFile(string fileName);
        bool Exists(string fileName);
        bool DirectoryExists(String path);
        Task SaveFile(string fileName, string mimeType, Stream stream);
        Stream OpenFile(string fileName);

        /// <summary>
        /// Enumerate through the directories in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="searchOption">The search options</param>
        /// <returns></returns>
        IEnumerable<String> GetDirectories(String path, String searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Enumerate through the files in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="searchOption">The search options</param>
        /// <returns></returns>
        IEnumerable<String> GetFiles(String path, String searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
    }

    public interface IFileRepository<InjectT> : IFileRepository
    {

    }
}