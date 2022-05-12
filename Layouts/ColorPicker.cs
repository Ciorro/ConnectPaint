using Connect.Widgets;
using HlyssUI.Components;
using HlyssUI.Layout;
using SFML.Graphics;

namespace Connect.Layouts
{
    internal class ColorPicker : Component
    {
        private ColorPreview _preview;

        public Action<Color> OnColorSelected;
        public Action<Color> OnCancelled;

        public ColorPicker()
        {
            Width = "100%";
            Height = "100%";
            Layout = LayoutType.Column;

            Children = new List<Component>
            {
                new Component()
                {
                    Width = "100%",
                    AutosizeY = true,
                    Children = new List<Component>
                    {
                        //Sliders
                        new Component()
                        {
                            Width = "75%",
                            AutosizeY = true,
                            Layout = LayoutType.Column,
                            Children = new List<Component>
                            {
                                new Slider(new Slider.SliderProperties()
                                {
                                    MinValue = 0,
                                    MaxValue = 255,
                                    DefaultValue = 255,
                                    Label = "Red"
                                })
                                {
                                    Name = "r",
                                    MarginBottom = "5px",
                                    OnValueChanged = (value) =>
                                    {
                                        _preview.CurrentColor = _preview.CurrentColor with
                                        {
                                            R = (byte)value
                                        };
                                    }
                                },
                                new Slider(new Slider.SliderProperties()
                                {
                                    MinValue = 0,
                                    MaxValue = 255,
                                    DefaultValue = 255,
                                    Label = "Green"
                                })
                                {
                                    Name = "g",
                                    MarginBottom = "5px",
                                    OnValueChanged = (value) =>
                                    {
                                        _preview.CurrentColor = _preview.CurrentColor with
                                        {
                                            G = (byte)value
                                        };
                                    }
                                },
                                new Slider(new Slider.SliderProperties()
                                {
                                    MinValue = 0,
                                    MaxValue = 255,
                                    DefaultValue = 255,
                                    Label = "Blue",
                                })
                                {
                                    Name = "b",
                                    MarginBottom = "5px",
                                    OnValueChanged = (value) =>
                                    {
                                        _preview.CurrentColor = _preview.CurrentColor with
                                        {
                                            B = (byte)value
                                        };
                                    }
                                },
                                new Slider(new Slider.SliderProperties()
                                {
                                    MinValue = 0,
                                    MaxValue = 255,
                                    DefaultValue = 255,
                                    Label = "Alpha",
                                })
                                {
                                    Name = "a",
                                    MarginBottom = "5px",
                                    OnValueChanged = (value) =>
                                    {
                                        _preview.CurrentColor = _preview.CurrentColor with
                                        {
                                            A = (byte)value
                                        };
                                    }
                                }
                            }
                        },
                        //Color preview
                        new AspectRatio()
                        {
                            Width = "25%",
                            PaddingLeft = "7px",
                            Children = new List<Component>
                            {
                                new ColorPreview()
                                {
                                    Width = "100%",
                                    Height = "100%",
                                    Name = "preview"
                                }
                            }
                        }
                    }
                },
                new Spacer(),
                //Buttons
                new Component()
                {
                    Width = "100%",
                    AutosizeY = true,
                    Reversed = true,
                    Children = new List<Component>
                    {
                        new Button("Save")
                        {
                            Appearance = Button.ButtonStyle.Filled,
                            Action = (_) =>
                            {
                                var r = FindChild("r") as Slider;
                                var g = FindChild("g") as Slider;
                                var b = FindChild("b") as Slider;
                                var a = FindChild("a") as Slider;

                                OnColorSelected?.Invoke(new Color((byte)r.Value, (byte)g.Value, (byte)b.Value, (byte)a.Value));
                            }
                        },
                        new Button("Cancel")
                        {
                            Appearance = Button.ButtonStyle.Outline,
                            MarginRight = "5px",
                            Action = (_) =>
                            {
                                var r = FindChild("r") as Slider;
                                var g = FindChild("g") as Slider;
                                var b = FindChild("b") as Slider;
                                var a = FindChild("a") as Slider;

                                OnCancelled?.Invoke(new Color((byte)r.Value, (byte)g.Value, (byte)b.Value, (byte)a.Value));
                            }
                        }
                    }
                }
            };

            _preview = FindChild("preview") as ColorPreview;
        }

        public void SetReferenceColor(Color color)
        {
            _preview.PrevColor = color;
        }

        public void SetOutputColor(Color color)
        {
            (FindChild("r") as Slider).Value = color.R;
            (FindChild("g") as Slider).Value = color.G;
            (FindChild("b") as Slider).Value = color.B;
            (FindChild("a") as Slider).Value = color.A;

            _preview.CurrentColor = color;
        }
    }
}
