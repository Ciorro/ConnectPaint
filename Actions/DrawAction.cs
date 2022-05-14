using Connect.CanvasUtils.Drawables;
using Connect.Widgets;

namespace Connect.Actions
{
    internal class DrawAction : IAction
    {
        private Canvas _canvas;
        private CanvasDrawable _drawable;

        private int _index = -1;

        public DrawAction(Canvas canvas, CanvasDrawable drawable)
        {
            _canvas = canvas;
            _drawable = drawable;
        }

        public void Do()
        {
            if (_index == -1)
            {
                _canvas.Add(_drawable);
            }
            else
            {
                _canvas.Insert(_index, _drawable);
            }
        }

        public void Undo()
        {
            _index = _canvas.IndexOf(_drawable);
            _canvas.RemoveAt(_index);
        }
    }
}
