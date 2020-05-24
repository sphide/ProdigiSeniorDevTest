using ColourMatcher.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ColourMatcher.Implementations
{
    public class ImageDownloader : IImageDownloader
    {
        //using a single static instance of the HttpClient class for performance reasons, more info here:
        //https://docs.microsoft.com/en-us/azure/azure-functions/manage-connections
        private static readonly HttpClient httpClient = new HttpClient { MaxResponseContentBufferSize = 256000 };
        public async Task<Image> DownloadImageFromUrl(Uri uri)
        {
            Stream responseStream = await httpClient.GetStreamAsync(uri.AbsoluteUri);
            var image = Bitmap.FromStream(responseStream);
            return image;
        }
    }
}
