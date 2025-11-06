using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace Gauge
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Comfortaa-VariableFont_wght.ttf", "Comfortaa");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //============
            /* 
            Короче в андроиде есть тема в <Entry/> и в <DatePicker/>под названием "нативное подчеркивание".
            Оно меня заебало и я его убираю.
            */
#if ANDROID
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(
                    Android.Graphics.Color.Transparent);
            });

            DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
                if (handler.PlatformView is AndroidX.AppCompat.Widget.AppCompatEditText platformView)
                {
                    platformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(
                        Android.Graphics.Color.Transparent);
                }
            });
#endif
            //============

            return builder.Build();
        }
    }
}
