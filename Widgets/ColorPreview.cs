using HlyssUI.Components;
using HlyssUI.Graphics;
using HlyssUI.Layout;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Widgets
{
    internal class ColorPreview : Component
    {
        private Color _refolor = Color.White;
        private Color _currColor = Color.White;

        private RoundedRectangle _refPreview;
        private RoundedRectangle _currPreview;
        private RoundedRectangle _transparent;

        public ColorPreview()
        {
            _refPreview = new RoundedRectangle()
            {
                BorderRadius = new BorderRadius(4)
            };

            _currPreview = new RoundedRectangle()
            {
                BorderRadius = new BorderRadius(4)
            };

            _transparent = new RoundedRectangle()
            {
                BorderRadius = new BorderRadius(4),
                Texture = new Texture("Data/transparent.png"),
                FillColor = Color.White
            };
            _transparent.Texture.Repeated = true;

            RefColor = Color.White;
            CurrentColor = Color.White;
        }

        public Color RefColor
        {
            get => _refolor;
            set
            {
                _refolor = value;
                _refPreview.FillColor = value;
            }
        }

        public Color CurrentColor
        {
            get => _currColor;
            set
            {
                _currColor = value;
                _currPreview.FillColor = value;
            }
        }

        public override void OnRefresh()
        {
            base.OnRefresh();

            _refPreview.Size = new Vector2f()
            {
                X = Size.X,
                Y = Size.Y / 2 - 1
            };
            _refPreview.Position = (Vector2f)GlobalPosition;
            _refPreview.UpdateGeometry();

            _currPreview.Size = new Vector2f()
            {
                X = Size.X,
                Y = Size.Y / 2 - 1
            };
            _currPreview.Position = _refPreview.Position + new Vector2f(0, Size.Y / 2 + 1);
            _currPreview.UpdateGeometry();

            _transparent.Size = _refPreview.Size;
            _transparent.TextureRect = new IntRect(0, 0, Size.X, Size.Y / 2 - 1);
            _transparent.UpdateGeometry();
        }

        public override void Draw(RenderTarget target)
        {
            base.Draw(target);

            _transparent.Position = _refPreview.Position;
            target.Draw(_transparent);
            target.Draw(_refPreview);

            _transparent.Position = _currPreview.Position;
            target.Draw(_transparent);
            target.Draw(_currPreview);
        }
    }
}
