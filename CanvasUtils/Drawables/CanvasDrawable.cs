using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Drawables
{
    internal abstract class CanvasDrawable : Drawable
    {
        protected float thickness;
        protected Color color;
        protected List<Vector2i> points;

        public CanvasDrawable(List<Vector2i> points)
        {
            this.points = points;
        }

        public float Thickness
        {
            get => thickness;
            set
            {
                if(thickness != value)
                {
                    thickness = value;
                    SetThickness(value);
                }
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    SetColor(value);
                }
            }
        }

        public IntRect GetBounds()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (var p in points)
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);
                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            }

            return new IntRect(minX, minY, maxX - minX, maxY - minY);
        }

        protected abstract void SetThickness(float thickness);

        protected abstract void SetColor(Color color);

        public abstract new string ToString();

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
