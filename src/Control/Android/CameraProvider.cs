using Android.App;
using Android.Content;
using Android.Provider;
using Java.IO;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
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
                Activity activity = Camera.GetActivity();

                File parentFile = activity.CacheDir;
                //File[] mediaDirs = activity.GetExternalMediaDirs();
                //if (mediaDirs.Length > 0)
                //{
                //    parentFile = mediaDirs[0];
                //}
                //else
                //{
                //    parentFile = activity.GetExternalFilesDir(null);
                //}

                File pictureDirectory = new File(parentFile, "Rox");
                if (!pictureDirectory.Exists())
                {
                    _ = pictureDirectory.Mkdirs();
                }

                CameraPhotoFile = new File(pictureDirectory, $"{Guid.NewGuid()}.jpg");

                //Uri photoURI = Uri.FromFile(CameraPhotoFile);
                Uri photoURI = FileProvider.GetUri(CameraPhotoFile);

                Intent intent = new Intent(MediaStore.ActionImageCapture)
                    .AddFlags(ActivityFlags.GrantReadUriPermission)
                    .AddFlags(ActivityFlags.GrantWriteUriPermission)
                    .PutExtra(MediaStore.ExtraOutput, photoURI);

                CameraTaskCompletionSource = new TaskCompletionSource<ImageSource>();

                //imageSource = ImageSource.FromUri(new System.Uri("https://www.rox.software/images/PrettyPuppy_128_128.png"));

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
                _ = CameraTaskCompletionSource.TrySetResult(null);
                return;
            }

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                Activity activity = Camera.GetActivity();
                System.IO.MemoryStream memoryStream;
                using (System.IO.Stream imageStream = activity.ContentResolver.OpenInputStream(Uri.FromFile(CameraPhotoFile)))
                {
                    memoryStream = new System.IO.MemoryStream();
                    imageStream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                }
                try
                {
                    _ = CameraPhotoFile.Delete();
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

            _ = CameraTaskCompletionSource.TrySetResult(imageSource);
        }
    }
}