using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace Rox
{
    [Activity(Label = "Rox Camera Control Harness", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity
        : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Rox.CameraControlAndroid.Initialise(this);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new HarnessApplication());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case 1:
                    {
                        CameraProvider.OnCameraResult(resultCode);
                        break;
                    }
            }
        }
    }
}