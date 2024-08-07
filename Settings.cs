using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssm
{
    internal class Settings
    {
        public static string windowMode = "Borderless";
        public static int fps = 165;

        public static float fov = 70;

        public static float sd = 15;
        public static float ar = 35;

        public static Model model = Raylib.LoadModel(@"assets\model.obj");
        public static Color[] colors = [Color.Violet, Color.Blue, Color.Green, Color.SkyBlue];
        public static float noteScale = 0.6f;
    }
}
