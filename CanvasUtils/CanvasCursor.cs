﻿using Connect.Utils;
using Connect.Widgets;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Connect.CanvasUtils
{
    internal class CanvasCursor : Drawable
    {
        private Canvas _canvas;

        private CircleShape _cursor;
        private CircleShape _gridPoint;

        public CanvasCursor(Canvas canvas)
        {
            _canvas = canvas;

            _cursor = new CircleShape(0.2f)
            {
                FillColor = new Color(0, 0, 0, 128),
                Origin = new Vector2f(0.2f, 0.2f)
            };

            _gridPoint = new CircleShape(0.1f, 8)
            {
                Origin = new Vector2f(0.1f, 0.1f)
            };
        }

        public bool DrawGrid { get; set; } = true;

        public bool DrawAxes { get; set; } = true;

        public Vector2i SelectedPoint
        {
            get => (Vector2i)_cursor.Position;
        }

        public void Update()
        {
            var mPos = Mouse.GetPosition(_canvas.Form.Window);
            var mPosRelativeToWidget = new Vector2f()
            {
                X = mPos.X - _canvas.GlobalPosition.X - Canvas.Spacing / 2f,
                Y = mPos.Y - _canvas.GlobalPosition.Y - Canvas.Spacing / 3f * 2
            };

            var mPosOnCanvas = _canvas.View.ScreenToCanvas(mPosRelativeToWidget);
            mPosOnCanvas.X = (int)mPosOnCanvas.X;
            mPosOnCanvas.Y = (int)mPosOnCanvas.Y;

            _cursor.Position = mPosOnCanvas;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_cursor, new RenderStates(_canvas.View.Transform));

            if (DrawGrid)
            {
                DrawDots(target, new RenderStates(_canvas.View.Transform));
            }
        }

        private void DrawDots(RenderTarget target, RenderStates states)
        {
            for (int x = -10; x < 10; x++)
            {
                for (int y = -10; y < 10; y++)
                {
                    _gridPoint.Position = new Vector2f()
                    {
                        X = _cursor.Position.X + x,
                        Y = _cursor.Position.Y + y
                    };

                    float pointDist = _gridPoint.Position.DistanceSquared(_cursor.Position);
                    float pointAlpha = Math.Clamp(1 - pointDist / (100), 0, 1);
                    _gridPoint.FillColor = new Color(255, 255, 255, (byte)(64 * pointAlpha));

                    target.Draw(_gridPoint, states);
                }
            }
        }
    }
}
