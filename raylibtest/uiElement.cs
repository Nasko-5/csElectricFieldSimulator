using System.Numerics;
using Raylib_CsLo;

namespace csElectricFieldSimulator
{
    public class uiElement
    {
        public int ControlGroup;
        public Rectangle Rect;
        public Rectangle Scaled;

        public uiElement(Rectangle rect)
        {
            ControlGroup = 0;
            Rect = rect;
            Scaled = rect;
        }

        public uiElement(Rectangle rect, int controlGroup)
        {
            ControlGroup = controlGroup;
            Rect = rect;
            Scaled = rect;
        }

        public void scaleRect(float scaleFactor)
        {
            Scaled = new Rectangle(
                    Rect.X * scaleFactor,
                    Rect.Y * scaleFactor,
                    Rect.width * scaleFactor,
                    Rect.height * scaleFactor
                );
        }

        public bool isMouseInRectanlge(Vector2 mousePos)
        {
            return Scaled.x < mousePos.X &&
                   Scaled.x + Scaled.width > mousePos.X &&
                   Scaled.y < mousePos.Y &&
                   Scaled.y + Scaled.height > mousePos.Y;
        }
    }
}
