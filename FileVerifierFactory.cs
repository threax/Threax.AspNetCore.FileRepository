using System;
using System.Collections.Generic;
using System.Text;
using Threax.AspNetCore.FileRepository;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class produces preconfigured magic number verifiers.
    /// </summary>
    public static class FileVerifierFactory
    {
        public static readonly String PdfMimeType = "application/pdf";

        public static IFileVerifier AddPdf(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".pdf", PdfMimeType, 0x25, 0x50, 0x44, 0x46));
            return fileVerifier;
        }

        public static readonly String DocxMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        public static IFileVerifier AddDocx(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".docx", DocxMimeType, 0x50, 0x4B, 0x03, 0x04));
            return fileVerifier;
        }

        public static readonly String XlsxMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public static IFileVerifier AddXlsx(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".xlsx", XlsxMimeType, 0x50, 0x4B, 0x03, 0x04));
            return fileVerifier;
        }

        public static readonly String PptxMimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";

        public static IFileVerifier AddPptx(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".pptx", PptxMimeType, 0x50, 0x4B, 0x03, 0x04));
            return fileVerifier;
        }

        public static readonly String DocMimeType = "application/msword";

        public static IFileVerifier AddDoc(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".doc", DocMimeType, 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1));
            return fileVerifier;
        }

        public static readonly String XlsMimeType = "application/vnd.ms-excel";

        public static IFileVerifier AddXls(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".xls", XlsMimeType, 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1));
            return fileVerifier;
        }

        public static readonly String PptMimeType = "application/vnd.ms-powerpoint";

        public static IFileVerifier AddPpt(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".ppt", PptMimeType, 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1));
            return fileVerifier;
        }

        /// <summary>
        /// Add html files to the verifier. Be careful adding html support since that will allow users to add random code
        /// to your website. This is normally not a good idea.
        /// </summary>
        /// <param name="fileVerifier">The file verifier to add support to.</param>
        /// <returns>The passed in file verifier.</returns>
        public static IFileVerifier AddHtml(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(new List<String> { ".html", ".htm" }, HtmlMimeType));
            return fileVerifier;
        }
        public static readonly String HtmlMimeType = "text/html";

        public static IFileVerifier AddBitmap(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".bmp", BitmapMimeType));
            return fileVerifier;
        }
        public static readonly String BitmapMimeType = "image/bmp";

        public static IFileVerifier AddGif(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".gif", GifMimeType));
            return fileVerifier;
        }
        public static readonly String GifMimeType = "image/gif";

        public static IFileVerifier AddJpeg(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(new List<String> { ".jpg", ".jpeg", ".jpe", ".jfif" }, JpegMimeType));
            return fileVerifier;
        }
        public static readonly String JpegMimeType = "image/jpeg";

        public static IFileVerifier AddPng(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".png", PngMimeType));
            return fileVerifier;
        }
        public static readonly String PngMimeType = "image/png";

        public static IFileVerifier AddSvgXml(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".svg", SvgXmlMimeType));
            return fileVerifier;
        }
        public static readonly String SvgXmlMimeType = "image/svg+xml";

        public static IFileVerifier AddJson(this IFileVerifier fileVerifier)
        {
            fileVerifier.addTypeVerifier(new MagicNumberVerifier(".json", JsonMimeType));
            return fileVerifier;
        }
        public static readonly String JsonMimeType = "application/json";


    }
}
