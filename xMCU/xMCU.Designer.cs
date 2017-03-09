namespace testGetID
{
    partial class xMCU
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xMCU));
            this.button_open = new System.Windows.Forms.Button();
            this.button_burn = new System.Windows.Forms.Button();
            this.MCU_MAC_ID = new System.Windows.Forms.Label();
            this.textBox_uid = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_erase = new System.Windows.Forms.Button();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.check_Fuse = new System.Windows.Forms.CheckBox();
            this.button_read = new System.Windows.Forms.Button();
            this.combo_device = new System.Windows.Forms.ComboBox();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridUID = new System.Windows.Forms.DataGridView();
            this.labelDll = new System.Windows.Forms.Label();
            this.textDllVer = new System.Windows.Forms.TextBox();
            this.labelJlink = new System.Windows.Forms.Label();
            this.textJlinkVer = new System.Windows.Forms.TextBox();
            this.but_clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUID)).BeginInit();
            this.SuspendLayout();
            // 
            // button_open
            // 
            this.button_open.Location = new System.Drawing.Point(157, 4);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(75, 23);
            this.button_open.TabIndex = 1;
            this.button_open.Text = "固件(&F)";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_burn
            // 
            this.button_burn.Location = new System.Drawing.Point(248, 4);
            this.button_burn.Name = "button_burn";
            this.button_burn.Size = new System.Drawing.Size(75, 23);
            this.button_burn.TabIndex = 2;
            this.button_burn.Text = "烧录(&B)";
            this.button_burn.UseVisualStyleBackColor = true;
            this.button_burn.Click += new System.EventHandler(this.button2_Click);
            // 
            // MCU_MAC_ID
            // 
            this.MCU_MAC_ID.AutoSize = true;
            this.MCU_MAC_ID.Location = new System.Drawing.Point(15, 344);
            this.MCU_MAC_ID.Name = "MCU_MAC_ID";
            this.MCU_MAC_ID.Size = new System.Drawing.Size(29, 12);
            this.MCU_MAC_ID.TabIndex = 3;
            this.MCU_MAC_ID.Text = "UID:";
            // 
            // textBox_uid
            // 
            this.textBox_uid.Location = new System.Drawing.Point(46, 341);
            this.textBox_uid.Name = "textBox_uid";
            this.textBox_uid.ReadOnly = true;
            this.textBox_uid.Size = new System.Drawing.Size(177, 21);
            this.textBox_uid.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_erase
            // 
            this.button_erase.Location = new System.Drawing.Point(337, 4);
            this.button_erase.Name = "button_erase";
            this.button_erase.Size = new System.Drawing.Size(75, 23);
            this.button_erase.TabIndex = 5;
            this.button_erase.Text = "擦除(&E)";
            this.button_erase.UseVisualStyleBackColor = true;
            this.button_erase.Click += new System.EventHandler(this.button_erase_Click);
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(13, 30);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.ReadOnly = true;
            this.textBox_path.Size = new System.Drawing.Size(310, 21);
            this.textBox_path.TabIndex = 6;
            // 
            // check_Fuse
            // 
            this.check_Fuse.AutoSize = true;
            this.check_Fuse.Location = new System.Drawing.Point(425, 8);
            this.check_Fuse.Name = "check_Fuse";
            this.check_Fuse.Size = new System.Drawing.Size(48, 16);
            this.check_Fuse.TabIndex = 7;
            this.check_Fuse.Text = "加密";
            this.check_Fuse.UseVisualStyleBackColor = true;
            // 
            // button_read
            // 
            this.button_read.Location = new System.Drawing.Point(229, 341);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(60, 23);
            this.button_read.TabIndex = 8;
            this.button_read.Text = "读取(&R)";
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button3_Click);
            // 
            // combo_device
            // 
            this.combo_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_device.FormattingEnabled = true;
            this.combo_device.Items.AddRange(new object[] {
            "STM32F051C8",
            "STM32F030F4",
            "STM32F031K4",
            "STM32F103C8",
            "STM32F103RB",
            "STM32F103VB",
            "STM32F103VE",
            "STM32L051K8"});
            this.combo_device.Location = new System.Drawing.Point(13, 4);
            this.combo_device.Name = "combo_device";
            this.combo_device.Size = new System.Drawing.Size(131, 20);
            this.combo_device.TabIndex = 10;
            this.combo_device.SelectedIndexChanged += new System.EventHandler(this.combo_device_SelectedIndexChanged);
            // 
            // textBox_serial
            // 
            this.textBox_serial.Location = new System.Drawing.Point(337, 30);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(75, 21);
            this.textBox_serial.TabIndex = 11;
            this.textBox_serial.Text = "0";
            this.textBox_serial.TextChanged += new System.EventHandler(this.textBox_serial_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "编号";
            // 
            // dataGridUID
            // 
            this.dataGridUID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUID.Location = new System.Drawing.Point(13, 57);
            this.dataGridUID.Name = "dataGridUID";
            this.dataGridUID.ReadOnly = true;
            this.dataGridUID.RowTemplate.Height = 23;
            this.dataGridUID.Size = new System.Drawing.Size(460, 278);
            this.dataGridUID.TabIndex = 13;
            // 
            // labelDll
            // 
            this.labelDll.AutoSize = true;
            this.labelDll.Location = new System.Drawing.Point(374, 346);
            this.labelDll.Name = "labelDll";
            this.labelDll.Size = new System.Drawing.Size(29, 12);
            this.labelDll.TabIndex = 14;
            this.labelDll.Text = "DLL:";
            // 
            // textDllVer
            // 
            this.textDllVer.Location = new System.Drawing.Point(406, 344);
            this.textDllVer.Name = "textDllVer";
            this.textDllVer.ReadOnly = true;
            this.textDllVer.Size = new System.Drawing.Size(67, 21);
            this.textDllVer.TabIndex = 15;
            // 
            // labelJlink
            // 
            this.labelJlink.AutoSize = true;
            this.labelJlink.Location = new System.Drawing.Point(228, 373);
            this.labelJlink.Name = "labelJlink";
            this.labelJlink.Size = new System.Drawing.Size(47, 12);
            this.labelJlink.TabIndex = 16;
            this.labelJlink.Text = "Burner:";
            // 
            // textJlinkVer
            // 
            this.textJlinkVer.Location = new System.Drawing.Point(274, 370);
            this.textJlinkVer.Name = "textJlinkVer";
            this.textJlinkVer.ReadOnly = true;
            this.textJlinkVer.Size = new System.Drawing.Size(199, 21);
            this.textJlinkVer.TabIndex = 17;
            // 
            // but_clear
            // 
            this.but_clear.Location = new System.Drawing.Point(295, 341);
            this.but_clear.Name = "but_clear";
            this.but_clear.Size = new System.Drawing.Size(66, 23);
            this.but_clear.TabIndex = 18;
            this.but_clear.Text = "清除(&C)";
            this.but_clear.UseVisualStyleBackColor = true;
            this.but_clear.Click += new System.EventHandler(this.but_clear_Click);
            // 
            // xMCU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 399);
            this.Controls.Add(this.but_clear);
            this.Controls.Add(this.textJlinkVer);
            this.Controls.Add(this.labelJlink);
            this.Controls.Add(this.textDllVer);
            this.Controls.Add(this.labelDll);
            this.Controls.Add(this.dataGridUID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_serial);
            this.Controls.Add(this.combo_device);
            this.Controls.Add(this.button_read);
            this.Controls.Add(this.check_Fuse);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.button_erase);
            this.Controls.Add(this.textBox_uid);
            this.Controls.Add(this.MCU_MAC_ID);
            this.Controls.Add(this.button_burn);
            this.Controls.Add(this.button_open);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "xMCU";
            this.Text = "xMCU Flash Utility";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.Button button_burn;
        private System.Windows.Forms.Label MCU_MAC_ID;
        private System.Windows.Forms.TextBox textBox_uid;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_erase;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.CheckBox check_Fuse;
        private System.Windows.Forms.Button button_read;
        private System.Windows.Forms.ComboBox combo_device;
        private System.Windows.Forms.TextBox textBox_serial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridUID;
        private System.Windows.Forms.Label labelDll;
        private System.Windows.Forms.TextBox textDllVer;
        private System.Windows.Forms.Label labelJlink;
        private System.Windows.Forms.TextBox textJlinkVer;
        private System.Windows.Forms.Button but_clear;
    }
}

