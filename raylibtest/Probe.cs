using System.Numerics;
using Raylib_CsLo;

namespace raylibtest
{
    public class Probe
    {
        private Vector2 _Position;

        public Vector2 Position 
        { 
            get { return _Position;  } 
            set
            {
                Path.Add(value);
                _Position = value;
            }
        }
        public Color ParticleColor { get; set; }
        public float Charge { get; set; }
        public float Size { get; set; }
        public List<Vector2> Path { get; set; }
        public bool Disabled;

        public Vector2 disabledPosition;

        public Probe(float charge, Vector2 position, float size = 2)
        {
                
            Path = new List<Vector2>();
            Position = position;
            Size = size;
            Charge = charge; 
            ParticleColor = Raylib.WHITE;
            Disabled = false;
            disabledPosition = Vector2.Zero;
        }


        public double IntensityAtPoint(Probe probeCharge)
        {
            return (Constants.k * Charge) / RayMath.Vector2DistanceSqr(Position, probeCharge.Position);
        }

        public void Draw()
        {
            Raylib.DrawCircle((int)Position.X, (int)Position.Y, Size, ParticleColor);
        }

    }
}
