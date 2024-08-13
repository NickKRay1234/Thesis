namespace Code.Gameplay.Movement
{
    public sealed class Spline
    {
        public Point StartPoint { get; }
        public Point MiddlePoint { get; }
        public Point EndPoint { get; }

        public Spline(Point startPoint, Point middlePoint, Point endPoint)
        {
            StartPoint = startPoint;
            MiddlePoint = middlePoint;
            EndPoint = endPoint;
        }
    }
}