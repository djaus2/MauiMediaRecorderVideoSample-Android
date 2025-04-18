using Android.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.Camera.Video;
using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using Android.Content;
using AndroidX.Core.Content;
using Android.OS;
using AndroidX.Lifecycle;

namespace MauiCameraViewSample.Services
{
    enum VideoRecorderState
    {
        IsNull,
        Idle,
        Recording,
        Paused
    }
    public interface IVideoRecorderService
    {
        void GetReady4Recording(string path);
        void StartRecording();
        void PauseRecording();

        void ContinueRecording();
        void StopRecording();
    }

}
