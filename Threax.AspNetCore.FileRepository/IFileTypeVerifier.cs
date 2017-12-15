using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileTypeVerifier
    {
        /// <summary>
        /// Validate a file. This will throw exceptions if there are errors.
        /// </summary>
        /// <param name="fileStream">The file stream to validate, should be seekable.</param>
        /// <param name="fileName">The name of the file to be written.</param>
        /// <param name="mimeType">The mime type of the file.</param>
        void Validate(Stream fileStream, String fileName, String mimeType);

        /// <summary>
        /// The mime type this verifier supports.
        /// </summary>
        String MimeType { get; }
    }
}
