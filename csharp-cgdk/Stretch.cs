using System;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class Stretch
    {
        public Stretch()
        {
            _p1 = new Point();
            _p2 = new Point();
            _l = new Line();
        }
        public Stretch(Point p1, Point p2)
        {
            _p1 = p1;
            _p2 = p2;
            _l = new Line(p1, p2);
        }

        private Point _p1;
        private Point _p2;
        private Line _l;

        public Point P1
        {
            get { return _p1; }
        }
        public Point P2
        {
            get { return _p2; }
        }
        public Line line { get { return _l; } }
    }
}
