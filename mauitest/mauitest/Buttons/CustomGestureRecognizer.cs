using System.Windows.Input;

namespace mauitest.Buttons
{
    public class CustomGestureRecognizer : Element, IGestureRecognizer
    {
        private readonly TapGestureRecognizer _customAnimationTapGestureRecognizer = new();

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(CustomGestureRecognizer),
            null,
            BindingMode.OneTime,
            propertyChanged: CommandPropertyChanged);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(CustomGestureRecognizer),
            null,
            propertyChanged: CommandParameterPropertyChanged);

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
            nameof(IsEnabled),
            typeof(bool),
            typeof(CustomGestureRecognizer),
            true);

        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        private static void CommandPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue is not ICommand command)
            {
                return;
            }

            var parent = GetParent(bindable);
            if (parent == null || bindable is not CustomGestureRecognizer gestureRecognizer)
            {
                return;
            }

            gestureRecognizer._customAnimationTapGestureRecognizer.Command =
                CreateAnimationCommand(gestureRecognizer, command, parent);
        }

        private static void CommandParameterPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var parent = GetParent(bindable);
            if (parent == null || bindable is not CustomGestureRecognizer gestureRecognizer)
            {
                return;
            }

            gestureRecognizer._customAnimationTapGestureRecognizer.CommandParameter = newvalue;
        }

        private static BindableObject? GetParent(BindableObject bindable)
        {
            if (bindable is CustomGestureRecognizer customGestureRecognizer)
            {
                return customGestureRecognizer.Parent;
            }

            return null;
        }

        private static Command CreateAnimationCommand(CustomGestureRecognizer gestureRecognizer,
            ICommand command,
            BindableObject parent)
        {
            return new Command(
                async () =>
                {
                    if (!command.CanExecute(gestureRecognizer.CommandParameter) || !gestureRecognizer.IsEnabled)
                    {
                        return;
                    }

                    try
                    {
                        if (parent is VisualElement visualElement)
                        {
                            await visualElement.FadeTo(0.5, 50).ConfigureAwait(true);
                            await visualElement.FadeTo(1, 50).ConfigureAwait(true);
                        }
                    }
                    catch (Exception)
                    {
                        //Do nothing
                    }

                    command.Execute(gestureRecognizer.CommandParameter);
                });
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            switch (Parent)
            {
                case null:
                    return;
                case View view:
                    view.GestureRecognizers.Add(_customAnimationTapGestureRecognizer);
                    break;
            }

            // if the gesture recognizer was created via code behind and no property change events are triggered, e.g. via binding
            if (Command != null)
            {
                _customAnimationTapGestureRecognizer.Command = CreateAnimationCommand(this, Command, Parent);
            }

            if (CommandParameter != null)
            {
                _customAnimationTapGestureRecognizer.CommandParameter = CommandParameter;
            }
        }
    }
}