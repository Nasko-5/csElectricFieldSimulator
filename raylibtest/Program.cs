// See https://aka.ms/new-console-template for more information
using Raylib_CsLo;
using raylibtest;
using System.Numerics;
using System.Diagnostics;
using csElectricFieldSimulator;

const int screenWidth = 1200;
const int screenHeight = 800;

Random random = new Random();

Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
Raylib.InitWindow(screenWidth, screenHeight, "Electric Field Simulator");
Raylib.SetTargetFPS(60);


Console.WriteLine(new string('*', 40));

List<Probe> SimParallel(List<Particle> particles)
{
    // generate our probes
    List<Probe> probes = Simulation.generateProbes(particles, 25, 50);

    Vector4 bbox = Visualization.getParticleBoundingBox(particles);

    int LoDBoxExtend = 45;

    Vector4 LoDBox = new Vector4(
            (float)(bbox.X - LoDBoxExtend),
            (float)(bbox.Y - LoDBoxExtend),
            (float)(bbox.Z + LoDBoxExtend * 2),
            (float)(bbox.W + LoDBoxExtend * 2)
        );

    int probeCount = probes.Count;

    // just statistics stuff, not really needed but they're nice to have
    //int totalpoints = 0;
    //var timer = new Stopwatch();
    //timer.Start();

    int t = 0;
    while (probeCount != 0 && t < 200)
    {
        t++;
        Parallel.ForEach(probes, probe =>
        {
                if (probe.Disabled) return; // skip "disabled" particles
                                            //Console.WriteLine($"para{random.Next(10)} - {probeCount}");

                Vector2 E = Simulation.getEFieldDirection(particles, probe);

                // get the distance to the closes charge to our probe, this now also includes positive charges
                // * Checking each particle probecount number of times probably makes this a little slow...
                // * there was a try catch here before, i am unsure why it was here but if problems arise it should
                //   probably be added back

                Particle particle = Simulation.nearestParticle(particles, probe);    

                // the dist value here is also used to cut the points off cleanly
                float dist = RayMath.Vector2Distance(probe.Position, particle.Position) / 50;

                Vector2 probePos = probe.Position;

                // Checks if the probe is inside of our LoD box
                // if it is, it gets more detail, it it isn't it gets less detail..
                if (probePos.X > LoDBox.X &&
                    probePos.X < LoDBox.X + LoDBox.Z &&
                    probePos.Y > LoDBox.Y &&
                    probePos.Y < LoDBox.Y + LoDBox.W)
                {
                    E = RayMath.Vector2Normalize(E) * (4.5f + dist); // sprase-ify points
                }
                else
                {
                    E = RayMath.Vector2Normalize(E) * (10 + dist);
                }

                // Point culling based on 
                bool outsideDomain = Simulation.outsideDomain(probe.Position + E, screenWidth, screenHeight);
                bool negativeProximity = Simulation.tooCloseToParticle(particles, probe.Position + E);

                if (!outsideDomain || !negativeProximity)
                {
                    if (outsideDomain)
                    {
                        // This code here just evens out the lines near negative chargers
                        // it finds the angle between the charge and the probe, and compares the probe's
                        // last position with one a set radius away. If the point is farther away, a new point is
                        // created at the given radius, if it's closer than the radius, the last point is set to
                        // the given radius.
                        // 
                        float angle = (float)Math.Atan2(probe.Position.Y - particle.Position.Y, probe.Position.X - particle.Position.X);
                        float radius = 25;

                        Vector2 cutoffPos = new Vector2(
                                (float)(particle.Position.X + radius * Math.Cos(angle)),
                                (float)(particle.Position.Y + radius * Math.Sin(angle))
                            );

                        if (dist > radius)
                        {
                            probe.Position = cutoffPos;
                        }
                        else if (dist < radius)
                        {
                            probe.Path[probe.Path.Count - 1] = cutoffPos;
                        }
                    }

                    probe.Disabled = true;
                    probeCount--;
                    return;
                }

                // Add vector to the probe's position
                probe.Position += E;

                //totalpoints++;

        });
    }

    //timer.Stop();

    //TimeSpan timeTaken = timer.Elapsed;
    //string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff") + " | " + "Total points: " + totalpoints; ;
    //Console.WriteLine(foo);

    return probes;
}
List<Particle> generateRandomParticles(int particleCount)
{
    List<Particle> particles = new List<Particle>();
    for (int i = 0; i < particleCount; i++)
    {
        // Flip our charge randomly
        float q = Constants.e;
        if (random.Next(100) > 50) q *= -1;

        // Create new particle to add to list
        Particle particle = new Particle(q, new Vector2(random.Next(2000+screenWidth) - 1000, random.Next(2000+screenHeight)-1000), 15    );

        particles.Add(particle);
    }
    return particles;
}

List<Particle> particles = new List<Particle>();

List<Probe> probes = new();

Gui g = new();

Particle draggedParticle = null;
Particle chargeEditParticle = null;

Vector2 mousePos;
Vector2 offsetMousePos;

Vector2 offset = new(0);
Vector2? cameraDragInitialPos = null;

bool changed = false;

while (!Raylib.WindowShouldClose())
{
    mousePos = Raylib.GetMousePosition();
    offsetMousePos = mousePos - offset;

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Raylib.BLACK);

    if (!g.isMouseOnControls(mousePos))
    {
        
        // right mouse button
        if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_RIGHT_BUTTON))
        {
            if (draggedParticle == null && chargeEditParticle == null)
            {
                int pc = particles.Count;
                for (int i = 0; i < pc; i++)
                {
                    if (particles[i].isOver(offsetMousePos))
                    {
                        particles.Remove(particles[i]);
                        break;
                    }
                }
                int npc = particles.Count;

                if (pc == npc)
                {
                    particles.Add(new Particle(-Constants.e, offsetMousePos, 15));
                }
                changed = true;
            }
        }

        // Left mouse button,
        if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
        {
            foreach (Particle particle in particles)
            {
                if (particle.isOver(offsetMousePos))
                {
                    particle.isBeingDragged = true;
                    draggedParticle = particle;
                }
            }

            if (draggedParticle == null)
            {
                particles.Add(new Particle(Constants.e, offsetMousePos, 15));
            }
            changed = true;
        } else if (Raylib.IsMouseButtonDown(Raylib.MOUSE_LEFT_BUTTON))
        {
            if (draggedParticle != null)
            {
                draggedParticle.Position = offsetMousePos;
                changed = true;
            }
        } 
        else if (Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON))
        {
            if (draggedParticle != null)
            {
                draggedParticle.isBeingDragged = false;
                draggedParticle = null;
                changed = true;
            }
        }

        // Middle mouse button
        if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_MIDDLE_BUTTON))
        {
            // Camera dragging
            if (cameraDragInitialPos == null)
            {
                cameraDragInitialPos = mousePos;
            }

            // Charge editing
    
                foreach (Particle particle in particles)
                {
                    if (particle.isOver(offsetMousePos))
                    {
                        particle.isChargeEdited = true;
                        chargeEditParticle = particle;
                        cameraDragInitialPos = mousePos;
                    }
                }

        } 
        if (Raylib.IsMouseButtonDown(Raylib.MOUSE_MIDDLE_BUTTON) )
        {
            Vector2 changeBy = ((Vector2)(cameraDragInitialPos - mousePos));

            if (chargeEditParticle != null && cameraDragInitialPos != null)
            {
                if (Raylib.IsMouseButtonDown(Raylib.MOUSE_RIGHT_BUTTON))
                {
                    chargeEditParticle.Charge = 0;
                }
                else
                {
                    chargeEditParticle.Charge += changeBy.Y / 5000;
                }
                changed = true;
            }

            if (cameraDragInitialPos != null && chargeEditParticle == null)
                offset += changeBy / 10;
        } 
        if (Raylib.IsMouseButtonReleased(Raylib.MOUSE_MIDDLE_BUTTON))
        {
            if (chargeEditParticle != null)
            {
                chargeEditParticle.isChargeEdited = false;
                chargeEditParticle = null;
            
            }

            if (cameraDragInitialPos != null)
            {
                cameraDragInitialPos = null;
            }
        }

        if (Raylib.IsMouseButtonDown(Raylib.MOUSE_MIDDLE_BUTTON) && Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            particles.Clear();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
        {
            particles.AddRange(generateRandomParticles(15));
            changed = true;
        }
    }
   



    Raylib.DrawRectangleLinesEx(new Rectangle(-1000+offset.X, -1000+offset.Y, screenWidth + 2000, screenHeight + 2000), 2, Raylib.YELLOW);

    if (particles.Count != 0)
    {
        try { if (changed) { probes = SimParallel(particles); changed = false; } }
        catch { Console.WriteLine("lol whoops"); }
        Visualization.DrawFieldLines(probes, offset);
        Visualization.DrawParticles(particles, offset);
    }

    g.DrawPollGui();

    Raylib.EndDrawing();
}