using Connect.CanvasUtils;
using Connect.Dialogs;
using HlyssUI.Components;
using HlyssUI.Components.Dialogs;
using HlyssUI.Graphics;
using SFML.Graphics;
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
                                NewCanvas();
                            }
                        },
                        new MenuItem("Open")
                        {
                            Icon = Icons.Folder,
                            Action = (_) =>
                            {
                                Open();
                            }
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
                            Icon = Icons.FileExport,
                            Action = (_) =>
                            {
                                Export();
                            }
                        },
                        new MenuDivider(),
                        new MenuItem("Exit")
                        {
                            Icon = Icons.X,
                            Action = (_) =>
                            {
                                Exit();
                            }
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

        private bool NewCanvas()
        {
            bool cleared = false;

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
                        _filename = "";
                        _canvas.Clear();
                        cleared = true;
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

                        _filename = "";
                        _canvas.Clear();
                        cleared = true;
                    }
                };

                Form.Application.RegisterAndShow(confirm);
            }
            else
            {
                _filename = "";
                _canvas.Clear();
                cleared = true;
            }

            return cleared;
        }

        private bool Exit()
        {
            bool cleared = false;

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
                        Form.Window.Close();
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
                            Form.Window.Close();
                        }
                    }
                };

                Form.Application.RegisterAndShow(confirm);
            }
            else
            {
                Form.Window.Close();
            }

            return cleared;
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

        private void Open()
        {
            var file = new BrowseFileDialog()
            {
                StartDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                OnFileSelected = (_, fsEntry) =>
                {
                    if (fsEntry.Length == 1 && !fsEntry[0].IsDirectory)
                    {
                        if (NewCanvas())
                        {
                            var content = File.ReadAllText(fsEntry[0].FullPath);
                            _canvas.Import(content);
                            _filename = fsEntry[0].FullPath;
                        }
                    }
                }
            };

            Form.Application.RegisterAndShow(file);
        }

        private void Export()
        {
            var export = new ExportDialog()
            {
                OnSaved = (filename, res) =>
                {
                    var render = _canvas.RenderToTexture(res);
                    var img = render.Texture.CopyToImage();
                    img.SaveToFile(filename);
                }
            };

            Form.Application.RegisterAndShow(export);
        }
    }
}
