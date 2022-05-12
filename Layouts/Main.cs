using Connect.Widgets;
using HlyssUI.Components;
using HlyssUI.Layout;

namespace Connect.Layouts
{
    internal class Main : Panel
    {
        public Main()
        {
            Width = "100%";
            Height = "100%";
            Style = "container";
            Layout = LayoutType.Column;

            Children = new List<Component>
            {
                //MenuBar
                new Panel()
                {
                    Width = "100%",
                    AutosizeY = true,
                    Style = "toolbar",
                    Children = new List<Component>
                    {
                        new MenuBar()
                    }
                },
                //ToolBox and Canvas
                new Component()
                {
                    Expand = true,
                    Width = "100%",
                    PaddingRight = "5px",
                    Children = new List<Component>
                    {
                        new Panel()
                        {
                            Height = "100%",
                            AutosizeX = true,
                            Style = "toolbar",
                            Children = new List<Component>
                            {
                                new ToolBarMini()
                            }
                        },
                        new Panel()
                        {
                            Expand = true,
                            Height = "100%",
                            Style = "canvas",
                            Children = new List<Component>
                            {
                                new Canvas()
                                {
                                    Name = "canvas"
                                }
                            }
                        }
                    }
                },
                //ActionBar
                new Panel()
                {
                    Width = "100%",
                    AutosizeY = true,
                    Style = "toolbar",
                    Children = new List<Component>
                    {
                        new ActionBar()
                    }
                }
            };
        }
    }
}
