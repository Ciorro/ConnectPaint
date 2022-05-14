using Connect.Dialogs;
using HlyssUI.Components;
using HlyssUI.Components.Dialogs;
using HlyssUI.Graphics;
using SFML.System;

namespace Connect.Widgets
{
    internal class MenuBar : Component
    {
        private const int MenuY = 30;

        private string _filename;
        private Canvas _canvas;

        public MenuBar()
        {
            Width = "100%";
            AutosizeY = true;

            Children = new List<Component>
            {
                //Buttons
                new Button("File")
                {
                    Padding = "8",
                    Style = "menu-item flat_button_default",
                    Action = (cmp) =>
                    {
                        int x = (cmp as Component).GlobalPosition.X;
                        (FindChild("file-menu") as Menu).Show(new Vector2i(x, MenuY));
                    }
                },
                new Button("Edit")
                {
                    Padding = "8",
                    Style = "menu-item flat_button_default",
                    Action = (cmp) =>
                    {
                        int x = (cmp as Component).GlobalPosition.X;
                        (FindChild("edit-menu") as Menu).Show(new Vector2i(x, MenuY));
                    }
                },
                new Button("View")
                {
                    Padding = "8",
                    Style = "menu-item flat_button_default",
                    Action = (cmp) =>
                    {
                        int x = (cmp as Component).GlobalPosition.X;
                        (FindChild("view-menu") as Menu).Show(new Vector2i(x, MenuY));
                    }
                },
                new Button("Help")
                {
                    Padding = "8",
                    Style = "menu-item flat_button_default",
                    Action = (cmp) =>
                    {
                        int x = (cmp as Component).GlobalPosition.X;
                        (FindChild("help-menu") as Menu).Show(new Vector2i(x, MenuY));
                    }
                },

                //Menus
                new Menu()
                {
                    Name = "file-menu",
                    Items = new List<MenuItem>
                    {
                        new MenuItem("New")
                        {
                            Icon = Icons.File,
                            Action = (_) =>
                            {
                                if (_canvas.HasDrawings)
                                {
                                    var confirm = new MessageBox(
                                        title: "New",
                                        content: "Your document has unsaved changes. What would you like to do?",
                                        buttons: new[]
                                        {
                                            "Cancel", "Discard", "Save"
                                        }
                                    );
                                    confirm.ResultHandler = (_, result) =>
                                    {
                                        //Save 2, Discard 1, Cancel 0
                                        if (result == 1)
                                        {
                                            NewCanvas();
                                        }
                                        if (result == 2)
                                        {
                                            if (string.IsNullOrEmpty(_filename))
                                            {
                                                SaveAs();
                                            }
                                            else
                                            {
                                                Save(_filename);
                                            }
                                        }
                                    };

                                    Form.Application.RegisterAndShow(confirm);
                                }
                                else
                                { 
                                    NewCanvas();
                                }
                            }
                        },
                        new MenuItem("Open")
                        {
                            Icon = Icons.Folder
                        },
                        new MenuDivider(),
                        new MenuItem("Save")
                        {
                            Icon = Icons.Save,
                            Action = (_) =>
                            {
                                if (string.IsNullOrEmpty(_filename))
                                {
                                    SaveAs();
                                }
                                else
                                {
                                    Save(_filename);
                                }
                            }
                        },
                        new MenuItem("Save as...")
                        {
                            Icon = Icons.Save,
                            Action = (_) =>
                            {
                                SaveAs();
                            }
                        },
                        new MenuItem("Export")
                        {
                            Icon = Icons.FileExport
                        },
                        new MenuDivider(),
                        new MenuItem("Exit")
                        {
                            Icon = Icons.X
                        }
                    }
                },
                new Menu()
                {
                    Name = "edit-menu",
                    Items = new List<MenuItem>
                    {
                        new MenuItem("Undo")
                        {
                            Icon = Icons.Undo,
                            Action = (_) =>
                            {
                                _canvas.Tool.Undo();
                            }
                        },
                        new MenuItem("Redo")
                        {
                            Icon = Icons.Redo,
                            Action = (_) =>
                            {
                                _canvas.Tool.Redo();
                            }
                        }
                    }
                },
                new Menu()
                {
                    Name = "view-menu",
                    Items = new List<MenuItem>
                    {
                        new CheckMenuItem("Show grid")
                        {
                            Action = (grid) =>
                            {
                                _canvas.Cursor.ShowGrid = !(grid as CheckMenuItem).IsChecked;
                            },
                            IsChecked = true
                        },
                        new CheckMenuItem("Show axes")
                        {
                            Action = (grid) =>
                            {
                                _canvas.Cursor.ShowAxes = !(grid as CheckMenuItem).IsChecked;
                            },
                            IsChecked = true
                        }
                    }
                },
                new Menu()
                {
                    Name = "help-menu",
                    Items = new List<MenuItem>
                    {
                        new MenuItem("About ConnectPaint")
                        {
                            Icon = Icons.InfoCircle
                        }
                    }
                }
            };
        }

        private void NewCanvas()
        {
            _filename = "";
            _canvas.Clear();
        }

        public override void OnInitialized()
        {
            base.OnInitialized();
            _canvas = Form.Root.FindChild("canvas") as Canvas;
        }

        private void Save(string filename)
        {
            var dir = Path.GetDirectoryName(filename);

            if (Directory.Exists(dir))
            {
                File.WriteAllText(filename, _canvas.Export());
            }
            else
            {
                Form.Application.RegisterAndShow(new MessageBox(
                    title: "Error",
                    content: "Invalid directory.",
                    buttons: "Ok"
                ));
            }
        }

        private void SaveAs()
        {
            var save = new SaveDialog()
            {
                OnSaved = (filename) =>
                {
                    Save(filename);
                    _filename = filename;
                }
            };

            Form.Application.RegisterAndShow(save);
        }
    }
}
