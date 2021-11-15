using MarkSetBot_ToolKit.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MarkSetBot_ToolKit.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}