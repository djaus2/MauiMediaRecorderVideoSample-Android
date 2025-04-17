# .NET MAUI Media Recorder Sample - Android
Sample code to test using MediaRecorder to record video. Fails at StartRecording.


```
  //Context context = Android.App.Application.Context;
  // CA1422: This call site is reachable on: 'Android' 35 or later.
  // GHCopilot:This is a warning that the code may not work on older Android versions.
  // 'MediaRecorder is obsoleted on 'Android' 32 or later.
  /*
    An API that's marked with ObsoletedOSPlatformAttribute is called from a
    call site that's marked as supporting the obsoleted operating system (OS).
    This rule is similar to CA1416: Validate platform compatibility 
    except that it warns about APIs that are obsolete on a given platform 
    versus unsupported entirely.
  */
```

>  When I try to start video the phone beeps and I get the message as above. I note though that the file is created

Fails at this point:
```cs
  _mediaRecorder.Prepare();
  _mediaRecorder.Start();  <-- Fails here
```

Error Message:

```
  [MediaRecorder] start failed: -22
  [0:] RuntimeException: start failed.
```

Its a ```Java.Lang.RuntimeException```

GitHub Copilot Siggests:

The beep typically signals that the camera hardware is locked or unavailable. While the file creation confirms that the recorder was partially prepared, it seems like the camera source wasn't properly acquired before recording started. This may happen due to:
1. Another application or process using the camera.
2. A race condition where the camera isn't fully initialized before _mediaRecorder.Start() is called.

Reset the phone before running app. No change, so not 1.
2. Put a 5 second delay, still fails:

```
[0:] RuntimeException: start failed.
[MediaRecorder] start failed: -22
[Choreographer] Skipped 472 frames!  The application may be doing too much work on its main thread.
```

And a longer delay

```
[MediaRecorder] start failed: -22
[Choreographer] Skipped 2466 frames!  The application may be doing too much work on its main thread.
[HWUI] Davey! duration=27374ms; Flags=0, FrameTimelineVsyncId=181826,
```




