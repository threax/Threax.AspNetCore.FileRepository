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
        public static readonly String PdfMimeType = "application/pdf";

        public static IFileVerifier CreatePdfVerifier()
        {
            return new MagicNumberVerifier(".pdf", PdfMimeType, 0x25, 0x50, 0x44, 0x46);
        }

        public static readonly String DocxMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        public static IFileVerifier CreateDocxVerifier()
        {
            return new MagicNumberVerifier(".docx", DocxMimeType, 0x50, 0x4B, 0x03, 0x04);
        }

        public static readonly String XlsxMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public static IFileVerifier CreateXlsxVerifier()
        {
            return new MagicNumberVerifier(".xlsx", XlsxMimeType, 0x50, 0x4B, 0x03, 0x04);
        }

        public static readonly String PptxMimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";

        public static IFileVerifier CreatePptxVerifier()
        {
            return new MagicNumberVerifier(".pptx", PptxMimeType, 0x50, 0x4B, 0x03, 0x04);
        }

        public static readonly String DocMimeType = "application/msword";

        public static IFileVerifier CreateDocVerifier()
        {
            return new MagicNumberVerifier(".doc", DocMimeType, 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1);
        }

        public static readonly String XlsMimeType = "application/vnd.ms-excel";

        public static IFileVerifier CreateXlsVerifier()
        {
            return new MagicNumberVerifier(".xls", XlsMimeType, 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1);
        }

        public static readonly String PptMimeType = "application/vnd.ms-powerpoint";

        public static IFileVerifier CreatePptVerifier()
        {
            return new MagicNumberVerifier(".ppt", PptMimeType, 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1);
        }
    }
}
