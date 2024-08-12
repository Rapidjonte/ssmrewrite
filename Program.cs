namespace ssm
{
    using Raylib_cs;
    using System;
    using System.Diagnostics;
    using System.Numerics;

    internal class Program
    {
        public static int screenWidth = 0;
        public static int screenHeight = 0;

        public static Stopwatch songTime = new Stopwatch();
        public static double skippedSeconds = 0; 

        static void Main(string[] args)
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Sound Space Minus");
            Raylib.SetTargetFPS(Settings.fps);
            Methods.SetWindowMode(Settings.windowMode);
            Raylib.InitAudioDevice();
            screenWidth = Raylib.GetScreenWidth();
            screenHeight = Raylib.GetScreenHeight();

            PlayMap(new SSPMap(@"assets\map.sspm"));

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }

        static void PlayMap(IBeatmapSet map)
        {
            Sound song = Raylib.LoadSoundFromWave(Raylib.LoadWaveFromMemory(Settings.songFileType, map.AudioData));

            Model model = Settings.model;
            Texture2D border = Settings.border;

            Vector2 center = new Vector2(screenWidth / 2, screenWidth / 2);

            List<Note> renderedNotes = new List<Note>();
            int noteIndex = 0;
            int colorIndex = 0;

            Raylib.DisableCursor();
            Camera3D camera = new Camera3D
            {
                Position = new Vector3(0.0f, 0.0f, -Settings.cameraDistance),
                Target = new Vector3(0, 0.0f, 0.0f),
                Up = new Vector3(0.0f, 1.0f, 0.0f),
                FovY = Settings.fov,
                Projection = CameraProjection.Perspective
            };

            Raylib.PlaySound(song);
            songTime.Start();
            while (!Raylib.WindowShouldClose()) // ------ LOOP ------
            {
                Raylib.SetSoundPitch(song, (float)(Settings.modifier / 100));

                while (noteIndex < map.Difficulties[0].Notes.Length && (float)((songTime.Elapsed.TotalSeconds + skippedSeconds) * (Settings.modifier / 100)) + Settings.sd/Settings.ar > map.Difficulties[0].Notes[noteIndex].Time)
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

                Vector2 mousePosition = Raylib.GetMousePosition();

                if (Settings.debugCam)
                {
                    Raylib.UpdateCamera(ref camera, CameraMode.Free);
                }

                Raylib.BeginMode3D(camera);
                for (int i = 0; i < renderedNotes.Count; i++) 
                {
                    float noteZ = (renderedNotes[i].Time - (float)((songTime.Elapsed.TotalSeconds + skippedSeconds) * (Settings.modifier / 100))) * Settings.ar;
                    if (noteZ < 0)
                    {
                        renderedNotes.RemoveAt(i);
                        i--;
                        continue;
                    }

                    Raylib.DrawModel(model, new Vector3(new Vector2(renderedNotes[i].X, renderedNotes[i].Y)+-Methods.Parallax(mousePosition, center), noteZ), Settings.noteScale, renderedNotes[i].Color);
                }
                // Raylib.DrawTextureEx(border, center, 0, 0.001f, Color.White); work in progress
                Raylib.EndMode3D();

                Raylib.DrawText("FPS: " + Raylib.GetFPS(), 0, 0, 100, Color.White);

                Raylib.EndDrawing();
            }

            Raylib.UnloadSound(song);
            Raylib.UnloadModel(model);
            Raylib.UnloadTexture(border);

            Raylib.EnableCursor();
        }
    }
}