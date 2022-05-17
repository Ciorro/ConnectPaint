using HlyssUI.Components;
using HlyssUI.Graphics;

namespace Connect.Widgets
{
    internal class ToolBarFull : Component
    {
        public ToolBarFull()
        {
            Width = "200px";
            Height = "100%";
            Padding = "5px";

            Children = new List<Component>
            {
                new Label("Full")
                {
                    Font = Fonts.MontserratSemiBold
                }
            };
        }
    }
}
