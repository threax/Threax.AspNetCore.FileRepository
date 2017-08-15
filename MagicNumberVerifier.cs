using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Threax.AspNetCore.FileRepository
{
    public class MagicNumberVerifier : IFileVerifier
    {
        private byte[] magicBytes;
        private List<String> extensions;
        private String mimeType;

        public MagicNumberVerifier(String extension, String mimeType, params byte[] magicBytes)
            :this(new List<String>() { extension }, mimeType, magicBytes)
        {
            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="extensions">A list of supported extensions, should be lowercase.</param>
        /// <param name="mimeType">The mime type that the incoming file should have.</param>
        /// <param name="magicBytes">Magic bytes the file should start with, pass null to not check this.</param>
        public MagicNumberVerifier(List<String> extensions, String mimeType, params byte[] magicBytes)
        {
            this.extensions = extensions;
            this.mimeType = mimeType;
            this.magicBytes = magicBytes;
        }

        public bool IsValid(Stream fileStream, String fileName = null, String mimeType = null)
        {
            //Extensions
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            var foundMatch = false;
            foreach(var testExt in extensions)
            {
                if(testExt.Equals(extension))
                {
                    foundMatch = true;
                    break;
                }
            }
            if (!foundMatch)
            {
                return false;
            }

            //Mime type
            if (!this.mimeType.Equals(mimeType, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //Magic bytes
            if (this.magicBytes != null && this.magicBytes.Length > 0)
            {
                var streamBytes = new byte[this.magicBytes.Length];
                fileStream.Seek(0, SeekOrigin.Begin);
                int readAmount = fileStream.Read(streamBytes, 0, streamBytes.Length);
                fileStream.Seek(0, SeekOrigin.Begin); //Be sure to reset the stream

                if (readAmount != magicBytes.Length)
                {
                    return false;
                }

                for (int i = 0; i < magicBytes.Length; ++i)
                {
                    if (streamBytes[i] != magicBytes[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
