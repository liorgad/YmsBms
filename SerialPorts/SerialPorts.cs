using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SerialPorts
{
    public class SerialPorts : IDisposable, ISerialPorts
    {       
        public string SelectedPort { get; private set; }
        public bool IsConnected { get; private set; }

        public byte[] Send(byte[] data)
        {
            return null;
        }

        public string Send(string data)
        {
            return null;
        }

        public Task<byte[]> SendAsync(byte[] data)
        {
            return null;
        }

        public Task<string> SendAsync(string data)
        {
            return null;
        }

        public string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public bool Connect(string port)
        {
            if (SerialPort.GetPortNames().Contains(port))
            {
                SelectedPort = port;
                var serPort = new SerialPort(port);
                serPort.
            }

            else
            {

            }

            return false;
        }

        public bool Disconnect()
        {
            return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
