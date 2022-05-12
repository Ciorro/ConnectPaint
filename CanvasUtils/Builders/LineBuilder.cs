using Connect.CanvasUtils.Drawables;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Builders
{
    internal class LineBuilder : IDrawableBuilder
    {
        public CanvasDrawable Build(List<Vector2i> points, Color color)
        {
            return new CanvasLine(points)
            {
                Color = color,
                Thickness = 2 / 20f
            };
        }
    }
}
