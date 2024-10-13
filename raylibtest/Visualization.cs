using System.Numerics;
using Raylib_CsLo;


namespace raylibtest
{
    public static class Visualization
    {

        public static void DrawProbes(List<Probe> probes)
        {
            foreach (Probe probe in probes) 
            { 
                foreach (Vector2 pos in probe.Path)
                {
                    Raylib.DrawCircle((int)pos.X, (int)pos.Y, probe.Size, probe.ParticleColor);
                }
            }
        }

        public static void DrawParticles(List<Particle> particles)
        {
            foreach (Particle particle in particles) particle.Draw();
        }

        public static void DrawFieldLines(List<Probe> probes)
        {
            foreach (Probe probe in probes)
            {
                for (int index = 0; index < probe.Path.Count - 1; index++)
                {

                    Raylib.DrawLine((int)probe.Path[index].X, (int)probe.Path[index].Y,
                                    (int)probe.Path[index + 1].X, (int)probe.Path[index + 1].Y,
                                    Raylib.WHITE);
                }
            }
        }

        public static Vector4 getParticleBoundingBox(List<Particle> particles)
        {
            float x1 = particles.OrderBy(x => x.Position.X).First().Position.X;
            float y1 = particles.OrderBy(x => x.Position.Y).First().Position.Y;
            float x2 = particles.OrderByDescending(x => x.Position.X).First().Position.X;
            float y2 = particles.OrderByDescending(x => x.Position.Y).First().Position.Y;
            x2 = x2 - x1;
            y2 = y2 - y1;

            return new Vector4(x1, y1, x2, y2);
        }

        public static void DrawFps(Vector2 Pos)
        {
            Raylib.DrawRectangle((int)Pos.X, (int)Pos.Y, 100, 25, Raylib.WHITE);
            Raylib.DrawFPS(10, 10);
        }

        public static void ClearScreen(Color color)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(color);
            Raylib.EndDrawing();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(color);
            Raylib.EndDrawing();
        }
    }
}
