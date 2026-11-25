using NUnit.Framework;
using ZeroTrace.Domain;

namespace ZeroTrace.Domain.Tests
{
    public sealed class StableIdentifierTests
    {
        [Test]
        public void Parse_ValidCanonicalGuid_PreservesValue()
        {
            const string canonical = "12345678-1234-1234-1234-123456789abc";

            var parsed = StableIdentifier.TryParse(canonical, out var identifier);

            Assert.That(parsed, Is.True);
            Assert.That(identifier.IsValid, Is.True);
            Assert.That(identifier.ToString(), Is.EqualTo(canonical));
        }

        [Test]
        public void Parse_SameCanonicalGuid_ProducesEqualValues()
        {
            const string canonical = "12345678-1234-1234-1234-123456789abc";

            StableIdentifier.TryParse(canonical, out var first);
            StableIdentifier.TryParse(canonical, out var second);

            Assert.That(first, Is.EqualTo(second));
            Assert.That(first.GetHashCode(), Is.EqualTo(second.GetHashCode()));
        }

        [Test]
        public void Parse_EmptyGuid_IsRejected()
        {
            var parsed = StableIdentifier.TryParse("00000000-0000-0000-0000-000000000000", out var identifier);

            Assert.That(parsed, Is.False);
            Assert.That(identifier.IsValid, Is.False);
        }

        [Test]
        public void Parse_NonCanonicalValues_AreRejected()
        {
            Assert.That(StableIdentifier.TryParse("12345678-1234-1234-1234-123456789ABC", out _), Is.False);
            Assert.That(StableIdentifier.TryParse("12345678123412341234123456789abc", out _), Is.False);
            Assert.That(StableIdentifier.TryParse("not-a-guid", out _), Is.False);
        }

        [Test]
        public void DefaultValue_IsInvalidAndStable()
        {
            var first = default(StableIdentifier);
            var second = default(StableIdentifier);

            Assert.That(first.IsValid, Is.False);
            Assert.That(first.ToString(), Is.Empty);
            Assert.That(first, Is.EqualTo(second));
            Assert.That(first.GetHashCode(), Is.EqualTo(0));
        }
    }
}
