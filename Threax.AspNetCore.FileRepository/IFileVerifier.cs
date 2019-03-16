using System.IO;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileVerifier
    {
        /// <summary>
        /// Add a type verifier.
        /// </summary>
        /// <param name="verifier"></param>
        void AddTypeVerifier(IFileTypeVerifier verifier);

        /// <summary>
        /// Validate a stream/filename.
        /// </summary>
        /// <param name="fileStream">The stream to validate.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="mimeType">The mime type to verify the file with.</param>
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