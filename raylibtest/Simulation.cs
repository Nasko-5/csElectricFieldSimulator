using System.Numerics;
using Raylib_CsLo;

namespace raylibtest
{
    public static class Simulation
    {
        public static Vector2 getEFieldDirection(List<Particle> list, Probe probe)
        {
            Vector2 E = new Vector2(0, 0);

            foreach (Particle particle in list)
            {
                // calculate intesnity of particle's electric field at probe's position according to Coulomb's law
                float intensity = (float)particle.IntensityAtPoint(probe);

                // Get the direction of that influence and add sum it with the rest            
                E += RayMath.Vector2Normalize(probe.Position - particle.Position) * intensity;

            }

            return E;
        }

        public static List<Probe> generateProbes(List<Particle> particles, int radius, int count, int size = 1)
        {
            List<Probe> probes = new();
            foreach (Particle particle in particles)
            {
                if (particle.Charge <= 0) continue;
                for (int i = 0; i < count; i++)
                {
                    float angle = (float)(2 * Math.PI * i / count);
                    float x = (float)(particle.Position.X + radius * Math.Cos(angle));
                    float y = (float)(particle.Position.Y + radius * Math.Sin(angle));
                    Probe p = new Probe(Constants.e, new Vector2(x, y), size);
                    p.ParticleColor = Raylib.RED;
                    probes.Add(p);
                }
            }
            return probes;
        }

        public static Particle nearestParticle(List<Particle> particles, Probe probe)
        {
            Particle closest = null;
            float minDistance = float.MaxValue;

            foreach (var particle in particles) //Where(p => p.Charge < 0)
            {
                float distance = (float)RayMath.Vector2Distance(particle.Position, probe.Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = particle;
                }
            }

            return closest;
        }

        public static bool outsideDomain(Vector2 newPos, int screenWidth, int screenHeight)
        {

            // check if probe is outside of the window
            return !(newPos.X < -1000 || newPos.Y < -1000 || newPos.X > screenWidth + 1000 || newPos.Y > screenHeight + 1000);
        }
        public static bool tooCloseToParticle(List<Particle> list, Vector2 newPos)
        {
            // Check if the probe is close to a negatively charged particle
            foreach (Particle particle in list.Where(a => a.Charge < 0))
            {
                if (RayMath.Vector2Distance(newPos, particle.Position) < 25)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
