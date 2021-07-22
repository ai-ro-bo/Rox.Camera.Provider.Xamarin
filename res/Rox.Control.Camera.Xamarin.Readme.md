## Rox Camera Control for Xamarin.Forms

| Site | [rox.tools/control/camera](https://rox.tools/control/camera/) |
| NuGet | [nuget.org/packages/Rox.Xamarin.Camera](https://www.nuget.org/packages/Rox.Xamarin.Camera/) |
| Source | [github.com/ai-ro-bo/Rox.Control.Camera.Xamarin](https://github.com/ai-ro-bo/Rox.Control.Camera.Xamarin) |

---

Acquire images with the device camera in Xamarin.Forms with the Rox Camera Control. Supports Android, iOS, and UWP.

The CameraProvider component uses native platform components.

---

### Android

In your Android project "MainActivity" code file, you must call "Rox.Camera.Init(Activity)" before "Xamarin.Forms.Forms.Init()". It should look something like:

```csharp
    global::Rox.Camera.Init(this);

    global::Xamarin.Forms.Forms.Init();

    LoadApplication(new MyCameraApplication());
```

### iOS

In your iOS project "AppDelegate" code file, you must call "CameraControlApple.Initialise()" before "Xamarin.Forms.Forms.Init()". It should look something like:

```csharp
    global::Rox.Camera.Init();

    global::Xamarin.Forms.Forms.Init();

    LoadApplication(new MyCameraApplication());
```

---

The Rox Camera Control has the following methods:

```csharp
    Task<ImageSource> AcquirePicture(); //Take a picture with the device camera
```

---
