namespace Pos.App.Services.Interfaces
{
    public interface IToastService
    {
        event Action<string, string, ToastType>? OnShow;
        void ShowSuccess(string message, string title = "Éxito");
        void ShowError(string message, string title = "Error");
        void ShowWarning(string message, string title = "Advertencia");
        void ShowInfo(string message, string title = "Información");
    }

    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }
}
