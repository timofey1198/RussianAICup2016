using System;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class Line
    {
        public Line() { _a = 0; _b = 0; _c = 0; }
        public Line(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }
        public Line(Line l, double x, double y)
        {
            _a = l.a;
            _b = l.b;
            _c = _a * x + _b * y;
        }
        public Line(Line l, Point p)
        {
            _a = l.a;
            _b = l.b;
            _c = _a * p.X + _b * p.Y;
        }
        public Line(Point p1, Point p2)
        {
            if (p1.X == p2.X)
            {
                _a = 1;
                _b = 0;
                _c = p1.X;
            }
            else
            {
                double k = (p1.Y - p2.Y) / (p1.X - p2.X);
                _a = -k;
                _b = 1;
                _c = p1.Y - k * p1.X;
            }
        }
        public Line(double x1, double y1, double x2, double y2)
        {
            if (x1 == x2)
            {
                _a = 1;
                _b = 0;
                _c = x1;
            }
            else
            {
                double k = (y1 - y2) / (x1 - x2);
                _a = -k;
                _b = 1;
                _c = y1 - k * x1;
            }
        }

        private double _a;
        private double _b;
        private double _c;

        public double a
        {
            get { return _a; }
        }
        public double b
        {
            get { return _b; }
        }
        public double c
        {
            get { return _c; }
        }

        public Point Cross(Line l)
        {
            double det = this.a * l.b - this.b * l.a;
            if (det == 0)
                return new Point();
            else
            {
                double det1 = this.c * l.b - l.c * this.b;
                double det2 = this.a * l.c - this.c * l.a;
                double x = det1 / det;
                double y = det2 / det;
                Point p = new Point(x, y);
                return p;
            }
        }
        public Point Cross(Stretch s)
        {
            Line l = s.line;
            double det = this.a * l.b - this.b * l.a;
            if (det == 0)
                throw new Exception("LinesNotCrossedException");
            else
            {
                double det1 = this.c * l.b - l.c * this.b;
                double det2 = this.a * l.c - this.c * l.a;
                double x = det1 / det;
                double y = det2 / det;
                double x1 = Math.Min(s.P1.X, s.P2.X);
                double x2 = Math.Max(s.P1.X, s.P2.X);
                double y1 = Math.Min(s.P1.Y, s.P2.Y);
                double y2 = Math.Max(s.P1.Y, s.P2.Y);
                if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                    throw new Exception("LinesNotCrossedException");
                Point p = new Point(x, y);
                return p;
            }
        }
        public Line Normal(Point p)
        {
            if (a == 0)
                return new Line(1, 0, p.X);
            if (b == 0)
                return new Line(0, 1, p.Y);
            else
            {
                double k = -a / b;
                double k1 = -1 / k;
                return new Line(-k1, 1, p.Y - k1 * p.X);
            }
        }
        public Line Parallel(Point p)
        {
            if (a == 0)
                return new Line(0, 1, p.Y);
            if (b == 0)
                return new Line(1, 0, p.X);
            else
            {
                double k = -a / b;
                return new Line(k, 1, p.Y - k * p.X);
            }
        }
        public Point[] MakeStretch(Point p, double d)
        {
            double a = this.a;
            double b = this.b;
            double c = this.c;
            double x = p.X;
            double y = p.Y;
            Point[] res = new Point[2];
            res[0] = new Point();
            res[1] = new Point();
            res[0].X = (1 / (2 * (a * a + b * b))) * (Math.Sqrt(Math.Pow(2 * a * b * y - 2 * a * c - 2 * b * b * x, 2) - 4 * (a * a + b * b) * (-b * b * d * d + b * b * x * x + b * b * y * y - 2 * b * c * y + c * c)) - 2 * a * b * y + 2 * a * c + 2 * b * b * x);
            res[0].Y = (c - a * res[0].X) / b;
            res[1].X = -res[0].X;
            res[1].Y = (c - a * res[1].X) / b;
            return res;
        }
        //public void move(Move move, double angle)
        //{

        //}
    }
}
