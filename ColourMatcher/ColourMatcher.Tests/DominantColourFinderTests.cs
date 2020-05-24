using ColourMatcher.Implementations;
using ColourMatcher.Tests.ExtensionMethods;
using System.Drawing;
using System.Reflection;
using Xunit;

namespace ColourMatcher.Tests
{
    public class DominantColourFinderTests
    {
        [Fact]
        public void ExampleGreyImageHaveDominantColourOf_56_70_87()
        {
            //Arrange
            var sut = new DominantColourFinder();
            var bitmapStream = Assembly.GetExecutingAssembly().GetEmbeddedResourceStream("Resources.test-sample-grey.png");
            var image = Bitmap.FromStream(bitmapStream) as Bitmap;
            //dominant colour of grey sample image as determined by https://imagecolorpicker.com/en/
            var expectedColour = Color.FromArgb(56, 70, 87);
            //Act
            var result = sut.FindDominantColour(image);
            //Assert
            Assert.Equal(expectedColour, result);
        }
    }
}
