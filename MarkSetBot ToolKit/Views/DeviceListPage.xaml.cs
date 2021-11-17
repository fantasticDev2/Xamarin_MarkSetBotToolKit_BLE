using MarkSetBot_ToolKit.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MarkSetBot_ToolKit.Views
{
    public partial class DeviceListPage : ContentPage
    {
        DeviceListViewModel _viewModel;

        public DeviceListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DeviceListViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
