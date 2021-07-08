using Foundation;
using System;
using UIKit;

namespace Rox
{
    internal static class Camera
    {
        private static readonly UIImagePickerController CameraPicker;
        private static Action<NSDictionary> CameraCallback;

        static Camera()
        {
            CameraPicker = new UIImagePickerController
            {
                Delegate = new CameraDelegate()
            };
        }

        private class CameraDelegate
            : UIImagePickerControllerDelegate
        {
            public override void FinishedPickingMedia(UIImagePickerController cameraPicker, NSDictionary info)
            {
                Action<NSDictionary> cameraCallback = CameraCallback;
                CameraCallback = null;

                cameraPicker.DismissViewController(true, null);
                cameraCallback(info);
            }

            public override void Canceled(UIImagePickerController cameraPicker)
            {
                Action<NSDictionary> cameraCallback = CameraCallback;
                CameraCallback = null;

                cameraPicker.DismissViewController(true, null);
                cameraCallback(null);
            }
        }

        public static void TakePicture(UIViewController parent, Action<NSDictionary> cameraCallback)
        {
            CameraPicker.SourceType = UIImagePickerControllerSourceType.Camera;
            CameraPicker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
            try
            {
                CameraPicker.SourceType = UIImagePickerControllerSourceType.Camera;
            }
            catch
            {
                //Note: Allow Simulator to use PictureLibrary when Camera is not available

                CameraPicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            }
            CameraCallback = cameraCallback;
            parent.PresentViewController(CameraPicker, true, null);
        }

        public static void SelectPicture(UIViewController parent, Action<NSDictionary> cameraCallback)
        {
            CameraPicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

            CameraCallback = cameraCallback;
            parent.PresentViewController(CameraPicker, true, null);
        }
    }
}