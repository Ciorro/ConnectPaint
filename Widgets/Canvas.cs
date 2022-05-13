using Connect.CanvasUtils;
using Connect.CanvasUtils.Builders;
using Connect.CanvasUtils.Drawables;
using HlyssUI.Components;
using HlyssUI.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Connect.Widgets
{
    internal class Canvas : Component
    {
        public const int Spacing = 20;

        private Observer<Vector2i> _sizeObserver;
        private List<CanvasDrawable> _drawables;

        private RenderTexture _canvas;
        private RectangleShape _rect;
        private RenderStates _states;

        private Vector2i _prevMPos;

        public Canvas()
        {
            Width = "100%";
            Height = "100%";

            _sizeObserver = new Observer<Vector2i>(Size);
            _drawables = new List<CanvasDrawable>();

            View = new CanvasView(this);
            Cursor = new CanvasCursor(this);

            Tool = new CanvasTool(this);
            _states = RenderStates.Default;

            CreateCanvas();
        }

        public CanvasTool Tool { get; }

        public CanvasView View { get; }

        public CanvasCursor Cursor { get; }

        public Color Color
        {
            get => Tool.ToolColor;
            set => Tool.ToolColor = value;
        }

        public override void OnInitialized()
        {
            base.OnInitialized();
            Form.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }

        public void Clear()
        {
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

            Cursor.Update();
            Tool.CursorPoint = Cursor.SelectedPoint;
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (Tool != null)
            {
                if (Hovered && e.Button == Mouse.Button.Left)
                {
                    Tool.PushPoint(Cursor.SelectedPoint);
                }
                else if (e.Button == Mouse.Button.Right)
                {
                    Tool.Finish();
                }
            }
        }

        public override void OnMouseMovedAnywhere(Vector2i location)
        {
            base.OnMouseMovedAnywhere(location);

            if (Hovered && Mouse.IsButtonPressed(Mouse.Button.Middle))
            {
                Vector2f pos = new Vector2f()
                {
                    X = _prevMPos.X - location.X,
                    Y = _prevMPos.Y - location.Y
                };

                View.Center += pos / (View.Zoom * Spacing);
            }

            _prevMPos = location;
        }

        public override void Draw(RenderTarget target)
        {
            base.Draw(target);

            _states.Transform = View.Transform;
            _canvas.Clear(Color.Transparent);

            foreach (var drawable in _drawables)
            {
                _canvas.Draw(drawable, _states);
            }

            _canvas.Draw(Cursor);

            _canvas.Draw(Tool, _states);

            _canvas.Display();
            target.Draw(_rect);
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

        //TODO: Extract cursor tool to a separate class (DrawGrid, DrawAxes, DrawCursor);
        //TODO: Extract drawable stack to a history class with undo and redo functionality;
    }
}
