using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class CommPort
    {
        SerialPort serialPort;
        
        public bool IsOpen
        {
            get
            {
                return serialPort != null ? serialPort.IsOpen : false;
            }
        }        

        public CommPort()
        {

        }      

        public async Task<string> SendReceive(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(data);
            if(serialPort.IsOpen)
            {
                Stream strm = serialPort.BaseStream;

                await strm.WriteAsync(bytes, 0, bytes.Length);
                byte[] buffer = new byte[1024];
                var bytesRead = await serialPort.BaseStream.ReadAsync(buffer, 0, buffer.Length);
                byte[] resultBuffer = new byte[bytesRead];
                Buffer.BlockCopy(buffer, 0, resultBuffer, 0, bytesRead);
                return Encoding.ASCII.GetString(resultBuffer);
            }

            return null;
        }

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        public void InitializePort(string portName)
        {
            if (serialPort != null)
            {
                serialPort.PortName = portName;
                serialPort.BaudRate = Configuration.Default.BaudRate;
                serialPort.Parity = Configuration.Default.ParityType;
                serialPort.DataBits = Configuration.Default.DataBits;
                serialPort.StopBits = Configuration.Default.StopBitsType;
                serialPort.Handshake = Configuration.Default.HandShakeType;
                serialPort.ReadTimeout = Configuration.Default.ReadTimeout;
                serialPort.WriteTimeout = Configuration.Default.WriteTimeout;
            }
        }

        public void Open()
        {
            serialPort.Open();
        }
    }
}
