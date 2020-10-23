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

        public static async Task HideLoading()
        {
            UserDialogs.Instance.HideLoading();
            await Task.Delay(TimeSpan.FromMilliseconds(10));
        }
    }
}
