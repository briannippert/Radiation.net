namespace Devices;

/// <summary>
/// Interface for controlling and retrieving data from a Geiger counter device
/// </summary>
public interface IGeigerCounter
{
    /// <summary>
    /// Returns the version information of the device
    /// </summary>
    /// <returns>A string containing the device version information</returns>
    string GetDeviceVersion();

    /// <summary>
    /// Retrieves the serial number of the device
    /// </summary>
    /// <returns>A string containing the device serial number</returns>
    String GetSerialNumber();

    /// <summary>
    /// Powers off the device
    /// </summary>
    void PowerOff();

    /// <summary>
    /// Powers on the device
    /// </summary>
    void PowerOn();

    /// <summary>
    /// Performs a device reboot
    /// </summary>
    void Reboot();

    /// <summary>
    /// Gets the current voltage reading from the device
    /// </summary>
    /// <returns>The voltage as a double value</returns>
    double GetVoltage();

    /// <summary>
    /// Gets the current counts per minute (CPM) reading from the device
    /// </summary>
    /// <returns>The CPM as an integer value</returns>
    Int32 GetCpm();
}