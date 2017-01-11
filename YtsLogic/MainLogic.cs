﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.IO;
using Common;
using System.Diagnostics;
using NLog;
using GenericParser;
using System.Threading;
using System.Drawing;

namespace YtsLogic
{
    public class MainLogic : IDisposable
    {
        SerialPort serialPort = new SerialPort();
        IEventAggregator evAgg;
        System.Timers.Timer samplingTimer;
        EventQueue<string> incommingQueue;

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public MainLogic()
        {
            evAgg = Common.EventAggregatorProvider.EventAggregator;
            incommingQueue = new EventQueue<string>(HandleParsing);
        }

        public void Initialize(string portName)
        {
            try
            {
                if (File.Exists("configuration.json"))
                {
                    Configuration.Default.Load();

                    InitializePort(portName);

                    /*toolStripLabel1.Text =*/
                    var msg =  new ToolStripMessage()
                    {
                        Text = string.Format("Connecting : {0} {1} {2}", serialPort.PortName,
                        serialPort.BaudRate, serialPort.Parity)
                    };

                    evAgg.PublishOnUIThread(msg);


                    serialPort.DataReceived += SerialPort_DataReceived;
                    //serialPort.PinChanged += SerialPort_PinChanged;
                    //serialPort.ErrorReceived += SerialPort_ErrorReceived;

                    serialPort.Open();

                    if (serialPort.IsOpen)
                    {

                        evAgg.PublishOnUIThread(new ToolStripMessage()
                        {
                            Text = string.Format("Connected : {0} {1} {2}", serialPort.PortName,
                        serialPort.BaudRate, serialPort.Parity)
                        });

                        //toolStripLabel1.Text = string.Format("Connected : {0} {1} {2}", serialPort.PortName,
                        //serialPort.BaudRate, serialPort.Parity);

                        evAgg.PublishOnUIThread(new PortConnected());

                        samplingTimer = new System.Timers.Timer(Configuration.Default.SamplingTimerIntervalMilisec);
                        samplingTimer.Elapsed += SamplingTimer_Elapsed;
                        samplingTimer.Enabled = true;
                        samplingTimer.Start();
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
                else
                {
                    evAgg.PublishOnUIThread(new ToolStripMessage()
                    {
                        Text = string.Format("Could not connect, missing coonfiguration file")
                    });

                    //toolStripLabel1.Text = string.Format("Could not connect, missing coonfiguration file");
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                //MessageBox.Show("Error, " + e.Message);
                evAgg.PublishOnUIThread(new MessageBoxMessage()
                {
                    Message = e.Message,
                    Title = "Error"
                });

                //this.Close();
                //this.Dispose();
            }

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var message = serialPort.ReadTo("~") + "~";

                Debug.WriteLine(message);
                logger.Trace("Received : {0}", message);

                incommingQueue.Add(message);

            }
            catch (TimeoutException ex)
            {
                Debug.WriteLine(ex.Message);
                logger.Warn(ex, "Time out exeption");
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
                logger.Error(ex, "IO Expetion");
            }
        }

        private void HandleParsing(string message)
        {
            try
            {
                FrameFormat frame = GenericParser.GenericParser.Parse<FrameFormat>(message);

                if(null == frame)
                {
                    return;
                }

                var subStr = message.TrimStart(new char[] { frame.SOI }).TrimEnd(new char[] { frame.EOI });

                subStr = subStr.Substring(0, subStr.Length - 2);

                var CRCResultStr = FrameFormat.CalculateCRC(subStr);

                byte CalculatedCRC = byte.Parse(CRCResultStr,System.Globalization.NumberStyles.HexNumber);

                if(CalculatedCRC != frame.CRC)
                {
                    logger.Error("CRC not compatible, original message = " + message);
                    return;
                }

                logger.Trace(frame.ToStringWithAtt<ParserDefinitionAttribute>());

                switch (frame.Version)
                {
                    case (byte)GenericParser.Version.Version82:
                        ParseCommand(frame);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //evAgg.PublishOnUIThread(new MessageBoxMessage() { Title = "Error", Message = ex.Message });
                //MessageBox.Show(this, ex.Message, "Error");
                logger.Error(ex, "Error parsing incomming message");
            }
        }

        private void InitializePort(string portName)
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

        private void SamplingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                foreach (var item in SharedData.Default.BatteryPackContainer)
                {
                    if (item.Key == "cluster")
                    {
                        continue;
                    }

                    FrameFormat realTimeCmd = new FrameFormat()
                    {
                        SOI = ':',
                        Address = Convert.ToByte(item.Key),
                        Cmd = 2,
                        Version = 82,
                        EOI = '~'
                    };

                    if (serialPort.IsOpen)
                    {
                        //ring data = ":000252000EFE~";

                        string data = realTimeCmd.AsString;
                        // Create two different encodings.
                        Encoding ascii = Encoding.ASCII;
                        Encoding unicode = Encoding.Unicode;

                        // Convert the string into a byte array.
                        byte[] unicodeBytes = unicode.GetBytes(data);

                        // Perform the conversion from one encoding to the other.
                        byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

                        // Convert the new byte[] into a char[] and then into a string.
                        char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                        ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                        string asciiString = new string(asciiChars);

                        Debug.WriteLine("Sending " + asciiString);
                        logger.Trace("Sending {0}", asciiString);

                        serialPort.Write(asciiChars, 0, asciiChars.Length);

                        Thread.Sleep(Configuration.Default.WaitTimePeriodBetweenCommandSendMilliSec);
                    }


                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error sending message to pack");
            }
        }

        private void ParseCommand(FrameFormat frame)
        {
            try
            {
                switch (frame.Cmd)
                {
                    case (byte)CommandResponse.RealTimeData:
                        RealtimeDataMap_V82 realTimeData = GenericParser.GenericParser.Parse<RealtimeDataMap_V82>(frame.Data);

                        if(null ==realTimeData)
                        {
                            return;
                        }

                        HandleRealTimeData(frame, realTimeData);

                        logger.Trace(realTimeData.ToStringWithAtt<ParserDefinitionAttribute>());

                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error parsing realtime data");
            }
        }

        private void HandleRealTimeData(FrameFormat frame, RealtimeDataMap_V82 realTimeData)
        {
            try
            {
                BatteryStatViewModel vm;
                if (SharedData.Default.BatteryPackContainer.TryGetValue(frame.Address.ToString(), out vm))
                {
                    UpdateViewModel(realTimeData, vm);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error updateing view model");
            }
        }

        private static void UpdateViewModel(RealtimeDataMap_V82 realTimeData, BatteryStatViewModel vm)
        {
            if (realTimeData.Current[1] != 0 && realTimeData.Current[0] != 0)
            {
                vm.Current = 0;
            }
            else
            {
                vm.Current = realTimeData.Current[0] == 0 ? realTimeData.Current[1] : realTimeData.Current[0];
            }

            vm.SOC = realTimeData.SOC;
            vm.Temperature = realTimeData.Temp.Max();
            vm.Voltage = realTimeData.Vbat;
            vm.CFET = (realTimeData.FETState & (byte)FETSTATE.CFET) == (byte)FETSTATE.CFET;
            vm.DFET = (realTimeData.FETState & (byte)FETSTATE.DFET) == (byte)FETSTATE.DFET;
            vm.ChargeState = realTimeData.CState;
            vm.TemperatureState = realTimeData.TState;
            vm.VoltageState = realTimeData.VState;

            if (vm.VoltageState == 0)
            {
                vm.Protection = string.Empty;
                vm.ProtectionBackColor = Color.Transparent;
            }
            else if ((vm.VoltageState & (ushort)VSTATE.VUV) == (ushort)VSTATE.VUV)
            {
                vm.Protection = "Single cell undervoltage";
                vm.ProtectionBackColor = Color.Red;
            }
            else if ((vm.VoltageState & (ushort)VSTATE.BVUV) == (ushort)VSTATE.BVUV)
            {
                vm.Protection = "Battery pack undervoltage ";
                vm.ProtectionBackColor = Color.Red;
            }
            else
            {
                vm.Protection = ((VSTATE)vm.VoltageState).ToEnumDescription();
                vm.ProtectionBackColor = Color.Orange;
            }

            if (vm.ChargeState == 0)
            {
                vm.Protection = string.Empty;
                vm.ProtectionBackColor = Color.Transparent;
            }
            else
            {
                vm.Protection = ((CSTATE)vm.ChargeState).ToEnumDescription();
                vm.ProtectionBackColor = Color.Orange;
            }

            if (vm.TemperatureState == 0)
            {
                vm.Protection = string.Empty;
                vm.ProtectionBackColor = Color.Transparent;
            }
            else
            {
                vm.Protection = ((TSTATE)vm.TemperatureState).ToEnumDescription();
                vm.ProtectionBackColor = Color.Orange;
            }

            Debug.WriteLine(vm.ToString());
            logger.Trace(vm.ToStringAllProperties());
        }

        public void Dispose()
        {
            if (samplingTimer != null)
            {
                samplingTimer.Stop();
                samplingTimer.Enabled = false;
                samplingTimer.Dispose();
            }

            if (serialPort != null)
            {
                serialPort.Close();
                serialPort.Dispose();
            }
        }
    }
}