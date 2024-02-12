using Bookkeepify.Interfaces;

namespace Bookkeepify.Services
{

    public class ToastService : IToastService
    {
        // Event for emitting toast messages
        public event Action<string, ToastType> OnShowToast;

        public void ShowError(string message)
        {
            OnShowToast?.Invoke(message, ToastType.Error);
        }

        public void ShowSuccess(string message)
        {
            OnShowToast?.Invoke(message, ToastType.Success);
        }

        public void ShowWarning(string message)
        {
            OnShowToast?.Invoke(message, ToastType.Warning);
        }

        public void ShowInfo(string message)
        {
            OnShowToast?.Invoke(message, ToastType.Info);
        }

        void IToastService.ShowToast(string title, string message, ToastType type)
        {
            throw new NotImplementedException();
        }
    }
    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }
}
