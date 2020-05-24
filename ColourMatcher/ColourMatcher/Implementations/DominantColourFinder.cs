using ColourMatcher.ExtensionMethods;
using ColourMatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace ColourMatcher.Implementations
{
	public class DominantColourFinder : IDominantColourFinder
    {
		private const int RESIZE_PERCENTAGE_OF_ORIGINAL = 1;
		private readonly Size MAX_SIZE_BEFORE_RESIZE = new Size(10, 10);
        public Color FindDominantColour(Bitmap image)
        {
			bool shouldResize = image.Size.IsGreaterThan(MAX_SIZE_BEFORE_RESIZE);
			Bitmap sizedImage = image;
			if (shouldResize)
			{
				Size newSize = CalcResize(image.Size);
				sizedImage = ResizeImage(image, newSize.Width, newSize.Height);
			}
			return GetMostUsedColor(sizedImage);
		}

		private Bitmap ResizeImage(Image image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using var wrapMode = new ImageAttributes();
				wrapMode.SetWrapMode(WrapMode.TileFlipXY);
				graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
			}

			return destImage;
		}
		private Size CalcResize(Size originalSize)
		{
			int newWidth = (originalSize.Width / 100) * RESIZE_PERCENTAGE_OF_ORIGINAL;
			int newHeight = (originalSize.Height / 100) * RESIZE_PERCENTAGE_OF_ORIGINAL;
			return new Size(newWidth, newHeight);
		}
		private Color GetMostUsedColor(Bitmap bitMap)
		{
			var colorIncidence = new Dictionary<int, int>();
			
			for (var x = 0; x < bitMap.Size.Width; x++)
				for (var y = 0; y < bitMap.Size.Height; y++)
				{
					var pixelColor = bitMap.GetPixel(x, y).ToArgb();
					if (colorIncidence.Keys.Contains(pixelColor))
						colorIncidence[pixelColor]++;
					else
						colorIncidence.Add(pixelColor, 1);
				}

			return Color.FromArgb(
				colorIncidence
				.OrderByDescending(x => x.Value)
				.ToDictionary(x => x.Key, x => x.Value)
				.First()
				.Key
			);
		}
	}
}
