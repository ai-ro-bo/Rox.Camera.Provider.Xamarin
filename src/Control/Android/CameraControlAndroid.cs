using Android.App;

namespace Rox
{
    public static class CameraControlAndroid
    {
        private static Activity CameraActivity = null;

        public static void Initialise(Activity cameraActivity)
        {
            CameraActivity = cameraActivity;
        }

        public static Activity GetActivity()
        {
            if (CameraActivity == null) throw new System.NullReferenceException("You must call Initialise first.");

            return CameraActivity;
        }
    }
}