using Caliburn.Micro;
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
    public class CommPort : IDisposable
    {
        SerialPort serialPort;
        IEventAggregator evAgg;
        private ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        
        public bool IsOpen
        {
            get
            {
                return serialPort != null ? serialPort.IsOpen : false;
            }
        }        

        public CommPort()
        {
            evAgg = EventAggregatorProvider.EventAggregator;
            serialPort = new SerialPort();
            Configuration.Default.Load();
        }      

        public string SendReceive(string data)
        {

            SendWrite(data);

            if (serialPort.IsOpen)
            {
                //var bytes = Encoding.ASCII.GetBytes(data);
                //Stream strm = serialPort.BaseStream;
                //await strm.WriteAsync(bytes, 0, bytes.Length);
                byte[] buffer = new byte[1024];
                Thread.Sleep(50);
                var bytesRead = serialPort.BaseStream.Read(buffer, 0, buffer.Length);
                byte[] resultBuffer = new byte[bytesRead];
                Buffer.BlockCopy(buffer, 0, resultBuffer, 0, bytesRead);
                return Encoding.ASCII.GetString(resultBuffer);
            }

            return null;
        }

        public void SendWrite(string data)
        {
            locker.EnterWriteLock();
            var bytes = Encoding.ASCII.GetBytes(data);
            if (serialPort.IsOpen)
            {
                Stream strm = serialPort.BaseStream;
                strm.Write(bytes, 0, bytes.Length);
            }
            Thread.Sleep(200);
            locker.ExitWriteLock();
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
                //serialPort.DataBits = Configuration.Default.DataBits;
                //serialPort.StopBits = Configuration.Default.StopBitsType;
                //serialPort.Handshake = Configuration.Default.HandShakeType;
                //serialPort.ReadTimeout = Configuration.Default.ReadTimeout;
                //serialPort.WriteTimeout = Configuration.Default.WriteTimeout;
            }
        }

        public void Open()
        {
            serialPort.Open();

            if (serialPort.IsOpen)
            {
                evAgg.PublishOnUIThread(new ToolStripMessage()
                {
                    Text = string.Format("Connected : {0} {1} {2}", serialPort.PortName,
                    serialPort.BaudRate, serialPort.Parity)
                    //Text = string.Format("Connected : {0}", portName)
                });
            }
        }

        public void Dispose()
        {
            if (null != serialPort)
            {
                serialPort.Dispose();
            }
            else
            {
                //toolStripLabel1.Text = string.Format("Not Connected");

                evAgg.PublishOnUIThread(new ToolStripMessage()
                {
                    Text = string.Format("Not Connected")
                });
            }
        }

        public void Close()
        {
            serialPort.Close();
        }
    }
}
