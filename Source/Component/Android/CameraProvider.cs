using Android.App;
using Android.Content;
using Android.Provider;
using Java.IO;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(Rox.CameraProvider))]
namespace Rox
{
    public class CameraProvider
        : ICameraProvider
    {
        private static File CameraPhotoFile;
        private static TaskCompletionSource<ImageSource> CameraTaskCompletionSource;

        public async Task<ImageSource> AcquirePicture()
        {
            ImageSource imageSource = null;
            try
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);

                File pictureDirectory = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "RoxTemp");
                if (!pictureDirectory.Exists())
                {
                    pictureDirectory.Mkdirs();
                }

                CameraPhotoFile = new File(pictureDirectory, $"{Guid.NewGuid()}.jpg");
                intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(CameraPhotoFile));

                CameraTaskCompletionSource = new TaskCompletionSource<ImageSource>();

                //imageSource = ImageSource.FromUri(new System.Uri("https://www.rox.software/images/PrettyPuppy_128_128.png"));

                Activity activity = (Activity)Forms.Context;
                activity.StartActivityForResult(intent, 1);

                //imageSource = ImageSource.FromUri(new System.Uri("https://www.rox.software/images/SnarlingPuppy_128_128.png"));

                imageSource = await CameraTaskCompletionSource.Task;

                //if (imageSource==null)
                //{
                //    imageSource = ImageSource.FromUri(new System.Uri("https://www.rox.software/images/PrettyPuppy_128_128.png"));
                //}
            }
            //catch (Exception exception)
            //{
            //    imageSource = ImageSource.FromUri(new System.Uri("https://www.rox.software/images/SnarlingPuppy_128_128.png"));
            //}
            catch
            {
            }
            return imageSource;
        }

        public static void OnCameraResult(Result resultCode)
        {
            if ((resultCode == Result.Canceled) || (resultCode != Result.Ok))
            {
                CameraTaskCompletionSource.TrySetResult(null);
                return;
            }

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                System.IO.MemoryStream memoryStream;
                using (System.IO.Stream imageStream = ((Activity)Forms.Context).ContentResolver.OpenInputStream(Uri.FromFile(CameraPhotoFile)))
                {
                    memoryStream = new System.IO.MemoryStream();
                    imageStream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                }
                try
                {
                    CameraPhotoFile.Delete();
                }
                catch
                {
                }
                return memoryStream;

                //System.IO.Stream imageStream = ((Activity)Forms.Context).ContentResolver.OpenInputStream(Uri.FromFile(CameraPhotoFile));
                //try
                //{
                //    CameraPhotoFile.Delete();
                //}
                //catch
                //{
                //}
                //return imageStream;
            });

            CameraTaskCompletionSource.TrySetResult(imageSource);
        }
    }
}