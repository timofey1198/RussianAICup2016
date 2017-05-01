using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        public void Move(Wizard self, World world, Game game, Move move)
        {
            move.Turn = self.GetAngleTo(2700, 0);
            move.Speed = 4;
            List<DoubleTriangle> bariers = new List<DoubleTriangle>();
            foreach(Tree unit in world.Trees)
            {
                if (self.GetDistanceTo(unit) < 600)
                {
                    Point p = new Point(unit.X, unit.Y);
                    bariers.Add(new DoubleTriangle(p, unit.Radius + self.Radius, 80));
                }
            }
            foreach (Wizard unit in world.Wizards)
            {
                if (self.GetDistanceTo(unit) < 600)
                {
                    Point p = new Point(unit.X, unit.Y);
                    bariers.Add(new DoubleTriangle(p, unit.Radius + self.Radius, 80));
                }
            }
            foreach (Minion unit in world.Minions)
            {
                if (self.GetDistanceTo(unit) < 600)
                {
                    Point p = new Point(unit.X, unit.Y);
                    bariers.Add(new DoubleTriangle(p, unit.Radius + self.Radius, 80));
                }
            }
            foreach (Building unit in world.Buildings)
            {
                if (self.GetDistanceTo(unit) < 600)
                {
                    Point p = new Point(unit.X, unit.Y);
                    bariers.Add(new DoubleTriangle(p, unit.Radius + self.Radius, 100));
                }
            }
            //double r = self.Radius;
            //bariers.Add(new DoubleTriangle(new Point(500-r, 3700+r), new Point(2000, 2200-r), new Point(3500+r, 3700+r), 20));
            //bariers.Add(new DoubleTriangle(new Point(300-r, 3500+r), new Point(1800+r, 2000), new Point(300-r, 500-r), 20));
            //bariers.Add(new DoubleTriangle(new Point(500-r, 300-r), new Point(2000, 1800+r), new Point(3500+r, 300-r), 20));
            //bariers.Add(new DoubleTriangle(new Point(3700+r, 3500+r), new Point(2200-r, 2000), new Point(3700+r, 500-r), 20));
            //bariers.Add(new DoubleTriangle(new Point(100+r, 3900-r), new Point(2000, 4000), new Point(3900-r, 3900-r), 20));
            //bariers.Add(new DoubleTriangle(new Point(100+r, 3900-r), new Point(0, 2000), new Point(100+r, 100+r), 20));
            //bariers.Add(new DoubleTriangle(new Point(100+r, 100+r), new Point(2000, 0), new Point(3900-r, 100+r), 20));
            //bariers.Add(new DoubleTriangle(new Point(3900-r, 3900-r), new Point(4000, 2000), new Point(3900-r, 100+r), 20));

            foreach (DoubleTriangle b in bariers)
            {
                b.move(self, move);
                Console.WriteLine(b);
            }
            if(world.TickIndex<200)
            {
                move.Speed = -0.3;
                move.StrafeSpeed = 0;
            }
        }
    }
}