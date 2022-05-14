using HlyssUI.Components;
using HlyssUI.Components.Dialogs;
using HlyssUI.Graphics;
using SFML.System;

namespace Connect.Widgets
{
    internal class MenuBar : Component
    {
        private const int MenuY = 30;

        private string _saveName;
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
                                            _canvas.Clear();
                                        }
                                        if (result == 2)
                                        {

                                        }
                                    };

                                    Form.Application.RegisterAndShow(confirm);
                                }
                                else
                                {
                                    _canvas.Clear();
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

                            }
                        },
                        new MenuItem("Save as...")
                        {
                            Icon = Icons.Save
                        },
                        new MenuDivider(),
                        new MenuItem("Exit")
                        {
                            Icon = Icons.FileExport
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

        public override void OnInitialized()
        {
            base.OnInitialized();
            _canvas = Form.Root.FindChild("canvas") as Canvas;
        }
    }
}
