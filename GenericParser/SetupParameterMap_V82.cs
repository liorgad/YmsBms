using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericParser
{
    public class SetupParameterMap_V82
    {
        /// <summary>
        /// No.of Cells from 5~16
        /// </summary>
        [ParserDefinition(0, 1)]
        public byte CellNum { get; set; }

        [ParserDefinition(1, 1)]
        public byte Addr { get; set; } //BMS RS485 address 1~255
        [ParserDefinition(2, 1)]
        public byte EngDesign { get; set; } //Design capacity 1~100 Ah

        [ParserDefinition(3, 2)]
        public ushort Rsense { get; set; } //Sense Resistor value in 0.01mΩ

        [ParserDefinition(4, 2)]
        public ushort Vref { get; set; } //Reference Voltage

        [ParserDefinition(5, 1)]
        public byte PsaveEn { get; set; } //Saving Mode 0~1

        [ParserDefinition(6, 1)]
        public byte BMode { get; set; } //Balancing Mode 0~2
                                        //0x00 = no Balancing
                                        //0x01 = balancing during CHG
                                        //0x02 = Balance During rest
        [ParserDefinition(7, 2)]
        public byte BSVolt { get; set; } //Balancing Starting Voltage[mV]

        [ParserDefinition(8, 2)]
        public byte BDVolt { get; set; } //Min.Balancing Voltage Delta[mV]

        [ParserDefinition(9, 2)]
        public byte VHpVal { get; set; } //Over Voltage TH[mV]

        [ParserDefinition(10, 2)]
        public byte VHdelay { get; set; } //Over Voltage Delay[ms]

        [ParserDefinition(11, 2)]
        public byte VHrVal { get; set; } //Over Voltage Recovery[mV]

        [ParserDefinition(12, 2)]
        public byte VLpVAL { get; set; } //Under Voltage TH[mV]

        [ParserDefinition(13, 2)]
        public byte VLdelay { get; set; } //Under Voltage Delay[ms]

        [ParserDefinition(14, 2)]
        public byte VLrVAL { get; set; } //Under Voltage Recovery[mV]

        [ParserDefinition(15, 1)]
        public byte CSpVAL { get; set; } //Short circuit Voltage 0~3 – 100mV – 400mV

        [ParserDefinition(16, 2)]
        public byte CCpVAL { get; set; }

        //Charge Over-Current Protection TH[10mA]
        [ParserDefinition(17, 2)]
        public byte CCdelay { get; set; }

        //Charge Over-Current Protection Delay[10ms]

        [ParserDefinition(18, 2)]
        public byte CDpVAL1 { get; set; }

        //Discharge Over-Current Protection TH 1 [10mA]

        [ParserDefinition(19, 2)]
        public byte CDdelay1 { get; set; }

        //Discharge Over-Current Protection delay 1 [10ms]

        [ParserDefinition(20, 2)]
        public byte CDpVAL2 { get; set; }

        //Discharge Over-Current Protection TH 2 [10mA]

        [ParserDefinition(21, 2)]
        public byte CDdelay2 { get; set; }

        //Discharge Over-Current Protection delay 2 [10ms]
        [ParserDefinition(22, 1)]
        public byte TCHpVAL { get; set; }

        //Charging Temp High TH[-40°C]
        [ParserDefinition(23, 1)]
        public byte TCHrVAL { get; set; }

        //Charging Temp High Rec[-40°C]
        [ParserDefinition(24, 1)]
        public byte TCLpVAL { get; set; }

        ///Charging Temp Low TH[-40°C]

        [ParserDefinition(25, 1)]
        public byte TCLrVAL { get; set; }

        //Charging Temp Low Rec[-40°C]

        [ParserDefinition(26, 1)]
        public byte TDHpVAL { get; set; }

        //Discharging Temp High TH[-40°C]
        [ParserDefinition(27, 1)]
        public byte TDHrVAL { get; set; }

        //Discharging Temp High Rec[-40°C]

        [ParserDefinition(28, 1)]
        public byte TDLpVAL { get; set; }

        //Discharging Temp Low TH[-40°C]

        [ParserDefinition(29, 1)]
        public byte TDLrVAL { get; set; }

        //Discharging Temp Low Rec[-40°C]

        [ParserDefinition(30, 1)]
        public byte Clr { get; set; }

        //Restore Default parameters 0~1
        //0 – Modification, 1 - Recovery
    }
}
