namespace Connect.Actions
{
    internal class ActionStack
    {
        private List<IAction> _actions = new();
        private int _currentIndex;

        public void Do(IAction action)
        {
            if (_currentIndex != _actions.Count)
            {
                int diff = _actions.Count - _currentIndex;
                _actions.RemoveRange(_currentIndex, diff);
            }

            _actions.Add(action);
            _currentIndex++;
            action.Do();
        }

        public void Undo()
        {
            if (_currentIndex > 0)
            {
                _actions[--_currentIndex].Undo();
            }
        }

        public void Redo()
        {
            if (_currentIndex < _actions.Count)
            {
                _actions[_currentIndex++].Do();
            }
        }

        public void Clear()
        {
            _actions.Clear();
            _currentIndex = 0;
        }
    }
}
