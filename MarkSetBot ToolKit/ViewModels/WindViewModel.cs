using System;
using System.Windows.Input;
using MarkSetBot_ToolKit.Helper;
using Xamarin.Forms;
using MarkSetBot_ToolKit.Views;

namespace MarkSetBot_ToolKit.ViewModels
{
    public class WindViewModel: BaseViewModel
    {
        public WindViewModel()
        {
            Title = "Wind";
        }

        public ICommand SearchDeviceCommand {
            get
            {
                return new DelegateCommand(async (arg) => {
                    await Shell.Current.GoToAsync(nameof(DeviceListPage));
                });
            }
        }
    }
}
