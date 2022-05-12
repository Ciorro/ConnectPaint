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
        private Color _prevColor = Color.White;
        private Color _currColor = Color.White;

        private RoundedRectangle _prevPreview;
        private RoundedRectangle _currPreview;
        private RoundedRectangle _transparent;

        public ColorPreview()
        {
            _prevPreview = new RoundedRectangle()
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

            PrevColor = Color.White;
            CurrentColor = Color.White;
        }

        public Color PrevColor
        {
            get => _prevColor;
            set
            {
                _prevColor = value;
                _prevPreview.FillColor = value;
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

            _prevPreview.Size = new Vector2f()
            {
                X = Size.X,
                Y = Size.Y / 2 - 1
            };
            _prevPreview.Position = (Vector2f)GlobalPosition;
            _prevPreview.UpdateGeometry();

            _currPreview.Size = new Vector2f()
            {
                X = Size.X,
                Y = Size.Y / 2 - 1
            };
            _currPreview.Position = _prevPreview.Position + new Vector2f(0, Size.Y / 2 + 1);
            _currPreview.UpdateGeometry();

            _transparent.Size = _prevPreview.Size;
            _transparent.TextureRect = new IntRect(0, 0, Size.X, Size.Y / 2 - 1);
            _transparent.UpdateGeometry();
        }

        public override void Draw(RenderTarget target)
        {
            base.Draw(target);

            _transparent.Position = _prevPreview.Position;
            target.Draw(_transparent);
            target.Draw(_prevPreview);

            _transparent.Position = _currPreview.Position;
            target.Draw(_transparent);
            target.Draw(_currPreview);
        }
    }
}
