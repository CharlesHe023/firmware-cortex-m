using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace testGetID
{
    public partial class xMCU : Form
    {
        //You may read these from an ini file
        Int32 serial_number = 1;
        uint MCU_MAC_ADDRESS = 0x1FFFF7AC;
        int MCU_MAC_LENGTH = 3;
        uint MCU_ADDRESS_OFFSET = 0x08000000;
        int MCU_PROG_INTERFACE = 1;
        int MCU_PROG_SPEED = 4000;
        string MCU_PROG_INTERFACE_SWD = "S";
        string MCU_PROG_INTERFACE_JTAG = "J";
        string MCU_FAMILY = "Cortex-M0";
        string MCU_DEVICE = "STM32F051C8";
        string JLINK_NOT_CONNECTED = "Can not connect to J-Link via USB";
        string binFilePath = "";
        string execProg = System.Environment.CurrentDirectory + "\\JLink.exe";
        string DLL_VERSION_STRING = "DLL version V";
        string COMPILED_STRING = ", compiled";
        string JLINK_HARDWARE_OLD_STRING = "Hardware: ";
        string JLINK_HARDWARE_NEW_STRING = "Hardware version: ";
        string FIRMWARE_STRING = "Firmware:";
        string JLINK_COMPILED_STRING = "compiled";
        Boolean new_jlink = false;
        

        DataTable myTable = new DataTable();

        Process pConsole = new Process();

        public xMCU()
        {
            InitializeComponent();
            combo_device.SelectedIndex = 0;
            combo_device.Enabled = true;
            check_Fuse.Enabled = false;
            textBox_serial.Enabled = true;
            serial_number = Convert.ToInt32(textBox_serial.Text);
            if (false == File.Exists(execProg))
            {
                MessageBox.Show("Jlink程序组件不存在!");
                return ;   //Jlink.exe dose not exists.
            }
            pConsole.StartInfo.FileName = execProg;
            pConsole.StartInfo.UseShellExecute = false;
            pConsole.StartInfo.RedirectStandardInput = true;
            pConsole.StartInfo.RedirectStandardOutput = true;
            pConsole.StartInfo.RedirectStandardError = true;
            pConsole.StartInfo.CreateNoWindow = true;

            myTable.Columns.Add("编号");
            myTable.Columns.Add("UID");
            this.dataGridUID.DataSource = myTable;
            this.dataGridUID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private int Check_Status(string DeviceFamily)
        {
            int index = 0;
            if (("" == binFilePath) || (false == File.Exists(binFilePath)))
            {
                MessageBox.Show("请先选择一个固件文件!");
                return 1;   //Bin file dose not exists.
            }
            if (false == File.Exists(execProg))
            {
                MessageBox.Show("缺少jlink组件!");
                return 2;   //Jlink.exe dose not exists.
            }
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            sro.Close();
            pConsole.WaitForExit();
            index = message.IndexOf(JLINK_NOT_CONNECTED);
            if (index > 0)
            {
                MessageBox.Show("Jlink编程器未连接!");
                return 3;   //Jlink dose not connected.
            }
            index = message.IndexOf(MCU_FAMILY + " identified");
            if (index < 0)
            {
                MessageBox.Show("未检测到正确的CPU!");
                return 4;   //MCU dose not identified.
            }
            return 0;   //OK
        }

        private int Check_Status_Enhanced(string DeviceFamily)
        {
            int dll_ver,index = 0;
            if (false == File.Exists(execProg))
            {
                MessageBox.Show("缺少jlink组件!");
                return 2;   //Jlink.exe dose not exists.
            }
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            sro.Close();
            pConsole.WaitForExit();
            index = message.IndexOf(DLL_VERSION_STRING);
            if (index < 0)
            {
                MessageBox.Show("Jlink DLL版本获取错误!");
                return 3;   //Jlink dose not connected.
            }
            string strDllVer = message.Substring(index + DLL_VERSION_STRING.Length, (message.IndexOf(COMPILED_STRING) - index - DLL_VERSION_STRING.Length));
            textDllVer.Text = strDllVer;
            strDllVer = System.Text.RegularExpressions.Regex.Replace(strDllVer, @"[^\d]*", "");
            dll_ver = int.Parse(strDllVer);
            if (dll_ver >= 512)
            {
                new_jlink = true;
            }
            string strJlinkHardware;
            if (true == new_jlink)
            {
                index = message.IndexOf(JLINK_HARDWARE_NEW_STRING);
                if (index < 0)
                {
                    MessageBox.Show("Jlink 硬件版本获取错误!");
                    return 3;   //Jlink dose not connected.
                }
                strJlinkHardware = message.Substring(index + JLINK_HARDWARE_NEW_STRING.Length, (message.Length - index - JLINK_HARDWARE_NEW_STRING.Length));
                index = strJlinkHardware.IndexOf("\r\n");
                strJlinkHardware = strJlinkHardware.Substring(0, index);
                textJlinkVer.Text = strJlinkHardware;
                index = message.IndexOf(FIRMWARE_STRING);
                strJlinkHardware = message.Substring(index, (message.Length - index));
                index = strJlinkHardware.IndexOf(JLINK_COMPILED_STRING);
                strJlinkHardware = strJlinkHardware.Substring(index + JLINK_COMPILED_STRING.Length, (strJlinkHardware.IndexOf("\r\n") - index - JLINK_COMPILED_STRING.Length));
                textJlinkVer.Text += strJlinkHardware;
            }
            else
            {
                index = message.IndexOf(JLINK_HARDWARE_OLD_STRING);
                if (index < 0)
                {
                    MessageBox.Show("Jlink 硬件版本获取错误!");
                    return 3;   //Jlink dose not connected.
                }
                strJlinkHardware = message.Substring(index + JLINK_HARDWARE_OLD_STRING.Length, (message.Length - index - JLINK_HARDWARE_OLD_STRING.Length));
                index = strJlinkHardware.IndexOf("\r\n");
                strJlinkHardware = strJlinkHardware.Substring(0, index);
                textJlinkVer.Text = strJlinkHardware;
                index = message.IndexOf(FIRMWARE_STRING);
                strJlinkHardware = message.Substring(index, (message.Length - index));
                index = strJlinkHardware.IndexOf(JLINK_COMPILED_STRING);
                strJlinkHardware = strJlinkHardware.Substring(index + JLINK_COMPILED_STRING.Length, (strJlinkHardware.IndexOf("\r\n") - index - JLINK_COMPILED_STRING.Length));
                textJlinkVer.Text += strJlinkHardware;
            }
            index = message.IndexOf(JLINK_NOT_CONNECTED);
            if (index > 0)
            {
                MessageBox.Show("Jlink编程器未连接!");
                return 4;   //Jlink dose not connected.
            }
            if (false == new_jlink)
            {
                index = message.IndexOf(MCU_FAMILY + " identified");
                if (index < 0)
                {
                    MessageBox.Show("未检测到正确的CPU!");
                    return 5;   //MCU dose not identified.
                }
            }
            else
            {
                pConsole.Start();
                swi = pConsole.StandardInput;
                sro = pConsole.StandardOutput;
                swi.WriteLine("connect");
                swi.WriteLine(MCU_DEVICE.ToString());
                swi.WriteLine(MCU_PROG_INTERFACE_SWD.ToString());
                swi.WriteLine(MCU_PROG_SPEED.ToString("D"));
                swi.WriteLine("qc");
                swi.Close();
                message = sro.ReadToEnd();
                sro.Close();
                pConsole.WaitForExit();
                index = message.IndexOf(MCU_FAMILY + " identified");
                if (index < 0)
                {
                    MessageBox.Show("未检测到正确的CPU!");
                    return 5;   //MCU dose not identified.
                }
            }
            
            return 0;   //OK
        }

        private string Get_MCU_MAC(uint mac_address,int mac_length, Boolean flag)
        {
            string strMAC = "";
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            if (true == flag)
            {
                swi.WriteLine("connect");
                swi.WriteLine(MCU_DEVICE.ToString());
                swi.WriteLine(MCU_PROG_INTERFACE_SWD.ToString());
                swi.WriteLine(MCU_PROG_SPEED.ToString("D"));
            }
            swi.WriteLine("mem32 0x" + mac_address.ToString("X8") + " " + mac_length.ToString("D"));
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            sro.Close();
            pConsole.WaitForExit();
            int index = message.IndexOf(MCU_MAC_ADDRESS.ToString("X8") + " = ");
            if (index >= 0)
            {
                strMAC = message.Substring(index + 11, 26);
                string[] temp = strMAC.Split(' ');
                strMAC = "";
                if (3 == temp.Length)
                {
                    foreach (var key in temp)
                    {
                        for (int i = 4; i > 0; i--)
                        {
                            strMAC += key.Substring((i - 1) * 2, 2);
                        }
                    }

                }
            }
            if (("FFFFFFFFFFFFFFFFFFFFFFFF" == strMAC) || ("000000000000000000000000" == strMAC) || (strMAC.Length != 2*4 * mac_length))
            {
                MessageBox.Show("MAC读取错误！", "MAC读取状态", MessageBoxButtons.OK, MessageBoxIcon.Error);
                strMAC = "";
            }
            return strMAC;
        }

        private int MCU_Erase_Flash(string mcu_device, uint addressOffset, int interfaceProg, int speedProg, Boolean flag)
        {
            int index = -1;
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            if (false == flag)
            {
                swi.WriteLine("si " + interfaceProg.ToString("D"));
                swi.WriteLine("speed " + speedProg.ToString("D"));
                swi.WriteLine("exec device = " + mcu_device);
            }
            else
            {
                swi.WriteLine("connect");
                swi.WriteLine(MCU_DEVICE.ToString());
                swi.WriteLine(MCU_PROG_INTERFACE_SWD.ToString());
                swi.WriteLine(MCU_PROG_SPEED.ToString("D"));
                swi.WriteLine("exec device = " + mcu_device);
            }
            swi.WriteLine("erase");
            swi.WriteLine("r0");
            swi.WriteLine("sleep 100");
            swi.WriteLine("r1");
            swi.WriteLine("qc");
            swi.Close();
            string message = "";
            while (sro.Peek() > 0)
            {
                message = sro.ReadLine();
                index = message.IndexOf("Erasing done.");
                if (index >= 0)
                {
                    break;
                }
            }
            sro.Close();
            pConsole.WaitForExit();
            if (index < 0)
            {
                return 1;
            }
            else
            {
                //richText_log.AppendText(message);
                if (message.IndexOf("Erasing done") < 0)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }

        private int MCU_Prog_Flash(string mcu_device, uint addressOffset, int interfaceProg, int speedProg, Boolean flag, string fw_file)
        {
            int index = -1;
            if (("" == fw_file) || (false == File.Exists(fw_file)))
            {
                MessageBox.Show("请先选择一个固件文件!");
                return 1;   //Bin file dose not exists.
            }
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            if (false == flag)
            {
                swi.WriteLine("si " + interfaceProg.ToString("D"));
                swi.WriteLine("speed " + speedProg.ToString("D"));
                swi.WriteLine("exec device = " + mcu_device);
            }
            else
            {
                swi.WriteLine("connect");
                swi.WriteLine(MCU_DEVICE.ToString());
                swi.WriteLine(MCU_PROG_INTERFACE_SWD.ToString());
                swi.WriteLine(MCU_PROG_SPEED.ToString("D"));
                swi.WriteLine("exec device = " + mcu_device);
            }
            swi.WriteLine("loadfile " + fw_file + " 0x" + addressOffset.ToString("X8"));
            swi.WriteLine("r0");
            swi.WriteLine("sleep 100");
            swi.WriteLine("r1");
            swi.WriteLine("qc");
            swi.Close();
            string message = "";
            while (sro.Peek() > 0)
            {
                message = sro.ReadLine();
                index = message.IndexOf("Flash download:");
                if (index >= 0)
                {
                    message = message + sro.ReadToEnd();
                    /*
                    if (message.IndexOf("Flash programming performed for ") >= 0)
                    {
                        message = message + ".\r\n" + sro.ReadLine();
                    }*/
                    break;
                }
            }
            sro.Close();
            pConsole.WaitForExit();
            if (index < 0)
            {
                return 2;
            }
            else
            {
                message = message.Replace("Flash download: ", "");
                message = message.Replace("Info: J-Link: ", "");
                message = message.Replace("\r\n", "");
                //richText_log.AppendText(message);
                if (message.Contains("Error while programming flash") || message.Contains("Programming failed"))
                {
                    return 3;
                }
                if (message.Contains("Flash contents already match") || message.Contains("O.K."))
                {
                    return 0;
                }
                else
                {
                    return 4;
                }
            }
        }

        private string MCU_Read_Memory32(uint address, int length)
        {
            int index = -1;
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            swi.WriteLine("r0");
            swi.WriteLine("sleep 2");
            swi.WriteLine("r1");
            swi.WriteLine("halt");
            swi.WriteLine("mem32 0x" + address.ToString("X8") + " " + length.ToString("D"));
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            sro.Close();
            pConsole.WaitForExit();
            index = message.IndexOf(address.ToString("X8"));
            if (index < 0)
            {
                return "";
            }
            else
            {
                message = message.Substring(index);
                message = message.Replace(" \r\nJ-Link>", "");
                //richText_log.AppendText("Read: " + message + "\r\n");
                message = message.Substring(11);
                return message;
            }
        }

        private void MCU_Write_Memory32(uint[] address, uint[] value, int length)
        {
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            swi.WriteLine("halt");
            for (int i = 0; i < length; i++)
            {
                swi.WriteLine("w4 0x" + address[i].ToString("X8") + " " + value[i].ToString("X8"));
            }
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            message = message.Substring(message.IndexOf("Writing "));
            message = message.Replace("J-Link>", "");
            //richText_log.AppendText(message);
            sro.Close();
            pConsole.WaitForExit();
        }

        private void MCU_Write_Memory16(uint[] address, UInt16[] value, int length)
        {
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            swi.WriteLine("halt");
            for (int i = 0; i < length; i++)
            {
                swi.WriteLine("w2 0x" + address[i].ToString("X8") + " " + value[i].ToString("X4"));
            }
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            message = message.Substring(message.IndexOf("Writing "));
            message = message.Replace("J-Link>", "");
            //richText_log.AppendText(message);
            sro.Close();
            pConsole.WaitForExit();
        }

        private void MCU_Write_Memory8(uint[] address, byte[] value, int length)
        {
            pConsole.Start();
            StreamWriter swi = pConsole.StandardInput;
            StreamReader sro = pConsole.StandardOutput;
            swi.WriteLine("halt");
            for (int i = 0; i < length; i++)
            {
                swi.WriteLine("w1 0x" + address[i].ToString("X8") + " " + value[i].ToString("X2"));
            }
            swi.WriteLine("qc");
            swi.Close();
            string message = sro.ReadToEnd();
            message = message.Substring(message.IndexOf("Writing "));
            message = message.Replace("J-Link>", "");
            //richText_log.AppendText(message);
            sro.Close();
            pConsole.WaitForExit();
        }

        private int MCU_Get_Flash_Status(uint address, uint status, bool bInvert)
        {
            uint number;
            string message = "";
            for (int i = 0; i < 5; i++)
            {
                message = MCU_Read_Memory32(address, 1);
                if ("" != message)
                {
                    number = Convert.ToUInt32(message, 16);
                    if (true == bInvert)
                    {
                        number &= status;
                        status = ~status;
                        number |= status;
                        if (status == number)
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        number &= status;
                        if (status == number)
                        {
                            return 0;
                        }
                    }
                }
                System.Threading.Thread.Sleep(500);
            }
            return 1; //Timeout!
        }

        private int MCU_Unlock_Flash()
        {
            uint[] FlashKeyr = new uint[2] { 0x40022004, 0x40022004 };
            uint[] FlashKeyv = new uint[2] { 0x45670123, 0xCDEF89AB };
            if (0 != MCU_Get_Flash_Status(0x40022010, 0x00000080, false))
            {
                return 1;   //Get flash status timeout!
            }
            MCU_Write_Memory32(FlashKeyr, FlashKeyv,2);
            return 0;
        }

        private int MCU_Unlock_OB_Flash()
        {
            uint[] FlashKeyr = new uint[2] { 0x40022008, 0x40022008 };
            uint[] FlashKeyv = new uint[2] { 0x45670123, 0xCDEF89AB };
            if (0 != MCU_Get_Flash_Status(0x40022010, 0x00000200, true))
            {
                return 1;   //Get OB flash status timeout!
            }
            MCU_Write_Memory32(FlashKeyr, FlashKeyv, 2);
            return 0;
        }

        private int MCU_Flash_Read_Protect(byte nLevel)
        {
            uint[] FlashKeyr = new uint[3] { 0x40022010, 0x40022010, 0x40022010 };
            uint[] FlashKeyv = new uint[3] { 0, 0, 0 };
            string message = "";
            uint value = 0;
            if (0 != MCU_Get_Flash_Status(0x4002200C, 0x00000001, true))
            {
                return 1;   //Get OB flash status timeout!
            }
            message = MCU_Read_Memory32(0x40022010,1);
            if ("" == message)
            {
                return 2;   //Get memory error!
            }
            else
            {
                value = Convert.ToUInt32(message,16);
                FlashKeyv[0] = value | 0x00000020;
                FlashKeyv[1] = value | 0x00000040;
            }
            MCU_Write_Memory32(FlashKeyr, FlashKeyv, 2);
            if (0 != MCU_Get_Flash_Status(0x4002200C, 0x00000001, true))
            {
                return 3;   //Get OB flash status timeout!
            }
            message = MCU_Read_Memory32(0x40022010, 1);
            if ("" == message)
            {
                return 4;   //Get memory error!
            }
            else
            {
                value = Convert.ToUInt32(message,16);
                FlashKeyv[0] = 0x00000020;
                FlashKeyv[0] = ~FlashKeyv[0];
                FlashKeyv[0] = value & FlashKeyv[0];
                FlashKeyv[1] = value | 0x00000010;
            }
            MCU_Write_Memory32(FlashKeyr, FlashKeyv, 2);
            uint[] addrBuffer = new uint[1] { 0x1FFFF800 };
            byte[] valueBuffer = new byte[1] { nLevel };
            MCU_Write_Memory8(addrBuffer, valueBuffer, 1);
            if (0 != MCU_Get_Flash_Status(0x4002200C, 0x00000001, true))
            {
                return 5;   //Get OB flash status timeout!
            }
            message = MCU_Read_Memory32(0x40022010, 1);
            if ("" == message)
            {
                return 6;   //Get memory error!
            }
            else
            {
                value = Convert.ToUInt32(message,16);
                FlashKeyv[0] = 0x00000200;
                FlashKeyv[0] = ~FlashKeyv[0];
                FlashKeyv[0] = value & FlashKeyv[0];
                FlashKeyv[1] = value | 0x00000080;
                FlashKeyv[2] = value | 0x00002000;
            }
            MCU_Write_Memory32(FlashKeyr, FlashKeyv, 3);
            return 0;
        }

        private int MCU_Fuse_Flash()
        {
            if (0 != MCU_Unlock_Flash())
            {
                return 1;   //Unlock flash error!
            }
            System.Threading.Thread.Sleep(500);
            if (0 != MCU_Unlock_OB_Flash())
            {
                return 2;   //Unlock OB flash error!
            }
            if (0 != MCU_Flash_Read_Protect(0xBB))
            {
                return 3;   //Set flash read protect error!
            }
            return 0;
        }

        private void UpdateResultList(object str)
        {
            if (dataGridUID.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) =>
                {
                    DataRow[] myRows = myTable.Select("UID ='" + str.ToString() + "'");
                    if (myRows.Length != 0)
                    {
                        this.dataGridUID.ClearSelection();
                        this.dataGridUID.Rows[myTable.Rows.IndexOf(myRows[0])].Selected = true;
                        return;
                    }
                    string[] myStringArry = new string[2];
                    myStringArry[0] = serial_number.ToString();
                    myStringArry[1] = str.ToString();
                    myTable.Rows.Add(myStringArry);
                    serial_number++;
                    textBox_serial.Text = serial_number.ToString();
                    //UpdateBarCodeText(barCode);
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.dataGridUID.Invoke(actionDelegate, str);
            }
            else
            {
                DataRow[] myRows = myTable.Select("UID ='" + str.ToString() + "'");
                if (myRows.Length != 0)
                {
                    this.dataGridUID.ClearSelection();
                    this.dataGridUID.Rows[myTable.Rows.IndexOf(myRows[0])].Selected = true;
                    return;
                }
                string[] myStringArry = new string[2];
                myStringArry[0] = serial_number.ToString();
                myStringArry[1] = str.ToString();
                myTable.Rows.Add(myStringArry);
                serial_number++;
                textBox_serial.Text = serial_number.ToString();
                //UpdateBarCodeText(barCode);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //*.bin, *.mot, *.hex, *.srec supported
            openFileDialog1.Filter = "固件文件(*.bin,*.mot,*.hex,*.srec)|*.bin;*.hex;*.mot;*.srec";
            openFileDialog1.FileName = "";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                binFilePath = openFileDialog1.FileName;
                textBox_path.Text = binFilePath;
                FileInfo fi = new FileInfo(binFilePath);
                //richText_log.AppendText("Bin file size:" + fi.Length + "\r\n");
            }
            else
            {
                textBox_path.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (0 != Check_Status_Enhanced(MCU_FAMILY))
            {
                return;
            }
            textBox_uid.Text = Get_MCU_MAC(MCU_MAC_ADDRESS, MCU_MAC_LENGTH, new_jlink);
            if ("" != textBox_uid.Text)
            {
                UpdateResultList(textBox_uid.Text);
                //MessageBox.Show("Read MCU MAC OK!\r\n");
            }
            else 
            {                
                return;
            }
            int res = MCU_Prog_Flash(MCU_DEVICE, MCU_ADDRESS_OFFSET, MCU_PROG_INTERFACE, MCU_PROG_SPEED, new_jlink, binFilePath);
            if(0 == res)
            {
                //richText_log.AppendText("MCU flash program OK!\r\n");
                MessageBox.Show("烧录成功！", "编程状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //richText_log.AppendText("MCU flash program error!\r\n");
                MessageBox.Show("烧录失败！", "编程状态", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (true == check_Fuse.Checked)
            {
                if (0 == MCU_Fuse_Flash())
                {
                    MessageBox.Show("MCU flash fuse OK!\r\n");
                }
                else
                {
                    MessageBox.Show("MCU flash fuse error!\r\n");
                }
            }
        }

        private void button_erase_Click(object sender, EventArgs e)
        {
            if (0 != Check_Status_Enhanced(MCU_FAMILY))
            {
                return;
            }
            textBox_uid.Text = Get_MCU_MAC(MCU_MAC_ADDRESS, MCU_MAC_LENGTH, new_jlink);
            if ("" != textBox_uid.Text)
            {
                UpdateResultList(textBox_uid.Text);
                //MessageBox.Show("Read MCU MAC OK!\r\n");
            }
            int res = MCU_Erase_Flash(MCU_DEVICE, MCU_ADDRESS_OFFSET, MCU_PROG_INTERFACE, MCU_PROG_SPEED, new_jlink);
            if (0 == res)
            {
                //richText_log.AppendText("MCU flash program OK!\r\n");
                MessageBox.Show("擦除成功！", "擦除状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //richText_log.AppendText("MCU flash program error!\r\n");
                MessageBox.Show("擦除失败！", "擦除状态", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (0 != Check_Status_Enhanced(MCU_FAMILY))
            {
                return;
            }
            textBox_uid.Text = Get_MCU_MAC(MCU_MAC_ADDRESS, MCU_MAC_LENGTH, new_jlink);
            if ("" != textBox_uid.Text)
            {
                UpdateResultList(textBox_uid.Text);
                MessageBox.Show("Read MCU UID OK!\r\n");
                //richText_uid.AppendText(textBox_uid.Text + "\r\n");
            }
            Clipboard.SetDataObject(textBox_uid.Text);
        }

        private void combo_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            MCU_DEVICE = combo_device.SelectedItem.ToString();
            switch (MCU_DEVICE)
            {
                case "STM32F030F4":
                case "STM32F031K4":
                case "STM32F051C8":
                    MCU_FAMILY = "Cortex-M0";
                    MCU_MAC_ADDRESS = 0x1FFFF7AC;
                    break;
                case "STM32L051K8":
                    MCU_FAMILY = "Cortex-M0";
                    MCU_MAC_ADDRESS = 0x1FF80050;
                    break;
                case "STM32F103C8":
                case "STM32F103RB":
                case "STM32F103VB":
                case "STM32F103VE":
                    MCU_FAMILY = "Cortex-M3";
                    MCU_MAC_ADDRESS = 0x1FFFF7E8;
                    break;
            }
            /*
            if (("STM32F051C8" == MCU_DEVICE)||("STM32F030F4" == MCU_DEVICE))
            {
                MCU_FAMILY = "Cortex-M0";
                MCU_MAC_ADDRESS = 0x1FFFF7AC;
            }
            else if (("STM32F103RB" == MCU_DEVICE) || ("STM32F103VB" == MCU_DEVICE))
            {
                MCU_FAMILY = "Cortex-M3";
                MCU_MAC_ADDRESS = 0x1FFFF7E8;
            }
             * */
        }

        private void textBox_serial_TextChanged(object sender, EventArgs e)
        {
            serial_number = int.Parse(textBox_serial.Text);
        }

        private void but_clear_Click(object sender, EventArgs e)
        {
            textBox_uid.Clear();
        }
    }
}
