using System.Drawing;

namespace ColourMatcher
{
    public class ColourReference
    {
        public readonly int RedValue;
        public readonly int GreenValue;
        public readonly int BlueValue;
        public readonly string ColourName;

        public ColourReference(int redValue, int greenValue, int blueValue, string colourName)
        {
            RedValue = redValue;
            GreenValue = greenValue;
            BlueValue = blueValue;
            ColourName = colourName;
        }

        public ColourReference(Color colour, string colourName = null)
        {
            RedValue = colour.R;
            GreenValue = colour.G;
            BlueValue = colour.B;
            ColourName = colourName ?? colour.Name;
        }
    }
}
