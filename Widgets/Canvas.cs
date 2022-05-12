using Connect.CanvasUtils;
using Connect.CanvasUtils.Builders;
using Connect.CanvasUtils.Drawables;
using Connect.Utils;
using HlyssUI.Components;
using HlyssUI.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Connect.Widgets
{
    internal class Canvas : Component
    {
        private Observer<Vector2i> _sizeObserver;
        private RenderTexture _canvas;
        private RectangleShape _rect;
        private Vector2i _prevMPos;
        private CircleShape _cursor;
        private CircleShape _gridPoint;
        private VertexArray _cursorAxes;
        private Vector2f _viewPosition;
        private CanvasTool _tool;
        private RenderStates _states;
        private List<CanvasDrawable> _drawables;
        private int _spacing = 20;

        public Canvas()
        {
            Width = "100%";
            Height = "100%";

            _sizeObserver = new Observer<Vector2i>(Size);

            _cursor = new CircleShape(4)
            {
                FillColor = new Color(0, 0, 0, 128),
                Origin = new Vector2f(4, 4)
            };

            _gridPoint = new CircleShape(2, 8)
            {
                Origin = new Vector2f(2, 2)
            };

            _cursorAxes = new VertexArray(PrimitiveType.Lines, 4);

            for (int i = 0; i < 4; i++)
            {
                _cursorAxes.Append(new Vertex());
            }

            _drawables = new List<CanvasDrawable>();

            _tool = new CanvasTool(this);
            _states = RenderStates.Default;

            CreateCanvas();
        }

        public int Spacing
        {
            get => _spacing;
            set
            {
                float scale = (float)value / _spacing;
                _spacing = value;
            }
        }

        public Color Color
        {
            get => _tool.ToolColor;
            set => _tool.ToolColor = value;
        }

        public Vector2i CursorPosition
        {
            get => GetCanvasPointFromCursor();
        }

        public override void OnInitialized()
        {
            base.OnInitialized();
            Form.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }

        public void Clear()
        {
        }

        public void SetDrawable(IDrawableBuilder builder)
        {
            _tool.SetDrawable(builder);
        }

        public void PushDrawable(CanvasDrawable drawable)
        {
            _drawables.Add(drawable);
        }

        public override void Update()
        {
            base.Update();

            if (_sizeObserver.Changed(Size))
            {
                CreateCanvas();
            }
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (_tool != null)
            {
                if (Hovered && e.Button == Mouse.Button.Left)
                {
                    _tool.PushPoint(GetCanvasPointFromCursor());
                }
                else if (e.Button == Mouse.Button.Right)
                {
                    _tool.Finish();
                }
            }
        }

        public override void OnMouseMovedAnywhere(Vector2i location)
        {
            base.OnMouseMovedAnywhere(location);

            SetCursorPosition(location - GlobalPosition);
            _tool.CursorPoint = GetCanvasPointFromCursor();

            if (Hovered && Mouse.IsButtonPressed(Mouse.Button.Middle))
            {
                Vector2f pos = new Vector2f()
                {
                    X = _prevMPos.X - location.X,
                    Y = _prevMPos.Y - location.Y
                };

                _viewPosition += pos;
            }

            _prevMPos = location;
        }


        public override void Draw(RenderTarget target)
        {
            base.Draw(target);

            _states.Transform = GetTransform();
            _canvas.Clear(Color.Transparent);

            foreach (var drawable in _drawables)
            {
                _canvas.Draw(drawable, _states);
            }

            DrawGrid();
            DrawAxes();
            _canvas.Draw(_tool, _states);
            _canvas.Draw(_cursor);

            _canvas.Display();
            target.Draw(_rect);
        }

        private Transform GetTransform()
        {
            var transform = Transform.Identity;
            transform.Translate(-_viewPosition);
            transform.Scale(Spacing, Spacing);
            return transform;
        }

        private void CreateCanvas()
        {
            _canvas = new RenderTexture((uint)Size.X, (uint)Size.Y)
            {
                Smooth = true
            };

            _rect = new RectangleShape()
            {
                Size = (Vector2f)Size,
                Position = (Vector2f)GlobalPosition,
                Texture = _canvas.Texture
            };
        }

        private void DrawGrid()
        {
            for (int x = -10; x < 10; x++)
            {
                for (int y = -10; y < 10; y++)
                {
                    _gridPoint.Position = new Vector2f()
                    {
                        X = _cursor.Position.X + x * Spacing,
                        Y = _cursor.Position.Y + y * Spacing
                    };

                    float pointDist = _gridPoint.Position.DistanceSquared(_cursor.Position);
                    float pointAlpha = Math.Clamp(1 - pointDist / (Spacing * Spacing * 100), 0, 1);
                    _gridPoint.FillColor = new Color(255, 255, 255, (byte)(64 * pointAlpha));

                    _canvas.Draw(_gridPoint);
                }
            }
        }

        private void DrawAxes()
        {
            _cursorAxes[0] = new Vertex(new Vector2f(_cursor.Position.X, -9999), new Color(0, 0, 0, 64));
            _cursorAxes[1] = new Vertex(new Vector2f(_cursor.Position.X,  9999), new Color(0, 0, 0, 64));
            _cursorAxes[2] = new Vertex(new Vector2f(-9999, _cursor.Position.Y), new Color(0, 0, 0, 64));
            _cursorAxes[3] = new Vertex(new Vector2f(9999,  _cursor.Position.Y), new Color(0, 0, 0, 64));

            _canvas.Draw(_cursorAxes);
        }

        private void SetCursorPosition(Vector2i location)
        {
            //Create cursor position with convenience correction
            Vector2f mPosOnCanvas = new Vector2f()
            {
                X = location.X + Spacing / 2 + (_viewPosition.X % Spacing),
                Y = location.Y + Spacing / 3 + (_viewPosition.Y % Spacing)
            };

            //Round mouse position to spacing
            mPosOnCanvas.X = (int)(mPosOnCanvas.X / Spacing) * Spacing - (_viewPosition.X % Spacing);
            mPosOnCanvas.Y = (int)(mPosOnCanvas.Y / Spacing) * Spacing - (_viewPosition.Y % Spacing);

            //Set cursor position
            _cursor.Position = mPosOnCanvas;
        }

        private Vector2i GetCanvasPointFromCursor()
        {
            return (Vector2i)((_cursor.Position + _viewPosition) / Spacing);
        }

        //TODO: Extract cursor tool to a separate class (DrawGrid, DrawAxes, DrawCursor);
        //TODO: Extract drawable stack to a history class with undo and redo functionality;
    }
}
