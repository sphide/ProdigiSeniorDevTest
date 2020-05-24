using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Drawing;
using ColourMatcher.Interfaces;

namespace ColourMatcher
{
	public class MatchColour
    {
        //maximum allowable difference when matching colours using the ColourDiff function
		//could be read in from settings/config etc
        const int maxVar = 70;
		//services/helpers used by the function, instances injected in via constructor
		private readonly IColourReferenceMatcher _colourReferenceMatcher;
		private readonly IImageDownloader _imageDownloader;
		private readonly IDominantColourFinder _dominantColourFinder;
		private readonly IColourReferencesProvider _colourReferencesProvider;

		public MatchColour(
			IColourReferenceMatcher colourReferenceMatcher, 
			IImageDownloader imageDownloader, 
			IDominantColourFinder dominantColourFinder,
			IColourReferencesProvider colourReferencesProvider
		)
		{
			_colourReferenceMatcher = colourReferenceMatcher;
			_imageDownloader = imageDownloader;
			_dominantColourFinder = dominantColourFinder;
			_colourReferencesProvider = colourReferencesProvider;
		}

        [FunctionName(nameof(MatchColour))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            #region Request Validation
            //Try to get image url from QueryString
            string url = req.Query["imageurl"];
			//Try to get image url from request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
			//if QueryString contained an image url use that, otherwise use the one from the request body, if there is one
            url ??= data?.imageurl;
			//check that we got an image url from somewhere, if not we can't continue
			if (string.IsNullOrWhiteSpace(url))
			{
				log.LogInformation("Colour match process started but then ended prematurely due to missing image url");
				return new BadRequestObjectResult("No image url supplied");
			}

			Uri imageUri = new Uri(url);
			//Basic validation of the image url
			if (!imageUri.IsWellFormedOriginalString())
			{
				log.LogInformation("Colour match process started but then ended prematurely due to invalid image url");
				return new BadRequestObjectResult("Invalid image url supplied");
			}
			#endregion Request Validation

			log.LogInformation("MatchColour function request validated succesfully");

			Bitmap bitmap;
			try
			{
				bitmap = await _imageDownloader.DownloadImageFromUrl(imageUri) as Bitmap;
				log.LogInformation("Image downloaded", imageUri.AbsoluteUri);
			}
			catch(Exception ex)
			{
				log.LogError(ex, "An error occurred while attempting to download image", imageUri.AbsoluteUri);
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			var mostUsedColour = _dominantColourFinder.FindDominantColour(bitmap);
			var matchedColourResult = _colourReferenceMatcher.MatchColourReference(mostUsedColour, _colourReferencesProvider.ReferenceColours, maxVar);

			log.LogInformation($"Colour match process finished with the result {matchedColourResult}");

			return new OkObjectResult(matchedColourResult.MatchedColourName);
        }
	}
}
