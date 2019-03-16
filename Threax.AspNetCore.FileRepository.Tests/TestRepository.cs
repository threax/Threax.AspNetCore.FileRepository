using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        [Fact]
        public void GetFilesValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = repo.GetFiles("");
            Assert.NotEmpty(files);
            Assert.Equal(15, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public void GetGifFilesValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = repo.GetFiles("", "*.gif");
            Assert.NotEmpty(files);
            Assert.Equal(1, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public void GetFilesRecursiveValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = repo.GetFiles("", searchOption: SearchOption.AllDirectories);
            Assert.NotEmpty(files);
            Assert.Equal(16, files.Count());
            foreach(var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public void GetDirectoriesValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = repo.GetDirectories("");
            Assert.NotEmpty(files);
            Assert.Equal(1, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public void GetDirectoriesRecursiveValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = repo.GetDirectories("", searchOption: SearchOption.AllDirectories);
            Assert.NotEmpty(files);
            Assert.Equal(1, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public void ExistsValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.True(repo.Exists("Png.png"));
        }

        [Fact]
        public void DirectoryExistsValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.True(repo.DirectoryExists("TestFolder"));
        }

        [Fact]
        public void DirectoryExistsFalse()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.False(repo.DirectoryExists("Png.png"));
        }

        [Fact]
        public void TryToBreakOutGetFiles()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.Throws<InvalidOperationException>(() => repo.GetFiles("../"));
        }

        [Fact]
        public void TryToBreakOutGetDirectories()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.Throws<InvalidOperationException>(() => repo.GetDirectories("../"));
        }

        [Fact]
        public void TryToBreakOutDeleteFile()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.Throws<InvalidOperationException>(() => repo.DeleteFile("../file.txt"));
        }

        [Fact]
        public void TryToBreakOutOpen()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.Throws<InvalidOperationException>(() => repo.OpenFile("../file.txt"));
        }

        [Fact]
        public async Task TryToBreakOutSave()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            using (var stream = new MemoryStream())
            {
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.SaveFile("../file.txt", "text", stream));
            }
        }

        [Fact]
        public void TryToBreakOutExists()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.Throws<InvalidOperationException>(() => repo.Exists("../file.txt"));
        }

        [Fact]
        public void TryToBreakOutDirectoryExists()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.Throws<InvalidOperationException>(() => repo.DirectoryExists("../"));
        }
    }
}
