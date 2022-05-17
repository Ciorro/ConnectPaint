namespace Connect.Actions
{
    internal interface IAction
    {
        public void Do();
        public void Undo();
    }
}
