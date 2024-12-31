namespace mauitest
{
    public class FileDownloadManager : IFileDownloadManager
    {
        public Task<byte[]> DownloadFileFromUrlAsync(string url, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Array.Empty<byte>());
        }

        public void PreloadFileFromUrl(string url)
        {
            // do nothing
        }
    }
}