using Connect.Widgets;
using HlyssUI.Components;
using HlyssUI.Layout;

namespace Connect.Layouts
{
    internal class Export : Component
    {
        public Action<string, int> OnSaved;
        public Action<string, int> OnCancelled;

        public Export()
        {
            Width = "100%";
            Height = "100%";
            Layout = LayoutType.Column;

            Children = new List<Component>
            {
                new PreciseSlider(new PreciseSlider.SliderProperties()
                {
                    MinValue = 16,
                    MaxValue = 4096,
                    DefaultValue = 512,
                    Label = "Resolution"
                })
                {
                    Width = "100%",
                    Name = "resolution"
                },
                new Save()
                {
                    Height = "220px",
                    MarginTop = "10px",
                    DefaultName = "Unnamed.png",
                    OnSaved = (_) =>
                    {
                        OnSaved?.Invoke(_, (FindChild("resolution") as PreciseSlider).Value);
                    },
                    OnCancelled = (_) =>
                    {
                        OnCancelled?.Invoke(_, (FindChild("resolution") as PreciseSlider).Value);
                    }
                }
            };
        }
    }
}
