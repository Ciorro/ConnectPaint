using HlyssUI.Components;
using HlyssUI.Graphics;
using HlyssUI.Layout;

namespace Connect.Widgets
{
    internal class Slider : Panel
    {
        public struct SliderProperties
        {
            public int MaxValue;
            public int MinValue;
            public int DefaultValue;
            public string Label;
        }

        private TrackBar _tb;
        private SpinButton _sb;
        private SliderProperties _vProps;

        public Action<int> OnValueChanged;

        public int Value
        {
            get => _tb.Value + _vProps.MinValue;
            set
            {
                int val = Math.Clamp(value, _vProps.MinValue, _vProps.MaxValue);

                _tb.Value = val;
                _sb.Value = val;
            }
        }

        public Slider(SliderProperties properties)
        {
            _vProps = properties;

            Width = "100%";
            AutosizeY = true;
            Padding = "5px";
            CenterContent = true;
            Layout = LayoutType.Column;
            Style = "slider";

            Children = new List<Component>()
            {
                new Component()
                {
                    Width = "95%",
                    AutosizeY = true,
                    CenterContent = true,
                    Children = new List<Component>()
                    {
                        new Label(properties.Label)
                        {
                            Font = Fonts.MontserratSemiBold
                        },
                        new Spacer(),
                        new SpinButton()
                        {
                            Width = "100px",
                            MinValue = properties.MinValue,
                            MaxValue = properties.MaxValue,
                            Name = "sb"
                        },
                        new Button()
                        {
                            Padding = "8",
                            Appearance = Button.ButtonStyle.Flat,
                            Children = new List<Component>()
                            {
                                new Icon(Icons.UndoAlt)
                            },
                            Action = (_) =>
                            {
                                _tb.Value = _vProps.DefaultValue - _vProps.MinValue;
                                _sb.Value = Value;
                                OnValueChanged?.Invoke(Value);
                            }
                        }
                    }
                },
                new TrackBar()
                {
                    Width = "90%",
                    Height = "30px",
                    Name = "tb",
                    MaxValue = properties.MaxValue - properties.MinValue
                }
            };

            _tb = FindChild("tb") as TrackBar;
            _sb = FindChild("sb") as SpinButton;

            _tb.Value = _vProps.DefaultValue - _vProps.MinValue;
            _sb.Value = Value;

            _tb.OnConfirmed += (_, __) =>
            {
                _sb.Value = Value;
                OnValueChanged?.Invoke(Value);
            };
        }

        public override void Update()
        {
            base.Update();

            if (_sb.Focused)
            {
                int tempVal = _tb.Value;
                _tb.Value = _sb.Value - _vProps.MinValue;

                if (_tb.Value != tempVal)
                {
                    OnValueChanged?.Invoke(Value);
                }
            }
        }
    }
}
