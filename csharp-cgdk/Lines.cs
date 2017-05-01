using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    class Lines
    {
        private static double linesDelta = 205;
        public static bool CrossD1L1(Wizard self, World world)
        {
            if (self.Y <= -self.X + 4000 - linesDelta)
            {
                if (self.X <= 2000 - linesDelta && self.Y >= 2000)
                    return true;
                if (self.X >= 2000 && self.Y <= 2000 - linesDelta)
                    return true;
            }
            return false;
        }
        public static bool CrossD1L2(Wizard self, World world)
        {
            if (self.Y >= -self.X + 4000 + linesDelta)
            {
                if (self.X <= 2000 && self.Y >= 2000 + linesDelta)
                    return true;
                if (self.X >= 2000 + linesDelta && self.Y <= 2000)
                    return true;
            }
            return false;
        }
        public static bool CrossD2L1(Wizard self, World world)
        {
            if (self.Y >= self.X + linesDelta)
            {
                if (self.X <= 2000 - linesDelta && self.Y < 2000)
                    return true;
                if (self.X >= 2000 && self.Y > 2000 + linesDelta)
                    return true;
            }
            return false;
        }
        public static bool CrossD2L2(Wizard self, World world)
        {
            if (self.Y <= self.X - linesDelta)
            {
                if (self.X <= 2000 && self.Y < 2000 - linesDelta)
                    return true;
                if (self.X >= 2000 + linesDelta && self.Y > 2000)
                    return true;
            }
            return false;
        }
    }
}
