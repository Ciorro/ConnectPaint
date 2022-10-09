using Connect.CanvasUtils;
using Connect.CanvasUtils.Builders;
using Connect.CanvasUtils.Drawables;
using HlyssUI.Components;
using HlyssUI.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Text;

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
            Tool.SetDrawable(new LineBuilder());

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

        public bool HasDrawings
        {
            get => _drawables.Count > 0;
        }

        public override void OnInitialized()
        {
            base.OnInitialized();
            Form.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }

        public void Add(CanvasDrawable item)
            => _drawables.Add(item);

        public void Insert(int index, CanvasDrawable item)
            => _drawables.Insert(index, item);

        public void Remove(CanvasDrawable item)
            => _drawables.Remove(item);

        public void RemoveAt(int index)
            => _drawables.RemoveAt(index);

        public int IndexOf(CanvasDrawable item)
            => _drawables.IndexOf(item);

        public void Clear()
        {
            _drawables.Clear();
            Tool.ClearActionStack();
            View.Center = new Vector2f();
        }

        public void Import(string file)
        {
            string[] lines = file.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                string[] args = line.Split(' ');

                byte r = byte.Parse(args[1]);
                byte g = byte.Parse(args[2]);
                byte b = byte.Parse(args[3]);
                byte a = byte.Parse(args[4]);

                List<Vector2i> points = new();

                for (int i = 5; i < args.Length; i += 2)
                {
                    int p1 = int.Parse(args[i + 0]);
                    int p2 = int.Parse(args[i + 1]);
                    points.Add(new Vector2i(p1, p2));
                }

                CanvasDrawable drawable = args[0] switch
                {
                    "line" => new CanvasLine(points),
                    "polygon" => new CanvasPolygon(points),
                    "points" => new CanvasPoints(points),
                    _ => throw new InvalidDataException($"Invalid drawable type: {args[0]}")
                };

                drawable.Color = new Color(r, g, b, a);
                drawable.Thickness = 0.1f;
                _drawables.Add(drawable);
            }
        }

        public string Export()
        {
            var strBuilder = new StringBuilder(_drawables.Count);

            foreach (var d in _drawables)
            {
                strBuilder.Append($"{d.ToString()},");
            }

            return strBuilder.ToString();
        }

        public RenderTexture RenderToTexture(int pixelsWidth)
        {
            var bounds = GetBounds(_drawables);
            float pointSize = (float)pixelsWidth / bounds.Width;
            float aspectRatio = (float)bounds.Height / bounds.Width;
            uint textureWidth = (uint)pixelsWidth;
            uint textureHeight = (uint)(pixelsWidth * aspectRatio);

            var texture = new RenderTexture(textureWidth, textureHeight);
            texture.Clear(Color.Transparent);

            Transform transform = Transform.Identity;
            transform.Scale(pointSize, pointSize);
            transform.Translate(-bounds.Left, -bounds.Top);
            var states = new RenderStates(transform);

            foreach (var d in _drawables)
            {
                texture.Draw(d, states);
            }

            texture.Display();
            return texture;
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

        private IntRect GetBounds(List<CanvasDrawable> drawables)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (var d in drawables)
            {
                var bounds = d.GetBounds();
                int l = bounds.Left;
                int r = bounds.Left + bounds.Width;
                int t = bounds.Top;
                int b = bounds.Top + bounds.Height;

                minX = Math.Min(minX, l);
                minY = Math.Min(minY, t);
                maxX = Math.Max(maxX, r);
                maxY = Math.Max(maxY, b);
            }

            return new IntRect(minX, minY, maxX - minX, maxY - minY);
        }

        //TODO: Extract drawable stack to a history class with undo and redo functionality;
    }
}
