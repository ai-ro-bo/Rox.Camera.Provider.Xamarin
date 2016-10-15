using Xamarin.Forms;

namespace Rox
{
    public partial class CameraApplication
        : Application
    {
        public CameraApplication()
        {
            InitializeComponent();

            MainView mainView = new MainView();
            MainViewModel mainViewModel = new MainViewModel();
            mainView.BindingContext = mainViewModel;
            NavigationPage navigationPage = new NavigationPage(mainView);

            MainPage = navigationPage;
        }
    }
}