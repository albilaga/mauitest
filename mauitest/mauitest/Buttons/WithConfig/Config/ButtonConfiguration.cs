namespace mauitest.Buttons.WithConfig.Config
{
    [AcceptEmptyServiceProvider]
    public abstract class ButtonConfiguration : IMarkupExtension<ButtonConfiguration>
    {
        public abstract ButtonConfiguration BasedOn { set; }

        public abstract View ComposeView();

        public ButtonConfiguration ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public abstract void ApplyGestureHandler(View parentView,
            GestureRecognizer customGestureRecognizer);
    }
}
