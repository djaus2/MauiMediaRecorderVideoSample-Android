using Android.Media;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MauiCameraViewSample.Platforms.Android;
using MauiCameraViewSample.Services;
using Microsoft.Maui.Media;

namespace MauiCameraViewSample
{
    public partial class MainPage : ContentPage
{

    private readonly IVideoRecorderService? _videoRecorderService;

    public MainPage(IVideoRecorderService videoRecorderService)
    {
        InitializeComponent();
        _videoRecorderService = videoRecorderService;
    }



        string? path = "";
        private void Button_StartRecording_Clicked(object sender, EventArgs e) //, Android.OS.Environment AndroidEnvironment)
        {
            string? directory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies)?.AbsolutePath;
            if (string.IsNullOrEmpty(directory))
            {
                // Handle the error
                return;
            }
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            path = Path.Combine(directory, "video.mp4");

            StartVideoRecording(path);
        }

        public void StartVideoRecording(string res)
        {
            _videoRecorderService?.StartRecording(res);
            
        }

        private void Button_PauseRecording_Clicked(object sender, EventArgs e)
        {
            _videoRecorderService?.PauseRecording();
        }

        private void Button_ContinueRecording_Clicked(object sender, EventArgs e)
        {
            _videoRecorderService?.ContinueRecording();
        }

        private void Button_StopRecordingClicked(object sender, EventArgs e)
        {
            _videoRecorderService?.StopRecording();
        }


    }


}
