using System;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public class Settings
    {
        private static double Pi = Math.PI;
        public static double GeneralAngle(Wizard self, World world)
        {
            double x = self.X;
            double y = self.Y;
            if (y > x)
                return self.GetAngleTo(world.Width, world.Height);
            else
                return self.GetAngleTo(world.Width, 0);
        }
        public static CircularUnit NearestHostile(Wizard self, World world)
        {
            CircularUnit hostile = null;
            double minDist = world.Width;
            double dist;
            foreach(Building h in world.Buildings)
            {
                dist = self.GetDistanceTo(h);
                if (self.Faction != h.Faction && dist < minDist)
                {
                    minDist = dist;
                    hostile = h;
                }
            }
            foreach (Wizard h in world.Wizards)
            {
                dist = self.GetDistanceTo(h);
                if (self.Faction != h.Faction && dist < minDist)
                {
                    minDist = dist;
                    hostile = h;
                }
            }
            foreach (Minion h in world.Minions)
            {
                dist = self.GetDistanceTo(h);
                if (self.Faction != h.Faction && dist < minDist)
                {
                    if (!(h.Faction == Faction.Neutral && Math.Abs(h.GetAngleTo(self)) < Pi / 10 && (h.SpeedX > 0.2 || h.SpeedY > 0.2)))
                        continue;
                    minDist = dist;
                    hostile = h;
                }
            }
            return hostile;
        }
    }
}
