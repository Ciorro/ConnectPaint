﻿using SFML.Graphics;
using SFML.System;
using System.Text;

namespace Connect.CanvasUtils.Drawables
{
    internal class CanvasPoints : CanvasDrawable
    {
        private CircleShape _dot;

        public CanvasPoints(List<Vector2i> points) : base(points)
        {
            _dot = new CircleShape();
        }

        protected override void SetColor(Color color)
        {
            _dot.FillColor = color;
        }

        protected override void SetThickness(float thickness)
        {
            _dot.Radius = thickness;
            _dot.Origin = new Vector2f(thickness, thickness);
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder("points", points.Count * 2 + 4);
            strBuilder.AppendFormat(" {0} {1} {2} {3}", color.R, color.G, color.B, color.A);

            foreach (var p in points)
            {
                strBuilder.AppendFormat(" {0}", p.X);
                strBuilder.AppendFormat(" {0}", p.Y);
            }

            return strBuilder.ToString();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var p in points)
            {
                _dot.Position = (Vector2f)p;
                target.Draw(_dot, states);
            }
        }
    }
}
