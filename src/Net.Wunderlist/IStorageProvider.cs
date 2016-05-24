using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Wunderlist
{
    /// <summary>
    /// Local storage provider
    /// </summary>
    public interface IStorageProvider
	{
		Task<Stream> OpenAsync(string filepath, CancellationToken cancellationToken);

        Task<string> GetMimeTypeAsync(string filepath);
    }
}