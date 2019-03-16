using System.IO;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileVerifier
    {
        void AddTypeVerifier(IFileTypeVerifier verifier);
        void Validate(Stream fileStream, string fileName, string mimeType);
    }

    public interface IFileVerifier<InjectT> : IFileVerifier
    {

    }
}