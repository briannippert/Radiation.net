using System.Net;
using System.Text;
using Devices.Classes;

namespace Devices;
//C# Implementation of https://www.gqelectronicsllc.com/download/GQ-RFC1201.txt
class GMC300E : IGeigerCounter
{
    private COMDevice _device;
    public GMC300E()
    {
        bool connected = false;
        List<string> attemptedDevices = new List<string>();
        string[] availablePorts = COMDevice.GetAvailablePorts();
        foreach (string device in availablePorts)
        {
            attemptedDevices.Add(device);
            try
            {
                var comDevice = new COMDevice(device, 57600);
                comDevice.Open();
                comDevice.WriteData("GETVER");
                var response = comDevice.ReadBytes(100);
                if (Encoding.Default.GetString(response).Contains("GMC-300E"))
                {
                    connected = true;
                    _device = comDevice;
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        if (connected == false)
        {
            throw new Exception(
                $"Unable to Connect to GMC-300E\nTried connecting to:\n{string.Join(" ", attemptedDevices)}\n");
        }
    }
    
    ///<inheritdoc />
    public string GetDeviceVersion()
    {
        _device.WriteData("GETVER");
        var response = _device.ReadBytes(100);
        return Encoding.Default.GetString(response);
    }
    ///<inheritdoc />
    public String GetSerialNumber()
    {
        _device.WriteData("GETSERIAL");
        var response = _device.ReadBytes(7);
        return Convert.ToHexString(response);
    }
    ///<inheritdoc />
    public void PowerOff()
    {
        _device.WriteData("POWEROFF");
    }
    ///<inheritdoc />
    public void PowerOn()
    {
        _device.WriteData("POWERON");
    }
    ///<inheritdoc />
    public void Reboot()
    {
        _device.WriteData("REBOOT");
    }
    ///<inheritdoc />
    public double GetVoltage()
    {
        _device.WriteData("GETVOLT");
        var response = _device.ReadBytes(1);
        return response[0] /10.0;
    }
    ///<inheritdoc />
    public Int32 GetCpm()
    {
        _device.WriteData("GETCPM");
        var response = _device.ReadBytes(2);
        UInt16 cpm = (UInt16)((response[0] << 8) | response[1]);
        return cpm;
    }

    static void Main(string[] args)
    {
        var _gmc300 = new GMC300E();
        Console.WriteLine("Device Version: " + _gmc300.GetDeviceVersion());
        Console.WriteLine("Serial Number: " + _gmc300.GetSerialNumber());
        Console.WriteLine("Voltage: " + _gmc300.GetVoltage() + "V");
        Console.WriteLine("CPM: " + _gmc300.GetCpm());
    }
}