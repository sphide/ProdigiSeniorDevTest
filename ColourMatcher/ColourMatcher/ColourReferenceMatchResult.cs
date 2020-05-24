namespace ColourMatcher
{
    public class ColourReferenceMatchResult
    {
        public readonly bool IsMatched;
        public readonly ColourMatchCertainty MatchScore;
        public readonly string MatchedColourName;

        public ColourReferenceMatchResult(bool isMatched, ColourMatchCertainty matchScore, string matchedColourName)
        {
            IsMatched = isMatched;
            MatchScore = matchScore;
            MatchedColourName = matchedColourName;
        }

        public static ColourReferenceMatchResult NoMatchResult =>
            new ColourReferenceMatchResult(false, new ColourMatchCertainty(0), "No match");

        public override string ToString()
        {
            string matchedMsg = IsMatched ? "Match" : "No Match";
            return $"{matchedMsg} {MatchedColourName} Score: {MatchScore}";
        }
    }
}
