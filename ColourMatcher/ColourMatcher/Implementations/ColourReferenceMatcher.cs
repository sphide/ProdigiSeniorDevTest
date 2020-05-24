using ColourMatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ColourMatcher.Implementations
{
    public class ColourReferenceMatcher : IColourReferenceMatcher
    {
        public ColourReferenceMatchResult MatchColourReference(Color colourToMatch, IEnumerable<ColourReference> colourReferences, int maxVariance)
        {
            var matchedColour = colourReferences
                .Select(rc => new { Diff = ColourDiff(rc, colourToMatch), Colour = rc })
                .Where(a => a.Diff <= maxVariance)
                .OrderBy(a => a.Diff).FirstOrDefault();

            if (matchedColour == null)
                return ColourReferenceMatchResult.NoMatchResult;
            else
                return new ColourReferenceMatchResult(true, new ColourMatchCertainty(CalculateMatchScore(maxVariance, matchedColour.Diff)), matchedColour.Colour.ColourName);
        }
        private int CalculateMatchScore(int maxVariance, int diff)
        {
            if (diff <= 0)
            {
                return 100;
            }
            if (diff == maxVariance)
            {
                return 1;
            }

            var percentage = (diff / maxVariance);
            var inverseMatchScore = (percentage * 100);
            var matchScore = (100 - inverseMatchScore);

            return (matchScore > 100 ? 100 : matchScore < 0 ? 0 : matchScore);
        }
        private int ColourDiff(ColourReference c1, Color c2)
        {
            return (int)Math.Sqrt((c1.RedValue - c2.R) * (c1.RedValue - c2.R)
                                   + (c1.GreenValue - c2.G) * (c1.GreenValue - c2.G)
                                   + (c1.BlueValue - c2.B) * (c1.BlueValue - c2.B));
        }
    }
}
