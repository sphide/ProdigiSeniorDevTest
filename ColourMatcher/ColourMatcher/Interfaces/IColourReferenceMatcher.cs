using System.Collections.Generic;
using System.Drawing;

namespace ColourMatcher.Interfaces
{
    public interface IColourReferenceMatcher
    {
        ColourReferenceMatchResult MatchColourReference(Color colourToMatch, IEnumerable<ColourReference> colourReferences, int maxVariance);
    }
}
