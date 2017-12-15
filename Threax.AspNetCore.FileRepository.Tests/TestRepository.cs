using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Xunit;

namespace Threax.AspNetCore.FileRepository.Tests
{
    public class TestRepository
    {
        [Fact]
        public void OpenFile()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            using (var stream = repo.OpenFile("Pdf.pdf"))
            {
                Assert.True(stream.Length > 0, "Opened stream is empty");
            }
        }
    }
}
