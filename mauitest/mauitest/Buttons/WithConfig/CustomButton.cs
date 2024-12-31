using System.Windows.Input;
using mauitest.Buttons.WithConfig.Config;

namespace mauitest.Buttons.WithConfig
{
    public class CustomButton : ContentView
    {
        private readonly TapGestureRecognizer _tapGestureRecognizer = new TapGestureRecognizer();

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(CustomButton), null, propertyChanged: CommandPropertyChanged);

        private static void CommandPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue is ICommand command)
            {
                ((CustomButton)bindable)._tapGestureRecognizer.Command = command;
            }
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(CustomButton), null, propertyChanged: CommandParameterPropertyChanged);

        private static void CommandParameterPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((CustomButton)bindable)._tapGestureRecognizer.CommandParameter = newvalue;
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        private ButtonConfiguration? _containedButtonConfiguration;

        public ButtonConfiguration? ButtonConfig
        {
            get => _containedButtonConfiguration;
            set
            {
                _containedButtonConfiguration = value;
                Draw();
            }
        }

        public CustomButton()
        {
            HorizontalOptions = VerticalOptions = LayoutOptions.Start;
        }

        private void Draw()
        {
            if (ButtonConfig != null)
            {
                Content = ButtonConfig.ComposeView();
                ButtonConfig.ApplyGestureHandler(this, _tapGestureRecognizer);
            }
        }
    }
}