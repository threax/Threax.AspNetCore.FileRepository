using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public class FileRepository : IFileRepository
    {
        private String baseDir;
        private IFileVerifier fileVerifier;

        public FileRepository(String baseDir, IFileVerifier fileVerifier)
        {
            this.baseDir = Path.GetFullPath(baseDir);
            this.fileVerifier = fileVerifier;
        }

        public async Task SaveFile(String fileName, String mimeType, Stream stream)
        {
            fileVerifier.Validate(stream, fileName, mimeType);

            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            var path = GetPhysicalPath(fileName);

            using (var write = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await stream.CopyToAsync(write);
            }
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

        private String GetPhysicalPath(string fileName)
        {
            if (fileName == null)
            {
                throw new InvalidOperationException("Filename cannot be null.");
            }

            var path = Path.GetFullPath(Path.Combine(baseDir, fileName));
            if (!path.StartsWith(baseDir))
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
