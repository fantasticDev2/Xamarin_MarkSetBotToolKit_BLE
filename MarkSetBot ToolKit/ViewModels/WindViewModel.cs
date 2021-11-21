using System;
using System.Windows.Input;
using MarkSetBot_ToolKit.Helper;
using Xamarin.Forms;
using MarkSetBot_ToolKit.Views;
using Microcharts;
using SkiaSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
//using Entry = Microcharts.Chart.;


namespace MarkSetBot_ToolKit.ViewModels
{
    public class WindViewModel : BaseViewModel
    {
        private BarChart barChart;
        private LineChart lineChart;
        private BarChart barChart1;
        private LineChart lineChart1;

        public WindViewModel()
        {
            Title = "Wind";

            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UpdateCharts();
                });                
                return true;
            });

            List<ChartEntry> entries = new List<ChartEntry>();
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
                    if(AppConfig.speedCharacteristic != null && AppConfig.directionCharacteristic != null)
                    {
                        _ = ReadSpeed();
                        _ = ReadDirection();
                    }
                });
            }
        }

        private async Task ReadSpeed()
        {
            AppConfig.speedCharacteristic.ValueUpdated += (o, args) =>
            {
                var bytes = args.Characteristic.Value;
                AppConfig.speedValues.Enqueue(floatConversion(bytes));
                if(AppConfig.speedValues.Count > 3600)
                {
                    AppConfig.speedValues.Dequeue();
                }

            };
            await AppConfig.speedCharacteristic.StartUpdatesAsync();
        }

        private async Task ReadDirection()
        {
            AppConfig.directionCharacteristic.ValueUpdated += (o, args) =>
            {
                var bytes = args.Characteristic.Value;
                AppConfig.directionValues.Enqueue(floatConversion(bytes));
                if(AppConfig.directionValues.Count > 3600)
                {
                    AppConfig.directionValues.Dequeue();
                }
            };
            await AppConfig.directionCharacteristic.StartUpdatesAsync();
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

        private void UpdateCharts() {
            /*
            if (AppConfig.speedCharacteristic != null) {
                var speedData = await AppConfig.speedCharacteristic.ReadAsync();
            }
            if (AppConfig.directionCharacteristic != null) {
                var directionData = await AppConfig.directionCharacteristic.ReadAsync();
            }
            */
            List<ChartEntry> speedEntries = new List<ChartEntry>();
            List<ChartEntry> directionEntries = new List<ChartEntry>();

            foreach(float value in MiscExtensions.TakeLast<float>(AppConfig.speedValues, 100))
            {
                speedEntries.Add(new ChartEntry(value)
                {
                    Label = "",
                    ValueLabel = value.ToString(),
                    Color = SKColor.Parse("#2c3e50")
                });
            }

            foreach(float value in MiscExtensions.TakeLast<float>(AppConfig.directionValues, 100))
            {
                directionEntries.Add(new ChartEntry(value)
                {
                    Label = "",
                    ValueLabel = value.ToString(),
                    Color = SKColor.Parse("#b455b6")
                });
            }

            BarChart = new BarChart() { Entries = speedEntries };
            LineChart = new LineChart() { Entries = directionEntries };

        }

        private float floatConversion(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes); 
            }
            float myFloat = BitConverter.ToSingle(bytes, 0);
            return myFloat;
        }
    }

    public static class MiscExtensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }
}
