﻿using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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


        public static void drawFieldLinesDirection(List<Probe> probes, int countArrows, Vector2 offset, float toffset)
        {
            foreach(Probe probe in probes)
            {
                drawFieldLineDirection(probe, countArrows, offset, toffset);
            }
        }

        public static void drawFieldLineDirection(Probe probe, int countArrows, Vector2 offset, float toffset)
        {
            List<Vector2> points = equallySpacedPointOnLine(probe.Path, countArrows, toffset);

            for(int i = 0; i < points.Count - 1; i++)
            {
                Vector2 current = points[i];
                Vector2 next = points[i + 1];
                //float angle = (float)Math.Atan2(current.Y - next.Y, current.X - next.X);

                //float angleDegrees = angle * RayMath.RAD2DEG;

                //int size = 10;

                Raylib.DrawCircle((int)(current.X+offset.X), (int)(current.Y+offset.Y), 4, Raylib.WHITE);
            }
            
        }

        public static List<Vector2> equallySpacedPointOnLine(List<Vector2> line, int count, float toffset)
        {
            List<Vector2> points = new();

            float dist = 1f / count;
            float along = 0;

            for (int i = 0; i < count; i++)
            {
                points.Add(pointAlongPolyline(line, along+toffset));
                along += dist;  
            }

            return points;
        }

        public static Vector2 pointAlongPolyline(List<Vector2> polyLine, float t)
        {
            float totalDistance = 0;
            for (int i = 0; i < polyLine.Count - 1; i++)
            {
                totalDistance += RayMath.Vector2Distance(polyLine[i], polyLine[i + 1]);
            }

            float targetDistance = t * totalDistance;

            float traverse = 0;
            Vector2 point = new(0);

            for (int i = 0; i < polyLine.Count - 1; i++)
            {
                float segmentLength = RayMath.Vector2Distance(polyLine[i], polyLine[i + 1]);

                if (segmentLength + traverse >= targetDistance)
                {
                    float segmentT = (targetDistance - traverse) / segmentLength;
                    point = RayMath.Vector2Lerp(polyLine[i], polyLine[i+1], segmentT);
                    break;
                }

                traverse += segmentLength;
            }

            return point;
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
