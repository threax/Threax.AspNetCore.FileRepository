using System.IO;
using System.Threading.Tasks;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileRepository
    {
        void DeleteFile(string fileName);
        bool Exists(string fileName);
        Task SaveFile(string fileName, string mimeType, Stream stream);
        Stream OpenFile(string fileName);
    }

    public interface IFileRepository<InjectT> : IFileRepository
    {

    }
}