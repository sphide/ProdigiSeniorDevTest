using System.Collections.Generic;

namespace ColourMatcher.Interfaces
{
    public interface IColourReferencesProvider
    {
        IReadOnlyList<ColourReference> ReferenceColours { get; }
    }
}
