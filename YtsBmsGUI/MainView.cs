using Caliburn.Micro;
using Common;
using GenericParser;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtsLogic;

namespace YtsBmsGUI
{
    public partial class MainView : Form,
        IHandle<ToolStripMessage>,
        IHandle<MessageBoxMessage>,
        IHandle<PortConnected>,
        IHandle<BatteryRemoveView>
    {
        //System.Timers.Timer samplingTimer;
        //Configuration.Defaulturation Configuration.Default = new Configuration.Defaulturation(); 

        IEventAggregator evAgg { get; set; }
        Dictionary<string, Control> batteryAddressCtrlMap = new Dictionary<string, Control>();
        MainLogic logic;
        

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainView()
        {
            InitializeComponent();

            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;

            evAgg = EventAggregatorProvider.EventAggregator;
            evAgg.Subscribe(this);

            //ImageList treeViewImageList = new ImageList();
            //treeViewImageList.Images.Add(Image.FromFile(@"Resources\fatcow-farm-fresh-battery.ico"));
            //treeViewImageList.Images.Add(Image.FromFile(@"Resources\battery_pack_256.png"));

            //treeView1.ImageList = treeViewImageList;
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowBatteryStat(e.Node.Name);
        }

        private void ShowBatteryStat(string name,bool shouldClear = false)
        {
            try
            {
                BatteryStatViewModel viewModel;
                if (SharedData.Default.BatteryPackContainer.TryGetValue(name, out viewModel))
                {
                    Control statUC;
                    if (viewModel.IsSeries)
                    {
                        statUC = new ClusterStatistics(viewModel);
                    }
                    else
                    {
                        statUC = new BatteryStats(name, viewModel);                        
                    }
                    
                    this.flowLayoutPanel1.SuspendLayout();
                    if(shouldClear || viewModel.IsSeries)
                    {
                        this.flowLayoutPanel1.Controls.Clear();
                        this.batteryAddressCtrlMap.Clear();
                    }
                    if (!this.batteryAddressCtrlMap.ContainsKey(name))
                    {
                        this.flowLayoutPanel1.Controls.Add(statUC);
                        this.batteryAddressCtrlMap.Add(name, statUC);
                    }
                    this.flowLayoutPanel1.ResumeLayout(true);
                }
                else
                {
                    RemoveFromTreeView(name);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error adding battery statistics view");
            }
        }

        private void RemoveBatteryStat(string name)
        {
            try
            {
                this.flowLayoutPanel1.SuspendLayout();
                if(batteryAddressCtrlMap.ContainsKey(name))
                {
                    this.flowLayoutPanel1.Controls.Remove(batteryAddressCtrlMap[name]);
                    batteryAddressCtrlMap.Remove(name);
                }
                this.flowLayoutPanel1.ResumeLayout(true);
            }
            catch(Exception e)
            {
                logger.Error(e, "Error removing battery statistics view");
            }
        }

        //private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        //{
        //    Debug.WriteLine(e.EventType);
        //    logger.Warn("Serial Port error received " + e.EventType);
        //}

        //private void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        //{
        //    Debug.WriteLine(e.EventType);
        //    logger.Warn("Serial Port pin changed received " + e.EventType);
        //}

        //private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{


        //}

        //private void ParseCommand(FrameFormat frame)
        //{
        //    try
        //    {
        //        switch (frame.Cmd)
        //        {
        //            case (byte)CommandResponse.RealTimeData:
        //                RealtimeDataMap_V82 realTimeData = GenericParser.GenericParser.Parse<RealtimeDataMap_V82>(frame.Data);

        //                HandleRealTimeData(frame, realTimeData);

        //                logger.Trace(realTimeData.ToStringWithAtt<ParserDefinitionAttribute>());

        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch  (Exception e)
        //    {
        //        logger.Error(e, "Error parsing realtime data");
        //    }
        //}

        //private void HandleRealTimeData(FrameFormat frame, RealtimeDataMap_V82 realTimeData)
        //{
        //    try
        //    {
        //        BatteryStatViewModel vm;
        //        if (SharedData.Default.BatteryPackContainer.TryGetValue(frame.Address.ToString(), out vm))
        //        {
        //            if (realTimeData.Current[1] != 0 && realTimeData.Current[0] != 0)
        //            {
        //                vm.Current = 0;
        //            }
        //            else
        //            {
        //                vm.Current = realTimeData.Current[0] == 0 ? realTimeData.Current[1] : realTimeData.Current[0];
        //            }

        //            vm.SOC = realTimeData.SOC;
        //            vm.Temperature = realTimeData.Temp.Max();
        //            vm.Voltage = realTimeData.Vbat;
        //            vm.CFET = (realTimeData.FETState & (byte)FETSTATE.CFET) == (byte)FETSTATE.CFET;
        //            vm.DFET = (realTimeData.FETState & (byte)FETSTATE.DFET) == (byte)FETSTATE.DFET;
        //            vm.ChargeState = realTimeData.CState;
        //            vm.TemperatureState = realTimeData.TState;
        //            vm.VoltageState = realTimeData.VState;

        //            if (vm.VoltageState == 0)
        //            {
        //                vm.Protection = string.Empty;
        //                vm.ProtectionBackColor = Color.Transparent;
        //            }
        //            else if ((vm.VoltageState & (ushort)VSTATE.VUV) == (ushort)VSTATE.VUV)
        //            {
        //                vm.Protection = "Single cell undervoltage";
        //                vm.ProtectionBackColor = Color.Red;
        //            }
        //            else if ((vm.VoltageState & (ushort)VSTATE.BVUV) == (ushort)VSTATE.BVUV)
        //            {
        //                vm.Protection = "Battery pack undervoltage ";
        //                vm.ProtectionBackColor = Color.Red;
        //            }
        //            else
        //            {
        //                vm.Protection = ((VSTATE)vm.VoltageState).ToEnumDescription();
        //                vm.ProtectionBackColor = Color.Orange;
        //            }

        //            if (vm.ChargeState == 0)
        //            {
        //                vm.Protection = string.Empty;
        //                vm.ProtectionBackColor = Color.Transparent;
        //            }
        //            else
        //            {
        //                vm.Protection = ((CSTATE)vm.ChargeState).ToEnumDescription();
        //                vm.ProtectionBackColor = Color.Orange;
        //            }

        //            if (vm.TemperatureState == 0)
        //            {
        //                vm.Protection = string.Empty;
        //                vm.ProtectionBackColor = Color.Transparent;
        //            }
        //            else
        //            {
        //                vm.Protection = ((TSTATE)vm.TemperatureState).ToEnumDescription();
        //                vm.ProtectionBackColor = Color.Orange;
        //            }

        //            Debug.WriteLine(vm.ToString());
        //            logger.Trace(vm.ToStringAllProperties());
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        logger.Error(e, "Error updateing view model");
        //    }
        //}

        //private void SamplingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        foreach (var item in SharedData.Default.BatteryPackContainer)
        //        {
        //            if(item.Key == "cluster")
        //            {
        //                continue;
        //            }

        //            FrameFormat realTimeCmd = new FrameFormat()
        //            {
        //                SOI = ':',
        //                Address = Convert.ToByte(item.Key),
        //                Cmd = 2,
        //                Version = 82,
        //                EOI = '~'
        //            };

        //            if (serialPort.IsOpen)
        //            {
        //                //ring data = ":000252000EFE~";

        //                string data = realTimeCmd.AsString;
        //                // Create two different encodings.
        //                Encoding ascii = Encoding.ASCII;
        //                Encoding unicode = Encoding.Unicode;

        //                // Convert the string into a byte array.
        //                byte[] unicodeBytes = unicode.GetBytes(data);

        //                // Perform the conversion from one encoding to the other.
        //                byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

        //                // Convert the new byte[] into a char[] and then into a string.
        //                char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
        //                ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
        //                string asciiString = new string(asciiChars);

        //                Debug.WriteLine("Sending " + asciiString);
        //                logger.Trace("Sending {0}", asciiString);

        //                serialPort.Write(asciiChars, 0, asciiChars.Length);

        //                Thread.Sleep(Configuration.Default.WaitTimePeriodBetweenCommandSendMilliSec);
        //            }


        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        logger.Error(ex, "Error sending message to pack");
        //    }
        //}


        private void Form1_Load(object sender, EventArgs e)
        {
            logic = new MainLogic();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            logic.Dispose();
            //if(samplingTimer != null)
            //{
            //    samplingTimer.Stop();
            //    samplingTimer.Enabled = false;
            //    samplingTimer.Dispose();
            //}

            //if(serialPort != null)
            //{
            //    serialPort.Close();
            //    serialPort.Dispose();
            //}            
        }

        private void communicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commSelection = new CommSelection();
            var DialogResult = commSelection.ShowDialog(this);
            if (DialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(commSelection.SelectedComm))
                {
                    if (File.Exists("configuration.json"))
                    {
                        Configuration.Default.Load();

                        Configuration.Default.PortName = commSelection.SelectedComm;

                        Configuration.Default.Save();
                    }

                    logic.Initialize(commSelection.SelectedComm);
                }
                else
                {
                    MessageBox.Show("Error, no COM port chosen");
                    logger.Info("Com port not selected");
                }
            }
        }

        private void addBatteryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newBatteryView = new NewBatteryView();
            var dialogResult = newBatteryView.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                AddBatteries(newBatteryView.Address, newBatteryView.IsPartOfCluster);
            }
        }

        private void AddBatteries(IEnumerable<byte> addresses,bool isPartOfCluster)
        {
            foreach (var item in addresses)
            {
                if (!SharedData.Default.BatteryPackContainer.ContainsKey(item.ToString()))
                {
                    var viewModel = new BatteryStatViewModel(WindowsFormsSynchronizationContext.Current) { IsPartOfCluster = isPartOfCluster };
                    viewModel.Address = item.ToString();

                    logic.AddBattery(viewModel);
                    //SharedData.Default.BatteryPackContainer.TryAdd(
                    //    newBatteryView.Address.ToString(),
                    //    viewModel);

                    if (!viewModel.IsPartOfCluster)
                    {
                        ShowBatteryStat(viewModel.Address);
                        //var statUC = new BatteryStats(newBatteryView.Address.ToString(), viewModel);
                        //this.flowLayoutPanel1.Controls.Add(statUC);
                    }

                    //toolStripLabel1.Text = string.Format("Battery added : {0}", viewModel.Address);
                }
                else
                {
                    if (!isPartOfCluster)
                    {
                        BatteryStatViewModel viewModel;
                        bool succeeded = SharedData.Default.BatteryPackContainer.TryGetValue(item.ToString(), out viewModel);
                        if (succeeded)
                        {
                            viewModel.IsPartOfCluster = isPartOfCluster;

                            ShowBatteryStat(viewModel.Address);
                            //var statUC = new BatteryStats(newBatteryView.Address.ToString(), viewModel);
                            //this.flowLayoutPanel1.Controls.Add(statUC);
                        }
                    }
                }

                AddToTreeView(item.ToString(), string.Format("Battery : {0}", item), 0);

                logger.Info(string.Format("Added Battery : {0}", item));
            }
        }

        private void AddToTreeView(string key,string text,int imageIndex)
        {
            if (!treeView1.Nodes.ContainsKey(key))
            {
                treeView1.BeginUpdate();

                treeView1.Nodes.Add(key, text, imageIndex);
                treeView1.EndUpdate();
            }
        }

        private void clusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addresses = SharedData.Default.BatteryPackContainer.Keys.ToList();
            var clusterView = new ClusterView(addresses);
            var dialogResult = clusterView.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                if (clusterView.IsSerialConnected)
                {
                    CreateSeries(clusterView.SelectedBatteriesGroup1, clusterView.SelectedBatteriesGroup2);
                }
                else if (clusterView.SelectedBatteriesGroup1.Count > 0)/* && clusterView.SelectedBatteriesGroup2.Count == 0 ||
                    clusterView.SelectedBatteriesGroup1.Count == 0 && clusterView.SelectedBatteriesGroup2.Count > 0)*/
                {
                    CreateParallel(clusterView.SelectedBatteriesGroup1, clusterView.SelectedBatteriesGroup2);

                }
                else
                {
                    MessageBox.Show("Cannot Create Cluster Statistics, no batteries group select");
                }

            }
            else
            {

            }

        }
        private void CreateParallel(List<string> selectedBatteriesGroup1, List<string> selectedBatteriesGroup2)
        {
            IEnumerable<BatteryStatViewModel> vmGroup;
            SeriesStatViewModel group;
            if (selectedBatteriesGroup1.Count > 0)
            {
                vmGroup = SharedData.Default.BatteryPackContainer.Values.Where((vm) => selectedBatteriesGroup1.Contains(vm.Address));
                group = new SeriesStatViewModel(WindowsFormsSynchronizationContext.Current, "Group1",
                vmGroup);
            }
            else
            {
                vmGroup = SharedData.Default.BatteryPackContainer.Values.Where((vm) => selectedBatteriesGroup2.Contains(vm.Address));
                group = new SeriesStatViewModel(WindowsFormsSynchronizationContext.Current, "Group2",
                    vmGroup);
            }

            group.IsSeries = true;
            group.Address = "cluster";

            logic.AddCluster(group);
            //SharedData.Default.BatteryPackContainer.TryAdd("cluster", group);

            //var clusterStatisticsView = new ClusterStatistics(group);

            ShowBatteryStat(group.Address, true);
            //this.flowLayoutPanel1.Controls.Clear();
            //this.batteryAddressCtrlMap.Clear();
            //this.flowLayoutPanel1.Controls.Add(clusterStatisticsView);

            AddToTreeView(group.Address, "Cluster", 1);
        }

        private void CreateSeries(List<string> selectedBatteriesGroup1, List<string> selectedBatteriesGroup2)
        {
            if (selectedBatteriesGroup1.Count > 0 && selectedBatteriesGroup2.Count > 0)
            {
                var vmGroup1 = SharedData.Default.BatteryPackContainer.Values.Where((vm) => selectedBatteriesGroup1.Contains(vm.Address));
                var group1 = new SeriesStatViewModel(WindowsFormsSynchronizationContext.Current, "Group1",
                vmGroup1);

                var vmGroup2 = SharedData.Default.BatteryPackContainer.Values.Where((vm) => selectedBatteriesGroup2.Contains(vm.Address));
                var group2 = new SeriesStatViewModel(WindowsFormsSynchronizationContext.Current, "Group2",
                    vmGroup2);

                var cluster = new List<SeriesStatViewModel>();
                cluster.Add(group1);
                cluster.Add(group2);

                var clusterVm = new ClusterStatViewModel(WindowsFormsSynchronizationContext.Current,
                    cluster);

                

                clusterVm.Address = "cluster";
                clusterVm.IsSeries = true;

                //SharedData.Default.ClusterStatisticsVM = clusterVm;

                logic.AddCluster(clusterVm);

                //SharedData.Default.BatteryPackContainer.TryAdd(
                //    viewModel.Address,
                //    viewModel);

                ShowBatteryStat(clusterVm.Address, true);

                //var clusterStatisticsView = new ClusterStatistics(clusterVm);

                //this.flowLayoutPanel1.Controls.Clear();
                //this.flowLayoutPanel1.Controls.Add(clusterStatisticsView);

                AddToTreeView(clusterVm.Address, "Cluster", 1);
            }
        }       

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharedData.Default.Save();
            MessageBox.Show(this, "Configuration Saved", "Save");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confDeserializtion = SharedData.Default.Load(WindowsFormsSynchronizationContext.Current);

            if ((null != confDeserializtion) && (null != confDeserializtion.DefinedAddresses) )
            {
                ClearFlowPanel();
                ClearTreeView();

                AddBatteries(confDeserializtion.DefinedAddresses.Select(a => byte.Parse(a)).AsEnumerable(), confDeserializtion.HasCluster);

                if (confDeserializtion.HasCluster)
                {
                    if (confDeserializtion.IsParallel)
                    {
                        CreateParallel(confDeserializtion.Group1, confDeserializtion.Group2);
                    }
                    else
                    {
                        CreateSeries(confDeserializtion.Group1, confDeserializtion.Group2);
                    }
                }      
            }      
        }
        

        void IHandle<ToolStripMessage>.Handle(ToolStripMessage message)
        {            
            this.toolStripLabel1.Text = message.Text;
        }

        void IHandle<MessageBoxMessage>.Handle(MessageBoxMessage message)
        {
            MessageBox.Show(this, message.Message, message.Title);
        }

        void IHandle<PortConnected>.Handle(PortConnected message)
        {
            addBatteryToolStripMenuItem.Enabled = true;
            clusterToolStripMenuItem.Enabled = true;
        }

        
        void IHandle<BatteryRemoveView>.Handle(BatteryRemoveView message)
        {
            RemoveBatteryStat(message.Address);            
        }

        private void RemoveFromTreeView(string key)
        {
            // Display a wait cursor while the TreeNodes are being created.
            Cursor.Current = Cursors.WaitCursor;

            // Suppress repainting the TreeView until all the objects have been created.
            treeView1.BeginUpdate();

            // Clear the TreeView each time the method is called.
            //treeView1.Nodes.Clear();

            treeView1.Nodes.RemoveByKey(key);

            //foreach (var item in SharedData.Default.BatteryPackContainer.Keys.Reverse())
            //{
            //    treeView1.Nodes.Add(item, string.Format("Battery : {0}", item), 0);
            //}

            // Reset the cursor to the default for all controls.
            // Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeView1.EndUpdate();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if(null != selectedNode)
            {
                ShowBatteryStat(selectedNode.Name);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (null != selectedNode)
            {
                RemoveBatteryStat(selectedNode.Name);
                RemoveFromTreeView(selectedNode.Name);
                logic.RemoveBattery(selectedNode.Name);
            }
        }

        private void ClearTreeView()
        {
            // Display a wait cursor while the TreeNodes are being created.
            Cursor.Current = Cursors.WaitCursor;

            // Suppress repainting the TreeView until all the objects have been created.
            treeView1.BeginUpdate();

            // Clear the TreeView each time the method is called.
            //treeView1.Nodes.Clear();

            treeView1.Nodes.Clear();

            //foreach (var item in SharedData.Default.BatteryPackContainer.Keys.Reverse())
            //{
            //    treeView1.Nodes.Add(item, string.Format("Battery : {0}", item), 0);
            //}

            // Reset the cursor to the default for all controls.
            // Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeView1.EndUpdate();
        }

        private void ClearFlowPanel()
        {
            this.flowLayoutPanel1.SuspendLayout();
            
            this.flowLayoutPanel1.Controls.Clear();             
            
            this.flowLayoutPanel1.ResumeLayout(true);
        }
    }
}
