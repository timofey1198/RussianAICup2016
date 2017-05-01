using System;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class Point
    {
        public Point()
        {
            _x = 0;
            _y = 0;
        }
        public Point(double x,double y)
        {
            _x = x;
            _y = y;
        }

        private double _x;
        private double _y;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Dist(Point p)
        {
            double d = Math.Sqrt((p.X - this.X) * (p.X - this.X) + (p.Y - this.Y) * (p.Y - this.Y));
            return d;
        }
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
