using System.Numerics;
using Raylib_CsLo;


namespace raylibtest
{
    public static class Visualization
    {

        public static void DrawProbes(List<Probe> probes, Vector2 offset)
        {
            foreach (Probe probe in probes) 
            { 
                foreach (Vector2 pos in probe.Path)
                {
                    Raylib.DrawCircle((int)pos.X + (int)offset.X, (int)pos.Y + (int)offset.Y, probe.Size, probe.ParticleColor);
                }
            }
        }

        public static void DrawParticles(List<Particle> particles, Vector2 offset)
        {
            foreach (Particle particle in particles) particle.Draw(offset);
        }

        public static void DrawFieldLines(List<Probe> probes, Vector2 offset, int thickness)
        {
            foreach (Probe probe in probes)
            {
                for (int index = 0; index < probe.Path.Count - 1; index++)
                {
                    Raylib.DrawLineEx(
                        new Vector2((int)probe.Path[index].X + (int)offset.X, (int)probe.Path[index].Y + (int)offset.Y),       
                        new Vector2((int)probe.Path[index + 1].X + (int)offset.X, (int)probe.Path[index + 1].Y + (int)offset.Y),
                        thickness,
                        Raylib.WHITE
                    );
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
    }
}
