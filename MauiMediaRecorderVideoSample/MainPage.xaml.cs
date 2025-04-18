using Android.Content;
using Android.Media;
using Android.Provider;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Java.IO;
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
        private void Button_GetReady4Recording(object sender, EventArgs e) //, Android.OS.Environment AndroidEnvironment)
        {
            /*
             * string? directory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies)?.AbsolutePath;
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
            */

            // Get the external storage directory for movies as suggested by GitHub Copilot
            ContentResolver? resolver = Android.App.Application.Context.ContentResolver;
            if(resolver == null)
            {
                // Handle error
                System.Diagnostics.Debug.WriteLine("ContentResolver is null");
                return;
            }
            ContentValues values = new ContentValues();
            values.Put(MediaStore.Video.Media.InterfaceConsts.DisplayName, "video.mp4");
            values.Put(MediaStore.Video.Media.InterfaceConsts.MimeType, "video/mp4");
            values.Put(MediaStore.Video.Media.InterfaceConsts.RelativePath, "Movies/");
            if(MediaStore.Video.Media.ExternalContentUri==null)
            {
                // Handle error
                System.Diagnostics.Debug.WriteLine("ExternalContentUri is null");
                return;
            }
            Android.Net.Uri? uri = resolver.Insert(MediaStore.Video.Media.ExternalContentUri, values);
            if (uri == null)
            {
                // Handle error
                System.Diagnostics.Debug.WriteLine("Uri is null");
                return;
            }

            FileDescriptor? fileDescriptor = resolver.OpenFileDescriptor(uri, "w").FileDescriptor;
            if (fileDescriptor == null)
            {
                // Handle error
                System.Diagnostics.Debug.WriteLine("FileDescriptor is null");
                return;
            }

            GetReady4Recording(fileDescriptor);
        }

        public void GetReady4Recording(FileDescriptor res)
        {
            _videoRecorderService?.GetReady4Recording(res);

        }

        private void Button_StartRecording_Clicked(object sender, EventArgs e)
        {
            _videoRecorderService?.StartRecording();
            
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
