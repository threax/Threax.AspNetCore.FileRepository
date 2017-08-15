using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Threax.AspNetCore.FileRepository
{
    public interface IFileVerifier
    {
        bool IsValid(Stream fileStream, String extension, String mimeType);
    }
}
