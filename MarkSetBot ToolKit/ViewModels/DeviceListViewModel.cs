using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MarkSetBot_ToolKit.Helper;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;


namespace MarkSetBot_ToolKit.ViewModels
{    
    public class DeviceListViewModel : BaseViewModel
    {
        public ContentPage CurrentPage;
        private bool scanning = false;

        public ObservableCollection<IDevice> bleDevices = new ObservableCollection<IDevice> { };
        public ObservableCollection<IDevice> _bleDevices = new ObservableCollection<IDevice> { };

        public DeviceListViewModel(ContentPage currentPage)
        {
            Title = "Device List";
            CurrentPage = currentPage;
        }

        public ObservableCollection<IDevice> DeviceNames
        {
            get => bleDevices;
            set
            {
                bleDevices = value;
                OnPropertyChanged();
            }
        }

        public bool Scanning
        {
            get
            {
                return scanning;
            }
            set
            {
                scanning = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchDevice
        {
            get
            {
                return new DelegateCommand(async (args) => {
                    try
                    {
                        bleDevices.Clear();
                        Scanning = true;
                        AppConfig.adapter.DeviceDiscovered += (s, a) => {
                            _bleDevices.Add(a.Device);
                            DeviceNames = _bleDevices;
                        };
                        if (!AppConfig.ble.Adapter.IsScanning)
                        {
                            AppConfig.adapter.ScanTimeout = 30000;
                            await AppConfig.adapter.StartScanningForDevicesAsync();
                            Scanning = false;
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                });
            }
        }

        public ICommand ConnectDevice
        {
            get
            {
                return new DelegateCommand(async (object sender) => {
                    try
                    {
                        IDevice device = bleDevices[bleDevices.IndexOf((IDevice)sender)];
                        await AppConfig.adapter.ConnectToDeviceAsync(device);
                        AppConfig.connectedDevice = device;

                        AppConfig.speedService = await AppConfig.connectedDevice.GetServiceAsync(Guid.Parse(AppConfig.speed_service_guid));
                        AppConfig.speedCharacteristic = await AppConfig.speedService.GetCharacteristicAsync(Guid.Parse(AppConfig.speed_characteristic_guid));

                        AppConfig.directionService = await AppConfig.connectedDevice.GetServiceAsync(Guid.Parse(AppConfig.direction_service_guid));
                        AppConfig.directionCharacteristic = await AppConfig.directionService.GetCharacteristicAsync(Guid.Parse(AppConfig.direction_characteristic_guid));
                        await CurrentPage.Navigation.PopAsync();
                        
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                });
            }
        }
    }
}
