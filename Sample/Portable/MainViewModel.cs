using Rox;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rox
{
    public class MainViewModel
        : INotifyPropertyChanged
    {
        public string ViewTitle
        {
            get
            {
                return "Rox Camera Sample";
            }
        }

        private ImageSource _PictureImage = null;
        public ImageSource PictureImage
        {
            get
            {
                return _PictureImage;
            }
        }

        public ICommand AcquirePictureCommand
        {
            get
            {
                return new Command(async () =>
                {
                    ICameraProvider cameraProvider = DependencyService.Get<ICameraProvider>();

                    _PictureImage = await cameraProvider.AcquirePicture();

                    OnPropertyChanged(nameof(PictureImage));
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}