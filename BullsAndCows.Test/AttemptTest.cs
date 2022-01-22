using NUnit.Framework;

namespace BullsAndCows.Test
{
    [TestFixture]
    public class AttemptTest
    {
        [Test]
        public void Constructor_NumberIsNull_Exception()
        {
            Assert.Catch<ArgumentNullException>(() => new Attempt(null, 2, 3));
        }

        [Test]
        [TestCase(5, 0)]
        [TestCase(0, 5)]
        [TestCase(-1, 2)]
        [TestCase(3, -2)]
        [TestCase(-1, -2)]
        [TestCase(1, 4)]
        [TestCase(2, 3)]
        [TestCase(1, 3)]
        public void Constructor_WrongBullsAndCowsAmounts_Exception(int bulls, int cows)
        {
            var validNumber = new Number(1234);
            Assert.Catch<Exception>(() => new Attempt(validNumber, bulls, cows));
        }

        [Test]
        public void Equals_SameAttempts_True()
        {
            var numberOne = new Number(1234);
            var numberTwo = new Number(1234);

            var attemptOne = new Attempt(numberOne, 1, 2);
            var attemptTwo = new Attempt(numberTwo, 1, 2);

            Assert.AreEqual(attemptOne, attemptTwo);
        }
    }
}
