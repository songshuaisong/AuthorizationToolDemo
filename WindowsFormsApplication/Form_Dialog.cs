using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public partial class Form_Dialog : Form
    {
        public const UInt16 CPUID_LENGTH = 8; /* CPUID的长度，字符串形式 */
        public const UInt16 MAC_LENGTH = 6; /* MAC地址的长度，字符串形式 */
        public const UInt16 CPUID_INDEX = 0;  /* CPUID在数组的下标 */
        public const UInt16 MAC_INDEX = 8;  /* MAC地址在数组中下标 */


        /* 加密实现的方法 */
        [DllImport("vvsGalPcakageMaker.dll", EntryPoint = "Encrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Encrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);
        /* 解密实现的方法 */
        [DllImport("vvsGalPcakageMaker.dll", EntryPoint = "Decrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Decrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);


        /*************************************************************************************************
            String_SendtoNUGCode：发送给新誉的序列号，为 CPUID + MAC + 随机码 组成的共16个字节的 十六进制字符串 的密文
            String_Dialog_TipMag：从主界面判断用于授权情况并在授权页面显示的提示语句
            UserOperationLevel  ：用于在授权页面进行按键操作的记录， 1：退出， 0：继续
        **************************************************************************************************/
        public string String_SendtoNUGCode = "";
        public string String_Dialog_TipMag = "";
        public Byte UserOperationLevel = 0;


    
        public Form_Dialog()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            // Remove the control box so the form will only display client area.
            this.ControlBox = false;
            //this.TopMost = true; /* 始终显示在所有程序的最前端 */
            this.WindowState = FormWindowState.Normal;

        }

        private void Form_Dialog_Load(object sender, EventArgs e)
        {
            /* 显示提示信息，来源于主界面 */
            label4.Text = String_Dialog_TipMag;

            label1.Text = "请将下面代码发送到新誉获取授权！";
            textBox2.Text = String_SendtoNUGCode;
            label2.Text = "请输入新誉发送的授权码！";
        }

        void print_Hex(String tipMsg, Byte[] buff, int len)
        {
            Console.WriteLine(tipMsg);
            for (int i = 0; i < len; i++)
                Console.Write(buff[i].ToString("X2"));
            Console.WriteLine();
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            /* 判断输入的授权的基础信息是否正常 */
            if (textBox1.Text.Trim().Equals("") || textBox1.Text.Trim().Length != 32)
            {
                label3.Text = "请输入正确的授权码！";
                label3.ForeColor = Color.Red;
                return;
            }

            /*****************************************************************************************************************
               textBox1.Text         ：授权码的原码字符串的密文，由新誉提供并从输入框获取到
               ManData_Code_Encrypted：授权码的原码字符串对应的数组的密文，由字符串筒灯转换而来
               ManData_Code_Decrypted：授权码的原码数组的明文，由密文解密后得来
            *****************************************************************************************************************/
            Byte[] ManData_Code_Encrypted = new Byte[16];
            Byte[] ManData_Code_Decrypted = new Byte[16];

            /*****************************************************************************************************************
                将十六进制字符串转换为对应的十六进制数组
                ManData_Code_Encrypted: 来源于新誉的加密着的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的密文，CPUID来源于新誉的授权码
            *****************************************************************************************************************/
            Form_Main.StringToHex(textBox1.Text.Trim(), ManData_Code_Encrypted);

            /* 输出到终端 */
            print_Hex("ManData_Code_Encrypted:", ManData_Code_Encrypted, 16);
            String_Dialog_TipMag = "";
            String_Dialog_TipMag += "\r\n原始授权码 ：" + ManData_Code_Encrypted;





            Byte[] tmp_Key = new Byte[16];
            Byte[] tmp_Iv = new Byte[16];
            /*****************************************************************************************************************
               将授权码解密，密钥和向量的规则如下： 
               tmp_Key 密钥：NU MAC CPUID 里面包含的CPIID来源于系统本身
               tmp_Iv  向量：CPUID MAC XZ 里面包含的MAC来源于系统本身
               CPUID_MAC_GetFrom_System ：从系统信息中获取到的CPUID和MAC地址，明文数组
            *****************************************************************************************************************/
            tmp_Key[0] = (Byte)'N';
            tmp_Key[1] = (Byte)'U';
            for (int i = 0; i < MAC_LENGTH; i++)
            {
                tmp_Key[2 + i] = Form_Main.CPUID_MAC_GetFrom_System[MAC_INDEX + i]; /* 将MAC赋值到tmpKey的对应的位置 */
            }
            for (int i = 0; i < CPUID_LENGTH; i++)
            {
                tmp_Key[8 + i] = Form_Main.CPUID_MAC_GetFrom_System[CPUID_INDEX + i]; /* 将CPUID赋值到tmpKey的对应的位置 */
            }
            for (int i = 0; i < CPUID_LENGTH; i++)
            {
                tmp_Iv[i] = Form_Main.CPUID_MAC_GetFrom_System[CPUID_INDEX + i];  /* 将CPUID赋值到tmpIv的对应的位置 */
            }
            for (int i = 0; i < MAC_LENGTH; i++)
            {
                tmp_Iv[8 + i] = Form_Main.CPUID_MAC_GetFrom_System[MAC_INDEX + i]; /* 将MAC赋值到tmpIv的对应的位置 */
            }
            tmp_Iv[14] = (Byte)'X';
            tmp_Iv[15] = (Byte)'Z';




            /* 输出到终端 */
            print_Hex("tmpKey:", tmp_Key, 16);
            print_Hex("tmpIv :", tmp_Iv,  16);
            String tmpppp1 = "", tmpppp2 = "";
            for (int i = 0; i < 16; i++)
            {
                tmpppp1 += tmp_Key[i].ToString("X2") + " ";
                tmpppp2 += tmp_Iv[i].ToString("X2") + " ";
            }
            String_Dialog_TipMag += "\r\n重组后的密钥 ：" + tmpppp1 + "\r\n重组后的向量 ：" + tmpppp2;




            /*****************************************************************************************************************
                用新组成的秘钥解密授权码
                tmp_Key ：NU MAC CPUID 里面包含的 CPIID 和 MAC 来源于系统信息
                tmp_Iv  ：CPUID MAC XZ 里面包含的 CPIID 和 MAC 来源于系统信息
                ManData_Code_Encrypted: 来源于新誉的加密着的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的密文，CPUID来源于新誉的授权码
                ManData_Code_Decrypted: 来源于新誉的解密后的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的明文，CPUID来源于新誉的授权码
            *****************************************************************************************************************/
            Decrypted_CBC(ManData_Code_Encrypted, 16, tmp_Key, tmp_Iv, ManData_Code_Decrypted);

            /* 输出到终端 */
            print_Hex("ManData_Code_Decrypted:", ManData_Code_Decrypted, 16);
            tmpppp1 = "";
            for (int i = 0; i < 16; i++)
            {
                tmpppp1 += ManData_Code_Decrypted[i].ToString("X2") + " ";
            }
            String_Dialog_TipMag += "\r\n解密后授权码 ：" + tmpppp1;




            /*************************************************************************************************
               使用组合的新版密钥和向量加密特定的字符串
               特定字符串为：vvs-v.2.0 CPUID， 里面包含的 CPIID 来源于系统信息
           **************************************************************************************************/
            Byte[] tmp_Dst = new Byte[16];
            tmp_Dst[0] = (Byte)'v';
            tmp_Dst[1] = (Byte)'v';
            tmp_Dst[2] = (Byte)'s';
            tmp_Dst[3] = (Byte)'-';
            tmp_Dst[4] = (Byte)'v';
            tmp_Dst[5] = (Byte)'2';
            tmp_Dst[6] = (Byte)'.';
            tmp_Dst[7] = (Byte)'0';
            for(int i = 0; i < 8; i++)
            {
                /**************************************************************************************************************************** 
                    CPUID_MAC_GetFrom_System : 加上 8字节CPUID 的明文，CPUID 来源于 系统信息
                *****************************************************************************************************************************/
                tmp_Dst[i + 8] = Form_Main.CPUID_MAC_GetFrom_System[CPUID_INDEX + i];
            }

            /* 输出到终端 */
            print_Hex("tmp_Dst:", tmp_Dst, 16);
            tmpppp1 = "";
            for (int i = 0; i < 16; i++)
            {
                tmpppp1 += tmp_Dst[i].ToString("X2") + " ";
            }
            String_Dialog_TipMag += "\r\n原始的比较码 ：" + tmpppp1;






            for (int i = 0; i < 16; i++)
            {
                /****************************************************************************************************************************
                    tmp_Dst         : 来源于系统自定义的授权码校验码，包含的信息：vvs-v2.0 加上 8字节CPUID 的明文，CPUID来源于 系统信息
                    Decrypted_Output: 来源于新誉的解密后的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的明文，CPUID来源于新誉的 授权码
                *****************************************************************************************************************************/
                if (!tmp_Dst[i].Equals(ManData_Code_Decrypted[i]))
                {
                    label3.Text = "请输入正确的授权码！";
                    label3.ForeColor = Color.Red;
                    return;
                }
            }

            /****************************************************************************************************************************
                tmp_SerialNum_Buff : 序列号 的明文对应的十六进制数组，密文
            *****************************************************************************************************************************/
            Byte[] tmp_SerialNum_Buff = new Byte[16];
            Form_Main.StringToHex(String_SendtoNUGCode, tmp_SerialNum_Buff);

            /* 输出到终端 */
            print_Hex("String_SendtoNUGCodeBuff:", tmp_SerialNum_Buff, 16);
            /* 输出到终端 */
            print_Hex("String_SendtoNUGCodeBuff:", ManData_Code_Encrypted, 16);

            /****************************************************************************************************************************
                xor_Value      : 生成随机数用于修改 序列号 和 授权码
                write_File_Buff：要写入到文件中的序列号和授权码暂存数组，已进行异或操作
            *****************************************************************************************************************************/
            Random ran = new Random();
            Byte xor_Value = (Byte)ran.Next(0x00, 0xFF);
            Byte[] write_File_Buff = new Byte[40];

            /****************************************************************************************************************************
                write_File_Buff：固定位置为 异或 的数值
            *****************************************************************************************************************************/
            write_File_Buff[0] = xor_Value;
            write_File_Buff[9] = xor_Value;
            write_File_Buff[18] = xor_Value;
            write_File_Buff[27] = xor_Value;
            /**************************************************************************************************
                写入文件的内容（二进制）为：
                    随机码1字节 原序列号的前8字节 随机码1字节 后授权码的前8字节
                    随机码1字节 后授权码的后8字节 随机码1字节 原序列号的后8字节
                    字节的校验和2字节
            **************************************************************************************************/
            for (int i = 0; i < 8; i++)
            {
                /****************************************************************************************************************************
                    write_File_Buff       ：要写入到文件中的序列号和授权码暂存数组，已进行异或操作
                    tmp_SerialNum_Buff    : 来源于本系统自身生成的加密后的序列号，包含：8字节CPUID + 6字节MAC + 2字节随机码 的密文，CPUID和MAC来源于系统本身
                    ManData_Code_Encrypted: 来源于新誉的加密后的授权码，包含的信息：vvs-v2.0 + 8字节CPUID 的密文，CPUID来源于新誉的授权码
                *****************************************************************************************************************************/
                write_File_Buff[1 + i] = (Byte)(tmp_SerialNum_Buff[i] ^ xor_Value);
                write_File_Buff[10 +i] = (Byte)(ManData_Code_Encrypted[8 + i] ^ xor_Value);
                write_File_Buff[19 +i] = (Byte)(ManData_Code_Encrypted[i] ^ xor_Value);
                write_File_Buff[28 +i] = (Byte)(tmp_SerialNum_Buff[8 + i] ^ xor_Value);
            }

            for(int i = 0; i < 18; i++)
            {
                /**************************************************************************************************
                    计算文件的校验和，文件中的校验和位于第37,38字节两个字节位置
                    write_File_Buff[36]：前面的18个字节的加和
                    write_File_Buff[37]：后面的18个字节的加和
                **************************************************************************************************/
                write_File_Buff[36] += write_File_Buff[i];
                write_File_Buff[37] += write_File_Buff[18 + i];
            }

            /* 输出到终端 */
            print_Hex("write_File_Buff:", write_File_Buff, 38);


            #region 下面是将数值异或后在此异或回来的测试操作
            /****************************************************************************************************************************
                write_File_Buff     ：要写入到文件中的序列号和授权码暂存数组，已进行异或操作
                write_File_Buff_Xor ：将写入文件中的系列号和授权码在此异或进行与原始数据进行比较，以保证正确性
            *****************************************************************************************************************************/
            Byte[] write_File_Buff_Xor = new Byte[38];
            for (int i = 0; i < 36; i++)
            {
                write_File_Buff_Xor[i] = (Byte)(write_File_Buff[i] ^ xor_Value);
            }
            /* 输出到终端 */
            print_Hex("tempOutputBuff:", write_File_Buff_Xor, 38);
            #endregion

            /****************************************************************************************************************************
                将组合后的信息写入到指定文件中
                write_File_Buff     ：要写入到文件中的序列号和授权码暂存数组，已进行异或操作
            *****************************************************************************************************************************/
            FileStream inStream = new FileStream("vvs-cfg\\vvsconfig.bin", FileMode.OpenOrCreate, FileAccess.Write);
            inStream.Write(write_File_Buff, 0, write_File_Buff.Length);
            inStream.Close();
            /* 用户操作的是确定操作并且校验通过了 */
            UserOperationLevel = 0; 
            /* 将此窗体进行影藏显示 */
            this.Hide();
        }

        bool userOnClosing()
        {
            string abcd = "                         ";
            DialogResult dr = MessageBox.Show(abcd + "是否退出应用？", "提示", MessageBoxButtons.YesNo);
            if (DialogResult.No == dr)//如果点否按钮  
            {
                return false;
                // e.Cancel = true;//取消关闭  
            }
            else
            {
                while (this.Opacity > 0)//在点击退出按键之后，界面渐渐消失的效果
                {
                    this.Opacity -= 0.02;
                    System.Threading.Thread.Sleep(20);
                }
                Application.Exit();
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* 用户选择的是退出操作 */
            UserOperationLevel = 1;
            /* 选择退出，则关闭窗体并且退出程序 */
            userOnClosing();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            label3.Text = "";
            label3.ForeColor = Color.Red;
        }


       
    }
}
 