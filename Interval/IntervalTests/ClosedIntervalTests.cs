namespace IntervalTests
{
    using Interval;

    using NUnit.Framework;
    using NSubstitute;
    using System.Collections.Generic;

    [TestFixture]
    public class ClosedIntervalTests
    {
        [Test]
        public void WhenConstructorIsCalledWithLimitsWrongWayAround_ThenConstructedInstanceShouldHaveCorrectecLimits()
        {
            int wrongLowerLimit = 90;
            int wrongUpperLimit = 10;

            var classUnderTest = new ClosedInterval<int>(lowerLimitPoint: wrongLowerLimit, upperLimitPoint: wrongUpperLimit);

            var limitComparisonResult = classUnderTest.LowerLimitPoint.CompareTo(classUnderTest.UpperLimitPoint);

            Assert.That(limitComparisonResult, Is.LessThan(0), "Limit points should have been corrected.");
        }

        [Test]
        public void WhenConstructorIsCalledWithComparer_ThenGivenComparerShouldBeCalled()
        {
            var mockComparer = Substitute.For<IComparer<int>>();

            mockComparer.Compare(Arg.Any<int>(), Arg.Any<int>()).Returns(args => ((int)args[0]).CompareTo((int)args[1]));

            var classUnderTest = new ClosedInterval<int>(lowerLimitPoint: 1, upperLimitPoint: 5, comparesLimitPoints: mockComparer);

            mockComparer.ReceivedWithAnyArgs().Compare(1, 1);
        }

        [Test]
        public void WhenToMethodIsCalledOnAnInstance_ThenReturnedInstanceShouldBeDifferent()
        {
            var firstInstance = new ClosedInterval<int>(lowerLimitPoint: 0, upperLimitPoint: 0);

            var instanceFromToMethod = firstInstance.To(10);

            Assert.IsFalse(ReferenceEquals(firstInstance, instanceFromToMethod));
        }
    }
}
