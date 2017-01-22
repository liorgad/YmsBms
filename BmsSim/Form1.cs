using Common;
using GenericParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmsSim
{
    public partial class Form1 : Form, ICommPort
    {
        RealtimeDataMap_V82 rt;
        FrameFormat frameFormat;
        public Form1()
        {
            InitializeComponent();

            rt = new RealtimeDataMap_V82()
            {
                Alarm = 0,
                BalanceState = 0,
                CapFull = 700,
                CapNow = 175,
                ChgNum = 0,
                CState = 0,
                Current = new ushort[] { 0, 0 },
                DchgNum = 0,
                FETState = 15,
                SOC = 32,
                Temp = new byte[] { 62, 61, 62, 61, 62 },
                TempNum = 5,
                Time_t = 0,
                TState = 0,
                Vbat = 12514,
                VCell = new ushort[] { 3529, 3583, 3583, 3583, 3584, 3584, 3583 },
                VCell_num = 7,
                VState = 0,
                Warn_VHigh = 0,
                Warn_VLow = 0,
                Warn_VOV = 0,
                Warn_VUV = 0
            };

            frameFormat = new FrameFormat()
            {
                Address = 0,
                Cmd = (byte)CommandResponse.RealTimeData,
                Version = (byte)GenericParser.Version.Version82               
            };

        }

        public bool IsOpen
        {
            get
            {
                return true;
            }
        }

        public void InitializePort(string portName)
        {
            Debug.WriteLine("Comm Port = " + portName);
        }

        public void Open()
        {

        }

        public string SendReceive(string data)
        {
            Debug.WriteLine("Pack Received : " + data);

            var ff = GenericParser.GenericParser.Parse<FrameFormat>(data);


            frameFormat.Address = ff.Address;


            RealtimeDataMap_V82 newRt = propertyGrid1.SelectedObject as RealtimeDataMap_V82;

            var rtAsStr = newRt.AsString;

            // //if(!batteries.ContainsKey(ff.Address.ToString()))
            // //{
            //     
            //     //batteries.Add(ff.Address.ToString(),rt);                
            //// }

            // //foreach (var item in batteries)
            // //{
            //    
            // // }            

            frameFormat.Data = rtAsStr;

            // //return @":058252008A0000000000000030E2070DC90DFF0DFF0DFF0E000E000DFF00000000053E3D3E3D3E00000000000000000F00000000000000000000000000002000AF02BC54~";                     
            var str = frameFormat.AsString;
            return str;

            // Debug.WriteLine("Pack Send : " + str);            

        }

        public void SendWrite(string data)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = rt;
        }
    }
}
