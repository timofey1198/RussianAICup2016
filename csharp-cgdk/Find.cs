using System;
using System.Linq;
using System.Collections.Generic;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    class Find
    {
        private static double Pi = Math.PI;
        public static double AngleToNearestBarrier(Wizard self, World world)
        {
            double angleToNearestBarrier = 0;
            int count = 0;
            foreach(Tree tree in world.Trees)
            {
                angleToNearestBarrier += self.GetAngleTo(tree);
                count++;
            }
            return angleToNearestBarrier / count;
        }
        public static Tree NearestTreeBarrier(Wizard self, World world)
        {
            Tree nearestTreeBarrier = null;
            double minAngle = Math.PI/12;
            double minDist = world.Width;
            foreach (Tree tree in world.Trees)
            {
                if (self.GetDistanceTo(tree) < minDist && Math.Abs(self.GetAngleTo(tree)) < minAngle)
                {
                    minDist = self.GetDistanceTo(tree);
                    nearestTreeBarrier = tree;
                }
            }
            return nearestTreeBarrier;
        }
        public static Building NearestEnemyBuilding(Wizard self, World world)
        {
            Building nearestEnemyBuild = null;
            double minDist = world.Width;
            foreach (Building building in world.Buildings)
            {
                if (building.Faction != self.Faction && self.GetDistanceTo(building) < minDist)
                {
                    minDist = self.GetDistanceTo(building);
                    nearestEnemyBuild = building;
                }
            }
            return nearestEnemyBuild;
        }
        public static bool IsClosed(Wizard self, World world, Unit unit)
        {
            double angle = self.GetAngleTo(unit);
            double dist = self.GetDistanceTo(unit);
            double maxDeltaAngles = Pi / 20;

            foreach (Tree tree in world.Trees)
                if (Math.Abs(angle - self.GetAngleTo(tree)) < maxDeltaAngles && self.GetDistanceTo(tree) < dist)
                    return true;
            foreach(Minion minion in world.Minions)
                if (Math.Abs(angle - self.GetAngleTo(minion)) < maxDeltaAngles && self.GetDistanceTo(minion) < dist)
                    return true;
            foreach (Wizard wizard in world.Wizards)
                if (Math.Abs(angle - self.GetAngleTo(wizard)) < maxDeltaAngles && self.GetDistanceTo(wizard) < dist)
                    return true;
            foreach (Building building in world.Buildings)
                if (Math.Abs(angle - self.GetAngleTo(building)) < maxDeltaAngles && self.GetDistanceTo(building) < dist)
                    return true;

            return false;
        }
        public static bool IsClosed(Wizard self, World world, double x, double y, bool notTree)
        {
            double angle = self.GetAngleTo(x, y);
            double dist = self.GetDistanceTo(x, y);
            double maxDeltaAngles = Pi / 20;

            foreach (Minion minion in world.Minions)
                if (Math.Abs(angle - self.GetAngleTo(minion)) < maxDeltaAngles && self.GetDistanceTo(minion) < dist)
                    return true;
            foreach (Wizard wizard in world.Wizards)
                if (Math.Abs(angle - self.GetAngleTo(wizard)) < maxDeltaAngles && self.GetDistanceTo(wizard) < dist)
                    return true;
            foreach (Building building in world.Buildings)
                if (Math.Abs(angle - self.GetAngleTo(building)) < maxDeltaAngles && self.GetDistanceTo(building) < dist)
                    return true;

            return false;
        }
        public static Building NearestEnemyBuilding(Wizard self, World world, bool detail)
        {
            Building nearestEnemyBuild = null;
            double minDist = world.Width;
            foreach (Building building in world.Buildings)
            {
                if (building.Faction != self.Faction && self.GetDistanceTo(building) < minDist)
                {
                    if (detail)
                    {
                        if (IsClosed(self, world, building))
                            continue;
                    }
                    minDist = self.GetDistanceTo(building);
                    nearestEnemyBuild = building;
                }
            }
            return nearestEnemyBuild;
        }
        public static Wizard NearestEnemyWizard(Wizard self, World world)
        {
            Wizard nearestEnemyWizard = null;
            double minDist = world.Width;
            foreach (Wizard wizard in world.Wizards)
            {
                if (wizard.Faction != self.Faction && self.GetDistanceTo(wizard) < minDist)
                {
                    minDist = self.GetDistanceTo(wizard);
                    nearestEnemyWizard = wizard;
                }
            }
            return nearestEnemyWizard;
        }
        public static Wizard NearestEnemyWizard(Wizard self, World world, bool detail)
        {
            Wizard nearestEnemyWizard = null;
            double minDist = world.Width;
            foreach (Wizard wizard in world.Wizards)
            {
                if (wizard.Faction != self.Faction && self.GetDistanceTo(wizard) < minDist)
                {
                    if (detail)
                    {
                        if (IsClosed(self, world, wizard))
                            continue;
                    }
                    minDist = self.GetDistanceTo(wizard);
                    nearestEnemyWizard = wizard;
                }
            }
            return nearestEnemyWizard;
        }
        public static Minion NearestEnemyMinion(Wizard self, World world, bool detail)
        {
            Minion nearestEnemyMinion = null;
            double minDist = world.Width;
            foreach (Minion minion in world.Minions)
            {
                if (minion.Faction != self.Faction && minion.Faction != Faction.Neutral && self.GetDistanceTo(minion) < minDist)
                {
                    if (detail)
                    {
                        if (IsClosed(self, world, minion))
                            continue;
                    }
                    minDist = self.GetDistanceTo(minion);
                    nearestEnemyMinion = minion;
                }
            }
            return nearestEnemyMinion;
        }
        public static Minion NearestEnemyMinion(Wizard self, World world)
        {
            Minion nearestEnemyMinion = null;
            double minDist = world.Width;
            foreach (Minion minion in world.Minions)
            {
                if (minion.Faction != self.Faction && minion.Faction != Faction.Neutral && self.GetDistanceTo(minion) < minDist)
                {
                    minDist = self.GetDistanceTo(minion);
                    nearestEnemyMinion = minion;
                }
            }
            return nearestEnemyMinion;
        }
        public static Minion NearestNeutralMinion(Wizard self, World world)
        {
            Minion nearestNeutralMinion = null;
            double minDist = world.Width;
            foreach (Minion minion in world.Minions)
            {
                if (minion.Faction == Faction.Neutral && self.GetDistanceTo(minion) < minDist)
                {
                    minDist = self.GetDistanceTo(minion);
                    nearestNeutralMinion = minion;
                }
            }
            return nearestNeutralMinion;
        }
        //-------
        public static Building NearestOurBuilding(Wizard self, World world)
        {
            Building nearestOurBuild = null;
            double minDist = world.Width;
            foreach (Building building in world.Buildings)
            {
                if (building.Faction == self.Faction && self.GetDistanceTo(building) < minDist)
                {
                    minDist = self.GetDistanceTo(building);
                    nearestOurBuild = building;
                }
            }
            return nearestOurBuild;
        }
        public static Wizard NearestOurWizard(Wizard self, World world)
        {
            Wizard nearestOurWizard = null;
            double minDist = world.Width;
            foreach (Wizard wizard in world.Wizards)
            {
                if (wizard.Faction == self.Faction && !wizard.IsMe && self.GetDistanceTo(wizard) < minDist)
                {
                    minDist = self.GetDistanceTo(wizard);
                    nearestOurWizard = wizard;
                }
            }
            return nearestOurWizard;
        }
        public static Minion NearestOurMinion(Wizard self, World world)
        {
            Minion nearestOurMinion = null;
            double minDist = world.Width;
            foreach (Minion minion in world.Minions)
            {
                if (minion.Faction == self.Faction && self.GetDistanceTo(minion) < minDist)
                {
                    minDist = self.GetDistanceTo(minion);
                    nearestOurMinion = minion;
                }
            }
            return nearestOurMinion;
        }
        public static Bonus NearestBonus(Wizard self, World world)
        {
            Bonus nearestBonus = null;
            double minDist = world.Width;
            foreach(Bonus bonus in world.Bonuses)
            {
                if (self.GetDistanceTo(bonus) < minDist)
                {
                    minDist = self.GetDistanceTo(bonus);
                    nearestBonus = bonus;
                }
            }
            return nearestBonus;
        }
        public static List<Wizard> OurWizards(Wizard self, Move move, World world)
        {
            List<Wizard> ourWizards = new List<Wizard>();
            foreach(Wizard wizard in world.Wizards)
            {
                if (self.Faction == wizard.Faction)
                    ourWizards.Add(wizard);
            }
            return ourWizards;
        }
        public static List<Building> OurBuildings(Wizard self, Move move, World world)
        {
            List<Building> ourBuildings = null;
            foreach (Building building in world.Buildings)
            {
                if (self.Faction == building.Faction)
                    ourBuildings.Add(building);
            }
            return ourBuildings;
        }
        public static List<Minion> OurMinions(Wizard self, Move move, World world)
        {
            List<Minion> ourMinions = null;
            foreach (Minion minion in world.Minions)
            {
                if (self.Faction == minion.Faction)
                    ourMinions.Add(minion);
            }
            return ourMinions;
        }
        public static double AngleX(Wizard self, World world)
        {
            double angleX = self.GetAngleTo(self.X, 0);
            return angleX;
        }
        public static double AngleY(Wizard self, World world)
        {
            double angleY = self.GetAngleTo(0, self.Y);
            return angleY;
        }
        public static Tree NearestTree(Wizard self, World world)
        {
            Tree nearestTree = null;
            double minDist = world.Width;
            foreach(Tree tree in world.Trees)
            {
                if (self.GetDistanceTo(tree) < minDist)
                {
                    minDist = self.GetDistanceTo(tree);
                    nearestTree = tree;
                }
            }
            return nearestTree;
        }
        public static double DistToNearestTree(Wizard self, World world)
        {
            Tree nearestTree = NearestTree(self, world);
            return nearestTree == null ? world.Width : self.GetDistanceTo(nearestTree) - nearestTree.Radius;
        }
        public static double DistToNearestEnemyMinion(Wizard self, World world)
        {
            Minion nearestEnemyMinion = NearestEnemyMinion(self, world);
            return nearestEnemyMinion == null ? world.Width : self.GetDistanceTo(nearestEnemyMinion) - nearestEnemyMinion.Radius;
        }
        public static double DistToNearestNeutralMinion(Wizard self, World world)
        {
            Minion nearestNeutralMinion = NearestNeutralMinion(self, world);
            return nearestNeutralMinion == null ? world.Width : self.GetDistanceTo(nearestNeutralMinion) - nearestNeutralMinion.Radius;
        }
        public static double DistToNearestOurBuilding(Wizard self, World world)
        {
            Building nearestOurBuilding = NearestOurBuilding(self, world);
            return nearestOurBuilding == null ? world.Width : self.GetDistanceTo(nearestOurBuilding) - nearestOurBuilding.Radius;
        }
        public static double DistToNearestOurMinion(Wizard self, World world)
        {
            Minion nearestOurMinion = NearestOurMinion(self, world);
            return nearestOurMinion == null ? world.Width : self.GetDistanceTo(nearestOurMinion) - nearestOurMinion.Radius;
        }
        public static double DistToNearestOurWizard(Wizard self, World world)
        {
            Wizard nearestOurWizard = NearestOurWizard(self, world);
            return nearestOurWizard == null ? world.Width : self.GetDistanceTo(nearestOurWizard) - nearestOurWizard.Radius;
        }

        public static CircularUnit UnitForAttack(Wizard self, World world, bool mode)
        {
            double minHp = Math.Pow(10, 10);
            CircularUnit unitForAttack = null;

            foreach(Wizard wizard in world.Wizards)
            {
                if (self.GetDistanceTo(wizard) < 500 && (Find.IsClosed(self, world, wizard) || !mode) && self.Faction != wizard.Faction)
                {
                    if (wizard.Life < minHp)
                    {
                        minHp = wizard.Life;
                        unitForAttack = wizard;
                    }
                }
            }
            foreach (Minion minion in world.Minions)
            {
                if (self.GetDistanceTo(minion) < self.CastRange - 100 && (Find.IsClosed(self, world, minion) || !mode) && self.Faction != minion.Faction && minion.Faction != Faction.Neutral)
                {
                    if (minion.Life < minHp)
                    {
                        minHp = minion.Life;
                        unitForAttack = minion;
                    }
                }
            }
            foreach (Building building in world.Buildings)
            {
                if (self.GetDistanceTo(building) < self.CastRange - 100 && (Find.IsClosed(self, world, building) || !mode) && self.Faction != building.Faction)
                {
                    if (building.Life < minHp)
                    {
                        minHp = building.Life;
                        unitForAttack = building;
                    }
                }
            }

            // neutral
            foreach (Minion minion in world.Minions)
            {
                if (self.GetDistanceTo(minion) < 40 && (Find.IsClosed(self, world, minion) || !mode) && minion.Faction == Faction.Neutral)
                {
                    unitForAttack = minion;
                }
            }

            return unitForAttack;
        }

        public static CircularUnit NearestEnemyUnit(Wizard self, World world)
        {
            CircularUnit nearestEnemyUnit = null;
            Building nearestEnemyBuilding = NearestEnemyBuilding(self, world, true);
            Minion nearestEnemyMinion = NearestEnemyMinion(self, world, true);
            Wizard nearestEnemyWizard = NearestEnemyWizard(self, world, true);
            nearestEnemyBuilding = nearestEnemyBuilding == null ? NearestEnemyBuilding(self, world, false) : nearestEnemyBuilding;
            nearestEnemyMinion = nearestEnemyMinion == null ? NearestEnemyMinion(self, world, false) : nearestEnemyMinion;
            nearestEnemyWizard = nearestEnemyWizard == null ? NearestEnemyWizard(self, world, false) : nearestEnemyWizard;
            double minDist = world.Width;

            if (nearestEnemyBuilding != null && self.GetDistanceTo(nearestEnemyBuilding) < minDist)
            {
                minDist = self.GetDistanceTo(nearestEnemyBuilding);
                nearestEnemyUnit = nearestEnemyBuilding;
            }
            if (nearestEnemyMinion != null && self.GetDistanceTo(nearestEnemyMinion) < minDist)
            {
                minDist = self.GetDistanceTo(nearestEnemyMinion);
                nearestEnemyUnit = nearestEnemyMinion;
            }
            if (nearestEnemyWizard != null && self.GetDistanceTo(nearestEnemyWizard) < minDist)
            {
                minDist = self.GetDistanceTo(nearestEnemyWizard);
                nearestEnemyUnit = nearestEnemyWizard;
            }
            return nearestEnemyUnit;
        }
    }
}
