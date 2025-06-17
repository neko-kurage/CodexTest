using NUnit.Framework;
using SceneSystem;

namespace SceneSystem.Tests
{
    /// <summary>
    /// DelayUtility のテストです。
    /// </summary>
    public class DelayUtilityTests
    {
        [Test]
        public void GetRemainingTime_ReturnsExpectedValue()
        {
            float remaining = DelayUtility.GetRemainingTime(0f, 1f, 0.5f);
            Assert.AreEqual(0.5f, remaining, 0.01f);

            remaining = DelayUtility.GetRemainingTime(0f, 1f, 1.5f);
            Assert.AreEqual(0f, remaining, 0.01f);
        }

        [Test]
        public void IsTimeElapsed_ReturnsTrueWhenElapsed()
        {
            bool result = DelayUtility.IsTimeElapsed(0f, 1f, 1.1f);
            Assert.IsTrue(result);

            result = DelayUtility.IsTimeElapsed(0f, 1f, 0.5f);
            Assert.IsFalse(result);
        }
    }
}
