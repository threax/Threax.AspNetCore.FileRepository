using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Threax.AspNetCore.FileRepository
{
    public class FileVerifier : IFileVerifier
    {
        private Dictionary<String, IFileTypeVerifier> typeVerifiers = new Dictionary<string, IFileTypeVerifier>();

        public void AddTypeVerifier(IFileTypeVerifier verifier)
        {
            this.typeVerifiers.Add(verifier.MimeType, verifier);
        }

        public void Validate(Stream fileStream, String fileName, String mimeType)
        {
            IFileTypeVerifier verifier;
            if(!this.typeVerifiers.TryGetValue(mimeType, out verifier))
            {
                throw new InvalidOperationException($"Mime type '{mimeType}' not supported.");
            }

            verifier.Validate(fileStream, fileName, mimeType);
        }
    }

    public class FileVerifier<InjectT> : FileVerifier, IFileVerifier<InjectT>
    {

    }
}
