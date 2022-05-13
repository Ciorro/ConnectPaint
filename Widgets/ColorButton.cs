using Connect.Dialogs;
using HlyssUI.Components;
using HlyssUI.Components.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Connect.Widgets
{
    internal class ColorButton : Panel, ISelectable
    {
        public event ISelectable.SelectedHandler OnSelect;
        public event ISelectable.UnselectedHandler OnUnselect;

        private bool _isSelected;
        private CircleShape _transparent;
        private CircleShape _colorPreview;
        private Color _color;

        private ColorPickerDialog _colorPicker;
        private Canvas _canvas;

        public ColorButton(Color color, Canvas canvas)
        {
            Style = "color-selector-default";

            _colorPreview = new CircleShape()
            {
                OutlineThickness = 1
            };
            Color = color;

            _transparent = new CircleShape()
            {
                Texture = new Texture("Data/transparent.png")
            };

            _colorPicker = new ColorPickerDialog()
            {
                OnColorSelected = (color) =>
                {
                    Color = color;
                    _canvas.Color = Color;
                }
            };

            OnSelect += (_) =>
            {
                _canvas.Color = Color;
            };

            _canvas = canvas;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    if (value)
                    {
                        UnmarkOthers();
                        OnSelect?.Invoke(this);
                        Style = "color-selector-selected";
                    }
                    else
                    {
                        OnUnselect?.Invoke(this);
                        Style = "color-selector-default";
                    }

                    _isSelected = value;
                }
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _colorPreview.FillColor = value;
                _colorPreview.OutlineColor = value;

                _color = value;
            }
        }

        public override void OnRefresh()
        {
            base.OnRefresh();
            _colorPreview.Radius = Size.X / 2f - 3;
            _colorPreview.Position = new Vector2f()
            {
                X = GlobalPosition.X + 3,
                Y = GlobalPosition.Y + 3
            };

            _transparent.Radius = Size.X / 2f - 2;
            _transparent.Position = new Vector2f()
            {
                X = GlobalPosition.X + 2,
                Y = GlobalPosition.Y + 2
            };
            _transparent.TextureRect = new IntRect(0, 0, (int)_transparent.Radius * 2, (int)_transparent.Radius * 2);
        }

        public override void Draw(RenderTarget target)
        {
            base.Draw(target);
            target.Draw(_transparent);
            target.Draw(_colorPreview);
        }

        public override void OnClicked()
        {
            base.OnClicked();
            IsSelected = true;
        }

        public override void OnDoubleClicked()
        {
            base.OnDoubleClicked();

            Form.Application.RegisterAndShow(_colorPicker);
            _colorPicker.SetReferenceColor(Color);
        }

        private void UnmarkOthers()
        {
            if (Parent == null)
                return;

            foreach (var parentChild in Parent.Children)
            {
                if (parentChild is ISelectable && parentChild != this)
                {
                    ((ISelectable)parentChild).IsSelected = false;
                }
            }
        }
    }
}
