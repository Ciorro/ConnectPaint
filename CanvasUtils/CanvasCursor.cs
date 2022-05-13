using Connect.Utils;
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
        private VertexArray _axes;

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

            _axes = new VertexArray(PrimitiveType.Lines, 4);
            for (int i = 0; i < 4; i++)
            {
                _axes.Append(new Vertex());
            }
        }

        public bool ShowGrid { get; set; } = true;

        public bool ShowAxes { get; set; } = true;

        public Vector2i SelectedPoint
        {
            get => (Vector2i)_cursor.Position;
        }

        public void Update()
        {
            var mPos = Mouse.GetPosition(_canvas.Form.Window);
            var mPosRelativeToWidget = new Vector2f()
            {
                X = mPos.X - _canvas.GlobalPosition.X,
                Y = mPos.Y - _canvas.GlobalPosition.Y
            };

            var mPosOnCanvas = _canvas.View.ScreenToCanvas(mPosRelativeToWidget);
            mPosOnCanvas.X = (int)mPosOnCanvas.X;
            mPosOnCanvas.Y = (int)mPosOnCanvas.Y;

            if (Math.Sign(mPosOnCanvas.X) < 0)
            {
                mPosOnCanvas.X -= 1;
            }

            if (Math.Sign(mPosOnCanvas.Y) < 0)
            {
                mPosOnCanvas.Y -= 1;
            }

            _cursor.Position = mPosOnCanvas;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_cursor, new RenderStates(_canvas.View.Transform));

            if (ShowAxes)
            {
                DrawAxes(target, new RenderStates(_canvas.View.Transform));
            }

            if (ShowGrid)
            {
                DrawGrid(target, new RenderStates(_canvas.View.Transform));
            }
        }

        private void DrawGrid(RenderTarget target, RenderStates states)
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
                    _gridPoint.FillColor = new Color(128, 128, 128, (byte)(64 * pointAlpha));

                    target.Draw(_gridPoint, states);
                }
            }
        }

        private void DrawAxes(RenderTarget target, RenderStates states)
        {
            _axes[0] = new Vertex(new Vector2f(_cursor.Position.X, _cursor.Position.Y - 9999), new Color(128, 128, 128, 64));
            _axes[1] = new Vertex(new Vector2f(_cursor.Position.X, _cursor.Position.Y + 9999), new Color(128, 128, 128, 64));
            _axes[2] = new Vertex(new Vector2f(_cursor.Position.X - 9999, _cursor.Position.Y), new Color(128, 128, 128, 64));
            _axes[3] = new Vertex(new Vector2f(_cursor.Position.X + 9999, _cursor.Position.Y), new Color(128, 128, 128, 64));

            target.Draw(_axes, states);
        }
    }
}
