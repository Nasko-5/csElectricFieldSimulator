using System.Numerics;
using Raylib_CsLo;

namespace raylibtest
{
    public class Particle
    {
        public Color ParticleColor { get; set; }
        public float Size { get; set; }
        public Vector2 Position { get; set; }

        public bool isBeingDragged { get; set; }
        public bool isChargeEdited { get; set; }

        private float _Charge;

        public float Charge
        {
            get { return _Charge; }
            set 
            {
                if (value > 0)
                {
                    ParticleColor = Raylib.RED;
                }
                else if (value == 0)
                {
                    ParticleColor = Raylib.DARKGRAY;
                }
                else
                {
                    ParticleColor = Raylib.BLUE;
                }
                _Charge = value; 
            }
        }


        public Particle(float charge, Vector2 position, float size = 10)
        {
            Charge = charge;
            Position = position;
            Size = size;
            isBeingDragged = false; 
        }

        public float IntensityAtPoint(Probe probeCharge)
        {
            return (float)(Constants.k * Charge) / RayMath.Vector2DistanceSqr(Position, probeCharge.Position);
        }

        public bool isOver(Vector2 mousePos)
        {
            return RayMath.Vector2Distance(mousePos, Position) <= Size;
        }

        public void Draw(Vector2 offset)
        {
            Raylib.DrawCircle((int)Position.X + (int)offset.X, (int)Position.Y + (int)offset.Y, Size+10, Raylib.BLACK);
            Raylib.DrawCircle((int)Position.X + (int)offset.X, (int)Position.Y + (int)offset.Y, Size, ParticleColor);
            
            if(isChargeEdited)
            {
                string text = $"{Charge:f2}";
                Raylib.DrawRectangle(((int)Position.X + (int)offset.X-(int)Size/2) - 10, (int)Position.Y + (int)offset.Y - (int)Size / 2, (int)Size*2, 12, Raylib.BLACK);
                Raylib.DrawText(text, ((Position.X + offset.X) - Size / 2 ) - 10, (Position.Y + offset.Y) - Size / 2, 12, Raylib.WHITE);
            }
        }

    }
}
