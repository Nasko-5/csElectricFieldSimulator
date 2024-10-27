using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        public static void DrawFieldLines(List<Probe> probes, int thickness)
        {
            foreach (Probe probe in probes)
            {
                for (int index = 0; index < probe.Path.Count - 1; index++)
                {
                    Raylib.DrawLineEx(
                        new Vector2((int)probe.Path[index].X, (int)probe.Path[index].Y),       
                        new Vector2((int)probe.Path[index + 1].X, (int)probe.Path[index + 1].Y),
                        thickness,
                        Raylib.WHITE
                    );
                }
            }
        }


        public static void drawFieldLinesDirection(List<Probe> probes, int countArrows, float toffset)
        {
            foreach(Probe probe in probes)
            {
                drawFieldLineDirection(probe, countArrows, toffset);
            }
        }

        public static void drawFieldLineDirection(Probe probe, int countArrows, float toffset)
        {
            List<Vector2> points = equallySpacedPointOnLine(probe.Path, countArrows, toffset);
            List<Vector2> pointToPoints = equallySpacedPointOnLine(probe.Path, countArrows, toffset+0.001f);

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector2 current = points[i];

                // skip if the current coordinate is 0,0 
                if (current.X == 0 && current.Y == 0) continue;

                Vector2 next = pointToPoints[i];

                // if the next position is at 0,0, set it to the position at which the probe was disabled
                next = next == Vector2.Zero ? probe.disabledPosition : next;

                // calculate angle
                float angle = (float)Math.Atan2(next.Y - current.Y, next.X - current.X);

                float triangleBase = 5;
                float triangleHeight = 10;

                Vector2 p1 = new Vector2(
                    (float)(current.X + triangleHeight * Math.Cos(angle)),
                    (float)(current.Y + triangleHeight * Math.Sin(angle))
                );

                // 1.5708rad = 90deg
                Vector2 p2 = new Vector2(
                    (float)(current.X + triangleBase * Math.Cos(angle - 1.5708)),
                    (float)(current.Y + triangleBase * Math.Sin(angle - 1.5708))
                );

                Vector2 p3 = new Vector2(
                    (float)(current.X + triangleBase * Math.Cos(angle + 1.5708)),
                    (float)(current.Y + triangleBase * Math.Sin(angle + 1.5708))
                );


                Raylib.DrawTriangle(p2, p3, p1, Raylib.WHITE);

                //// debug
                //if (i == 3)
                //{
                //    Raylib.DrawCircle((int)(current.X), (int)(current.Y), 2, Raylib.BLUE);
                //    Raylib.DrawCircle((int)(next.X), (int)(next.Y), 5, Raylib.GREEN);
                //    Raylib.DrawLine((int)(current.X), (int)(current.Y), (int)(next.X), (int)(next.Y), Raylib.YELLOW);
                //} else
                //{
                //    Raylib.DrawCircle((int)(current.X), (int)(current.Y), 2, Raylib.RED);
                //    Raylib.DrawCircle((int)(next.X), (int)(next.Y), 5, Raylib.GREEN);
                //    Raylib.DrawLine((int)(current.X), (int)(current.Y), (int)(next.X), (int)(next.Y), Raylib.RED);
                //}
            }
        }

        public static List<Vector2> equallySpacedPointOnLine(List<Vector2> line, int count, float toffset)
        {
            List<Vector2> points = new();

            float dist = (1f / count)*1.5f;
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
