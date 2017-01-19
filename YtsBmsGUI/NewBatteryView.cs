using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YtsBmsGUI
{
    public partial class NewBatteryView : Form
    {
        public IEnumerable<byte> Address { get; set; }

        public bool IsPartOfCluster { get; set; }
        public NewBatteryView()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            //byte addr;

            var inputText = textBox_address.Text;

            var addresses = ParseRange(inputText);

            if(addresses.Count() == 0)
            {
                MessageBox.Show("Error, please insert address in correct form (0-16)");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            //if(byte.TryParse(textBox_address.Text,out addr))
            //{
            Address = addresses;
            IsPartOfCluster = checkBox1.Checked;
            this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    MessageBox.Show("Error, please insert address in correct form (0-16)");
            //    this.DialogResult = DialogResult.Cancel;
            //}

            this.Close();            
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        static IEnumerable<byte> ParseRange(string str)
        {
            foreach (string s in str.Split(','))
            {
                // try and get the number
                byte num;
                if (byte.TryParse(s, out num))
                {
                    yield return num;
                    continue; // skip the rest
                }

                // otherwise we might have a range
                // split on the range delimiter
                string[] subs = s.Split('-');
                int start, end;

                // now see if we can parse a start and end
                if (subs.Length > 1 &&
                    int.TryParse(subs[0], out start) &&
                    int.TryParse(subs[1], out end) &&
                    end >= start)
                {
                    // create a range between the two values
                    int rangeLength = end - start + 1;
                    foreach (byte i in Enumerable.Range(start, rangeLength))
                    {
                        yield return i;
                    }
                }
            }
        }
    }
}
