using Connect.CanvasUtils.Drawables;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Builders
{
    internal class PointsBuilder : IDrawableBuilder
    {
        public CanvasDrawable Build(List<Vector2i> points, Color color)
        {
            return new CanvasPoints(points)
            {
                Color = color,
                Thickness = 0.1f
            };
        }
    }
}
