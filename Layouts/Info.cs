using HlyssUI.Components;
using HlyssUI.Graphics;
using HlyssUI.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        new Label("v0.1"),
                        new LinkLabel("Created for The Tool Jam 2 by pachrusc0", "https://itch.io/jam/the-tool-jam-2")
                        {
                            MarginTop = "10px"
                        },
                        new Component()
                        {
                            Width = "100%",
                            AutosizeY = true,
                            MarginTop = "5px",
                            Children = new List<Component>
                            {
                                new LinkLabel("Itch.io", "https://pachrusc.itch.io/")
                                {
                                    MarginRight = "10px"
                                },
                                new LinkLabel("Source", "https://github.com/Ciorro/ConnectPaint")
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
                                        Console.WriteLine("sss");
                                        Form.Hide();
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
