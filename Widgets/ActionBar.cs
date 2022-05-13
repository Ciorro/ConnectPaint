using HlyssUI.Components;
using HlyssUI.Graphics;
using SFML.System;
using SFML.Window;

namespace Connect.Widgets
{
    internal class ActionBar : Component
    {
        private Canvas _canvas;
        private TrackBar _zoom;
        private Label _coords;
        private Label _length;

        public ActionBar()
        {
            Width = "100%";
            AutosizeY = true;
            CenterContent = true;
            Padding = "0 15";

            Children = new List<Component>
            {
                new Icon(Icons.Arrows)
                {
                    MarginRight = "5px"
                },
                new Label()
                {
                    Name = "coords"
                },
                new Icon(Icons.Ruler)
                {
                    MarginRight = "5px",
                    MarginLeft = "15px"
                },
                new Label()
                {
                    Name = "length"
                },
                new Spacer(),
                new Icon(Icons.Minus),
                new TrackBar()
                {
                    Width = "200px",
                    Height = "30px",
                    Margin = "0 14",
                    MaxValue = 350,
                    Value = 50,
                    Name = "zoom"
                },
                new Icon(Icons.Plus)
            };
        }

        public override void OnInitialized()
        {
            base.OnInitialized();

            _canvas = Form.Root.FindChild("canvas") as Canvas;
            _coords = FindChild("coords") as Label;
            _length = FindChild("length") as Label;
            _zoom = FindChild("zoom") as TrackBar;

            _zoom.OnConfirmed += (_, value) =>
            {
                _canvas.View.Zoom = (value + 50) / 100f;
                Console.WriteLine(_canvas.View.Zoom);
            };
        }

        public override void OnScrolledAnywhere(float scroll)
        {
            base.OnScrolledAnywhere(scroll);

            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
            {
                _zoom.Value += (int)scroll * 5;
                _zoom.Value = Math.Clamp(_zoom.Value, 0, 350);
                _canvas.View.Zoom = (_zoom.Value + 50) / 100f;
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                {
                    _canvas.View.Center -= new Vector2f(5 * scroll, 0);
                }
                else
                {
                    _canvas.View.Center -= new Vector2f(0, 5 * scroll);
                }
            }
        }

        public override void Update()
        {
            base.Update();

            var cursorCoords = _canvas.Cursor.SelectedPoint;
            _coords.Text = $"{cursorCoords.X}, {cursorCoords.Y}";

            var toolLength = _canvas.Tool.LineLength;
            _length.Text = $"{MathF.Round(toolLength, 2)}";
        }
    }
}
