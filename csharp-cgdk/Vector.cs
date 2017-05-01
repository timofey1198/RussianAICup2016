using System;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class Vector
    {
        public Vector(double x1, double y1, double x2, double y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }
        public Vector(double x1, double y1, Vector vect)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x1 + vect.x2;
            _y2 = y1 + vect.y2;
        }
        public Vector(double x2, double y2)
        {
            _x1 = 0;
            _y1 = 0;
            _x2 = x2;
            _y2 = y2;
        }
        public Vector()
        {
            _x1 = 0;
            _y1 = 0;
            _x2 = 0;
            _y2 = 0;
        }

        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;

        public double x1
        {
            get { return _x1; }
            set { _x1 = value; }
        }
        public double y1
        {
            get { return _y1; }
            set { _y1 = value; }
        }
        public double x2
        {
            get { return _x2; }
            set { _x2 = value; }
        }
        public double y2
        {
            get { return _y2; }
            set { _y2 = value; }
        }

        public double Length
        {
            get
            {
                double l = Math.Sqrt((_x1 - _x2) * (_x1 - _x2) + (_y1 - _y2) * (_y1 - _y2));
                return l;
            }
        }

        public double ScalarMult(Vector vect)
        {
            double mult = this.Length * vect.Length * this.CosFi(vect);
            return mult;
        }
        public double CosFi(Vector vect)
        {
            Vector b = new Vector(this.x1, this.y1, vect);
            Vector c = new Vector(this.x2, this.y2, b.x2, b.y2);
            double cos = (this.Length * this.Length + b.Length * b.Length - c.Length * c.Length) / (2 * this.Length * b.Length);
            return cos;
        }
    }
}
