namespace mauitest.Buttons.WithConfig.Config
{
    public class IconButtonConfiguration : ButtonConfiguration
    {
        public Color? Color { get; set; }
        public string? IconGlyph { get; set; } = string.Empty;
        public double IconSize { get; set; } = 14;
        public string? IconFontFamily { get; set; }
        public bool SpacingEnabled { get; set; }
        public ImageSource? ImageSource { get; set; }
        public Thickness Padding { get; set; }

        public override ButtonConfiguration BasedOn
        {
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException($"null configuration provided for {GetType().FullName}");
                }

                if (value is not IconButtonConfiguration iconButtonConfiguration)
                {
                    return;
                }

                Color ??= iconButtonConfiguration.Color;

                if (IconSize <= 0)
                {
                    IconSize = iconButtonConfiguration.IconSize;
                }

                if (SpacingEnabled == false)
                {
                    SpacingEnabled = iconButtonConfiguration.SpacingEnabled;
                }

                if (Padding.IsEmpty)
                {
                    Padding = iconButtonConfiguration.Padding;
                }

                IconGlyph ??= iconButtonConfiguration.IconGlyph;
                IconFontFamily ??= iconButtonConfiguration.IconFontFamily;
                ImageSource ??= iconButtonConfiguration.ImageSource;
            }
        }

        public override View ComposeView()
        {
            var fontImageSource = ImageSource ?? new FontImageSource
            {
                Glyph = IconGlyph ?? string.Empty,
                FontFamily = IconFontFamily,
                Size = IconSize,
                Color = Color ?? Colors.Transparent,
            };

            return new Image
            {
                Source = fontImageSource,
                Margin = GetSpacingAround(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = IconSize,
                HeightRequest = IconSize,
                Aspect = Aspect.AspectFit
            };
        }

        public override void ApplyGestureHandler(View parentView,
            GestureRecognizer customGestureRecognizer)
        {
            parentView.GestureRecognizers.Add(customGestureRecognizer);
        }

        protected virtual Thickness GetSpacingAround()
        {
            if (!SpacingEnabled)
            {
                return default;
            }

            return new Thickness(4);
        }
    }
}