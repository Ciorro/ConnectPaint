﻿using Connect.CanvasUtils.Builders;
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
                new IconButton(Icons.Undo)
                {
                    Action = (_) =>
                    {
                        _canvas.Tool.Undo();
                    }
                },
                new IconButton(Icons.Redo)
                {
                    Action = (_) =>
                    {
                        _canvas.Tool.Redo();
                    }
                },
                new Divider()
                {
                    Margin = "5px",
                    Width = "50%"
                },
                new IconButton(Icons.Pencil)
                {
                    Name = "line",
                    Action = (_) =>
                    {
                        _canvas.Tool.SetDrawable(new LineBuilder());
                    }
                },
                new IconButton(Icons.DrawPolygon)
                {
                    Name = "polygon",
                    Action = (_) =>
                    {
                        _canvas.Tool.SetDrawable(new PolygonBuilder());
                    }
                },
                new IconButton(Icons.DotCircle)
                {
                    Name = "point",
                    Action = (_) =>
                    {
                        _canvas.Tool.SetDrawable(new PointsBuilder());
                    }
                },
                new Divider()
                {
                    Margin = "5px",
                    Width = "50%"
                },
                new ToolTip()
                {
                    Text = "Line Tool",
                    TargetName = "line"
                },
                new ToolTip()
                {
                    Text = "Polygon Tool",
                    TargetName = "polygon"
                },
                new ToolTip()
                {
                    Text = "Point Tool",
                    TargetName = "point"
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
