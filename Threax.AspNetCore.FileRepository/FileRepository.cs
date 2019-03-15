﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public class FileRepository : IFileRepository
    {
        private String baseDir;
        private int removePathLength;
        private IFileVerifier fileVerifier;

        public FileRepository(String baseDir, IFileVerifier fileVerifier)
        {
            this.baseDir = Path.GetFullPath(baseDir);
            this.removePathLength = this.baseDir.Length + 1;
            this.fileVerifier = fileVerifier;
        }

        public async Task SaveFile(String fileName, String mimeType, Stream stream)
        {
            fileVerifier.Validate(stream, fileName, mimeType);

            if (!Directory.Exists(baseDir))
            {
                throw new InvalidOperationException("File repository directory does not exist.");
            }

            var path = GetPhysicalPath(fileName);

            //Make sure final directory exists
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var write = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await stream.CopyToAsync(write);
            }
        }

        public Stream OpenFile(String fileName)
        {
            if (!Directory.Exists(baseDir))
            {
                throw new FileNotFoundException("Cannot find file", fileName);
            }

            var path = GetPhysicalPath(fileName);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Cannot find file", fileName);
            }

            return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public bool Exists(String fileName)
        {
            if (!Directory.Exists(baseDir))
            {
                return false;
            }

            var physical = GetPhysicalPath(fileName);
            return File.Exists(physical);
        }

        public void DeleteFile(String fileName)
        {
            var physical = GetPhysicalPath(fileName);
            if (File.Exists(physical))
            {
                File.Delete(physical);
            }
        }

        /// <summary>
        /// Enumerate through the directories in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="searchOption">The search options</param>
        /// <returns></returns>
        public IEnumerable<String> GetDirectories(String path, String searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var physical = GetPhysicalPath(path);
            var dirs = Directory.GetDirectories(physical, searchPattern, searchOption);
            return dirs.Select(i => i.Substring(removePathLength));
        }

        /// <summary>
        /// Enumerate through the files in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The pattern to search for.</param>
        /// <param name="searchOption">The search options</param>
        /// <returns></returns>
        public IEnumerable<String> GetFiles(String path, String searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var physical = GetPhysicalPath(path);
            var files = Directory.GetFiles(physical, searchPattern, searchOption);
            return files.Select(i => i.Substring(removePathLength));
        }

        private String GetPhysicalPath(string fileName)
        {
            if (fileName == null)
            {
                throw new InvalidOperationException("Filename cannot be null.");
            }

            var path = Path.GetFullPath(Path.Combine(baseDir, fileName));
            if (!path.StartsWith(baseDir)) //This will fail if the uploader tried to navigate out of our upload folder.
            {
                throw new InvalidOperationException($"Illegal file name {fileName}");
            }
            return path;
        }
    }

    public class FileRepository<InjectT> : FileRepository, IFileRepository<InjectT>
    {
        public FileRepository(string baseDir, IFileVerifier fileVerifier) 
            : base(baseDir, fileVerifier)
        {
        }
    }
}
