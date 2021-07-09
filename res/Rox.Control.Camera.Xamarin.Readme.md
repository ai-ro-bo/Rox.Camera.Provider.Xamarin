### Rox Camera Control for Xamarin.Forms

---

Acquire images with the device camera in Xamarin.Forms with the Rox Camera Control. Supports Android, iOS, and UWP.

The CameraProvider component uses native platform components.

---

In your Android project "MainActivity" code file, you must call "CameraControlAndroid.Initialise(Activity)" before "Xamarin.Forms.Forms.Init()". It should look something like:

```csharp
    CameraControlAndroid.Initialise(this);

    global::Xamarin.Forms.Forms.Init();

    LoadApplication(new MyCameraApplication());
```

In your iOS project "AppDelegate" code file, you must call "CameraControlApple.Initialise()" before "Xamarin.Forms.Forms.Init()". It should look something like:

```csharp
    CameraControlApple.Initialise();

    global::Xamarin.Forms.Forms.Init();

    LoadApplication(new MyCameraApplication());
```

---

The Rox Camera Control has the following methods:

```csharp
    Task<ImageSource> AcquirePicture(); //Take a picture with the device camera
```

---
Source code and test harness are available at: https://github.com/ai-ro-bo/Rox.Control.Camera.Xamarin
