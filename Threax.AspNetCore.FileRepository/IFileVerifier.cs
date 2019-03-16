using System.IO;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileVerifier
    {
        void AddTypeVerifier(IFileTypeVerifier verifier);
        void Validate(Stream fileStream, string fileName, string mimeType);

        /// <summary>
        /// If this is set to true any files that do not have a type verifier will be considered valid.
        /// </summary>
        bool AllowUnknownFiles { get; set; }
    }

    public interface IFileVerifier<InjectT> : IFileVerifier
    {

    }
}