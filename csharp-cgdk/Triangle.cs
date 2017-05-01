using System;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class Triangle
    {
        public Triangle() { _A = new Point(); _B = new Point(); _C = new Point(); _a = new Stretch(); _b = new Stretch(); _b = new Stretch(); }
        public Triangle(Point A, Point B, Point C)
        {
            _A = A;
            _B = B;
            _C = C;
            _a = new Stretch(B, C);
            _b = new Stretch(A, C);
            _c = new Stretch(A, B);
        }
        public Triangle(Line l1, Line l2, Line l3)
        {
            _C = l1.Cross(l2);
            _B = l1.Cross(l3);
            _A = l3.Cross(l2);
            _a = new Stretch(B, C);
            _b = new Stretch(A, C);
            _c = new Stretch(A, B);
        }

        private Point _A;
        private Point _B;
        private Point _C;
        private Stretch _a;
        private Stretch _b;
        private Stretch _c;

        public Point A { get { return _A; } }
        public Point B { get { return _B; } }
        public Point C { get { return _C; } }
        public Stretch a { get { return _a; } }
        public Stretch b { get { return _b; } }
        public Stretch c { get { return _c; } }

        public bool In(Point p)
        {
            double d1 = p.Dist(A);
            double d2 = p.Dist(B);
            double d3 = p.Dist(C);
            Line l = new Line(A, p);
            Point p1 = l.Cross(a.line);
            double d4 = p1.Dist(A);
            if (d1 < d4 && d3 + d2 > B.Dist(C))
                return true;
            else
                return false;
        }
    }
}
