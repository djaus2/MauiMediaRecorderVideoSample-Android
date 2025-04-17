using CommunityToolkit.Maui;
using MauiCameraViewSample.Services;
using Microsoft.Extensions.Logging;
using MauiCameraViewSample.Platforms.Android;
[assembly: Dependency(typeof(MauiCameraViewSample.Platforms.Android.VideoRecorderService))]
namespace MauiCameraViewSample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCamera()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<MainPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
#if ANDROID
            builder.Services.AddSingleton<IVideoRecorderService, VideoRecorderService>();
#endif
            return builder.Build();
        }
    }
}
