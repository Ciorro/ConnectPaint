using Connect.CanvasUtils.Drawables;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils.Builders
{
    internal interface IDrawableBuilder
    {
        public CanvasDrawable Build(List<Vector2i> points, Color color);
    }
}
