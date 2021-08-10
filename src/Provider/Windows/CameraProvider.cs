using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Rox.CameraProvider))]
namespace Rox
{
    public class CameraProvider
        : ICameraProvider
    {
        public async Task<ImageSource> AcquirePicture()
        {
            ImageSource imageSource = null;
            try
            {
                CameraCaptureUI cameraCaptureUI = new CameraCaptureUI();
                cameraCaptureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;
                cameraCaptureUI.PhotoSettings.AllowCropping = false;
                //cameraCaptureUI.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;

                StorageFile captureFile = await cameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (captureFile != null)
                {
                    Stream captureStream = await captureFile.OpenStreamForReadAsync();

                    imageSource = ImageSource.FromStream(() =>
                    {
                        return captureStream;
                    });
                }
            }
            catch
            {
            }
            return imageSource;
        }
    }
}