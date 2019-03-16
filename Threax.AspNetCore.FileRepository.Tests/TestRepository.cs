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
        public async Task OpenFile()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            using (var stream = await repo.OpenRead("Pdf.pdf"))
            {
                Assert.True(stream.Length > 0, "Opened stream is empty");
            }
        }

        [Fact]
        public async Task GetFilesValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = await repo.GetFiles("");
            Assert.NotEmpty(files);
            Assert.Equal(15, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public async Task GetGifFilesValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = await repo.GetFiles("", "*.gif");
            Assert.NotEmpty(files);
            Assert.Equal(1, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public async Task GetFilesRecursiveValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = await repo.GetFiles("", searchOption: SearchOption.AllDirectories);
            Assert.NotEmpty(files);
            Assert.Equal(16, files.Count());
            foreach(var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public async Task GetDirectoriesValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = await repo.GetDirectories("");
            Assert.NotEmpty(files);
            Assert.Equal(1, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public async Task GetDirectoriesRecursiveValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            var files = await repo.GetDirectories("", searchOption: SearchOption.AllDirectories);
            Assert.NotEmpty(files);
            Assert.Equal(1, files.Count());
            foreach (var file in files)
            {
                Assert.DoesNotContain(Path.GetFullPath("TestFiles"), file);
            }
        }

        [Fact]
        public async Task ExistsValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.True(await repo.Exists("Png.png"));
        }

        [Fact]
        public async Task DirectoryExistsValid()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.True(await repo.DirectoryExists("TestFolder"));
        }

        [Fact]
        public async Task DirectoryExistsFalse()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            Assert.False(await repo.DirectoryExists("Png.png"));
        }

        [Fact]
        public async Task TryToBreakOutGetFiles()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.GetFiles("../"));
        }

        [Fact]
        public async Task TryToBreakOutGetDirectories()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.GetDirectories("../"));
        }

        [Fact]
        public async Task TryToBreakOutDeleteFile()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.DeleteFile("../file.txt"));
        }

        [Fact]
        public async Task TryToBreakOutOpenRead()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.OpenRead("../file.txt"));
        }

        [Fact]
        public async Task TryToBreakOutOpenWrite()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            using (var stream = new MemoryStream())
            {
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.OpenWrite("../file.txt", "text", stream));
            }
        }

        [Fact]
        public async Task TryToBreakOutExists()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.Exists("../file.txt"));
        }

        [Fact]
        public async Task TryToBreakOutDirectoryExists()
        {
            var repo = new FileRepository("TestFiles", new FileVerifier());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.DirectoryExists("../"));
        }
    }
}
