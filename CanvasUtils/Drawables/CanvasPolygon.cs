using Connect.Utils;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Drawables
{
    internal class CanvasPolygon : CanvasDrawable
    {
        private Polygon _poly;

        public CanvasPolygon(List<Vector2i> points) : base(points)
        {
            _poly = new Polygon();

            foreach (var p in points)
            {
                _poly.AddPoint((Vector2f)p);
            }
        }

        protected override void SetColor(Color color)
        {
            _poly.Color = color;
        }

        protected override void SetThickness(float thickness) { }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_poly, states);
        }
    }
}
