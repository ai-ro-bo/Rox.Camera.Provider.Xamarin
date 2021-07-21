using Android.App;

namespace Rox
{
    public static class Camera
    {
        private static Activity CameraActivity = null;

        public static void Init(Activity cameraActivity)
        {
            CameraActivity = cameraActivity;
        }

        public static Activity GetActivity()
        {
            if (CameraActivity == null) throw new System.NullReferenceException("You must call Init() first.");

            return CameraActivity;
        }
    }
}