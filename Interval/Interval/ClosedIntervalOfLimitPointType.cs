namespace Interval
{
    using System.Collections.Generic;

    public struct ClosedInterval<LimitPointType>
        where LimitPointType : struct
    {
                public LimitPointType LowerLimitPoint
        { get { return lowerLimitPoint; } }

        public LimitPointType UpperLimitPoint
        { get { return upperLimitPoint; } }
        
        public ClosedInterval(LimitPointType lowerLimitPoint, LimitPointType upperLimitPoint, IComparer<LimitPointType> comparesLimitPoints)

        {
            CheckComparerIsNotNull(comparesLimitPoints);

            this.comparesLimitPoints = comparesLimitPoints;

            if (comparesLimitPoints.Compare(x: lowerLimitPoint, y: upperLimitPoint) > 0)

            {
                this.lowerLimitPoint = upperLimitPoint;

                this.upperLimitPoint = lowerLimitPoint;
            }

            else

            {
                this.lowerLimitPoint = lowerLimitPoint;

                this.upperLimitPoint = upperLimitPoint;
            }

        }

        public ClosedInterval(LimitPointType lowerLimitPoint, LimitPointType upperLimitPoint)
            : this(lowerLimitPoint, upperLimitPoint, Comparer<LimitPointType>.Default)
        { }

        public static ClosedInterval<LimitPointType> From(LimitPointType lowerLimitPoint)
        {

            return new ClosedInterval<LimitPointType>(lowerLimitPoint: lowerLimitPoint, upperLimitPoint: lowerLimitPoint);
        }

        public ClosedInterval<LimitPointType> To(LimitPointType upperLimitPoint)
        {
            return new ClosedInterval<LimitPointType>(lowerLimitPoint: this.LowerLimitPoint, upperLimitPoint: upperLimitPoint);
        }

        public bool Contains(LimitPointType candidatePoint)
        {
            if (comparesLimitPoints.Compare(candidatePoint, lowerLimitPoint) < 0)
                return false;

            if (comparesLimitPoints.Compare(candidatePoint, upperLimitPoint) > 0)
                return false;

            return true;
        }

        private static void CheckComparerIsNotNull(IComparer<LimitPointType> comparesLimitPoints)
        {
            if (comparesLimitPoints == null)
                throw new System.ArgumentNullException("We need something to compare limit points");
        }

        private IComparer<LimitPointType> comparesLimitPoints;

        private readonly LimitPointType lowerLimitPoint;

        private readonly LimitPointType upperLimitPoint;
    }
}
