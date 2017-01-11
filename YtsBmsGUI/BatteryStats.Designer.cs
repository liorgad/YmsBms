namespace YtsBmsGUI
{
    partial class BatteryStats
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_volt = new System.Windows.Forms.Label();
            this.label_current = new System.Windows.Forms.Label();
            this.label_temperature = new System.Windows.Forms.Label();
            this.label_SOC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_voltage = new System.Windows.Forms.TextBox();
            this.bindingSource_batterStats = new System.Windows.Forms.BindingSource(this.components);
            this.textBox_current = new System.Windows.Forms.TextBox();
            this.textBox_temperature = new System.Windows.Forms.TextBox();
            this.progressBar_stateOfCharge = new System.Windows.Forms.ProgressBar();
            this.textBox_protection = new System.Windows.Forms.TextBox();
            this.label_DFET = new System.Windows.Forms.Label();
            this.label_CFET = new System.Windows.Forms.Label();
            this.button_delete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_batterStats)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.84615F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.15385F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Controls.Add(this.label_volt, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_current, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_temperature, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_SOC, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox_voltage, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_current, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_temperature, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressBar_stateOfCharge, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox_protection, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_DFET, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_CFET, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.button_delete, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.58974F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.41026F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(305, 250);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label_volt
            // 
            this.label_volt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_volt.AutoSize = true;
            this.label_volt.Location = new System.Drawing.Point(3, 7);
            this.label_volt.Name = "label_volt";
            this.label_volt.Size = new System.Drawing.Size(133, 16);
            this.label_volt.TabIndex = 0;
            this.label_volt.Text = "Voltage [V] :";
            // 
            // label_current
            // 
            this.label_current.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_current.AutoSize = true;
            this.label_current.Location = new System.Drawing.Point(3, 42);
            this.label_current.Name = "label_current";
            this.label_current.Size = new System.Drawing.Size(133, 16);
            this.label_current.TabIndex = 1;
            this.label_current.Text = "Current [A] :";
            // 
            // label_temperature
            // 
            this.label_temperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_temperature.AutoSize = true;
            this.label_temperature.Location = new System.Drawing.Point(3, 80);
            this.label_temperature.Name = "label_temperature";
            this.label_temperature.Size = new System.Drawing.Size(133, 16);
            this.label_temperature.TabIndex = 2;
            this.label_temperature.Text = "Temperature [C] :";
            // 
            // label_SOC
            // 
            this.label_SOC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_SOC.AutoSize = true;
            this.label_SOC.Location = new System.Drawing.Point(3, 120);
            this.label_SOC.Name = "label_SOC";
            this.label_SOC.Size = new System.Drawing.Size(133, 16);
            this.label_SOC.TabIndex = 3;
            this.label_SOC.Text = "State Of Charge [%] :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Protection :";
            // 
            // textBox_voltage
            // 
            this.textBox_voltage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_voltage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource_batterStats, "Voltage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.textBox_voltage.Location = new System.Drawing.Point(142, 4);
            this.textBox_voltage.Name = "textBox_voltage";
            this.textBox_voltage.ReadOnly = true;
            this.textBox_voltage.Size = new System.Drawing.Size(113, 22);
            this.textBox_voltage.TabIndex = 5;
            // 
            // bindingSource_batterStats
            // 
            this.bindingSource_batterStats.DataSource = typeof(Common.BatteryStatViewModel);
            // 
            // textBox_current
            // 
            this.textBox_current.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_current.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource_batterStats, "Current", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.textBox_current.Location = new System.Drawing.Point(142, 39);
            this.textBox_current.Name = "textBox_current";
            this.textBox_current.ReadOnly = true;
            this.textBox_current.Size = new System.Drawing.Size(113, 22);
            this.textBox_current.TabIndex = 6;
            // 
            // textBox_temperature
            // 
            this.textBox_temperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_temperature.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource_batterStats, "Temperature", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_temperature.Location = new System.Drawing.Point(142, 77);
            this.textBox_temperature.Name = "textBox_temperature";
            this.textBox_temperature.ReadOnly = true;
            this.textBox_temperature.Size = new System.Drawing.Size(113, 22);
            this.textBox_temperature.TabIndex = 7;
            // 
            // progressBar_stateOfCharge
            // 
            this.progressBar_stateOfCharge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_stateOfCharge.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource_batterStats, "SOC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.progressBar_stateOfCharge.Location = new System.Drawing.Point(142, 117);
            this.progressBar_stateOfCharge.Name = "progressBar_stateOfCharge";
            this.progressBar_stateOfCharge.Size = new System.Drawing.Size(113, 23);
            this.progressBar_stateOfCharge.TabIndex = 8;
            // 
            // textBox_protection
            // 
            this.textBox_protection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_protection.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource_batterStats, "Protection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_protection.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bindingSource_batterStats, "ProtectionBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_protection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBox_protection.Location = new System.Drawing.Point(142, 187);
            this.textBox_protection.Multiline = true;
            this.textBox_protection.Name = "textBox_protection";
            this.textBox_protection.ReadOnly = true;
            this.textBox_protection.Size = new System.Drawing.Size(113, 60);
            this.textBox_protection.TabIndex = 9;
            // 
            // label_DFET
            // 
            this.label_DFET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_DFET.AutoSize = true;
            this.label_DFET.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bindingSource_batterStats, "DFetColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label_DFET.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_DFET.ForeColor = System.Drawing.Color.White;
            this.label_DFET.Location = new System.Drawing.Point(3, 155);
            this.label_DFET.Name = "label_DFET";
            this.label_DFET.Size = new System.Drawing.Size(133, 24);
            this.label_DFET.TabIndex = 10;
            this.label_DFET.Text = "DFET";
            // 
            // label_CFET
            // 
            this.label_CFET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_CFET.AutoSize = true;
            this.label_CFET.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bindingSource_batterStats, "CFetColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label_CFET.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_CFET.ForeColor = System.Drawing.Color.White;
            this.label_CFET.Location = new System.Drawing.Point(142, 155);
            this.label_CFET.Name = "label_CFET";
            this.label_CFET.Size = new System.Drawing.Size(113, 24);
            this.label_CFET.TabIndex = 11;
            this.label_CFET.Text = "CFET";
            // 
            // button_delete
            // 
            this.button_delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_delete.Location = new System.Drawing.Point(261, 3);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(41, 24);
            this.button_delete.TabIndex = 14;
            this.button_delete.Text = "X";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource_batterStats, "SOC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label2.Location = new System.Drawing.Point(261, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "20%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 271);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // BatteryStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "BatteryStats";
            this.Size = new System.Drawing.Size(311, 271);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_batterStats)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_volt;
        private System.Windows.Forms.Label label_current;
        private System.Windows.Forms.Label label_temperature;
        private System.Windows.Forms.Label label_SOC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_voltage;
        private System.Windows.Forms.TextBox textBox_current;
        private System.Windows.Forms.TextBox textBox_temperature;
        private System.Windows.Forms.ProgressBar progressBar_stateOfCharge;
        private System.Windows.Forms.TextBox textBox_protection;
        private System.Windows.Forms.Label label_DFET;
        private System.Windows.Forms.Label label_CFET;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource bindingSource_batterStats;
        private System.Windows.Forms.Label label2;
    }
}
