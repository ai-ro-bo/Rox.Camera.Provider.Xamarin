using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rox
{
    public interface ICameraProvider
    {
        Task<ImageSource> AcquirePicture();
    }
}