namespace mauitest
{
    public interface IFileDownloadManager
    {
        Task<byte[]> DownloadFileFromUrlAsync(string url, CancellationToken cancellationToken = default);

        void PreloadFileFromUrl(string url);
    }
}