using Connect.CanvasUtils.Drawables;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Builders
{
    internal class PolygonBuilder : IDrawableBuilder
    {
        public CanvasDrawable Build(List<Vector2i> points, Color color)
        {
            if (points == null || points.Count < 2)
            {
                throw new ArgumentException("At least 2 points are required.", nameof(points));
            }

            return new CanvasPolygon(points)
            {
                Color = color
            };
        }
    }
}
