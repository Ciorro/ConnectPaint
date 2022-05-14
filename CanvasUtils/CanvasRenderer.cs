using Connect.CanvasUtils.Drawables;
using SFML.Graphics;

namespace Connect.CanvasUtils
{
    internal class CanvasRenderer
    {
        public int PixelsWidth { get; set; } = 512;

        public RenderTexture RenderToTexture(List<CanvasDrawable> drawables)
        {
            var bounds = GetBounds(drawables);
            float pointSize = PixelsWidth / bounds.Width;
            float aspectRatio = (float)bounds.Height / bounds.Width;
            uint textureWidth = (uint)PixelsWidth;
            uint textureHeight = (uint)(PixelsWidth * aspectRatio);

            var texture = new RenderTexture(textureWidth, textureHeight);
            texture.Clear(Color.Transparent);

            Transform transform = Transform.Identity;
            transform.Scale(pointSize, pointSize);
            var states = new RenderStates(transform);

            foreach (var d in drawables)
            {
                texture.Draw(d, states);
            }

            texture.Display();
            return texture;
        }

        private IntRect GetBounds(List<CanvasDrawable> drawables)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (var d in drawables)
            {
                var bounds = d.GetBounds();
                int l = bounds.Left;
                int r = bounds.Left + bounds.Width;
                int t = bounds.Top;
                int b = bounds.Top + bounds.Height;

                minX = Math.Min(minX, l);
                minY = Math.Min(minY, t);
                maxX = Math.Max(maxX, r);
                maxY = Math.Max(maxY, b);
            }

            return new IntRect(minX, minY, maxX - minX, maxY - minY);
        }
    }
}
