using System;
using System.Drawing;
using System.Threading.Tasks;

namespace ColourMatcher.Interfaces
{
    public interface IImageDownloader
    {
        Task<Image> DownloadImageFromUrl(Uri uri);
    }
}
