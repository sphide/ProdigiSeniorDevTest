using System;

namespace ColourMatcher
{
    public class ColourMatchCertainty
    {
        public readonly int Value;
        public ColourMatchCertainty(int value)
        {
            if (value > MaxValue || value < MinValue)
                throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {MinValue} and {MaxValue}");
            Value = value;
        }

        public static int MaxValue => 100;
        public static int MinValue => 0;
        public override string ToString() => Value.ToString();
    }
}
