
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
using AndroidX.Lifecycle;
using Microsoft.Maui.Controls;
using Android.OS;


using Android.Media;
using MauiCameraViewSample.Services;
using MauiCameraViewSample.Platforms.Android;
using static AndroidX.Camera.Video.VideoRecordEvent;

[assembly: Dependency(typeof(VideoRecorderService))]
namespace MauiCameraViewSample.Platforms.Android
{
    public class VideoRecorderService : IVideoRecorderService
    {
        private MediaRecorder? _mediaRecorder;
        private VideoRecorderState _state = VideoRecorderState.IsNull;

        public VideoRecorderService()
        {
            _state = VideoRecorderState.IsNull;
            System.Diagnostics.Debug.WriteLine("VideoRecorderService initialized.");
        }

        public void GetReady4Recording(string path)
        {
            if (_state == VideoRecorderState.IsNull)
            {
                if (_mediaRecorder == null)
                {

                    _mediaRecorder = new MediaRecorder(); //(context);

                    // Not using Audio
                    //_mediaRecorder.SetAudioSource(AudioSource.Mic);
                    _mediaRecorder.SetVideoSource(VideoSource.Camera);
                    System.Diagnostics.Debug.WriteLine("Camera source set successfully.");
                    _mediaRecorder.SetOutputFormat(OutputFormat.Mpeg4);
                    _mediaRecorder.SetOutputFile(path);
                    _mediaRecorder.SetVideoEncoder(VideoEncoder.Default);
                    //_mediaRecorder.SetAudioEncoder(AudioEncoder.Default);
                    try
                    {
                        _mediaRecorder.Prepare();
                        _state = VideoRecorderState.Idle;
                        Thread.Sleep(5000); // Wait for the MediaRecorder to prepare
                    }
                    catch (Java.Lang.IllegalStateException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"IllegalStateException: {ex.Message}");
                    }
                    catch (Java.Lang.RuntimeException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"RuntimeException: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }
        }

        public void StartRecording()
        {
            if (_state == VideoRecorderState.Idle)
            {
                if (_mediaRecorder != null)
                {
                    try
                    {
                        _mediaRecorder.Start();
                        _state = VideoRecorderState.Recording;
                    }
                    catch (Java.Lang.IllegalStateException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"IllegalStateException: {ex.Message}");
                        _mediaRecorder = null;
                        _state = VideoRecorderState.IsNull;
                    }
                    catch (Java.Lang.RuntimeException ex)
                    {
                        // Exits here
                        // [MediaRecorder] start failed: -22
                        // [0:] RuntimeException: start failed.
                        System.Diagnostics.Debug.WriteLine($"RuntimeException: {ex.Message}");
                        _mediaRecorder = null;
                        _state = VideoRecorderState.IsNull;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                        _mediaRecorder = null;
                        _state = VideoRecorderState.IsNull;
                    }
                }
            }
        }


        public void PauseRecording()
        {
            if (_state == VideoRecorderState.Recording)
            {
                // Pause functionality is available in Android API 24+
                if (_mediaRecorder != null)
                {
                    _mediaRecorder.Pause();
                    _state = VideoRecorderState.Paused;
                }
            }
        }

        public void ContinueRecording()
        {
            if (_state == VideoRecorderState.Paused)
            {
                // Pause functionality is available in Android API 24+
                if (_mediaRecorder != null)
                {
                    _mediaRecorder.Resume();
                    _state = VideoRecorderState.Recording;
                }
            }
        }

        public void StopRecording()
        {
            if (_state == VideoRecorderState.Recording)
            {
                if (_mediaRecorder != null)
                {
                    _mediaRecorder.Stop();
                    _mediaRecorder.Release();
                    _state = VideoRecorderState.Idle;
                    _mediaRecorder = null;
                    _state = VideoRecorderState.IsNull;
                }
            }
        }
    }

}
