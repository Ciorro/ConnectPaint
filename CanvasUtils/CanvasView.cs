using Connect.Widgets;
using SFML.Graphics;
using SFML.System;

namespace Connect.CanvasUtils
{
    internal class CanvasView
    {
        private Canvas _canvas;

        public CanvasView(Canvas canvas)
        {
            _canvas = canvas;
        }

        public Vector2f Center { get; set; }

        public float Zoom { get; set; } = 1f;

        public Transform Transform
        {
            get
            {
                var transform = Transform.Identity;

                transform.Translate(new Vector2f()
                {
                    X = (int)(_canvas.Size.X / 2),
                    Y = (int)(_canvas.Size.Y / 2)
                });
                transform.Scale(Zoom * Canvas.Spacing, Zoom * Canvas.Spacing);
                transform.Translate(-Center);

                return transform;
            }
        }

        public Vector2f ScreenToCanvas(Vector2f location)
        {
            return Transform.GetInverse().TransformPoint(location);
        }

        public Vector2f CanvasToScreen(Vector2f location)
        {
            return Transform.TransformPoint(location);
        }
    }
}
