using Foundation;
using System;
using UIKit;

namespace Rox
{
    internal static class Camera
    {
        static UIImagePickerController picker;
        static Action<NSDictionary> _callback;

        static void Initialise()
        {
            if (picker != null)
                return;

            picker = new UIImagePickerController
            {
                Delegate = new CameraDelegate()
            };
        }

        class CameraDelegate : UIImagePickerControllerDelegate
        {
            public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
            {
                var cb = _callback;
                _callback = null;

                picker.DismissViewController(true, (Action)null);
                cb(info);
            }

            public override void Canceled(UIImagePickerController picker)
            {
                var cb = _callback;
                _callback = null;

                picker.DismissViewController(true, (Action)null);
                cb(null);
            }
        }

        public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
        {
            Initialise();
			
			picker.SourceType = UIImagePickerControllerSourceType.Camera;
			//picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
			picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Video;
            try
            {
                picker.SourceType = UIImagePickerControllerSourceType.Camera;
            }
            catch
            {
                //Note: Allow Simulator to use PictureLibrary when Camera is not available
				
				//picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
				picker.SourceType = UIImagePickerControllerSourceType.VideoLibrary;
            }
            _callback = callback;
            parent.PresentViewController((UIViewController)picker, true, (Action)null);
        }

        public static void SelectPicture(UIViewController parent, Action<NSDictionary> callback)
        {
            Initialise();
			
            //picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            picker.SourceType = UIImagePickerControllerSourceType.VideoLibrary;
			
            _callback = callback;
            parent.PresentViewController((UIViewController)picker, true, (Action)null);
        }
    }
}