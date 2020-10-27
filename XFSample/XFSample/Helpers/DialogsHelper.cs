using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace XFSample.Helpers
{
    public static class DialogsHelper
    {
        public static async Task ShowLoading(string titulo = "")
        {
            UserDialogs.Instance.ShowLoading(titulo);
            await Task.Delay(TimeSpan.FromMilliseconds(10));
        }

        public static async Task ShowAlert(string titulo, string message, string okText = "Ok")
        {
            await UserDialogs.Instance.AlertAsync(message, titulo, okText);
        }

        public static async Task ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
            await Task.Delay(TimeSpan.FromMilliseconds(10));
        }

        public static async Task<bool> ShowConfirm(string title, string message)
        {
            return await UserDialogs.Instance.ConfirmAsync(message, title, "Ok", "Cancelar");
        }

        public static async Task HideLoading()
        {
            UserDialogs.Instance.HideLoading();
            await Task.Delay(TimeSpan.FromMilliseconds(10));
        }
    }
}
