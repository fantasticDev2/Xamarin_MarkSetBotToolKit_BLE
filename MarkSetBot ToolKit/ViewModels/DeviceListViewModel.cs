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
        private IBluetoothLE ble;
        private IAdapter adapter;
        private bool scanning = false;

        public ObservableCollection<IDevice> bleDevices = new ObservableCollection<IDevice> { };
        public ObservableCollection<IDevice> _bleDevices = new ObservableCollection<IDevice> { };

        public DeviceListViewModel()
        {
            Title = "Device List";
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
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
                        adapter.DeviceDiscovered += (s, a) => {
                            _bleDevices.Add(a.Device);
                            DeviceNames = _bleDevices;
                        };
                        if (!ble.Adapter.IsScanning)
                        {
                            adapter.ScanTimeout = 30000;
                            await adapter.StartScanningForDevicesAsync();
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
