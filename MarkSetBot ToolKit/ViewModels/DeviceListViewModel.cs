using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MarkSetBot_ToolKit.Helper;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;


namespace MarkSetBot_ToolKit.ViewModels
{    
    public class DeviceListViewModel : BaseViewModel
    {
        private IBluetoothLE ble;
        private IAdapter adapter;

        public ObservableCollection<String> deviceNames = new ObservableCollection<string> { "Device 1", "Device 2"};

        public DeviceListViewModel(IBluetoothLE ble, IAdapter adapter)
        {
            Title = "Device List";
            this.ble = ble;
            this.adapter = adapter;
        }

        public ObservableCollection<string> DeviceNames
        {
            get => deviceNames;
            set
            {
                deviceNames = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchDevice
        {
            get
            {
                return new DelegateCommand(async (args) => {
                    var scanner = CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
                    {
                        // do something with it
                        // the scanresult contains the device, RSSI, and advertisement packet

                    });

                    scanner.Dispose();
                });
            }
        }
    }
}
