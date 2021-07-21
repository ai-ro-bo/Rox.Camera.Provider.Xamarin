using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rox
{
    public class MainViewModel
        : INotifyPropertyChanged
    {
        public string ViewTitle => "Rox Camera Harness";

        private ImageSource _PictureImage = null;
        public ImageSource PictureImage => _PictureImage;

        public ICommand AcquirePictureCommand =>
            new Command(async () =>
            {
                ICameraProvider cameraProvider = DependencyService.Get<ICameraProvider>();

                _PictureImage = await cameraProvider.AcquirePicture();

                OnPropertyChanged(nameof(PictureImage));
            });

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}