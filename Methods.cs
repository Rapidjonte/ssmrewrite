using Raylib_cs;
using System.Numerics;

namespace ssm
{
    public class Methods
    {
        public static void SetWindowMode(string windowMode)
        {
            switch (windowMode)
            {
                case "Fullscreen":
                    Raylib.ToggleFullscreen(); break;

                case "Borderless":
                    Raylib.ToggleBorderlessWindowed(); break;

                case "Windowed":
                    break;

                default:
                    Log.Error($"ERROR: windowMode \"{Settings.windowMode}\" is unrecognized, resorting to windowed.."); break;
            }
        }

        public static Color HexToColor(string hex)
        {
            hex = hex.TrimStart('#');

            byte alpha = 255;
            if (hex.Length == 8)
            {
                alpha = Convert.ToByte(hex.Substring(0, 2), 16);
                hex = hex.Substring(2);
            }
            else if (hex.Length != 6)
            {
                throw new ArgumentException("invalid hex color code >:(");
            }

            byte red = Convert.ToByte(hex.Substring(0, 2), 16);
            byte green = Convert.ToByte(hex.Substring(2, 2), 16);
            byte blue = Convert.ToByte(hex.Substring(4, 2), 16);

            return new Color(red, green, blue, alpha);
        }

        public static Color[] GetColorsFromPath(string path)
        {
            string[] file = File.ReadAllLines(path);

            Color[] colors = new Color[file.Length];
            for (int i = 0; i < file.Length; i++)
            {
                colors[i] = HexToColor(file[i]);
            }
            return colors;
        }

        public static Vector2 Parallax(Vector2 mousePosition, Vector2 center)
        {
            return new Vector2(-(mousePosition.X - center.X) * Settings.cameraParallax / 10000, -(mousePosition.Y - center.Y) * Settings.cameraParallax / 10000);
        }
    }

    public class Log
    {
        public static void Error(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string infoMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(infoMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void InfoWithPath(string infoMessage, string path)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(infoMessage + path);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}