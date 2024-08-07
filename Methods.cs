using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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