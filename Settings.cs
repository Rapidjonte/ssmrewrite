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

        public static float fov = 50;
        public static float cameraParallax = 2;
        public static float cameraDistance = 3.8f;

        public static float sd = 15;
        public static float ar = 35;

        public static Model model = Raylib.LoadModel(@"assets\model.obj");
        public static float noteScale = 0.59f;

        public static Color[] colors = Methods.GetColorsFromPath(@"assets\colors.txt");

        public static double modifier = 100;
    }
}
