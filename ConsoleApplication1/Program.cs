using GenericParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using Common;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using YtsLogic;

namespace ConsoleApplication1
{
    class Program
    {
        string data = ":000264000EFE~";
        public static string realTimeData = @":0182520078000000000000001E3F040F2A0F050F3A0F1500000000023E3F00000000000000800F00000000000000000000000000054100410064F7~";
        public static string configurationData = @":01815200EA010F0000A0006D0CE4025A230FA0001E10040CE4781E60AE5F285F28692806400DAC03E80384109A006410040AF000640CE47B0C00C8781E57E401F460AE5F5A282B6E6414196E641419786E000F064000640BB800030DAC00640BB800031194000A0BB800030103E8000300232DF2~";
        public static string confData2 = @":01815200EA010400006400110CE4025A230F3C001E10040BB8213417705F285F28692813880DAC03E803841068006410040A28000A0BB8213400C8200815E001F417705F5A282B6E6414196E641419786E000F01F400640BB8000307D000640BB8000309C4000A0BB800030003E8000300232DBD~";
        public static string realTimeData82_3 = @":038252007E000000000000001E86040F520F260F5A0F3B00000000053C3C3D3D3C00000000000000000F00000000000000000000000000014601EA02BC51~";

        public static string realTimeData82_4 = @":038252007E000000000000001DA7040EC90EC30EEE0ED500000000053D3E3E3D3D00000000000000000F00000000000000000000000000014601A402BC1D~";
        //Address=3 Voltage=15.182 Current=0 Temp=22 SOC=70 DFET=True CFET = True Protection= ChargeState=0 TempState=0 VoltState=0

        public static string realTimeData82_5 = @":058252008A0000000000000030E2070DC90DFF0DFF0DFF0E000E000DFF00000000053E3D3E3D3E00000000000000000F00000000000000000000000000002000AF02BC54~";
        //Address=5 Voltage=25.028 Current=0 Temp=22 SOC=32 DFET=True CFET=True Protection= ChargeState=0 TempState=0 VoltState=0
        static void Main(string[] args)
        {
            #region
            string first = "008A0000000000000030E7070DF80DF90DF90DF90DF90DF90DF900000000053D3D3C3D3D00000000000000000F00000000000000000000000000000F00D202BC39~:04825200840000000000000030DD070DF60DF60";
            string sec = "DF60DF60DF60DF60DF600000000023E3E00000000000000000F00000000000000000000000000001E00EA030CC2~:0A82520084000000000000003";
            string third = "0F1070DFB0DFB0DFA0DFB0DFE0DFD0DFD00000000023E3D00000000000000000F00000000000000000000000000001E00EA030C6D~:098252008A0000000000000030D8070DF50DF3";

            MainLogic logic = new MainLogic();
            logic.HandleParsing(first);
            logic.HandleParsing(sec);
            logic.HandleParsing(third);



            return;
            #endregion

            #region
            //Configuration conf = new Configuration()
            //{
            //    PortName = "COM3",
            //    BaudRate = 9600,
            //    ParityType = Parity.None,
            //    DataBits = 8,
            //    StopBitsType = StopBits.One,
            //    HandShakeType = Handshake.XOnXOff,
            //    ReadTimeout = 500,
            //    WriteTimeout = 500
            //};

            //using (var stream = File.OpenWrite("configuration.json"))
            //{
            //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Configuration));
            //    ser.WriteObject(stream, conf);
            //}


            // PortChat.Start();    
            //#region TESt    
            //var portNames = SerialPort.GetPortNames();

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < portNames.Length; i++)
            //{
            //    sb.Append(i + 1 + ": ");
            //    sb.Append(portNames[i]);
            //}
            //Console.WriteLine(sb.ToString());
            //Console.WriteLine("Please Enter port number :");
            //var input = Console.ReadLine();
            //var choice = int.Parse(input);

            //Console.WriteLine(string.Format("port chosen - {0}, Connecting...", portNames[choice - 1]));
            //System.IO.Ports.SerialPort port = new SerialPort(portNames[choice - 1], 9600, Parity.None, 8, StopBits.One);
            //port.Handshake = Handshake.XOnXOff;

            //Console.WriteLine("Connected !");
            //Console.WriteLine("Press enter to send Data...");
            //Console.ReadLine();
            ////string data = ":000264000EFE~";
            //string data = ":000100000E09~";

            //// Create two different encodings.
            //Encoding ascii = Encoding.ASCII;
            //Encoding unicode = Encoding.Unicode;

            //// Convert the string into a byte array.
            //byte[] unicodeBytes = unicode.GetBytes(data);

            //// Perform the conversion from one encoding to the other.
            //byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            //// Convert the new byte[] into a char[] and then into a string.
            //char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            //ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            //string asciiString = new string(asciiChars);


            //Console.WriteLine("Sending :" + asciiString);

            //port.ReadBufferSize = 500;
            //port.WriteBufferSize = 500;

            //port.ReadTimeout = 5000;
            //port.WriteTimeout = 5000;


            //port.Open();

            //port.DataReceived += Port_DataReceived;

            //port.WriteLine(asciiString);

            //#endregion

            #endregion

            #region
            CommPort port = new CommPort();
            port.InitializePort("COM3");
            port.Open();

            FrameFormat test = new FrameFormat()
            {                
                Address = 4,
                Cmd = (byte)Command.RealTimeData,
                Version = (byte)GenericParser.Version.Version82                
            };

            Console.WriteLine(test.AsString);

            if (port.IsOpen)
            {
                port.SendWrite(test.AsString);
                Thread.Sleep(50);
            }

            //nsole.ReadKey();
            for (int i = 0; i < 100; i++)
            {
                if (port.IsOpen)
                {
                    var sw = Stopwatch.StartNew();
                    sw.Start();
                    var result = port.SendReceive(test.AsString);
                    sw.Stop();
                    Console.WriteLine(sw.ElapsedMilliseconds + " " + result);
                }
                Thread.Sleep(250);
            }
            Console.ReadLine();
            return;
            #endregion

            #region

            RealtimeDataMap_V82 rtm1 = new RealtimeDataMap_V82()
            {
                Current = new ushort[] { 0, 0 },
                SOC = 90,
                TempNum = 2,
                Temp = new byte[] { 68, 72 },
                Vbat = 24000
            };

            RealtimeDataMap_V82 rtm2 = new RealtimeDataMap_V82()
            {
                Current = new ushort[] { 0, 0 },
                SOC = 29,
                TempNum = 2,
                Temp = new byte[] { 55, 65 },
                Vbat = 18000
            };

            var r = (string)GenericParser.GenericParser.Build<RealtimeDataMap_V82>(rtm1);

            FrameFormat b = new FrameFormat()
            {                
                Address = 4,
                Cmd = (byte)Command.RealTimeData,
                Data = r,
                Version = (byte)GenericParser.Version.Version82,
                Length = 138               
            };

            var conf = Common.Configuration.Default;

            conf.Load();
            //Console.ReadKey();
            FrameFormat a3 = GenericParser.GenericParser.Parse<FrameFormat>(realTimeData82_4, DataType.ASCII_HEX);

            FrameFormat a5 = GenericParser.GenericParser.Parse<FrameFormat>(realTimeData82_5, DataType.ASCII_HEX);

            //a.ValidateCRC(realTimeData);

            RealtimeDataMap_V82 r3 = GenericParser.GenericParser.Parse<RealtimeDataMap_V82>(a3.Data);

            RealtimeDataMap_V82 r5 = GenericParser.GenericParser.Parse<RealtimeDataMap_V82>(a5.Data);

            BatteryStatViewModel vm3 = HandleRealTimeData(r3);

            BatteryStatViewModel vm5 = HandleRealTimeData(r5);

            BatteryStatViewModel vm4 = HandleRealTimeData(rtm1);

            BatteryStatViewModel vm6 = HandleRealTimeData(rtm2);

            var vmGroup1 = new List<BatteryStatViewModel>();
            vmGroup1.Add(vm3);
            vmGroup1.Add(vm4);

            var group1 = new SeriesStatViewModel(WindowsFormsSynchronizationContext.Current, "Group1",
            vmGroup1);

            var vmGroup2 = new List<BatteryStatViewModel>();
            vmGroup2.Add(vm5);
            vmGroup2.Add(vm6);

            var group2 = new SeriesStatViewModel(WindowsFormsSynchronizationContext.Current, "Group2",
                vmGroup2);

            group1.UpdateSeriesProperties();
            group2.UpdateSeriesProperties();

            List<SeriesStatViewModel> sgroup = new List<SeriesStatViewModel>();
            sgroup.Add(group1);
            sgroup.Add(group2);

            var cluster = new ClusterStatViewModel(WindowsFormsSynchronizationContext.Current, sgroup);

            cluster.UpdateProperties();

            //:000264000EFE~

            Console.ReadLine();
            return;
            #endregion
            //FrameFormat b = new FrameFormat()
            //{
            //    SOI = ':',
            //    Address = 3,
            //    Cmd = (byte)Command.RealTimeData,                
            //    Version = (byte)GenericParser.Version.Version82,
            //    EOI = '~'
            //};

            //var s = b.AsString;

            //Encoding ascii = Encoding.ASCII;
            //Encoding unicode = Encoding.Unicode;

            //// Convert the string into a byte array.
            //byte[] unicodeBytes = unicode.GetBytes(s);

            //// Perform the conversion from one encoding to the other.
            //byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            //// Convert the new byte[] into a char[] and then into a string.
            //char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            //ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            //string asciiString = new string(asciiChars);




            //using (SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One))
            //{
            //    Task.Run(() =>
            //    {

            //        port.Open();

            //        port.DataReceived += (sender, e) =>
            //        {

            //            var data = port.ReadTo("~");
            //            data += "~";

            //            Debug.WriteLine("Received : " + data);

            //            FrameFormat a1 = GenericParser.GenericParser.Parse<FrameFormat>(data, DataType.ASCII_HEX);

            //            RealtimeDataMap_V82 r1 = GenericParser.GenericParser.Parse<RealtimeDataMap_V82>(a.Data);
            //        };

            //        Debug.WriteLine("Sending : " + asciiString);

            //        port.Write(asciiChars,0,asciiChars.Length);

            //        Thread.Sleep(250);

            //        port.Write(asciiChars, 0, asciiChars.Length);

            //    });

            //    Console.ReadKey();
            //}

        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;


            port.Close();
            //Console.WriteLine("Received : "+ receivedData);
        }

        private static BatteryStatViewModel HandleRealTimeData(RealtimeDataMap_V82 realTimeData)
        {
            BatteryStatViewModel vm = new BatteryStatViewModel(WindowsFormsSynchronizationContext.Current);
            //if (SharedData.Default.BatteryPackContainer.TryGetValue(frame.Address.ToString(), out vm))
            //{
            if (realTimeData.Current[1] != 0 && realTimeData.Current[0] != 0)
            {
                vm.Current = 0;
            }
            else
            {
                vm.Current = realTimeData.Current[0] == 0 ? (realTimeData.Current[1] * (-1)) : realTimeData.Current[0];
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
                //vm.ProtectionBackColor = Color.Transparent;
            }
            else if ((vm.VoltageState & (ushort)VSTATE.VUV) == (ushort)VSTATE.VUV)
            {
                vm.Protection = "Single cell undervoltage";
                //vm.ProtectionBackColor = Color.Red;
            }
            else if ((vm.VoltageState & (ushort)VSTATE.BVUV) == (ushort)VSTATE.BVUV)
            {
                vm.Protection = "Battery pack undervoltage ";
                //vm.ProtectionBackColor = Color.Red;
            }
            else
            {
                vm.Protection = ((VSTATE)vm.VoltageState).ToEnumDescription();
                //vm.ProtectionBackColor = Color.Orange;
            }

            if (vm.ChargeState == 0)
            {
                vm.Protection = string.Empty;
                //vm.ProtectionBackColor = Color.Transparent;
            }
            else
            {
                vm.Protection = ((CSTATE)vm.ChargeState).ToEnumDescription();
                //vm.ProtectionBackColor = Color.Orange;
            }

            if (vm.TemperatureState == 0)
            {
                vm.Protection = string.Empty;
                //vm.ProtectionBackColor = Color.Transparent;
            }
            else
            {
                vm.Protection = ((TSTATE)vm.TemperatureState).ToEnumDescription();
                //vm.ProtectionBackColor = Color.Orange;
            }

            Debug.WriteLine(vm.ToString());
            //}

            return vm;
        }
    }
}

