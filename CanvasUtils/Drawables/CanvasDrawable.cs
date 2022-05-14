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

        protected abstract void SetThickness(float thickness);

        protected abstract void SetColor(Color color);

        public abstract new string ToString();

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
