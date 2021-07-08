using Foundation;
using UIKit;

namespace Rox
{
    [Register("AppDelegate")]
    public partial class AppDelegate
        : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Rox.CameraControlApple.Initialise();

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new HarnessApplication());

            return base.FinishedLaunching(app, options);
        }
    }
}