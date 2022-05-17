using Connect.Layouts;
using HlyssUI;
using SFML.System;
using SFML.Window;

namespace Connect.Dialogs
{
    internal class InfoDialog : HlyssForm
    {
        public InfoDialog()
        {
            Title = "ConnectPaint";
            WindowStyle = Styles.Titlebar;
            Size = new Vector2u(400, 270);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Root.AddChild(new Info()
            {
                Padding = "20px"
            });
        }
    }
}
