using Connect.Utils;
using SFML.Graphics;
using SFML.System;
using System.Text;

namespace Connect.CanvasUtils.Drawables
{
    internal class CanvasLine : CanvasDrawable
    {
        private Line _line;

        public CanvasLine(List<Vector2i> points) : base(points)
        {
            _line = new Line();

            foreach (var p in points)
            {
                _line.AddPoint((Vector2f)p);
            }
        }

        protected override void SetColor(Color color)
        {
            _line.Color = color;
        }

        protected override void SetThickness(float thickness)
        {
            _line.Thickness = thickness;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder("line", points.Count * 2 + 4);
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
            target.Draw(_line, states);
        }
    }
}
