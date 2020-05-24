using System.Drawing;

namespace ColourMatcher.Interfaces
{
    public interface IDominantColourFinder
    {
        Color FindDominantColour(Bitmap image);
    }
}
