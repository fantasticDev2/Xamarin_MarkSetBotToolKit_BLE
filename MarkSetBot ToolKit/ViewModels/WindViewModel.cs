using System;
using System.Windows.Input;
using MarkSetBot_ToolKit.Helper;
using Xamarin.Forms;
using MarkSetBot_ToolKit.Views;
using Microcharts;
using SkiaSharp;
using System.Threading.Tasks;
//using Entry = Microcharts.Chart.;


namespace MarkSetBot_ToolKit.ViewModels
{
    public class WindViewModel: BaseViewModel
    {
        private BarChart barChart;
        private LineChart lineChart;
        private BarChart barChart1;
        private LineChart lineChart1;

        public WindViewModel()
        {
            Title = "Wind";

            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await UpdateCharts();
                });                
                return true;
            });

            var entries = new[]
             {
                 new ChartEntry(212)
                 {
                     Label = "UWP",
                     ValueLabel = "212",
                     Color = SKColor.Parse("#2c3e50")
                 },
                 new ChartEntry(248)
                 {
                     Label = "Android",
                     ValueLabel = "248",
                     Color = SKColor.Parse("#77d065")
                 },
                 new ChartEntry(128)
                 {
                     Label = "iOS",
                     ValueLabel = "128",
                     Color = SKColor.Parse("#b455b6")
                 },
                 new ChartEntry(514)
                 {
                     Label = "Shared",
                     ValueLabel = "514",
                     Color = SKColor.Parse("#3498db")
            } };
            barChart = new BarChart() { Entries = entries };
            lineChart = new LineChart() { Entries = entries };
            barChart1 = new BarChart { Entries = entries };
            lineChart1 = new LineChart { Entries = entries };
        }

        public ICommand SearchDeviceCommand {
            get
            {
                return new DelegateCommand(async (arg) => {
                    await Shell.Current.GoToAsync(nameof(DeviceListPage));
                });
            }
        }

        public BarChart BarChart
        {
            get
            {
                return barChart;
            }
            set
            {
                barChart = value;
                OnPropertyChanged();
            }
        }

        public LineChart LineChart
        {
            get
            {
                return lineChart;
            }
            set
            {
                lineChart = value;
                OnPropertyChanged();
            }
        }

        public BarChart BarChart1
        {
            get
            {
                return barChart1;
            }
            set
            {
                barChart1 = value;
                OnPropertyChanged();
            }
        }

        public LineChart LineChart1
        {
            get
            {
                return lineChart1;
            }
            set
            {
                lineChart1 = value;
                OnPropertyChanged();
            }
        }

        private async Task UpdateCharts() {
            if (AppConfig.speedCharacteristic != null) {
                var speedData = await AppConfig.speedCharacteristic.ReadAsync();
            }
            if (AppConfig.directionCharacteristic != null) {
                var directionData = await AppConfig.directionCharacteristic.ReadAsync();
            }
        }
    }
}
