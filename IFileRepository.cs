using System.IO;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileRepository
    {
        void DeleteFile(string fileName);
        bool Exists(string fileName);
        Task SaveFile(string fileName, string mimeType, Stream stream);
    }

    public interface IFileRepository<InjectT> : IFileRepository
    {

    }
}