using System;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace MarkSetBot_ToolKit.Helper
{
    public static class AppConfig
    {
        public static IBluetoothLE ble = CrossBluetoothLE.Current;
        public static IAdapter adapter = CrossBluetoothLE.Current.Adapter;
        public static IDevice connectedDevice;

        public static IService speedService;
        public static IService directionService;

        public static ICharacteristic speedCharacteristic;
        public static ICharacteristic directionCharacteristic;

        public static string speed_service_guid = "0000181A-0000-1000-8000-00805F9B34FB";
        public static string speed_characteristic_guid = "00002A70-0000-1000-8000-00805F9B34FB";

        public static string direction_service_guid = "0000181A-0000-1000-8000-00805F9B34FB";
        public static string direction_characteristic_guid = "00002A71-0000-1000-8000-00805F9B34FB";
    }
}
