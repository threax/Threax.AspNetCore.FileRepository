using System.IO;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileVerifier
    {
        void addTypeVerifier(IFileTypeVerifier verifier);
        void Validate(Stream fileStream, string fileName, string mimeType);
    }

    public interface IFileVerifier<InjectT> : IFileVerifier
    {

    }
}