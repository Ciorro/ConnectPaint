using Connect.Layouts;
using HlyssUI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Connect.Dialogs
{
    internal class ColorPickerDialog : HlyssForm
    {
        public Action<Color> OnColorSelected;

        public ColorPickerDialog()
        {
            Title = "Color";
            Icon = new Image("Data/color.png");
            WindowStyle = Styles.Titlebar;
            Size = new Vector2u(400, 400);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Root.AddChild(new ColorPicker()
            {
                Padding = "10px",
                Name = "picker",
                OnColorSelected = (color) =>
                {
                    OnColorSelected?.Invoke(color);
                    Hide();
                },
                OnCancelled = (_) =>
                {
                    Hide();
                }
            });
        }

        public void SetReferenceColor(Color color)
        {
            (Root.GetChild("picker") as ColorPicker).SetReferenceColor(color);
            (Root.GetChild("picker") as ColorPicker).SetOutputColor(color);
        }
    }
}
