using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Devices.Classes
{
    /// <summary>
    /// Represents a COM port device and handles serial communication
    /// </summary>
    public class COMDevice : IDisposable
    {
        private SerialPort _serialPort;
        private bool _isConnected;

        public bool IsConnected => _isConnected;

        /// <summary>
        /// Creates new COM device instance
        /// </summary>
        /// <param name="portName">Name of the COM port</param>
        /// <param name="baudRate">Baud Rate</param>
        /// <param name="parity">Parity</param>
        /// <param name="dataBits">Data Bits</param>
        /// <param name="stopBits">Stop Bits</param>
        public COMDevice(string portName, int baudRate = 9600, Parity parity = Parity.None,
            int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            _serialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = baudRate,
                Parity = parity,
                DataBits = dataBits,
                StopBits = stopBits,
                ReadTimeout = 10000,
                WriteTimeout = 10000
            };
        }

        /// <summary>
        /// Opens Connection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Open()
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    _isConnected = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                _isConnected = false;
                throw new Exception($"Error opening COM port: {ex.Message}");
            }
        }

        /// <summary>
        /// Closes Connection
        /// </summary>
        public void Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                _isConnected = false;
            }
        }

        /// <summary>
        /// Writes String Data
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public void WriteData(string data)
        {
            if (!_serialPort.IsOpen)
                throw new InvalidOperationException("COM port is not open");

            try
            {
                _serialPort.Write("<" + data + ">>");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing to COM port: {ex.Message}");
            }
        }

        /// <summary>
        /// Writes Byte Data
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public void WriteData(byte[] data)
        {
            if (!_serialPort.IsOpen)
                throw new InvalidOperationException("COM port is not open");

            try
            {
                _serialPort.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing to COM port: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads Line
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public string ReadLine()
        {
            if (!_serialPort.IsOpen)
                throw new InvalidOperationException("COM port is not open");

            try
            {
                return _serialPort.ReadLine();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading from COM port: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads Bytes
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public byte[] ReadBytes(int count)
        {
            if (!_serialPort.IsOpen)
                throw new InvalidOperationException("COM port is not open");

            try
            {
                byte[] buffer = new byte[count];
                _serialPort.Read(buffer, 0, count);
                return buffer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading from COM port: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Close();
            _serialPort?.Dispose();
        }

        // Helper method to get available ports
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}