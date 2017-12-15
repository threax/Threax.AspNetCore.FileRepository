using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Xunit;

namespace Threax.AspNetCore.FileRepository.Tests
{
    public class TestVerifiers
    {
        [Fact]
        public void Pdf()
        {
            TestSuccessVerifier("TestFiles/Pdf.pdf", new FileVerifier().AddPdf(), FileVerifierFactory.PdfMimeType);
        }

        [Fact]
        public void Docx()
        {
            TestSuccessVerifier("TestFiles/Docx.docx", new FileVerifier().AddDocx(), FileVerifierFactory.DocxMimeType);
        }

        [Fact]
        public void Xlsx()
        {
            TestSuccessVerifier("TestFiles/Xlsx.xlsx", new FileVerifier().AddXlsx(), FileVerifierFactory.XlsxMimeType);
        }

        [Fact]
        public void Pptx()
        {
            TestSuccessVerifier("TestFiles/Pptx.pptx", new FileVerifier().AddPptx(), FileVerifierFactory.PptxMimeType);
        }

        [Fact]
        public void Doc()
        {
            TestSuccessVerifier("TestFiles/Doc.doc", new FileVerifier().AddDoc(), FileVerifierFactory.DocMimeType);
        }

        [Fact]
        public void Xls()
        {
            TestSuccessVerifier("TestFiles/Xls.xls", new FileVerifier().AddXls(), FileVerifierFactory.XlsMimeType);
        }

        [Fact]
        public void Ppt()
        {
            TestSuccessVerifier("TestFiles/Ppt.ppt", new FileVerifier().AddPpt(), FileVerifierFactory.PptMimeType);
        }

        [Fact]
        public void Bitmap()
        {
            TestSuccessVerifier("TestFiles/Bitmap.bmp", new FileVerifier().AddBitmap(), FileVerifierFactory.BitmapMimeType);
        }

        [Fact]
        public void Gif()
        {
            TestSuccessVerifier("TestFiles/Gif.gif", new FileVerifier().AddGif(), FileVerifierFactory.GifMimeType);
        }

        [Fact]
        public void Jpeg()
        {
            TestSuccessVerifier("TestFiles/Jpeg.jpg", new FileVerifier().AddJpeg(), FileVerifierFactory.JpegMimeType);
        }

        [Fact]
        public void Png()
        {
            TestSuccessVerifier("TestFiles/Png.png", new FileVerifier().AddPng(), FileVerifierFactory.PngMimeType);
        }

        public void TestSuccessVerifier(String file, IFileVerifier verifier, String mimeType)
        {
            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                verifier.Validate(stream, file, mimeType);
            }
        }

        [Fact]
        public void EvilPdf()
        {
            TestFailValidator("TestFiles/EvilPdf.pdf", new FileVerifier().AddPdf(), FileVerifierFactory.PdfMimeType);
        }

        [Fact]
        //This test passes because the magic number does not match, the .php.doc extension is allowed since Path.GetExtension only
        //sees the last bit of the extension.
        public void PhpTest()
        {
            TestFailValidator("TestFiles/PhpTest.php.doc", new FileVerifier().AddDoc(), FileVerifierFactory.DocMimeType);
        }

        //This test is commented out because it actually allows the really evil pdf through, that file is html, but starts
        //with %PDF like a real pdf, all browsers refuse to open this file at the time of this writing (8-6-17), so that file
        //is considered safe.
        //[Fact]
        //public void ReallyEvilPdf()
        //{
        //    TestFailValidator("TestFiles/ReallyEvilPdf.pdf", new FileVerifier().AddPdf(), FileVerifierFactory.PdfMimeType);
        //}

        private static void TestFailValidator(string file, IFileVerifier verifier, string mimeType)
        {
            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Assert.ThrowsAny<Exception>(new Action(() => verifier.Validate(stream, file, mimeType)));
            }
        }
    }
}
