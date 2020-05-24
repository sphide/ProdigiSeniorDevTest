using ColourMatcher.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ColourMatcher.Tests
{
    public class ColourMatcherIntegrationTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();
        [Fact]
        public async void GreySampleShouldMatch()
        {
            //Arrange
            var request = TestFactory.CreateHttpRequest("imageurl", "https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-grey.png");
            var sut = new MatchColour(new ColourReferenceMatcher(), new ImageDownloader(), new DominantColourFinder(), new ColourReferencesProvider());
            //Act
            var response = (OkObjectResult)await sut.Run(request, logger);
            //Assert
            Assert.IsType<string>(response.Value);
            Assert.Equal("Grey", response.Value.ToString());
        }
    }
}
