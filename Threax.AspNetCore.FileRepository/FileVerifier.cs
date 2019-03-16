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

        /// <summary>
        /// If this is set to true any files that do not have a type verifier will be considered valid.
        /// </summary>
        public bool AllowUnknownFiles { get; set; } = false;

        public void Validate(Stream fileStream, String fileName, String mimeType)
        {
            IFileTypeVerifier verifier;
            if(!this.typeVerifiers.TryGetValue(mimeType, out verifier))
            {
                if (AllowUnknownFiles)
                {
                    return; //Done here if we can't find a validator and we are allowing unknown files.
                }

                throw new InvalidOperationException($"Mime type '{mimeType}' not supported.");
            }

            verifier.Validate(fileStream, fileName, mimeType);
        }
    }

    public class FileVerifier<InjectT> : FileVerifier, IFileVerifier<InjectT>
    {

    }
}
