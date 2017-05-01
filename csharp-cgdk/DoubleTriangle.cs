using System;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class DoubleTriangle
    {
        public DoubleTriangle(Point A, Point B, Point C, double delta)
        {
            Contur = new Triangle(A, B, C);

            Point aM = new Point((B.X + C.X) / 2, (B.Y + C.Y) / 2);
            Line norm1 = Contur.a.line.Normal(aM);
            Point[] aNormPts = norm1.MakeStretch(aM, delta);
            Point bodyP1 = Contur.In(aNormPts[0]) ? aNormPts[0] : aNormPts[1];
            Line bodyL1 = new Line(Contur.a.line, bodyP1);

            Point bM = new Point((A.X + C.X) / 2, (A.Y + C.Y) / 2);
            Line norm2 = Contur.b.line.Normal(bM);
            Point[] bNormPts = norm2.MakeStretch(bM, delta);
            Point bodyP2 = Contur.In(bNormPts[0]) ? bNormPts[0] : bNormPts[1];
            Line bodyL2 = new Line(Contur.b.line, bodyP2);

            Point cM = new Point((A.X + B.X) / 2, (A.Y + B.Y) / 2);
            Line norm3 = Contur.c.line.Normal(cM);
            Point[] cNormPts = norm3.MakeStretch(cM, delta);
            Point bodyP3 = Contur.In(cNormPts[0]) ? cNormPts[0] : cNormPts[1];
            Line bodyL3 = new Line(Contur.c.line, bodyP3);

            Body = new Triangle(bodyL1, bodyL2, bodyL3);
            ATr = new Triangle(Contur.b.line, Contur.c.line, new Line(Contur.a.line, Body.A));
            BTr = new Triangle(Contur.c.line, Contur.a.line, new Line(Contur.b.line, Body.A));
            CTr = new Triangle(Contur.a.line, Contur.b.line, new Line(Contur.c.line, Body.A));
        }
        public DoubleTriangle(Point center, double radius, double delta)
        {
            Point A = new Point(center.X, center.Y - 2 * radius);
            Point B = new Point(center.X - Math.Sqrt(3) * radius, center.Y + radius);
            Point C = new Point(center.X + Math.Sqrt(3) * radius, center.Y + radius);
            double r = radius + delta;
            Point A1 = new Point(center.X, center.Y - 2 * r);
            Point B1 = new Point(center.X - Math.Sqrt(3) * r, center.Y + r);
            Point C1 = new Point(center.X + Math.Sqrt(3) * r, center.Y + r);
            Contur = new Triangle(A1, B1, C1);
            Body = new Triangle(A, B, C);
            ATr = new Triangle(Contur.b.line, Contur.c.line, new Line(Contur.a.line, Body.A));
            BTr = new Triangle(Contur.c.line, Contur.a.line, new Line(Contur.b.line, Body.A));
            CTr = new Triangle(Contur.a.line, Contur.b.line, new Line(Contur.c.line, Body.A));
        }

        private Triangle Contur;
        private Triangle Body;
        private Triangle ATr;
        private Triangle BTr;
        private Triangle CTr;

        public bool In(Point p)
        {
            if (Contur.In(p) && !ATr.In(p) && !BTr.In(p) && !CTr.In(p))
                return true;
            else
                return false;
        }
        public void move(Wizard self, Move move)
        {
            Point me = new Point(self.X, self.Y);
            double Pi = Math.PI;
            if (this.In(me))
            {
                Console.WriteLine(me + " " + this);
                Line n1 = Body.a.line.Normal(me);
                Point p1 = n1.Cross(Body.a.line);
                double d1 = p1.Dist(me);

                Line n2 = Body.b.line.Normal(me);
                Point p2 = n2.Cross(Body.b.line);
                double d2 = p2.Dist(me);

                Line n3 = Body.c.line.Normal(me);
                Point p3 = n3.Cross(Body.c.line);
                double d3 = p3.Dist(me);

                double angle;

                if (d1 < d2)
                {
                    if (d1 < d3)
                    {
                        angle = self.GetAngleTo(p1.X, p1.Y);
                    }
                    else
                    {
                        angle = self.GetAngleTo(p3.X, p3.Y);
                    }
                }
                else
                {
                    if (d2 < d3)
                    {
                        angle = self.GetAngleTo(p2.X, p2.Y);
                    }
                    else
                    {
                        angle = self.GetAngleTo(p3.X, p3.Y);
                    }
                }
                if (angle > 0 && angle <= Pi)
                {
                    move.StrafeSpeed = -3;
                    move.Speed /= 2;
                    Console.WriteLine(me + " 3 " + this);
                }
                else
                {
                    move.StrafeSpeed = 3;
                    move.Speed /= 2;
                    Console.WriteLine(me + " 3 " + this);
                }
            }
        }
        public override string ToString()
        {
            return "{(" + Contur.A.X + ", " + Contur.A.Y + "); (" + Contur.B.X + ", " + Contur.B.Y + "); (" + Contur.C.X + ", " + Contur.C.Y + ")}";
        }
        //public static double operator ^(Double a, double b)
        //{
        //    return Math.Pow(a, b);
        //}Decimal
    }
}
