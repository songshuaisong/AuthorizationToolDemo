using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MandateTool
{
    public partial class Form1 : Form
    {
        public const UInt16 CPUID_LENGTH = 8; /* CPUID的长度，字符串形式 */
        public const UInt16 MAC_LENGTH   = 6; /* MAC地址的长度，字符串形式 */
        public const UInt16 CPUID_INDEX = 0;  /* CPUID在数组的下标 */
        public const UInt16 MAC_INDEX = 8;  /* MAC地址在数组中下标 */

        /* 加密实现的方法 */
        [DllImport("vvsGalPcakageMaker.dll", EntryPoint = "Encrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Encrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);
        /* 解密实现的方法 */
        [DllImport("vvsGalPcakageMaker.dll", EntryPoint = "Decrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Decrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);

        /****************************************************************************************
             encDec_Key_DefaultValue：生成系统初始加密解密的默认的密钥
             encDec_Iv_DefaultValue ：生成系统初始加密解密的默认的向量
         ****************************************************************************************/
        public static Byte[] encDec_Key_DefaultValue = new Byte[16];
        public static Byte[] encDec_Iv_DefaultValue = new Byte[16];

        public Form1()
        {
            InitializeComponent();
            //this.MaximumSize = this.Size;
            //this.MinimumSize = this.Size;
            //this.MaximizeBox = false;
            this.ControlBox = false; /* 不显示右上角控制按键 */
            this.FormBorderStyle = FormBorderStyle.None; /* 不显示标题栏 */
            // this.ShowInTaskbar = false; /* 任务栏不显示图标 */
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /* 实现在最小化的时候点击任务栏图标用于窗体显示 */
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;  // Winuser.h中定义
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                return cp;
            }
        }
        
        private Point mouse_offset;
        /// <summary>
        /// 在form上按下鼠标时候的操作，用于在拖动鼠标的时候同时移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
        /// <summary>
        /// 在form上按下鼠标时候的操作，用于在拖动鼠标的时候同时移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBox_InputSerialNum.Font = new Font("黑体", 12, FontStyle.Regular); /* 设置字体和大小 */
            txtBox_InputSerialNum.Text = "请输入序列号";
            txtBox_InputSerialNum.ForeColor = Color.LightGray;

            this.AcceptButton = btn_Generate;/* 将回车与生成按键绑定 */

            panel4.Parent = label2;
            panel4.BringToFront();//将panel放在前面
            panel4.Location = new Point(-83, -5); // 设置器位置为下面的控件的左上角
        }

        static void EncDec_KeyIv_DefaultValue_Init()
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

        public static String Authorization_Code_Generation(String SerialNum)
        {
            /* 默认使用的key和IV的初始化 */
            EncDec_KeyIv_DefaultValue_Init();

            /****************************************************************************************
                将对方提供的序列号字符串转换为对应的十六进制数组
                serial_Number_Encrypted：序列号的数组，CPUID MAC 随机码 的密文
                serial_Number_Decrypted：序列号的数组，CPUID MAC 随机码 的明文
            ****************************************************************************************/
            Byte[] serial_Number_Encrypted = new Byte[16];
            Byte[] serial_Number_Decrypted = new Byte[16];
            /****************************************************************************************
                将对方提供的序列号字符串转换为对应的十六进制数组
                transTextBox1.Text     ：序列号字符串，8字节CPUID + 6字节MAC + 2字节随机码 的密文，由对方提供
                serial_Number_Encrypted：序列号的数组，CPUID MAC 随机码 的密文
            ****************************************************************************************/
            StringToHex(SerialNum, serial_Number_Encrypted);

            /****************************************************************************************
                解密输入的序列号获取到CPUID和MAC地址
                serial_Number_Encrypted ：序列号的数组，CPUID MAC 随机码 的密文
                encDec_Key_DefaultValue ：系统初始加密解密的默认的密钥，值为：NewUnitGroupMake
                encDec_Iv_DefaultValue  ：系统初始加密解密的默认的向量，值为：vvs-2.0byATOTeam
                serial_Number_Decrypted ：解密后的序列号明文，CPUID MAC 随机码 的明文
            ****************************************************************************************/
            Decrypted_CBC(serial_Number_Encrypted, 16, encDec_Key_DefaultValue, encDec_Iv_DefaultValue, serial_Number_Decrypted);

            /*************************************************************************************************
                serial_Number_Decrypted ：CPUID + MAC + 随机2字节
                BFEBFBFF000306C3 28D2443CA344 01 23
            **************************************************************************************************/
            Byte[] tmp_Key = new Byte[16];
            Byte[] tmp_Iv = new Byte[16];
            /*************************************************************************************************
               将授权码解密，密钥和向量的规则如下： 
               tmp_Key 密钥：NU MAC CPUID 里面包含的 CPIID 和 MAC 来源于解密后的序列号
               tmp_Iv  向量：CPUID MAC XZ 里面包含的 CPIID 和 MAC 来源于解密后的序列号
            **************************************************************************************************/
            tmp_Key[0] = (Byte)'N';
            tmp_Key[1] = (Byte)'U';
            for (int i = 0; i < MAC_LENGTH; i++)
            {
                tmp_Key[2 + i] = serial_Number_Decrypted[MAC_INDEX + i]; /* 将解密后的MAC赋值到tmpKey的对应的位置 */
            }
            for (int i = 0; i < CPUID_LENGTH; i++)
            {
                tmp_Key[8 + i] = serial_Number_Decrypted[CPUID_INDEX + i]; /* 将解密后的CPUID赋值到tmpKey的对应的位置 */
            }
            for (int i = 0; i < CPUID_LENGTH; i++)
            {
                tmp_Iv[i] = serial_Number_Decrypted[CPUID_INDEX + i]; /* 将解密后的CPUID赋值到tmpIV的对应的位置 */
            }
            for (int i = 0; i < MAC_LENGTH; i++)
            {
                tmp_Iv[8 + i] = serial_Number_Decrypted[MAC_INDEX + i]; /* 将解密后的MAC赋值到tmpIv的对应的位置 */
            }
            tmp_Iv[14] = (Byte)'X';
            tmp_Iv[15] = (Byte)'Z';

            /************************************************************************************** 
                使用组合的新版密钥和向量加密特定的字符串
                特定字符串为：vvs-v.2.0 CPUID
            ***************************************************************************************/
            Byte[] tmp_Dst = new Byte[16];
            Byte[] tmp_Dst_Encrypted = new Byte[16];
            /*************************************************************************************************
                使用组合的新版密钥和向量加密特定的字符串
                特定字符串为：vvs-v.2.0 CPUID， 里面包含的 CPIID 来源于解密后的序列号
            **************************************************************************************************/
            tmp_Dst[0] = (Byte)'v';
            tmp_Dst[1] = (Byte)'v';
            tmp_Dst[2] = (Byte)'s';
            tmp_Dst[3] = (Byte)'-';
            tmp_Dst[4] = (Byte)'v';
            tmp_Dst[5] = (Byte)'2';
            tmp_Dst[6] = (Byte)'.';
            tmp_Dst[7] = (Byte)'0';
            for (int i = 0; i < CPUID_LENGTH; i++)
            {
                tmp_Dst[i + 8] = serial_Number_Decrypted[CPUID_INDEX + i];
            }
            /*************************************************************************************************
                加密使用组合的特定的字符串
                tmp_Dst ：vvs-v.2.0 CPUID， 里面包含的 CPIID 来源于解密后的序列号
                tmp_Key ：NU MAC CPUID 里面包含的 CPIID 和 MAC 来源于解密后的序列号
                tmp_Iv  ：CPUID MAC XZ 里面包含的 CPIID 和 MAC 来源于解密后的序列号
                tmp_Dst_Encrypted：加密特定字符串 vvs-v.2.0 CPUID 的密文，用于授权码
            **************************************************************************************************/
            Encrypted_CBC(tmp_Dst, 16, tmp_Key, tmp_Iv, tmp_Dst_Encrypted);

            String ManData_Code_Output = "";
            /*************************************************************************************************
                将十六进制数组的授权码转换为对应的字符串
                ManData_Code_Output：加密特定字符串 vvs-v.2.0 CPUID 的字符串密文，用于授权码
            **************************************************************************************************/
            for (int i = 0; i < 16; i++)
            {
                ManData_Code_Output += tmp_Dst_Encrypted[i].ToString("X2");
            }

            return ManData_Code_Output;

        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            if(txtBox_InputSerialNum.Text.Trim().Equals("") || txtBox_InputSerialNum.Text.Length != 32)
            {
                label_TipMessage.Text = "请输入正确的序列号！";
                label_TipMessage.ForeColor = Color.Red;
                return;
            }

            String tmp_InputSerialNum = txtBox_InputSerialNum.Text.Trim();

            for (int counter = 0; counter < tmp_InputSerialNum.Length; counter++)
            {
                /* 判断是否为有的字符 -- 十六进制字符 */
                if ((tmp_InputSerialNum[counter] > '9' || tmp_InputSerialNum[counter] < '0') &&
                   ((tmp_InputSerialNum[counter] > 'F' || tmp_InputSerialNum[counter] < 'A') &&
                    (tmp_InputSerialNum[counter] > 'f' || tmp_InputSerialNum[counter] < 'a')))
                {
                    label_TipMessage.Text = "请输入正确的序列号！";
                    label_TipMessage.ForeColor = Color.Red;
                    return;
                }

            }
            /*************************************************************************************************
                将十六进制字符串的授权码输出到界面
            **************************************************************************************************/
            txtBox_outputMandateCode.Text = Authorization_Code_Generation(txtBox_InputSerialNum.Text.Trim());
            

            /* 更新提示框的内容 */
            label_TipMessage.Text = "生成授权码成功！";
            label_TipMessage.ForeColor = Color.Green;
        }
        
        static int StringToHex(string str, byte[] dat)
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

        Boolean textboxHasText = false;//判断输入框是否有文本
        //textbox获得焦点
        private void textBox1_Enter(object sender, EventArgs e)
        {
            label_TipMessage.Text = "";
            label_TipMessage.ForeColor = Color.Red;
            txtBox_outputMandateCode.Text = "";

            txtBox_InputSerialNum.Font = new Font("Courier New", (float)12.5, FontStyle.Bold); /* 设置字体和大小 */

            if (textboxHasText == false)
                txtBox_InputSerialNum.Text = "";

            txtBox_InputSerialNum.ForeColor = Color.Black;
        }
        // textbox失去焦点
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtBox_InputSerialNum.Text == "")
            {
                txtBox_InputSerialNum.Font = new Font("黑体", (float)12.5, FontStyle.Regular); /* 设置字体和大小 */
                txtBox_InputSerialNum.Text = "请输入序列号";
                txtBox_InputSerialNum.ForeColor = Color.LightGray;
                textboxHasText = false;
            }
            else
            {
                textboxHasText = true;
            }
        }

        private void transTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.txtBox_InputSerialNum.Parent.Refresh();
        }

        private void OnClosing(object sender, MouseEventArgs e)
        {
            string abcd = "                         ";
            DialogResult dr = MessageBox.Show(abcd + "是否退出应用？", "提示", MessageBoxButtons.YesNo);
            if (DialogResult.No == dr)//如果点否按钮  
            {
                return;
                // ex.Cancel = true;//取消关闭  
            }
            else
            {
                while (this.Opacity > 0)//在点击退出按键之后，界面渐渐消失的效果
                {
                    this.Opacity -= 0.01;
                    System.Threading.Thread.Sleep(15);
                }
                Application.Exit();
            }
        }
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        // 最小化按键
        private void btn_Minmize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//最小化 
        }
        // 关闭按键
        private void btn_CloseForm_Click(object sender, EventArgs e)
        {

        }

        private void btn_Minmize_MouseEnter(object sender, EventArgs e)
        {
            btn_Minmize.BackgroundImage = Properties.Resources.min2;
        }

        private void btn_Minmize_MouseLeave(object sender, EventArgs e)
        {
            btn_Minmize.BackgroundImage = Properties.Resources.min;
        }

        private void btn_CloseForm_MouseEnter(object sender, EventArgs e)
        {
            btn_CloseForm.BackgroundImage = Properties.Resources.close2;
        }

        private void btn_CloseForm_MouseLeave(object sender, EventArgs e)
        {
            btn_CloseForm.BackgroundImage = Properties.Resources.close;
        }

        private void btn_Generate_MouseEnter(object sender, EventArgs e)
        {
            btn_Generate.BackgroundImage = Properties.Resources.button3;
        }

        private void btn_Generate_MouseLeave(object sender, EventArgs e)
        {
            btn_Generate.BackgroundImage = Properties.Resources.button1;
        }
    }
 
}
