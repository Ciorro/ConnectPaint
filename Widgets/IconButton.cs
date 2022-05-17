using HlyssUI.Components;
using HlyssUI.Graphics;

namespace Connect.Widgets
{
    internal class IconButton : Button
    {
        public IconButton(Icons icon)
        {
            Padding = "8";

            Children = new List<Component>
            {
                new Icon(icon)
            };

            Appearance = ButtonStyle.Flat;
        }
    }
}
