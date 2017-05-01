using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;


namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    class Logic
    {
        private static Random rnd = new Random();
        private static double Pi = Math.PI;
        public static bool stop = false;
        // public static int moveTickCount = 0;
        public static void Stop(Move move, World world, int maxTick)
        {
            _beforeStartStopTime = maxTick + 4;
            stop = false;
            if (world.TickIndex < maxTick)
            {
                stop = true;
                move.Speed = 0;
                move.StrafeSpeed = 0;
            }
        }
        private static void AttackUnit(Wizard self, Move move, World world, CircularUnit unit)
        {
            double radius = unit.Radius;
            move.Turn = self.GetAngleTo(unit);
            move.Speed = 4;
            //Console.WriteLine(self.GetAngleTo(unit));
            if (self.GetDistanceTo(unit) < self.CastRange + radius - 100)
            {
                move.Speed = 0;
                move.StrafeSpeed = 0;
                stop = true;
            }
            if (stop)
            {
                if (self.RemainingCooldownTicksByAction[1] > 10)
                    move.Speed = -3;
                if (self.RemainingCooldownTicksByAction[1] > 35)
                    move.Speed = 3;
            }
        }
        public static void Attack(Wizard self, Move move, World world)
        {
            CircularUnit unitForAttack = Find.UnitForAttack(self, world, true) == null ? Find.UnitForAttack(self, world, false) : Find.UnitForAttack(self, world, true);
            CircularUnit nearestEnemyUnit = Find.NearestEnemyUnit(self, world);
            if (unitForAttack != null)
                AttackUnit(self, move, world, unitForAttack);
            else if (nearestEnemyUnit != null)
                AttackUnit(self, move, world, nearestEnemyUnit);
        }
        public static void Barrier(Wizard self, Move move, World world, Game game)
        {
            double angleX = Find.AngleX(self, world);
            double angleY = Find.AngleY(self, world);

            if (Lines.CrossD1L1(self, world))
            {
                move.Speed /= 2.5;
                if (angleX >= -Pi / 4 && angleX < Pi / 4)
                    move.StrafeSpeed = 3;
                if (angleY > -Pi / 4 && angleY < Pi / 4)
                    move.StrafeSpeed = -3;
                if (angleX < -3 * Pi / 4 || angleX > 3 * Pi / 4)
                    move.StrafeSpeed = -3;
                if (angleY < -3 * Pi / 4 || angleY > 3 * Pi / 4)
                    move.StrafeSpeed = 3;
            }
            if (Lines.CrossD1L2(self, world))
            {
                move.Speed /= 2.5;
                if (angleX < -3 * Pi / 4 || angleX > 3 * Pi / 4)
                    move.StrafeSpeed = 3;
                if (angleY < -3 * Pi / 4 || angleY > 3 * Pi / 4)
                    move.StrafeSpeed = -3;
                if (angleX >= -Pi / 4 && angleX < Pi / 4)
                    move.StrafeSpeed = -3;
                if (angleY > -Pi / 4 && angleY < Pi / 4)
                    move.StrafeSpeed = 3;
            }
            if (Lines.CrossD2L1(self, world))
            {
                move.Speed /= 2.5;
                if (angleX < -3 * Pi / 4 || angleX > 3 * Pi / 4)
                    move.StrafeSpeed = -3;
                if (angleY > -Pi / 4 && angleY < Pi / 4)
                    move.StrafeSpeed = 3;
                if (angleX >= -Pi / 4 && angleX < Pi / 4)
                    move.StrafeSpeed = 3;
                if (angleY < -3 * Pi / 4 || angleY > 3 * Pi / 4)
                    move.StrafeSpeed = -3;
            }
            if (Lines.CrossD2L2(self, world))
            {
                move.Speed /= 2.5;
                // Console.WriteLine("D2L2");
                if (angleX >= -Pi / 4 && angleX < Pi / 4)
                    move.StrafeSpeed = -3;
                if (angleY < -3 * Pi / 4 || angleY > 3 * Pi / 4)
                    move.StrafeSpeed = 3;
                if (angleX < -3 * Pi / 4 || angleX > 3 * Pi / 4)
                    move.StrafeSpeed = 3;
                if (angleY > -Pi / 4 && angleY < Pi / 4)
                    move.StrafeSpeed = -3;
            }
        }
        private static int _beforeStartStopTime = 5;
        private static bool IsStoped(Wizard self, World world)
        {
            if (Math.Abs(self.SpeedX) <= 0.1 && Math.Abs(self.SpeedY) <= 0.1 && !stop && world.TickIndex > _beforeStartStopTime + 20)
            {
                //Console.WriteLine("Stop! {0:3}, {1:3}", self.SpeedX, self.SpeedY);
                return true;
            }
            else
            {
                //Console.WriteLine();
                return false;
            }
        }
        private static int _goToAngleMaxTick = 0;
        private static double _goToAngleTurn = 0;
        private static int _bypassTime = 11;
        public static void Bypass(Wizard self, Move move, World world)
        {
            if (IsStoped(self, world) || true)
            {
                double minDistToOurBuilding = Find.DistToNearestOurBuilding(self, world);
                double minDistToTree = Find.DistToNearestTree(self, world);
                double minDistToOurWizard = Find.DistToNearestOurWizard(self, world);
                double minDistToOurMinion = Find.DistToNearestOurMinion(self, world);
                double minDistToNeutralMinion = Find.DistToNearestNeutralMinion(self, world);
                double minDistToEnemyMinion = Find.DistToNearestEnemyMinion(self, world);

                _goToAngleMaxTick = world.TickIndex + _bypassTime;
                double minDist = world.Width;
                double delta = self.Radius + 4;

                if (minDistToOurBuilding - 5 <= delta)
                {
                    _goToAngleTurn = self.GetAngleTo(Find.NearestOurBuilding(self, world));
                    minDist = minDistToOurBuilding;
                }
                if (minDistToEnemyMinion - 3 <= delta)
                {
                    _goToAngleTurn = self.GetAngleTo(Find.NearestEnemyMinion(self, world));
                    minDist = minDistToEnemyMinion;
                }
                if (minDistToNeutralMinion - 1 < delta)
                {
                    _goToAngleTurn = self.GetAngleTo(Find.NearestNeutralMinion(self, world));
                    minDist = minDistToNeutralMinion;
                }
                if (minDistToTree < delta)
                {
                    _goToAngleTurn = self.GetAngleTo(Find.NearestTree(self, world));
                    minDist = minDistToTree;
                }
                if (minDistToOurWizard - 4 < delta)
                {
                    _goToAngleTurn = self.GetAngleTo(Find.NearestOurWizard(self, world));
                    minDist = minDistToOurWizard;
                }
                if (minDistToOurMinion - 2 < delta)
                {
                    _goToAngleTurn = self.GetAngleTo(Find.NearestOurMinion(self, world));
                    minDist = minDistToOurMinion - 2;
                }
                if (minDist <= delta)
                {
                    _goToAngleTurn += Pi / 2;
                    _goToAngleTurn %= Pi;
                    if (world.TickIndex < _goToAngleMaxTick && !stop)
                    {
                        move.Turn = _goToAngleTurn;
                        move.StrafeSpeed += rnd.Next(0, 100) * 0.02;
                        move.Speed -= rnd.Next(0, 100) * 0.02;
                    }
                }
            }
        }
        public static void KeepDistance(Wizard self, Move move, World world, double delta)
        {
            Wizard nearestOurWizard = Find.NearestOurWizard(self, world);
            if (self.GetDistanceTo(nearestOurWizard) < delta + self.Radius && !stop)
            {
                move.Turn = Pi/2 - self.GetAngleTo(nearestOurWizard);
                move.StrafeSpeed = 1.5;
            }
        }
        public static void GoToBonus(Wizard self, Move move, World world)
        {
            Bonus nearestBonus = Find.NearestBonus(self, world);
            if (nearestBonus != null)
            {
                move.Turn = self.GetAngleTo(nearestBonus);
                move.Speed = 3;
            }
        }
        public static void CorrectCourse(Wizard self, Move move, World world)
        {
            double delta = 150;
            double angleDelta = Pi / 16;
            foreach (Wizard wizard in Find.OurWizards(self, move, world))
            {
                if (self.GetDistanceTo(wizard) < delta)
                {
                    if (self.GetAngleTo(wizard) < angleDelta)
                        move.StrafeSpeed = 2;
                    if (self.GetAngleTo(wizard) > -angleDelta)
                        move.StrafeSpeed = -2;
                }
            }
        }
        public static void DodgingBullets(Wizard self, Move move, World world)
        {
            foreach(Projectile pr in world.Projectiles)
            {
                if (pr.Faction != self.Faction && Math.Abs(pr.GetAngleTo(self)) < Pi / 16 && self.GetDistanceTo(pr) < 350)
                    move.StrafeSpeed = 4;
            }
        }

        // -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
        //-- -- -- -- -- -- -- -- -- -- -- -- -- -- -
        public static double x_begin = 0.0, y_begin = 0.0;
        public static bool flag_bonus1 = false, flag_turn = false, flag_take_bonuses = false;
        static void GoToBonus(World world, Wizard self, Move move, double BonusX, double BonusY)
        {
            double x = BonusX;
            double y = BonusY;
            if (world.TickIndex % 2500 < 1000&& world.TickIndex % 2500 > 500)
            {
                //Console.WriteLine(world.Bonuses.Length);
                //Bonus bonus = Find.NearestBonus(self, world);
                //if (bonus == null)
                //{
                //    Console.WriteLine("null");
                //    flag_take_bonuses = true;
                //    flag_bonus1 = false;
                //    return;
                //}
                //else
                //{
                //    x = bonus.X;
                //    y = bonus.Y;
                //}
            }
            move.Speed = 4;
            if (Math.Abs(self.X - x) <= self.Radius && Math.Abs(self.Y - y) <= self.Radius)
            {
                flag_take_bonuses = true;
                flag_bonus1 = false;
                return;
            }
            move.Turn = self.GetAngleTo(x, y); // поворачиваемся к бонусу
        }
        public static void GoBack(World world, Wizard self, Move move, double x_begin, double y_begin)
        {
            double delta = 100;
            move.Speed = 4;
            if (Math.Abs(x_begin - self.X) <= self.Radius + delta && Math.Abs(self.Y - y_begin) <= self.Radius + delta)
            {
                //move.Turn = self.GetAngleTo(world.Width, 0);
                flag_take_bonuses = false;
                return;
            }
            move.Turn = self.GetAngleTo(x_begin, y_begin);
        }
        public static void ActionWithBonus(World world, Wizard self, Move move)
        {
            int delta1 =  (int)(Math.Sqrt(Math.Pow((self.X - 1200), 2) + Math.Pow((self.Y - 1200), 2)) / 4.5);
            int delta2 = (int)(Math.Sqrt(Math.Pow((self.X - 2800), 2) + Math.Pow((self.Y - 2800), 2)) / 4.5);
            int delta = Math.Min(delta1, delta2);

            
            if (self.Life < 0.15 * self.MaxLife || self.Y > self.X + 1700)
            {
                flag_bonus1 = false;
            }

            if ((world.TickIndex + delta) % 2500 == 0 && world.TickIndex < 18500)
            {
                Console.WriteLine(world.TickIndex);
                flag_bonus1 = true;
                x_begin = self.X;
                y_begin = self.Y;
            }

            if (flag_bonus1 && self.Life > 0.35 * self.MaxLife)
            {
                stop = false;
                //if (delta1 > delta2)
                    GoToBonus(world, self, move, 1200, 1200);
                //else
                   // GoToBonus(world, self, move, 2800, 2800);
            }
            if (flag_take_bonuses)
            {
                stop = false;
                GoBack(world, self, move, x_begin, y_begin);
            }
        }
    }
}
