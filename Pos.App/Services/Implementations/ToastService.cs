using Pos.App.Services.Interfaces;

namespace Pos.App.Services.Implementations
{
    public class ToastService : IToastService
    {
        public event Action<string, string, ToastType>? OnShow;

        public void ShowSuccess(string message, string title = "Éxito")
        {
            OnShow?.Invoke(title, message, ToastType.Success);
        }

        public void ShowError(string message, string title = "Error")
        {
            OnShow?.Invoke(title, message, ToastType.Error);
        }

        public void ShowWarning(string message, string title = "Advertencia")
        {
            OnShow?.Invoke(title, message, ToastType.Warning);
        }

        public void ShowInfo(string message, string title = "Información")
        {
            OnShow?.Invoke(title, message, ToastType.Info);
        }
    }
}
