using HlyssUI.Components;
using HlyssUI.Components.Dialogs;
using HlyssUI.Graphics;
using HlyssUI.Layout;

namespace Connect.Layouts
{
    internal class Save : Component
    {
        public Action<string> OnSaved;
        public Action<string> OnCancelled;

        private string _preview;
        private string _extension;
        private Label _previewLbl;
        private TextBox _filenameBox;
        private TextBox _pathBox;

        public Save(string extension)
        {
            _extension = extension;

            Width = "100%";
            Height = "100%";
            Layout = LayoutType.Column;

            Children = new List<Component>
            {
                new Label("Name")
                {
                    Font = Fonts.MontserratSemiBold
                },
                new Component()
                {
                    Width = "100%",
                    AutosizeY = true,
                    Reversed = true,
                    MarginTop = "5",
                    MarginBottom = "10",
                    CenterContent = true,
                    Children = new List<Component>
                    {
                        new Label(_extension)
                        {
                            MarginLeft = "5px"
                        },
                        new TextBox()
                        {
                            Expand = true,
                            Name = "filename",
                            Text = "Untitled"
                        },
                    }
                },
                new Label("Path")
                {
                    Font = Fonts.MontserratSemiBold
                },
                new Component()
                {
                    Width = "100%",
                    AutosizeY = true,
                    Reversed = true,
                    MarginTop = "5",
                    Children = new List<Component>
                    {
                        new Button("Browse")
                        {
                            MarginLeft = "5",
                            Action = (_) =>
                            {
                                var dir = new BrowseFolderDialog()
                                {
                                    OnFolderSelected = (_, folder) =>
                                    {
                                        _pathBox.Text = folder;
                                        UpdatePreview();
                                    }
                                };

                                Form.Application.RegisterAndShow(dir);
                            }
                        },
                        new TextBox()
                        {
                            Expand = true,
                            Padding = "8",
                            Name = "path",
                            Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                        }
                    }
                },
                new Label(" ")
                {
                    Name = "save_preview",
                    MarginTop = "10px",
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
                                OnSaved?.Invoke(_preview);
                            }
                        },
                        new Button("Cancel")
                        {
                            Appearance = Button.ButtonStyle.Outline,
                            MarginRight = "5px",
                            Action = (_) =>
                            {
                                OnCancelled?.Invoke(_preview);
                            }
                        }
                    }
                }
            };

            _filenameBox = FindChild("filename") as TextBox;
            _pathBox = FindChild("path") as TextBox;
            _previewLbl = FindChild("save_preview") as Label;

            _filenameBox.OnTextChanged += Save_OnTextChanged;
            _pathBox.OnTextChanged += Save_OnTextChanged;

            UpdatePreview();
        }

        private void Save_OnTextChanged(object sender, string currentText)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            string fullFilename = _filenameBox.Text + _extension;
            _preview = Path.Combine(_pathBox.Text, fullFilename);
            _previewLbl.Text = _preview;
        }
    }
}
