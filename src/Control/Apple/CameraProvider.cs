using Foundation;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Rox.CameraProvider))]
namespace Rox
{
    public class CameraProvider
        : ICameraProvider
    {
        public Task<ImageSource> AcquirePicture()
        {
            TaskCompletionSource<ImageSource> taskCompletionSource = new TaskCompletionSource<ImageSource>();
            try
            {
                CameraController.TakePicture(UIApplication.SharedApplication.KeyWindow.RootViewController, (imagePickerResult) =>
                {
                    if (imagePickerResult == null)
                    {
                        _ = taskCompletionSource.TrySetResult(null);
                    }
                    else
                    {
                        UIImage pictureImage = imagePickerResult.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                        NSData pictureData = pictureImage.AsPNG();

                        ImageSource imageSource = ImageSource.FromStream(pictureData.AsStream);

                        _ = taskCompletionSource.TrySetResult(imageSource);
                    }
                });
            }
            catch
            {
            }
            return taskCompletionSource.Task;
        }
    }
}