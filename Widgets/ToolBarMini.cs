using Connect.CanvasUtils.Builders;
using HlyssUI.Components;
using HlyssUI.Graphics;
using HlyssUI.Layout;
using SFML.Graphics;

namespace Connect.Widgets
{
    internal class ToolBarMini : Component
    {
        private Canvas _canvas;

        public ToolBarMini()
        {
            Width = "50px";
            Height = "100%";
            Padding = "5px";
            Layout = LayoutType.Column;
            CenterContent = true;

            Children = new List<Component>
            {
                new IconButton(Icons.Undo),
                new IconButton(Icons.Redo),
                new Divider()
                {
                    Margin = "5px",
                    Width = "50%"
                },
                new IconButton(Icons.Pencil)
                {
                    Action = (_) =>
                    {
                        _canvas.SetDrawable(new LineBuilder());
                    }
                },
                new IconButton(Icons.DrawPolygon)
                {
                    Action = (_) =>
                    {
                        _canvas.SetDrawable(new PolygonBuilder());
                    }
                },
                new IconButton(Icons.DotCircle)
                {
                    Action = (_) =>
                    {
                        _canvas.SetDrawable(new PointsBuilder());
                    }
                },
                new Divider()
                {
                    Margin = "5px",
                    Width = "50%"
                }
            };
        }

        public override void OnInitialized()
        {
            base.OnInitialized();

            _canvas = Form.Root.FindChild("canvas") as Canvas;

            Color[] defaultColors =
            {
                new Color(237, 28, 36),
                new Color(255, 127, 39),
                new Color(255, 242, 0),
                new Color(34, 177, 76),
                new Color(0, 162, 232),
                new Color(63, 72, 204),
                new Color(163, 73, 164),
                new Color(0, 0, 0),
                new Color(128, 128, 128),
                new Color(255, 255, 255)
            };

            for (int i = 0; i < defaultColors.Length; i++)
            {
                AddChild(new ColorButton(defaultColors[i], _canvas)
                {
                    Width = "25",
                    Height = "25",
                    Margin = "5 0",
                    IsSelected = i == 0
                });
            }
        }
    }
}
