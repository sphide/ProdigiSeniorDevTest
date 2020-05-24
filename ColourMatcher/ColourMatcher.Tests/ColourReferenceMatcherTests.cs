using ColourMatcher.Implementations;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace ColourMatcher.Tests
{
    public class ColourReferenceMatcherTests
    {
        const int MAXVARIANCE = 62;
        [Fact]
        public void ExactMatchShouldHave100MatchScore()
        {
            //Arrange
            var sut = new ColourReferenceMatcher();
            var referenceColours = new List<ColourReference> { new ColourReference(Color.Black) }.AsReadOnly();
            //Act
            var result = sut.MatchColourReference(Color.Black, referenceColours, MAXVARIANCE);
            //Assert
            Assert.Equal(100, result.MatchScore.Value);
        }
        [Fact]
        public void NoMatchShouldHave0MatchScore()
        {
            //Arrange
            var sut = new ColourReferenceMatcher();
            var referenceColours = new List<ColourReference> { new ColourReference(Color.Red) }.AsReadOnly();
            //Act
            var result = sut.MatchColourReference(Color.Black, referenceColours, MAXVARIANCE);
            //Assert
            Assert.Equal(0, result.MatchScore.Value);
        }
        [Fact]
        public void ExampleColourGreyShouldMatch()
        {
            //Arrange
            var sut = new ColourReferenceMatcher();
            //rgb value taken using third-party colour-picker from dominant colour of sample images grey image
            var testColour = Color.FromArgb(56, 70, 87);
            var referenceColours = new ColourReferencesProvider();
            //Act
            var result = sut.MatchColourReference(testColour, referenceColours.ReferenceColours, MAXVARIANCE);
            //Assert
            Assert.True(result.MatchScore.Value > 0);
        }
    }
}
