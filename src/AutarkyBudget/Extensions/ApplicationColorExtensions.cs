using SkiaSharp;
using Xamarin.Forms;

namespace AutarkyBudget.Extensions
{
    public static class ApplicationColorExtensions
    {
        public static SKColor GetThemeColor(this Application app, AppColorTypes type)
        {
            SKColor result = default;

            switch (type)
            {
                case AppColorTypes.Primary:
                    result = GetColor(app, new SKColor(33, 150, 243), new SKColor(34, 34, 34));
                    break;
                case AppColorTypes.Secondary:
                    result = GetColor(app, new SKColor(239, 239, 239), new SKColor(68, 68, 68));
                    break;
                case AppColorTypes.Text:
                    result = GetColor(app, new SKColor(18, 18, 18), new SKColor(239, 239, 239));
                    break;
                case AppColorTypes.Accent:
                    result = GetColor(app, new SKColor(150, 209, 255), new SKColor(102, 102, 102));
                    break;
            }

            return result;
        }

        private static SKColor GetColor(Application app, SKColor light, SKColor dark)
        {
            return app.RequestedTheme == OSAppTheme.Light ? light
                 : app.RequestedTheme == OSAppTheme.Dark  ? dark
                 : /*  RequestedTheme == Unspecified */     dark;
        }
    }

    public enum AppColorTypes
    {
        Invalid = 0,
        Primary,
        Secondary,
        Text,
        Accent,
    }
}
