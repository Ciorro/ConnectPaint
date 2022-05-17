using Connect.Layouts;
using HlyssUI;
using SFML.System;
using SFML.Window;

namespace Connect.Dialogs
{
    internal class SaveDialog : HlyssForm
    {
        public Action<string> OnSaved;

        public SaveDialog()
        {
            Title = "Save as...";
            WindowStyle = Styles.Titlebar;
            Size = new Vector2u(400, 220);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Root.AddChild(new Save(".cpd")
            {
                Padding = "10px",
                Name = "picker",
                OnSaved = (path) =>
                {
                    OnSaved?.Invoke(path);
                    Hide();
                },
                OnCancelled = (_) =>
                {
                    Hide();
                }
            });
        }
    }
}
