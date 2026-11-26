using System;

namespace ZeroTrace.Domain
{
    /// <summary>
    /// A canonical, non-empty GUID value for stable domain identity.
    /// </summary>
    public readonly struct StableIdentifier : IEquatable<StableIdentifier>
    {
        private readonly Guid value;
        private readonly bool isValid;

        private StableIdentifier(Guid value)
        {
            this.value = value;
            isValid = true;
        }

        public bool IsValid => isValid;

        public static bool TryParse(string text, out StableIdentifier identifier)
        {
            identifier = default;

            if (!Guid.TryParseExact(text, "D", out var parsed) || parsed == Guid.Empty)
            {
                return false;
            }

            if (!string.Equals(text, parsed.ToString("D"), StringComparison.Ordinal))
            {
                return false;
            }

            identifier = new StableIdentifier(parsed);
            return true;
        }

        public override string ToString() => isValid ? value.ToString("D") : string.Empty;

        public bool Equals(StableIdentifier other) =>
            isValid == other.isValid && (!isValid || value.Equals(other.value));

        public override bool Equals(object obj) => obj is StableIdentifier other && Equals(other);

        public override int GetHashCode() => isValid ? value.GetHashCode() : 0;

        public static bool operator ==(StableIdentifier left, StableIdentifier right) => left.Equals(right);

        public static bool operator !=(StableIdentifier left, StableIdentifier right) => !left.Equals(right);
    }
}
