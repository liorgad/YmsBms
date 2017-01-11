using Common;
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
    public partial class ClusterView : Form
    {
        private List<string> BatteriesAddresses;
        public List<string> SelectedBatteriesGroup1 { get; private set; }
        public List<string> SelectedBatteriesGroup2 { get; private set; }

        public bool IsSerialConnected { get; private set; }

        public ClusterView(List<string> addresses)
        {
            InitializeComponent();

            BatteriesAddresses = addresses;

            SelectedBatteriesGroup1 = new List<string>();
            SelectedBatteriesGroup2 = new List<string>();
            //listBox_allPacks.DisplayMember = "Address";
            //listBox_series1.DisplayMember = "Address";
            //listBox_series2.DisplayMember = "Address";           

        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            IsSerialConnected = radioButton_serial.Checked;
            if (IsSerialConnected)
            {
                if (listBox_series1.Items.Count != listBox_series2.Items.Count)
                {
                    MessageBox.Show(this, "The 2 Series must contain the same number of battery packes", "Error");
                    return;
                }
                else
                {
                    SelectedBatteriesGroup1 = listBox_series1.Items.Cast<string>().ToList();
                    SelectedBatteriesGroup2 = listBox_series2.Items.Cast<string>().ToList();
                }
            }
            else
            {
                //if (listBox_series1.Items.Count > 0) && listBox_series2.Items.Count == 0 ||
                //    listBox_series1.Items.Count == 0 && listBox_series2.Items.Count > 0)
                //{
                    if (listBox_series1.Items.Count > 0)
                    {
                        SelectedBatteriesGroup1 = listBox_series1.Items.Cast<string>().ToList();
                    }
                    else
                    {
                        MessageBox.Show(this, "No items selected for parallel connection", "Error");
                        return;
                    }
                    //else
                    //{
                    //    SelectedBatteriesGroup2 = listBox_series2.Items.Cast<string>().ToList();
                    //}
                //}
            } 

            this.DialogResult = DialogResult.OK;
            this.Close();                            
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ClusterView_Load(object sender, EventArgs e)
        {
            listBox_allPacks.BeginUpdate();

            listBox_allPacks.Items.Clear();

            foreach (var item in BatteriesAddresses)
            {
                listBox_allPacks.Items.Add(item);
            }

            listBox_allPacks.EndUpdate();
        }

        private void button_addToSeries1_Click(object sender, EventArgs e)
        {
            var item = listBox_allPacks.SelectedItem;

            if (item != null)
            {
                listBox_series1.Items.Add(item);

                listBox_allPacks.Items.Remove(item);
            }
        }

        private void button_removeFromSeries1_Click(object sender, EventArgs e)
        {
            var item = listBox_series1.SelectedItem;

            if(item != null)
            {
                listBox_allPacks.Items.Add(item);

                listBox_series1.Items.Remove(item);
            }

            
        }

        private void button_removeFromSeries2_Click(object sender, EventArgs e)
        {
            var item = listBox_series2.SelectedItem;

            if (item != null)
            {
                listBox_allPacks.Items.Add(item);

                listBox_series2.Items.Remove(item);
            }
        }

        private void button_addToSeries2_Click(object sender, EventArgs e)
        {
            var item = listBox_allPacks.SelectedItem;

            if (item != null)
            {
                listBox_series2.Items.Add(item);

                listBox_allPacks.Items.Remove(item);
            }
        }

        private void radioButton_serial_CheckedChanged(object sender, EventArgs e)
        {
            listBox_series2.Enabled = true;
            //radioButton_Parallel.Checked = false;
            button_addToSeries2.Enabled = true;
        }

        private void radioButton_Parallel_CheckedChanged(object sender, EventArgs e)
        {            
            listBox_series2.Enabled = false;
            button_addToSeries2.Enabled = false;
            
            //radioButton_serial.Checked = false;
        }
    }
}
