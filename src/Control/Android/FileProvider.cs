namespace Rox
{
    [Android.Content.ContentProvider(new[] { "${applicationId}.fileProvider" }, Name = "rox.fileProvider", Exported = false, GrantUriPermissions = true)]
    [Android.App.MetaData("android.support.FILE_PROVIDER_PATHS", Resource = "@xml/rox_fileprovider_file_paths")]
    public class FileProvider
        : AndroidX.Core.Content.FileProvider
    {
        public static Android.Net.Uri GetUri(Java.IO.File file)
        {
            Android.Content.Context applicationContext = Camera.GetActivity().ApplicationContext;
            string authority = applicationContext.PackageName + ".fileProvider";

            return GetUriForFile(applicationContext, authority, file);
        }
    }
}