using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

namespace WindowsFormsApplication
{
    public partial class Form_Main : Form
    {
        public const UInt16 CPUID_LENGTH = 8; /* CPUID的长度，字符串形式 */
        public const UInt16 MAC_LENGTH = 6; /* MAC地址的长度，字符串形式 */
        public const UInt16 CPUID_INDEX = 0;  /* CPUID在数组的下标 */
        public const UInt16 MAC_INDEX = 8;  /* MAC地址在数组中下标 */


        /* 加密实现的方法 */
        [DllImport("vvs-cfg\\vvsGalPcakageMaker.dll", EntryPoint = "Encrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Encrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);
        /* 解密实现的方法 */
        [DllImport("vvs-cfg\\vvsGalPcakageMaker.dll", EntryPoint = "Decrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Decrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);

        /****************************************************************************************
             encDec_Key_DefaultValue：生成系统初始加密解密的默认的密钥
             encDec_Iv_DefaultValue ：生成系统初始加密解密的默认的向量
        ****************************************************************************************/
        public static Byte[] encDec_Key_DefaultValue = new Byte[16];
        public static Byte[] encDec_Iv_DefaultValue = new Byte[16];

        /****************************************************************************************
             String_CPUID_Mac_FromSystem：从系统信息中获取到的CPUID和MAC地址，明文字符串
             CPUID_MAC_GetFrom_System   ：从系统信息中获取到的CPUID和MAC地址，明文数组
        ****************************************************************************************/
        public static String String_CPUID_Mac_FromSystem = "";
        public static Byte[] CPUID_MAC_GetFrom_System = new Byte[16];
        
        public Form_Main()
        {
            /* 窗体显示之前先保证透明度为0，即不显示界面 */
            //this.Opacity = 0;

            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.MaximizeBox = false;
            //this.ControlBox = false; /* 不显示控制按键 */
            //this.FormBorderStyle = FormBorderStyle.None; /* 不显示标题栏 */
            // this.ShowInTaskbar = false; /* 任务栏不显示图标 */
            this.StartPosition = FormStartPosition.CenterScreen;


            label_Tip_MessageShow.BackColor = Color.Transparent;
            label_Tip_MessageShow.Parent = pictureBox1;

            /* 初始化密钥 */
            encDec_Key_Iv_DefaultValue_Init();

            /****************************************************************************************
                将信息显示
            ****************************************************************************************/
            String tmpMesg = "\t授权信息显示如下:\r\n\r\n默认密钥为：";
            for(int i = 0; i < 16; i++)
            {
                tmpMesg += encDec_Key_DefaultValue[i].ToString("X2") + " ";
            }
            tmpMesg += "\r\n        即：";
            tmpMesg += System.Text.Encoding.ASCII.GetString(encDec_Key_DefaultValue);
            tmpMesg += "\r\n默认向量为：";
            for (int i = 0; i < 16; i++)
            {
                tmpMesg += encDec_Iv_DefaultValue[i].ToString("X2") + " ";
            }
            tmpMesg += "\r\n        即：";
            tmpMesg += System.Text.Encoding.ASCII.GetString(encDec_Iv_DefaultValue);
            /****************************************************************************************
                将信息显示
            ****************************************************************************************/
            label_Tip_MessageShow.Text = tmpMesg;
         
        }

        void encDec_Key_Iv_DefaultValue_Init()
        {
            /****************************************************************************************
                生成系统初始加密解密的默认的密钥
            ****************************************************************************************/
            /* 密钥：NewUnitGroupMake */
            encDec_Key_DefaultValue[0x00] = (Byte)'N';
            encDec_Key_DefaultValue[0x01] = (Byte)'e';
            encDec_Key_DefaultValue[0x02] = (Byte)'w';
            encDec_Key_DefaultValue[0x03] = (Byte)'U';
            encDec_Key_DefaultValue[0x04] = (Byte)'n';
            encDec_Key_DefaultValue[0x05] = (Byte)'i';
            encDec_Key_DefaultValue[0x06] = (Byte)'t';
            encDec_Key_DefaultValue[0x07] = (Byte)'G';
            encDec_Key_DefaultValue[0x08] = (Byte)'r';
            encDec_Key_DefaultValue[0x09] = (Byte)'o';
            encDec_Key_DefaultValue[0x0A] = (Byte)'u';
            encDec_Key_DefaultValue[0x0B] = (Byte)'p';
            encDec_Key_DefaultValue[0x0C] = (Byte)'M';
            encDec_Key_DefaultValue[0x0D] = (Byte)'a';
            encDec_Key_DefaultValue[0x0E] = (Byte)'k';
            encDec_Key_DefaultValue[0x0F] = (Byte)'e';
            /* 向量：vvs-2.0byATOTeam */
            encDec_Iv_DefaultValue[0x00] = (Byte)'v';
            encDec_Iv_DefaultValue[0x01] = (Byte)'v';
            encDec_Iv_DefaultValue[0x02] = (Byte)'s';
            encDec_Iv_DefaultValue[0x03] = (Byte)'-';
            encDec_Iv_DefaultValue[0x04] = (Byte)'2';
            encDec_Iv_DefaultValue[0x05] = (Byte)'.';
            encDec_Iv_DefaultValue[0x06] = (Byte)'0';
            encDec_Iv_DefaultValue[0x07] = (Byte)'b';
            encDec_Iv_DefaultValue[0x08] = (Byte)'y';
            encDec_Iv_DefaultValue[0x09] = (Byte)'A';
            encDec_Iv_DefaultValue[0x0A] = (Byte)'T';
            encDec_Iv_DefaultValue[0x0B] = (Byte)'O';
            encDec_Iv_DefaultValue[0x0C] = (Byte)'T';
            encDec_Iv_DefaultValue[0x0D] = (Byte)'e';
            encDec_Iv_DefaultValue[0x0E] = (Byte)'a';
            encDec_Iv_DefaultValue[0x0F] = (Byte)'m';
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            MachineInfo info = MachineInfo.I();       //获取主机的对象信息

            /****************************************************************************************
                获取CPUID，字符串形式，十六进制字符串，8字节
                String_CPUID_Mac_FromSystem ：CPUID
            ****************************************************************************************/
            String_CPUID_Mac_FromSystem += info.GetCPUSerialNumber();// 1F8BFBFF00000657
            
            /****************************************************************************************
                String_CPUID_Mac_FromSystem ：CPUID + MAC
                获取MAC，字符串形式，十六进制字符串，6字节
            ****************************************************************************************/
            String_CPUID_Mac_FromSystem += info.GetNetCardMACAddress(); // 00505689F6AF


            label_Tip_MessageShow.Text += "\r\nCPU序列号：" + info.GetCPUSerialNumber();
            label_Tip_MessageShow.Text += "\r\n网卡地址 ：" + info.GetNetCardMACAddress();




            /* 生成一个的随机数 */
            Random ran = new Random();
            /****************************************************************************************
                判断字符串的长度是否符合预期(预期为CPUID+MAC总共28个字符)，如果不符合，则说明获取某个信息失败，使用随机数进行填充
                将字符串 String_CPUID_Mac_FromSystem 的长度凑整为 32
                String_CPUID_Mac_FromSystem ：CPUID + MAC + 随机码，字符串明文
                CPUID_MAC_GetFrom_System    ：CPUID + MAC + 随机码，数组明文
            ****************************************************************************************/
            for (int cnt = String_CPUID_Mac_FromSystem.Length; cnt < 32; cnt ++)
            {
                String_CPUID_Mac_FromSystem += ((Byte)ran.Next(0x0, 0xF)).ToString("X");
            }


            label_Tip_MessageShow.Text += "\r\n序列号明文 ：" + String_CPUID_Mac_FromSystem;
            Console.WriteLine("序列号明文：\r\n" + String_CPUID_Mac_FromSystem);


            /****************************************************************************************
                将十六进制字符串转换为对应的十六进制数组
               String_CPUID_Mac_FromSystem ：CPUID + MAC ，字符串明文
               CPUID_MAC_GetFrom_System    ：CPUID + MAC ，数组明文
            ****************************************************************************************/
            StringToHex(String_CPUID_Mac_FromSystem, CPUID_MAC_GetFrom_System);

            /****************************************************************************************
                CPUID_MAC_GetFrom_System ：从系统获取的 CPUID + MAC + 随机码，数组明文
                CPUID_MAC_Encrypted      ：加密后输出的 CPUID + MAC + 随机码，数组密文
            ****************************************************************************************/
            Byte[] CPUID_MAC_Encrypted = new Byte[16];

            /****************************************************************************************
               CPUID_MAC_GetFrom_System ：从系统获取的 CPUID + MAC + 随机码，数组明文
               encDec_Key_DefaultValue  ：系统初始加密解密的默认的密钥，值为：NewUnitGroupMake 
               encDec_Iv_DefaultValue   ：系统初始加密解密的默认的向量，值为：vvs-2.0byATOTeam
               CPUID_MAC_Encrypted      ：加密后的序列号密文，CPUID MAC 随机码 的数组密文
            ****************************************************************************************/
            Encrypted_CBC(CPUID_MAC_GetFrom_System, 16, encDec_Key_DefaultValue, encDec_Iv_DefaultValue, CPUID_MAC_Encrypted);

            /****************************************************************************************
                CPUID_MAC_Encrypted     ：加密后的序列号密文，CPUID MAC 随机码 的数组密文
                serial_Number_Encrypted ：加密后的序列号密文 CPUID + MAC + 随机码，数组密文，授权所需的序列号
            ****************************************************************************************/
            String serial_Number_Encrypted = "";
            for (int i = 0; i < 16; i++)
            {
                serial_Number_Encrypted += CPUID_MAC_Encrypted[i].ToString("X2");
            }




            label_Tip_MessageShow.Text += "\r\n序列号密文 ：" + serial_Number_Encrypted;






            /****************************************************************************************
                将序列号输出到授权界面进行显示
            ****************************************************************************************/
            f111111111111111.String_SendtoNUGCode = serial_Number_Encrypted;

            /**************************************************************************************************
               判断是否在VVS-cf目录下存在这个文件
               如果存在则打开进行判断，判断不通过则弹窗吧
               如果不存在就开始弹窗吧
            **************************************************************************************************/
            if (!File.Exists("vvs-cfg\\vvsconfig.bin"))
            {
                f111111111111111.String_Dialog_TipMag = "请将序列号联系新誉相关人员获取授权！";

                /* 弹窗显示信息 */
                showSubForm(f111111111111111);
                /* 判断弹窗界面用户选择的操作 1：退出 */
                if (f111111111111111.UserOperationLevel == 1)
                {
                    Application.Exit();
                }
                else
                {
                    /* 窗体显示，透明度为1 */
                    this.Opacity = 1;

                    label_Tip_MessageShow.Text += f111111111111111.String_Dialog_TipMag;
                }
            }
            else
            {
                try
                {
                    /* 读取二进制文件 */
                    FileStream inStream = new FileStream("vvs-cfg\\vvsconfig.bin", FileMode.Open, FileAccess.Read);
                    long nBytesToRead = inStream.Length;
                    /**************************************************************************************************
                       文件存在，说明可能授权过一次了
                       Read_File_Buffer：读取到的授权文件的内容，二进制文件
                    **************************************************************************************************/
                    byte[] Read_File_Buffer = new byte[nBytesToRead];
                    int m = inStream.Read(Read_File_Buffer, 0, Read_File_Buffer.Length);
                    inStream.Close();

                    /**************************************************************************************************
                       判断读取的文件内容的相关属性是否正确，固定为40字节
                    **************************************************************************************************/
                    if (Read_File_Buffer.Length != 40) /* 固定的二进制文件的长度 */
                    {
                        /* 设置弹窗的提示信息 */
                        f111111111111111.String_Dialog_TipMag = "请勿修改配置文件，请重新授权！";
                        /* 弹窗显示信息 */
                        showSubForm(f111111111111111);
                        return;
                    }

                    /* 二进制文件的校验和判断，是否被修改 */
                    Byte[] Check_Value = new Byte[2];
                    for (int i = 0; i < 18; i++)
                    {
                        /**************************************************************************************************
                           计算文件的校验和，文件中的校验和位于第37,38字节两个字节位置
                            Check_Value[0]：前面的18个字节的加和
                            Check_Value[1]：后面的18个字节的加和
                        **************************************************************************************************/
                        Check_Value[0] += Read_File_Buffer[i];
                        Check_Value[1] += Read_File_Buffer[18 + i];
                    }
                    /**************************************************************************************************
                        判断读取到自己计算的校验和与文件中记录的校验和是否一致
                    **************************************************************************************************/
                    if (Check_Value[0] != Read_File_Buffer[36] ||
                        Check_Value[1] != Read_File_Buffer[37])
                    {
                        /* 设置弹窗的提示信息 */
                        f111111111111111.String_Dialog_TipMag = "请勿修改配置文件，请重新授权！";
                        /* 弹窗显示信息 */
                        showSubForm(f111111111111111);
                        return;
                    }



                    String tmpppp1 = "", tmpppp2 = "" ;
                    for(int i = 0; i < 8; i++)
                    {
                        tmpppp1 += Read_File_Buffer[19 + i].ToString("X2"); 
                        tmpppp2 += Read_File_Buffer[10 + i].ToString("X2");
                    }
                    label_Tip_MessageShow.Text += "\r\n读取的序列号 ：" + tmpppp1 + tmpppp2;






                    /****************************************************************************************************
                        xor_Value             ：取到字符异或的随机值，在写入文件之前通过随机数得到
                        Read_File_Buffer      ：授权文件中记录的值，为密文的 序列号 和 授权码 通过xor_Value 异或 后值，以及原始的 xor_Value 值
                        CPUID_MAC_GetFrom_File：从 Read_File_Buffer 中获取到原始的 授权码 的密文
                    ****************************************************************************************************/
                    Byte[] CPUID_MAC_GetFrom_File = new Byte[16];

                    /**************************************************************************************************
                       写入文件的内容（二进制）为：
                           随机码（1字节） 原序列号的前8字节 随机码（1字节） 后授权码的前8字节
                           随机码（1字节） 后授权码的后8字节 随机码（1字节） 原序列号的后8字节
                           字节的校验和（2字节）
                   **************************************************************************************************/
                    Byte xor_Value = Read_File_Buffer[0]; /* 取到字符异或的随机值，在写入文件之前通过随机数得到 */

                    for (int i = 0; i < 8; i++)
                    {
                        /****************************************************************************************************
                            通过再次异或上次异或操作的值，即可获取原始的数据
                        ****************************************************************************************************/
                        CPUID_MAC_GetFrom_File[8 + i] = (Byte)(Read_File_Buffer[10 + i] ^ xor_Value);
                        CPUID_MAC_GetFrom_File[0 + i] = (Byte)(Read_File_Buffer[19 + i] ^ xor_Value);
                    }





                    tmpppp1 = "";
                    for (int i = 0; i < 16; i++)
                    {
                        tmpppp1 += CPUID_MAC_GetFrom_File[i].ToString("X2");
                    }
                    label_Tip_MessageShow.Text += "\r\n异或后序列号 ：" + tmpppp1 ;






                    Byte[] tmp_Key = new Byte[16];
                    Byte[] tmp_Iv = new Byte[16];
                    /*************************************************************************************************
                       将授权码解密，密钥和向量的规则如下： 
                       tmp_Key ：由 NU MAC CPUID 组成，里面包含的 CPIID 和 MAC 来源于系统本身
                       tmp_Iv  ：由 CPUID MAC XZ 组成，里面包含的 CPIID 和 MAC 来源于系统本身
                    **************************************************************************************************/
                    tmp_Key[0] = (Byte)'N';
                    tmp_Key[1] = (Byte)'U';
                    for (int i = 0; i < MAC_LENGTH; i++)
                    {
                        tmp_Key[2 + i] = CPUID_MAC_GetFrom_System[MAC_INDEX + i];
                    }
                    for (int i = 0; i < CPUID_LENGTH; i++)
                    {
                        tmp_Key[8 + i] = CPUID_MAC_GetFrom_System[CPUID_INDEX + i];
                    }
                    for (int i = 0; i < CPUID_LENGTH; i++)
                    {
                        tmp_Iv[i] = CPUID_MAC_GetFrom_System[CPUID_INDEX + i];
                    }
                    for (int i = 0; i < MAC_LENGTH; i++)
                    {
                        tmp_Iv[8 + i] = CPUID_MAC_GetFrom_System[MAC_INDEX + i];
                    }
                    tmp_Iv[14] = (Byte)'X';
                    tmp_Iv[15] = (Byte)'Z';


                    tmpppp1 = "";
                    tmpppp2 = "";
                    for (int i = 0; i < 16; i++)
                    {
                        tmpppp1 += tmp_Key[i].ToString("X2") + " ";
                        tmpppp2 += tmp_Iv[i].ToString("X2") + " ";
                    }
                    label_Tip_MessageShow.Text += "\r\n重组后的密钥 ：" + tmpppp1 + "\r\n重组后的向量 ：" + tmpppp2;
                    /* 输出到终端 */
                    print_Hex("tmpKey:", tmp_Key, 16);
                    print_Hex("tmpIv :", tmp_Iv, 16);




                    Byte[] Decrypted_Output = new Byte[32];
                    /**************************************************************************************************************************** 
                       用新组成的秘钥解密授权码 
                       CPUID_MAC_GetFrom_File: 从加密文件中获取到新誉发送过来的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的密文，CPUID来源于新誉的授权码
                       Decrypted_Output      : 从加密文件中获取到新誉发送过来的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的明文，CPUID来源于新誉的授权码
                    *****************************************************************************************************************************/
                    Decrypted_CBC(CPUID_MAC_GetFrom_File, 16, tmp_Key, tmp_Iv, Decrypted_Output);




                    /* 输出到终端 */
                    print_Hex("Decrypted_Output:", Decrypted_Output, 16);
                    tmpppp1 = "";
                    for (int i = 0; i < 16; i++)
                    {
                        tmpppp1 += Decrypted_Output[i].ToString("X2") + " ";
                    }
                    label_Tip_MessageShow.Text += "\r\n解密后授权码 ：" + tmpppp1;





                    /* 输出字符串比较 vvs-v2.0+cpuid */
                    Byte[] tmp_Dst = new Byte[16];
                    tmp_Dst[0] = (Byte)'v';
                    tmp_Dst[1] = (Byte)'v';
                    tmp_Dst[2] = (Byte)'s';
                    tmp_Dst[3] = (Byte)'-';
                    tmp_Dst[4] = (Byte)'v';
                    tmp_Dst[5] = (Byte)'2';
                    tmp_Dst[6] = (Byte)'.';
                    tmp_Dst[7] = (Byte)'0';
                    for (int i = 0; i < 8; i++)
                    {
                        /**************************************************************************************************************************** 
                            CPUID_MAC_GetFrom_System : 加上 8字节CPUID 的明文，CPUID 来源于 系统信息
                        *****************************************************************************************************************************/
                        tmp_Dst[i + 8] = CPUID_MAC_GetFrom_System[i];
                    }

                    /* 输出到终端 */
                    print_Hex("tmp_Dst:", tmp_Dst, 16);


                    for (int i = 0; i < 16; i++)
                    {
                        /****************************************************************************************************************************
                            tmp_Dst         : 来源于系统自定义的授权码校验码，包含的信息：vvs-v2.0 加上 8字节CPUID 的明文，CPUID来源于 系统信息
                            Decrypted_Output: 来源于新誉的解密后的授权码，包含的信息：vvs-v2.0 加上 8字节CPUID 的明文，CPUID来源于新誉的 授权码
                        *****************************************************************************************************************************/
                        if (!tmp_Dst[i].Equals(Decrypted_Output[i]))
                        {
                            /* 设置弹窗的提示信息 */
                            f111111111111111.String_Dialog_TipMag = "请勿修改配置文件，请重新授权！";
                            /* 弹窗显示信息 */
                            showSubForm(f111111111111111);
                            return;
                        }
                    }



                    tmpppp1 = "";
                    for (int i = 0; i < 16; i++)
                    {
                        tmpppp1 += tmp_Dst[i].ToString("X2") + " ";
                    }
                    label_Tip_MessageShow.Text += "\r\n解密后比较码 ：" + tmpppp1;





                    /* 窗体显示，透明度为1 */
                    this.Opacity = 1;
                }
                catch
                {
                }
            }
            
        }

        public static int StringToHex(string str, byte[] dat)
        {
            string p = str;
            int i = 0;
            int cnt = 0, tmplen = str.Length;
            if (str.IndexOf(" ") >= 0)
            {
                string[] strHex = str.Split(' ');
                tmplen = strHex.Length;
                if (strHex[strHex.Length - 1] == "")
                {
                    tmplen = strHex.Length - 1;
                }
                for (i = 0; i < tmplen; i++)
                {
                    dat[cnt] = (byte)Convert.ToInt32(strHex[i], 16);
                    cnt += 1;
                }
            }
            else
            {
                while (cnt < tmplen / 2)
                {
                    string tmpStr = str.Substring(i, 2);

                    dat[cnt] = (byte)Convert.ToInt32(tmpStr, 16);
                    cnt++;
                    i += 2;
                }
            }
            return cnt;
        }

        void print_Hex(String tipMsg, Byte[] buff, int len)
        {
            Console.WriteLine(tipMsg);
            for (int i = 0; i < len; i++)
                Console.Write(buff[i].ToString("X2"));
            Console.WriteLine();
        }


        Form_Dialog f111111111111111 = new Form_Dialog();

        /* 显示子窗体 */
        private void showSubForm(Form f)
        {
            f.BringToFront();                        /*将该窗体置到最上层*/
            f.WindowState = FormWindowState.Normal;  /*设置窗体大小为默认大小(显示已最小化的窗体)*/
            f.TopLevel = true;//置于最顶层
            f.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            string abcd = "                         ";
            DialogResult dr = MessageBox.Show(abcd + "是否退出应用？", "提示", MessageBoxButtons.YesNo);
            if (DialogResult.No == dr)//如果点否按钮  
            {
                e.Cancel = true;//取消关闭  
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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
#if false
        /// <summary>
        /// 这里是获取cpu的id
        /// </summary>
        /// <returns></returns>
        public string GetProcessID()
        {
            try
            {
                string str = string.Empty;
                ManagementClass mcCpu = new ManagementClass("win32_Processor");
                ManagementObjectCollection mocCpu = mcCpu.GetInstances();
                foreach (ManagementObject m in mocCpu)
                {
                    str = m["Processorid"].ToString().Trim().Substring(0, 8);//BFEBFBFF00000F65
                }
                return str;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 获取硬盘id
        /// </summary>
        /// <returns></returns>
        public string GetHardDiskID()
        {
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass("win32_DiskDrive");
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    //hdId = m["Model"].ToString().Trim();
                    hdId = m.Properties["Model"].Value.ToString();//WDC WD800BB-56JKC0
                }
                return hdId;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取网卡地址
        /// </summary>
        /// <returns></returns>
        public string GetNetwordAdapter()
        {
            try
            {
                string MoAddress = string.Empty;
                ManagementClass networkAdapter = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection adapterC = networkAdapter.GetInstances();
                foreach (ManagementObject m in adapterC)
                {
                    if ((bool)m["IPEnabled"] == true)
                    {
                        MoAddress = m["MacAddress"].ToString().Trim();
                        m.Dispose();
                    }
                }
                return MoAddress;
            }
            catch
            {
                return "";
            }
        }
        public string GetBaseboard()
        {
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
                string serNumber = string.Empty;
                string manufacturer = string.Empty;
                string product = string.Empty;

                foreach (ManagementObject m in mos.Get())
                {
                    serNumber = m["SerialNumber"].ToString();//序列号
                    manufacturer = m["Manufacturer"].ToString();//制造商
                    product = m["Product"].ToString();//型号
                }
                return serNumber + " " + manufacturer + " " + product;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 加密算法（利用到了cpuid）
        /// </summary>
        /// <param name="data">要加密的字符串</param>
        /// <returns></returns>
        public string Encode(string data, byte[] key, byte[] iv)
        {
            using (DESCryptoServiceProvider CP = new DESCryptoServiceProvider())
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, CP.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(data);
                sw.Flush();
                cs.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                //String strRet = "";
                //for(int i = 0; i < (int)ms.Length; i++)
                //{
                //    strRet += ms.GetBuffer()[i].ToString("X2");
                //}
                //return strRet;
            }
        }
        /// <summary>
        /// 加密算法（利用cpuid）
        /// </summary>
        /// <param name="data">需要解密的字符串</param>
        /// <returns></returns>
        public static string Decode(string data, byte[] key, byte[] iv)
        {
            byte[] Enc = null;
            try
            {
                Enc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cp = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(Enc);
            CryptoStream cs = new CryptoStream(ms, cp.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }
#endif
    }
    //此类用于获取当前主机的相关信息
    public class MachineInfo
    {
        static MachineInfo Instance;

        /// <summary>
        /// 获取当前类对象的一个实例
        /// </summary>
        public static MachineInfo I()
        {
            if (Instance == null) Instance = new MachineInfo();
            return Instance;
        }

        /// <summary>
        /// 获取本地ip地址，多个ip
        /// </summary>
        public String[] GetLocalIpAddress()
        {
            string hostName = Dns.GetHostName();                    //获取主机名称
            IPAddress[] addresses = Dns.GetHostAddresses(hostName); //解析主机IP地址


            string[] IP = new string[addresses.Length];             //转换为字符串形式
            for (int i = 0; i < addresses.Length; i++) IP[i] = addresses[i].ToString();


            return IP;
        }



        /// <summary>
        /// 获取外网ip地址
        /// </summary>
        public string[] GetExtenalIpAddress()
        {
            string[] IP = new string[] { "未获取到外网ip", "" };

            string address = "http://1111.ip138.com/ic.asp";
            string str = GetWebStr(address);

            try
            {
                //提取外网ip数据 [218.104.71.178]
                int i1 = str.IndexOf("[") + 1, i2 = str.IndexOf("]");
                IP[0] = str.Substring(i1, i2 - i1);

                //提取网址说明信息 "来自：安徽省合肥市 联通"
                i1 = i2 + 2; i2 = str.IndexOf("<", i1);
                IP[1] = str.Substring(i1, i2 - i1);
            }
            catch (Exception)
            { }

            return IP;
        }


        /// <summary>
        /// 获取网址address的返回的文本串数据
        /// </summary>
        public string GetWebStr(string address)
        {
            string str = "";
            try
            {
                //从网址中获取本机ip数据
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.Default;
                str = client.DownloadString(address);
                client.Dispose();
            }
            catch (Exception) { }


            return str;
        }

        
        //只能获取同网段的远程主机MAC地址. 因为在标准网络协议下，ARP包是不能跨网段传输的，故想通过ARP协议是无法查询跨网段设备MAC地址的。
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        /// <summary>
        /// 获取ip对应的MAC地址
        /// </summary>
        public string GetMacAddress(string ip)
        {
            Int32 ldest = inet_addr(ip);            //目的ip 
            Int32 lhost = inet_addr("127.0.0.1");   //本地ip 
            
            try
            {
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);  //使用系统API接口发送ARP请求，解析ip对应的Mac地址
                return Convert.ToString(macinfo, 16);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:{0}", err.Message);
            }
            return "";
        }


        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        public string GetBIOSSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string sBIOSSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sBIOSSerialNumber = mo["SerialNumber"].ToString().Trim();
                }
                return sBIOSSerialNumber;
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 获得CPU编号
        /// </summary>
        public string GetCPUID()
        {
            string cpuid = "";
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuid = mo.Properties["ProcessorId"].Value.ToString();
            }
            return cpuid;
        }
        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns></returns>
        public string GetCPUSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Processor");
                string sCPUSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sCPUSerialNumber = mo["ProcessorId"].ToString().Trim();
                }
                return sCPUSerialNumber;
            }
            catch
            {
                return "";
            }
        }
        //获取硬盘序列号
        public string GetHardDiskSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string sHardDiskSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sHardDiskSerialNumber = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return sHardDiskSerialNumber;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取本机的MAC;  //在项目-》添加引用....里面引用System.Management
        /// </summary>
        public string GetLocalMac()
        {
            string mac = null;
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return (mac);
        }
        //获取网卡地址
        public string GetNetCardMACAddress()
        {
            //try
            //{
            //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
            //    string NetCardMACAddress = "";
            //    foreach (ManagementObject mo in searcher.Get())
            //    {
            //        NetCardMACAddress = mo["MACAddress"].ToString().Trim();
            //    }
            //    return NetCardMACAddress;
            //}
            //catch
            //{
            //    return "";
            //}

            ManagementClass msssssc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection mssssssssoc = msssssc.GetInstances();

            foreach (ManagementObject mo in mssssssssoc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    String mac = mo["MacAddress"].ToString();
                    if (!mac.Equals(""))
                    {
                        mac = mac.Replace(":", ""); /* 将:删除 */
                        return mac;
                    }
                }
            }
            return "";
        }






        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        public string GetDiskSerialNumber()
        {
            //这种模式在插入一个U盘后可能会有不同的结果，如插入我的手机时
            String HDid = "";
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                HDid = (string)mo.Properties["Model"].Value;//SerialNumber
                break;//这名话解决有多个物理盘时产生的问题，只取第一个物理硬盘
            }
            return HDid;
            /*ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = mc.GetInstances();
            string str = "";
            foreach (ManagementObject mo in moc)
            {
                str = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return str;*/
        }


        /// <summary>
        /// 获取网卡硬件地址
        /// </summary>
        public string GetMacAddress()
        {
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }


        /// <summary>
        /// 获取IP地址
        /// </summary>
        public string GetIPAddress()
        {
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    //st=mo["IpAddress"].ToString(); 
                    System.Array ar;
                    ar = (System.Array)(mo.Properties["IpAddress"].Value);
                    st = ar.GetValue(0).ToString();
                    break;
                }
            }
            return st;
        }


        /// <summary>
        /// 操作系统的登录用户名
        /// </summary>
        public string GetUserName()
        {
            return Environment.UserName;
        }




        /// <summary>
        /// 获取计算机名
        /// </summary>
        public string GetComputerName()
        {
            return Environment.MachineName;
        }


        /// <summary>
        /// 操作系统类型
        /// </summary>
        public string GetSystemType()
        {
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                st = mo["SystemType"].ToString();
            }
            return st;
        }


        /// <summary>
        /// 物理内存
        /// </summary>
        public string GetPhysicalMemory()
        {
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                st = mo["TotalPhysicalMemory"].ToString();
            }
            return st;
        }


        /// <summary>
        /// 显卡PNPDeviceID
        /// </summary>
        public string GetVideoPNPID()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_VideoController");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["PNPDeviceID"].ToString();
            }
            return st;
        }


        /// <summary>
        /// 声卡PNPDeviceID
        /// </summary>
        public string GetSoundPNPID()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_SoundDevice");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["PNPDeviceID"].ToString();
            }
            return st;
        }


        /// <summary>
        /// CPU版本信息
        /// </summary>
        public string GetCPUVersion()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["Version"].ToString();
            }
            return st;
        }


        /// <summary>
        /// CPU名称信息
        /// </summary>
        public string GetCPUName()
        {
            string st = "";
            ManagementObjectSearcher driveID = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (ManagementObject mo in driveID.Get())
            {
                st = mo["Name"].ToString();
            }
            return st;
        }


        /// <summary>
        /// CPU制造厂商
        /// </summary>
        public string GetCPUManufacturer()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["Manufacturer"].ToString();
            }
            return st;
        }


        /// <summary>
        /// 主板制造厂商
        /// </summary>
        public string GetBoardManufacturer()
        {
            SelectQuery query = new SelectQuery("Select * from Win32_BaseBoard");
            ManagementObjectSearcher mos = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator data = mos.Get().GetEnumerator();
            data.MoveNext();
            ManagementBaseObject board = data.Current;
            return board.GetPropertyValue("Manufacturer").ToString();
        }


        /// <summary>
        /// 主板编号
        /// </summary>
        public string GetBoardID()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["SerialNumber"].ToString();
            }
            return st;
        }


        /// <summary>
        /// 主板型号
        /// </summary>
        public string GetBoardType()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["Product"].ToString();
            }
            return st;
        }
    }

}
