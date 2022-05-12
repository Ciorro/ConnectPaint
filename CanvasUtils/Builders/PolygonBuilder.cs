using Connect.CanvasUtils.Drawables;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.CanvasUtils.Builders
{
    internal class PolygonBuilder : IDrawableBuilder
    {
        public CanvasDrawable Build(List<Vector2i> points, Color color)
        {
            return new CanvasPolygon(points)
            {
                Color = color
            };
        }
    }
}
