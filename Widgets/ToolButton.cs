using HlyssUI.Components;
using HlyssUI.Graphics;

namespace Connect.Widgets
{
    internal class ToolButton : ToggleButton
    {
        public ToolButton(Icons icon)
        {
            Padding = "8";

            Children = new List<Component>
            {
                new Icon(icon)
            };
        }
    }
}
