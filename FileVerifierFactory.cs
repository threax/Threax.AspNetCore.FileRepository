using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.FileRepository
{
    /// <summary>
    /// This class produces preconfigured magic number verifiers.
    /// </summary>
    public static class FileVerifierFactory
    {
        public static IFileVerifier CreatePdfVerifier()
        {
            return new MagicNumberVerifier(".pdf", "application/pdf", 25, 50, 44, 46);
        }

        public static IFileVerifier CreateDocxVerifier()
        {
            return new MagicNumberVerifier(".docx", "application/pdf", 25, 50, 44, 46);
        }
    }
}
