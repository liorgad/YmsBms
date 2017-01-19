using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericParser
{
    public enum FETSTATE : byte
    {
        DFET = 1, //discharge on/off status，1 means on，0 means off
        CFET = 2, //charge on/off status, 1 means on
        SDFET =4, // discharge on/off，1 means on，0 means off
        SCFET =8, // charge on/off, 1 means on
        DFET_DAMAGE  = 16, //discharge MOS status，1 means damaged
        CFET_DAMAGE = 32, //charge MOS status ，1 means damaged
        CCFET = 64, //reserved，1 means on
    };

    public enum ALARM : ushort
    {
        VoltWarn = 1,//bit0:1; //voltage warning, dropout voltage protection, disconnection protection
        ChgFETDmg =2,// uint16_t bit1:1; //charge fet damage warning
        SDErr = 4, //uint16_t bit2:1; //SD ERR 1,error 0,normal
        ML523Comm = 8,//uint16_t SPI_ERR:1; //ML5238 communication
        E2PROMErr = 16,//uint16_t E2PROM_ERR:1; //external storage: E2PROM ERR 1,error 0,normal
        Unusued = 32,//uint16_t bit5:1; //reserved
        FCCUpdate = 64, //uint16_t FCC_UPDATING:1; //charge study turn on status
        DchgStat = 128 //uint16_t FCC_DSGLEARN:1; // discharge study turn on status
    };

    public enum VSTATE : ushort
    {
        [Description("single cell overvoltage")]
        [DefaultValue(typeof(Color),"Red")]
        VOV =1,//uint16_t VOV:1; //single cell overvoltage
        [Description("single cell undervoltage")]
        [DefaultValue(typeof(Color), "Orange")]
        VUV = 2,//uint16_t VUV:1; //single cell undervoltage
        [Description("battery pack overvoltage")]
        [DefaultValue(typeof(Color), "Red")]
        BVOV = 4,//uint16_t BVOV:1//battery pack overvoltage
        [Description("battery pack undervoltage")]
        [DefaultValue(typeof(Color), "Orange")]
        BVUV =8,//uint16_t BVUV:1; //battery pack undervoltage
        [Description("single cell overvoltage warning value")]
        wVOV =16,//uint16_t wVOV:1; //single cell overvoltage warning value
        [Description("single cell undervoltage warning value")]
        wVUV =32,//uint16_t wVUV:1; //single cell undervoltage warning value
        [Description("battery pack overvoltage warning value")]
        wBVOV =64,//uint16_t wBVOV:1; //battery pack overvoltage warning value
        [Description("battery pack undervoltage warning value")]
        wBVUV =128,//uint16_t wBVUV:1; //battery pack undervoltage warning value
        [Description("dropout voltage protection")]
        [DefaultValue(typeof(Color), "Red")]
        VDIFF =256,//uint16_t VDIFF:1; //dropout voltage protection
        [Description("disconnection")]
        [DefaultValue(typeof(Color), "Red")]
        VBREAK =512,//uint16_t VBREAK:1; //disconnection
        [Description("low voltage，prohibit charging")]
        [DefaultValue(typeof(Color), "Red")]
        CSGDIS =1024//uint16_t CSGDIS:1; //low voltage，prohibit charging
    };
    public enum CSTATE
    {
        [Description("charge status")]
        [DefaultValue(typeof(Color), "Green")]
        CING =1,//uint16_t CING:1; //charge status
        [Description("discharge status")]
        [DefaultValue(typeof(Color), "Green")]
        DING = 2,//uint16_t DING:1; //discharge status
        [Description("over-current charge")]
        [DefaultValue(typeof(Color), "Red")]
        OCCSG = 4,//uint16_t OCCSG:1; //over-current charge
        [Description("short-circuit protection")]
        [DefaultValue(typeof(Color), "Red")]
        SHORT = 8,//uint16_t SHORT:1; //short-circuit protection
        [Description("over-current discharge first-grade")]
        [DefaultValue(typeof(Color), "Red")]
        OCDSG1 = 16,//uint16_t OCDSG1:1; //over-current discharge first-grade
        [Description("over-current discharge second-class")]
        [DefaultValue(typeof(Color), "Red")]
        OCDSG2 = 32,//uint16_t OCDSG2:1;//over-current discharge second-class
        [Description("charge current warning value")]
        wOCCSG = 64,//uint16_t wOCCSG:1; //charge current warning value
        [Description("discharge current warning value")]
        wOCDSG = 128//uint16_t wOCDSG:1; //discharge current warning value
    };

    public enum TSTATE : ushort
    {
        [Description("charge high temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TCELL_CSGH = 1,//uint16_t TCELL_CSGH:1; //charge high temperature
        [Description("charge low temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TCELL_CSGL = 2,//uint16_t TCELL_CSGL:1; //charge low temperature
        [Description("discharge high temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TCELL_DSGH = 4,//uint16_t TCELL_DSGH:1; //discharge high temperature
        [Description("discharge low temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TCELL_DSGL = 8,//uint16_t TCELL_DSGL:1; //discharge low temperature
        [Description("environment high temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TENV_H = 16,//uint16_t TENV_H:1; //environment high temperature
        [Description("environment low temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TENV_L = 32,//uint16_t TENV_L:1; //environment low temperature
        [Description("power high temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TFET_H = 64,//uint16_t TFET_H:1; //power high temperature
        [Description("power low temperature")]
        [DefaultValue(typeof(Color), "Red")]
        TFET_L = 128,//uint16_t TFET_L:1; //power low temperature
        [Description("battery cell high temperature warning")]
        wTCELL_H = 256,//uint16_t wTCELL_H:1; //battery cell high temperature warning
        [Description("battery cell low temperature warning")]
        wTCELL_L = 512,//uint16_t wTCELL_L:1; // battery cell low temperature warning
        [Description("environment high temperature warning")]
        wTENV_H = 1024,//uint16_t wTENV_H:1; // environment high temperature warning
        [Description("environment low temperature warning")]
        wTENV_L = 2048,//uint16_t wTENV_L:1; // environment low temperature warning
        [Description("power high temperature warning")]
        wTFET_H = 4096,//uint16_t wTFET_H:1; // power high temperature warning
        [Description("power low temperature warning")]
        wTFET_L = 8192//uint16_t wTFET_L:1; // power low temperature warning
    };
    public class RealtimeDataMap_V82
    {
        [ParserDefinition(1, 14)]
        public ulong Time_t { get; set; } //expressed in seconds, to the number of seconds 1970-01-01 00:00:00 UTC 0 time zone

        [ParserDefinition(2, 4)]
        public ushort Vbat { get; set; } //battery voltage output is 0.5 times of the total voltage

        [ParserDefinition(3, 2)]
        public byte VCell_num { get; set; } //Vcell[16] in mV, a string of all voltages by order in ASCII

        [ParserDefinition(4, 4, "VCell_num")]
        public ushort[] VCell { get; set; } //Vcell[16] in mV, a string of all voltages by order in ASCII

        [ParserDefinition(5, 8)]
        public ushort[] Current { get; set; } //Current[0] = CHG current[10mA]
                                            //Current[1] = DSG Current[10mA]
                                            //In ASCII

        [ParserDefinition(6, 2)]
        public byte TempNum { get; set; } //Number of temp sensors(m)

        [ParserDefinition(7, 2 ,"TempNum")]
        public byte[] Temp { get; set; } //In 1°C + 40°C in ASCII

        [ParserDefinition(8,4)]
        public ushort VState { get; set; }

        [ParserDefinition(9, 4)]
        public ushort CState { get; set; }

        [ParserDefinition(10, 4)]
        public ushort TState { get; set; }

        [ParserDefinition(11, 4)]
        public ushort Alarm { get; set; }

        [ParserDefinition(12, 2)]
        public byte FETState { get; set; }

        [ParserDefinition(13, 4)]
        public ushort Warn_VOV { get; set; }

        [ParserDefinition(14, 4)]
        public ushort Warn_VUV { get; set; }

        [ParserDefinition(15, 4)]
        public ushort Warn_VHigh { get; set; }

        [ParserDefinition(16, 4)]
        public ushort Warn_VLow { get; set; }

        [ParserDefinition(17, 4)]
        public ushort BalanceState { get; set; }

        [ParserDefinition(18, 4)]
        public ushort DchgNum { get; set; }

        [ParserDefinition(19, 4)]
        public ushort ChgNum { get; set; }

        [ParserDefinition(20, 2)]
        public byte SOC { get; set; }

        [ParserDefinition(21, 4)]
        public ushort CapNow { get; set; }

        [ParserDefinition(22, 4)]
        public ushort CapFull { get; set; }

        //[ParserDefinition(7, 4)]
        //public byte WorkState { get; set; } //Status of battery:
        //                                    //uint16 CING: 1; // state of charge, charging 1
        //                                    //uint16 DING: 1; // discharging state
        //                                    //uint16 VoltH: 1; // overvoltage protection, 1, alarm
        //                                    //uint16 VoltL: 1; // Over-discharge protection
        //                                    //uint16 CurrC: 1; // charge overcurrent
        //                                    //uint16 CurrS: 1; // short-circuit protection
        //                                    //uint16 CurrD1: 1; // discharge overcurrent 1
        //                                    //uint16 CurrD2: 1; // discharge overcurrent 2
        //                                    //uint16 TempCH: 1; // charging temperature
        //                                    //uint16 TempCL: 1; // charging low
        //                                    //uint16 TempDH: 1; // discharge temperature
        //                                    //uint16 TempDL: 1; // low temperature discharge
        //                                    //uint16 DFET: 1; // DFET state 1, open
        //                                    //uint16 CFET: 1; // CFET state
        //                                    //uint16 SDFET: 1; // DFET switch, 1, open
        //                                    //uint16 SCFET: 1; // CFET switch

        //[ParserDefinition(8, 4)]
        //public byte Alarm { get; set; } //Alarm information

        //[ParserDefinition(9, 4)]
        //public byte BalanceState { get; set; } //Balance state for V0 to V15

        //[ParserDefinition(10, 4)]
        //public byte DchgNum { get; set; } //Number of discharge events

        //[ParserDefinition(11, 4)]
        //public byte ChgNum { get; set; } //Number of charge events

        //[ParserDefinition(12, 2)]
        //public byte pEnergy { get; set; } //Remaining Capacity 

        public string AsString { get { return ToString(); } }

        public override string ToString()
        {
            string tempStr = (string)GenericParser.Build<RealtimeDataMap_V82>(this);

            if (string.IsNullOrWhiteSpace(tempStr))
            {
                return null;
            }

            return tempStr;
        }

    }
}
