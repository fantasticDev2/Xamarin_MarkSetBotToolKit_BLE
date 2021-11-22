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
            for(int i = 0; i < 50; i++)
            {
                entries.Add(new ChartEntry(10) {
                    Label = "",
                    ValueLabel = "",
                    Color = SKColor.Parse("#b455b6")
                });
            }
            barChart = new BarChart() { Entries = entries };
            lineChart = new LineChart() { Entries = entries };
            barChart1 = new BarChart { Entries = entries };
            lineChart1 = new LineChart { Entries = entries };

            
        }

        private List<string> filterItems = new List<string>() { "5 minutes", "15 minutes", "30 minutes", "1 hour", "2 hours", "3 hours" };
        public List<string> FilterItems
        {
            get
            {
                return filterItems;
            }
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

                float lastSum = 0;
                List<float> lastItems = MiscExtensions.TakeLast<float>(AppConfig.speedValues, 3).ToList();
                foreach (float value in lastItems)
                {
                    lastSum += value;
                }
                AppConfig.averageSpeedValues.Enqueue(lastSum / lastItems.Count);

                if(AppConfig.speedValues.Count > 3600)
                {
                    AppConfig.speedValues.Dequeue();
                }
                if(AppConfig.averageSpeedValues.Count > 3600)
                {
                    AppConfig.averageSpeedValues.Dequeue();
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

                float lastSum = 0;
                List<float> lastItems = MiscExtensions.TakeLast<float>(AppConfig.directionValues, 3).ToList();
                foreach (float value in lastItems)
                {
                    lastSum += value;
                }
                AppConfig.averageDirectionValues.Enqueue(lastSum / lastItems.Count);

                if (AppConfig.directionValues.Count > 3600)
                {
                    AppConfig.directionValues.Dequeue();
                }
                if (AppConfig.averageDirectionValues.Count > 3600)
                {
                    AppConfig.averageDirectionValues.Dequeue();
                }
            };
            await AppConfig.directionCharacteristic.StartUpdatesAsync();
        }

        private string deviceConnectText = "Connect";

        public ICommand DeviceConnect
        {
            get
            {
                return new DelegateCommand(async (args) => {
                    try
                    {
                        //AppConfig.
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
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


        private int speedSelectedIndex;
        public int SpeedSelectedIndex
        {
            get
            {
                return speedSelectedIndex;
            }
            set
            {
                if(speedSelectedIndex != value)
                {
                    speedSelectedIndex = value;
                    OnPropertyChanged(nameof(SpeedSelectedIndex));
                    updateSpeedChart(speedSelectedIndex);
                }                
            }
        }

        private int directionSelectedIndex;
        public int DirectionSelectedIndex
        {
            get
            {
                return directionSelectedIndex;
            }
            set
            {
                if(directionSelectedIndex != value)
                {
                    directionSelectedIndex = value;
                    OnPropertyChanged(nameof(DirectionSelectedIndex));
                    // Update Direction chart
                }
            }
        }

        private int speedAverageSelectedIndex;
        public int SpeedAverageSelectedIndex
        {
            get
            {
                return speedAverageSelectedIndex;
            }
            set
            {
                if (speedAverageSelectedIndex != value)
                {
                    speedAverageSelectedIndex = value;
                    OnPropertyChanged(nameof(SpeedAverageSelectedIndex));
                    // Update Average speed chart
                }
            }
        }

        private int directionAverageSelectedIndex;
        public int DirectionAverageSelectedIndex
        {
            get
            {
                return directionAverageSelectedIndex;
            }
            set
            {
                if (directionAverageSelectedIndex != value)
                {
                    directionAverageSelectedIndex = value;
                    OnPropertyChanged(nameof(DirectionAverageSelectedIndex));
                    // Update Average direction chart
                }
            }
        }

        private void updateSpeedChart(int selectedIndex)
        {
            List<ChartEntry> speedEntries = new List<ChartEntry>();
            int timeCount = 0;
            if(selectedIndex == 0)
            {
                timeCount = 5 * 60;
            }
            else if(selectedIndex == 1)
            {
                timeCount = 15 * 60;
            }
            else if(selectedIndex == 2)
            {
                timeCount = 30 * 60;
            }
            foreach (float value in MiscExtensions.TakeLast<float>(AppConfig.speedValues, timeCount))
            {
                speedEntries.Add(new ChartEntry(value)
                {
                    Label = "",
                    ValueLabel = value.ToString(),
                    Color = SKColor.Parse("#2c3e50")
                });
            }
            BarChart = new BarChart() { Entries = speedEntries };
        }

        public ICommand DirectionSelectedIndexChanged
        {
            get
            {
                // Updates speed chart only
                return new DelegateCommand(async (arg) => {

                });
            }
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
