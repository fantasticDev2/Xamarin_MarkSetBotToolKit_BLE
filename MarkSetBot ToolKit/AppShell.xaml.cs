using MarkSetBot_ToolKit.ViewModels;
using MarkSetBot_ToolKit.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MarkSetBot_ToolKit
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(DeviceListPage), typeof(DeviceListPage));
        }
    }
}
