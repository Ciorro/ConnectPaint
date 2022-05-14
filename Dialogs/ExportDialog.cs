using Connect.Layouts;
using HlyssUI;
using SFML.System;
using SFML.Window;

namespace Connect.Dialogs
{
    internal class ExportDialog : HlyssForm
    {
        public Action<string, int> OnSaved;

        public ExportDialog()
        {
            Title = "Export";
            WindowStyle = Styles.Titlebar;
            Size = new Vector2u(400, 320);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Root.AddChild(new Export()
            {
                Padding = "10px",
                Name = "picker",
                OnSaved = (path, res) =>
                {
                    OnSaved?.Invoke(path, res);
                    Hide();
                },
                OnCancelled = (_, __) =>
                {
                    Hide();
                }
            });
        }
    }
}
