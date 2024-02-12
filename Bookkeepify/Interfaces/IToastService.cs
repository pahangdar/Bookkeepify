using Bookkeepify.Services;

namespace Bookkeepify.Interfaces
{
    public interface IToastService
    {
        void ShowToast(string title, string message, ToastType type);
    }
}
