﻿using Connect.CanvasUtils.Builders;
using Connect.Widgets;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils
{
    internal class CanvasTool : Drawable
    {
        private Canvas _canvas;
        private IDrawableBuilder _builder;
        private VertexArray _preview;

        protected Stack<Vector2i> Points;

        public CanvasTool(Canvas canvas)
        {
            _canvas = canvas;
            _preview = new VertexArray(PrimitiveType.LineStrip);
            Points = new Stack<Vector2i>();
        }

        public Vector2i CursorPoint { get; set; }

        public Color ToolColor { get; set; }

        public void SetDrawable(IDrawableBuilder builder)
        {
            if (Points.Count > 0)
            {
                Finish();
            }

            _builder = builder;
        }

        public virtual void PushPoint(Vector2i point)
        {
            if (_builder == null)
            {
                return;
            }

            if (Points.Count > 0 && Points.Peek() == point)
            {
                Points.Pop();
                BuildPreview();
            }
            else
            {
                Points.Push(point);
                BuildPreview();
            }
        }

        public void Finish()
        {
            if (_builder != null)
            {
                var drawable = _builder.Build(Points.ToList(), ToolColor);
                _canvas.PushDrawable(drawable);

                Points.Clear();
                BuildPreview();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (Points.Count >= 1)
            {
                _preview[(uint)Points.Count] = new Vertex()
                {
                    Position = (Vector2f)CursorPoint,
                    Color = ToolColor
                };
            }

            target.Draw(_preview, states);
        }

        private void BuildPreview()
        {
            _preview.Resize((uint)Points.Count + 1);
            _preview.Clear();

            foreach (var p in Points.Reverse())
            {
                _preview.Append(new Vertex((Vector2f)p, ToolColor));
            }

            _preview.Append(new Vertex());
        }
    }
}
