using NUnit.Framework;

namespace BullsAndCows.Test
{
    [TestFixture]
    public class NumberTest
    {
        [Test]
        [TestCase(54)]
        [TestCase(1841)]
        [TestCase(12345)]
        public void IsIntAllowed_WrongNumber_False(int numberInt)
        {
            var result = Number.IsIntAllowed(numberInt);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(123)]
        [TestCase(9876)]
        [TestCase(0821)]
        [TestCase(5678)]
        public void IsIntAllowed_ValidNumber_True(int numberInt)
        {
            var result = Number.IsIntAllowed(numberInt);
            Assert.IsTrue(result);
        }
    }
}
