using ColourMatcher.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace ColourMatcher.Implementations
{
    public class ColourReferencesProvider : IColourReferencesProvider
    {
        public IReadOnlyList<ColourReference> ReferenceColours =>
            new List<ColourReference>{
                new ColourReference(Color.Black),
                new ColourReference(Color.DimGray, "Grey"),
                new ColourReference(Color.Teal),
                new ColourReference(Color.Navy)
            }.AsReadOnly();
    }
}
