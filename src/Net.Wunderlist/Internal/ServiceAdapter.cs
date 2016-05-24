using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Wunderlist.Internal
{
    internal static class ServiceAdapter
    {
        public static IStorageProvider DefaultStorageProvider()
        {
            return new FileSystemStorage();
        }

        private sealed class FileSystemStorage : IStorageProvider
        {
            public Task<string> GetMimeTypeAsync(string filepath)
            {
                return Task.FromResult(Mime.MediaTypeNames.Application.Octet);
            }

            public Task<Stream> OpenAsync(string filepath, CancellationToken cancellationToken)
            {                
                return Task.FromResult((Stream)new FileStream(filepath,
                    FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true));
            }
        }
    }
}
