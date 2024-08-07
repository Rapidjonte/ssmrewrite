namespace ssm
{
    using Raylib_cs;
    using System.Diagnostics;
    using System.Numerics;
    using System.Reflection.Metadata;
    using static System.Formats.Asn1.AsnWriter;

    internal class Program
    {
        public static readonly int screenWidth = Raylib.GetScreenWidth();
        public static readonly int screenHeight = Raylib.GetScreenHeight();

        public static Stopwatch songTime = new Stopwatch();
        public static double skippedSeconds = 0; 

        static void Main(string[] args)
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Sound Space Minus");
            Raylib.SetTargetFPS(Settings.fps);
            Methods.SetWindowMode(Settings.windowMode);
            Raylib.InitAudioDevice();

            PlayMap(new SSPMap(@"assets\map.sspm"));

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }

        static void PlayMap(IBeatmapSet map)
        {
            Sound song = Raylib.LoadSoundFromWave(Raylib.LoadWaveFromMemory(".mp3", map.AudioData));
            Model model = Settings.model; 
            Raylib.PlaySound(song);

            List<Note> renderedNotes = new List<Note>();
            int noteIndex = 0;
            int colorIndex = 0;

            Camera3D camera = new Camera3D
            {
                Position = new Vector3(0.0f, 0.0f, -3.0f),
                Target = new Vector3(0, 0.0f, 0.0f),
                Up = new Vector3(0.0f, 1.0f, 0.0f),
                FovY = Settings.fov,
                Projection = CameraProjection.Perspective
            };

            songTime.Start();
            while (!Raylib.WindowShouldClose())
            {
                while (noteIndex < map.Difficulties[0].Notes.Length && (float)(songTime.Elapsed.TotalSeconds+skippedSeconds) + Settings.sd/Settings.ar > map.Difficulties[0].Notes[noteIndex].Time)
                {
                    Note toAdd = map.Difficulties[0].Notes[noteIndex];
                    toAdd.Color = Settings.colors[colorIndex];
                    renderedNotes.Add(toAdd);

                    if (colorIndex < Settings.colors.Length-1) {
                        colorIndex++;
                    } else {
                        colorIndex = 0;
                    }
                    noteIndex++;
                } 

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.BeginMode3D(camera);
                for (int i = 0; i < renderedNotes.Count; i++) 
                {
                    float noteZ = (renderedNotes[i].Time - (float)(songTime.Elapsed.TotalSeconds+skippedSeconds)) * Settings.ar;
                    if (noteZ < 0)
                    {
                        renderedNotes.RemoveAt(i);
                        i--;
                        continue;
                    }
                    
                    Raylib.DrawModel(model, new Vector3(renderedNotes[i].X, renderedNotes[i].Y, noteZ), Settings.noteScale, renderedNotes[i].Color);
                }
                Raylib.EndMode3D();

                Raylib.DrawText("FPS: " + Raylib.GetFPS(), 0, 0, 100, Color.White);

                Raylib.EndDrawing();
            }

            Raylib.UnloadSound(song);
            Raylib.UnloadModel(model);
        }
    }
}
