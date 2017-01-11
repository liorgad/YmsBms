namespace YtsBmsGUI
{
    partial class ClusterView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_ok = new System.Windows.Forms.Button();
            this.listBox_allPacks = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button_addToSeries1 = new System.Windows.Forms.Button();
            this.button_removeFromSeries1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button_addToSeries2 = new System.Windows.Forms.Button();
            this.button_removeFromSeries2 = new System.Windows.Forms.Button();
            this.listBox_series1 = new System.Windows.Forms.ListBox();
            this.listBox_series2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.radioButton_serial = new System.Windows.Forms.RadioButton();
            this.radioButton_Parallel = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.36242F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.63758F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.button_ok, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.listBox_allPacks, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.listBox_series1, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.listBox_series2, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.button_cancel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.radioButton_serial, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButton_Parallel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.59799F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.40201F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 478);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_ok
            // 
            this.button_ok.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_ok.Location = new System.Drawing.Point(444, 443);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 4;
            this.button_ok.Text = "Ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // listBox_allPacks
            // 
            this.listBox_allPacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_allPacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listBox_allPacks.FormattingEnabled = true;
            this.listBox_allPacks.ItemHeight = 16;
            this.listBox_allPacks.Location = new System.Drawing.Point(3, 81);
            this.listBox_allPacks.Name = "listBox_allPacks";
            this.tableLayoutPanel1.SetRowSpan(this.listBox_allPacks, 3);
            this.listBox_allPacks.Size = new System.Drawing.Size(161, 348);
            this.listBox_allPacks.Sorted = true;
            this.listBox_allPacks.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button_addToSeries1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_removeFromSeries1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(170, 88);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(135, 100);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // button_addToSeries1
            // 
            this.button_addToSeries1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addToSeries1.Location = new System.Drawing.Point(3, 3);
            this.button_addToSeries1.Name = "button_addToSeries1";
            this.button_addToSeries1.Size = new System.Drawing.Size(129, 44);
            this.button_addToSeries1.TabIndex = 0;
            this.button_addToSeries1.Text = "Add";
            this.button_addToSeries1.UseVisualStyleBackColor = true;
            this.button_addToSeries1.Click += new System.EventHandler(this.button_addToSeries1_Click);
            // 
            // button_removeFromSeries1
            // 
            this.button_removeFromSeries1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_removeFromSeries1.Location = new System.Drawing.Point(3, 53);
            this.button_removeFromSeries1.Name = "button_removeFromSeries1";
            this.button_removeFromSeries1.Size = new System.Drawing.Size(129, 44);
            this.button_removeFromSeries1.TabIndex = 1;
            this.button_removeFromSeries1.Text = "Remove";
            this.button_removeFromSeries1.UseVisualStyleBackColor = true;
            this.button_removeFromSeries1.Click += new System.EventHandler(this.button_removeFromSeries1_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.button_addToSeries2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button_removeFromSeries2, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(170, 280);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(135, 91);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // button_addToSeries2
            // 
            this.button_addToSeries2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addToSeries2.Location = new System.Drawing.Point(3, 3);
            this.button_addToSeries2.Name = "button_addToSeries2";
            this.button_addToSeries2.Size = new System.Drawing.Size(129, 39);
            this.button_addToSeries2.TabIndex = 0;
            this.button_addToSeries2.Text = "Add";
            this.button_addToSeries2.UseVisualStyleBackColor = true;
            this.button_addToSeries2.Click += new System.EventHandler(this.button_addToSeries2_Click);
            // 
            // button_removeFromSeries2
            // 
            this.button_removeFromSeries2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_removeFromSeries2.Location = new System.Drawing.Point(3, 48);
            this.button_removeFromSeries2.Name = "button_removeFromSeries2";
            this.button_removeFromSeries2.Size = new System.Drawing.Size(129, 40);
            this.button_removeFromSeries2.TabIndex = 1;
            this.button_removeFromSeries2.Text = "Remove";
            this.button_removeFromSeries2.UseVisualStyleBackColor = true;
            this.button_removeFromSeries2.Click += new System.EventHandler(this.button_removeFromSeries2_Click);
            // 
            // listBox_series1
            // 
            this.listBox_series1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listBox_series1.FormattingEnabled = true;
            this.listBox_series1.ItemHeight = 16;
            this.listBox_series1.Location = new System.Drawing.Point(311, 81);
            this.listBox_series1.Name = "listBox_series1";
            this.listBox_series1.Size = new System.Drawing.Size(126, 115);
            this.listBox_series1.Sorted = true;
            this.listBox_series1.TabIndex = 9;
            // 
            // listBox_series2
            // 
            this.listBox_series2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listBox_series2.FormattingEnabled = true;
            this.listBox_series2.ItemHeight = 16;
            this.listBox_series2.Location = new System.Drawing.Point(311, 222);
            this.listBox_series2.Name = "listBox_series2";
            this.listBox_series2.Size = new System.Drawing.Size(126, 207);
            this.listBox_series2.Sorted = true;
            this.listBox_series2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "All Packs :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(311, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Group 1 :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(311, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Group 2 :";
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_cancel.Location = new System.Drawing.Point(3, 443);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // radioButton_serial
            // 
            this.radioButton_serial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_serial.AutoSize = true;
            this.radioButton_serial.Checked = true;
            this.radioButton_serial.Location = new System.Drawing.Point(3, 33);
            this.radioButton_serial.Name = "radioButton_serial";
            this.radioButton_serial.Size = new System.Drawing.Size(161, 17);
            this.radioButton_serial.TabIndex = 14;
            this.radioButton_serial.TabStop = true;
            this.radioButton_serial.Text = "Packs Serial Connection";
            this.radioButton_serial.UseVisualStyleBackColor = true;
            this.radioButton_serial.CheckedChanged += new System.EventHandler(this.radioButton_serial_CheckedChanged);
            // 
            // radioButton_Parallel
            // 
            this.radioButton_Parallel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_Parallel.AutoSize = true;
            this.radioButton_Parallel.Location = new System.Drawing.Point(3, 57);
            this.radioButton_Parallel.Name = "radioButton_Parallel";
            this.radioButton_Parallel.Size = new System.Drawing.Size(161, 17);
            this.radioButton_Parallel.TabIndex = 15;
            this.radioButton_Parallel.Text = "Packs Parallel Connection";
            this.radioButton_Parallel.UseVisualStyleBackColor = true;
            this.radioButton_Parallel.CheckedChanged += new System.EventHandler(this.radioButton_Parallel_CheckedChanged);
            // 
            // ClusterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 478);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ClusterView";
            this.Text = "Cluster";
            this.Load += new System.EventHandler(this.ClusterView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.ListBox listBox_allPacks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button_addToSeries1;
        private System.Windows.Forms.Button button_removeFromSeries1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button_addToSeries2;
        private System.Windows.Forms.Button button_removeFromSeries2;
        private System.Windows.Forms.ListBox listBox_series1;
        private System.Windows.Forms.ListBox listBox_series2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_serial;
        private System.Windows.Forms.RadioButton radioButton_Parallel;
    }
}