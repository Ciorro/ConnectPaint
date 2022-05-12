using HlyssUI.Components;
using HlyssUI.Graphics;
using SFML.System;

namespace Connect.Widgets
{
    internal class MenuBar : Component
    {
        private const int MenuY = 30;


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
                            Icon = Icons.File 
                        },
                        new MenuItem("Open")
                        {
                            Icon = Icons.Folder
                        },
                        new MenuDivider(),
                        new MenuItem("Save")
                        {
                            Icon = Icons.Save
                        },
                        new MenuItem("Save as...")
                        {
                            Icon = Icons.Save
                        },
                        new MenuDivider(),
                        new MenuItem("Recent")
                        {
                            Icon = Icons.Clock
                        },
                        new MenuDivider(),
                        new MenuItem("Preferences")
                        {
                            Icon = Icons.Cog
                        },
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
                            Icon = Icons.Undo
                        },
                        new MenuItem("Redo")
                        {
                            Icon = Icons.Redo
                        }
                    }
                },
                new Menu()
                {
                    Name = "view-menu",
                    Items = new List<MenuItem>
                    {
                        new CheckMenuItem("Show grid"),
                        new CheckMenuItem("Show axes")
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
    }
}
