using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Drawables
{
    internal class CanvasPoints : CanvasDrawable
    {
        private List<CircleShape> _points;

        public CanvasPoints(List<Vector2i> points) : base(points)
        {
            _points = new List<CircleShape>(points.Count);

            foreach (var p in points)
            {
                _points.Add(new CircleShape());
            }
        }

        protected override void SetColor(Color color)
        {
            foreach (var p in _points)
            {
                p.FillColor = color;
            }
        }

        protected override void SetThickness(float thickness)
        {
            foreach (var p in _points)
            {
                p.Radius = thickness;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var p in _points)
            {
                target.Draw(p, states);
            }
        }
    }
}
