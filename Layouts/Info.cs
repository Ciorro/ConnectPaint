using HlyssUI.Components;
using HlyssUI.Graphics;
using HlyssUI.Layout;
using System.Diagnostics;

namespace Connect.Layouts
{
    internal class Info : Component
    {
        public Info()
        {
            Width = "100%";
            Height = "100%";

            Children = new List<Component>
            {
                new PictureBox("Data/favicon.png")
                {
                    Width = "64px",
                    Height = "64px",
                    SmoothImage = true
                },
                new Component()
                {
                    MarginLeft = "20px",
                    Width = "276px",
                    Height = "100%",
                    Layout = LayoutType.Column,
                    Children = new List<Component>
                    {
                        new Label("ConnectPaint")
                        {
                            Font = Fonts.MontserratBold,
                            Style = "header"
                        },
                        new Label("v0.1.1"),
                        new LinkLabel("Created for The Tool Jam 2", "https://itch.io/jam/the-tool-jam-2")
                        {
                            MarginTop = "10px"
                        },
                        new Label("Special thanks to:")
                        {
                            Font = Fonts.MontserratBold,
                            MarginTop = "10px"
                        },
                        new LinkLabel("LibTessDotNet", "https://github.com/speps/LibTessDotNet/"),
                        new Label("Links:")
                        {
                            Font = Fonts.MontserratBold,
                            MarginTop = "10px"
                        },
                        new Component()
                        {
                            Width = "100%",
                            AutosizeY = true,
                            MarginTop = "5px",
                            Children = new List<Component>
                            {
                                new Button("Itch.Io")
                                {
                                    Expand = true,
                                    MarginRight = "5px",
                                    OverwriteChildren = false,
                                    ChildrenBefore = new List<Component>
                                    {
                                        new PictureBox("Data/itchio.png")
                                        {
                                            Width = "16px",
                                            Height = "16px",
                                            SmoothImage = true,
                                            MarginRight = "8px"
                                        }
                                    },
                                    Action = (_) => OpenLink("https://pachrusc.itch.io/")
                                },
                                new Button("Source")
                                {
                                    Expand = true,
                                    MarginLeft = "5px",
                                    OverwriteChildren = false,
                                    ChildrenBefore = new List<Component>
                                    {
                                        new PictureBox("Data/github.png")
                                        {
                                            Width = "16px",
                                            Height = "16px",
                                            SmoothImage = true,
                                            MarginRight = "8px"
                                        }
                                    },
                                    Action = (_) => OpenLink("https://github.com/Ciorro/ConnectPaint/")
                                },
                            }
                        },
                        new Spacer(),
                        new Component()
                        {
                            Width = "100%",
                            AutosizeY = true,
                            Reversed = true,
                            Children = new List<Component>
                            {
                                new Button("Ok")
                                {
                                    Appearance = Button.ButtonStyle.Filled,
                                    Action = (_) =>
                                    {
                                        Form.Hide();
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private void OpenLink(string url)
        {
            ProcessStartInfo info = new ProcessStartInfo("cmd");
            info.Arguments = $"/c start {url}";
            Process.Start(info);
        }
    }
}
