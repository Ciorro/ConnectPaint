using HlyssUI.Components;
using HlyssUI.Graphics;
using SFML.Window;

namespace Connect.Widgets
{
    internal class ActionBar : Component
    {
        private Canvas _canvas;
        private TrackBar _zoom;
        private Label _coords;

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
                new Label("135, -345")
                {
                    Name = "coords"
                },
                new Spacer(),
                new Icon(Icons.Minus),
                new TrackBar()
                {
                    Width = "200px",
                    Height = "30px",
                    Margin = "0 14",
                    MaxValue = 90,
                    Value = 20,
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
            _zoom = FindChild("zoom") as TrackBar;

            _zoom.OnConfirmed += (_, value) =>
            {
                _canvas.Spacing = value + 10;
            };
        }

        public override void OnScrolledAnywhere(float scroll)
        {
            base.OnScrolledAnywhere(scroll);

            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
            {
                _zoom.Value += (int)scroll;
                _zoom.Value = Math.Clamp(_zoom.Value, 0, 90);
                _canvas.Spacing = _zoom.Value + 10;
            }
        }

        public override void Update()
        {
            base.Update();

            var cursorCoords = _canvas.CursorPosition;
            _coords.Text = $"{cursorCoords.X}, {cursorCoords.Y}";
        }
    }
}
