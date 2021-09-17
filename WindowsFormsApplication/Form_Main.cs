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
        /* 加密实现的方法 */
        [DllImport("vvs-cfg\\vvsGalPcakageMaker.dll", EntryPoint = "Encrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Encrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);
        /* 解密实现的方法 */
        [DllImport("vvs-cfg\\vvsGalPcakageMaker.dll", EntryPoint = "Decrypted_CBC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Decrypted_CBC(byte[] src, int len, byte[] key, byte[] iv, byte[] output);


        Form_Dialog f111111111111111 = new Form_Dialog();

        /* 日志文件路径默认为当前的根目录 */
        private String vvs_log_file_name = "\\record.log";

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



            //获取当前系统时间
            DateTime dt = System.DateTime.Now;

            /* 将日志记录的文件名称重新赋值 */
            vvs_log_file_name = "log\\" + dt.ToString("yyyy_MM_dd") + vvs_log_file_name;






            label_Tip_MessageShow.BackColor = Color.Transparent;
            label_Tip_MessageShow.Parent = pictureBox1;

            String retStringMesg = "";
            String serialNumOutput = "";

            Write_VVS_Log(vvs_log_file_name, "Initialize the default Keys and Vectors.");
            Authorization.encDec_Key_Iv_DefaultValue_Init();
            UInt32 retVal = Authorization.Authorization_Judgment(ref serialNumOutput, ref retStringMesg);



            /* 设置弹窗的提示信息 */
            f111111111111111.String_Dialog_TipMag = retStringMesg;
            f111111111111111.String_SendtoNUGCode = serialNumOutput;

            Write_VVS_Log(vvs_log_file_name, "serialNumOutput is : " + serialNumOutput + ".");

            if (retVal == Authorization.AUTH_RETVAL_NORMAL)
            {
                Write_VVS_Log(vvs_log_file_name, "Authorization correct : Starting normally.");
            }
            else 
            {
                /* 弹窗显示信息 */
                Authorization.showSubForm_Dialog(f111111111111111);
                /* 判断弹窗界面用户选择的操作 1：退出 */
                if (f111111111111111.UserOperationLevel == 1)
                {
                    Write_VVS_Log(vvs_log_file_name, "Authorization errors : User opts exit.");
                    /* 退出当前虚拟车的运行程序 */
                    frmMain_FormClosing(null, null);
                }
                else if (f111111111111111.UserOperationLevel == 0)
                {
                    Write_VVS_Log(vvs_log_file_name, "Authorization passed : Starting normally.");
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            this.Dispose();
            this.Close();
            Environment.Exit(1); /* 暴力退出 */
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {

        }

        public void Write_VVS_Log(string fileName, String msg)
        {
            /* 参数判断 */
            if (fileName == null || fileName.Length <= 0 || msg == null || msg.Length <= 0)
            {
                return;
            }
            /* 获取当前时间 */
            String str = "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "]-[" + msg + "]";
            /* 将字符写入到文件中 */
            Write_String_To_Txt(fileName, str);
        }

        /* 向txt文件中写入信息 */
        public static void Write_String_To_Txt(string filePath, string massage)
        {
            FileStream fs1;
            /* 判断是否已经有了这个文件 */
            if (!File.Exists(filePath))
            {
                /* 文件路径是固定的，所以根据\分离出前面的目录名称 */
                string[] words = filePath.Split('\\');
                if (!Directory.Exists(words[0])) /* 是否存此文件夹*/
                {
                    Directory.CreateDirectory(words[0]); /* 创建文件夹 */
                }
                if (!Directory.Exists(words[1])) /* 是否存此文件夹*/
                {
                    Directory.CreateDirectory(words[0] + "\\" + words[1]); /* 创建文件夹 */
                }
                fs1 = new FileStream(filePath, FileMode.Create);/* 没有则创建这个文件 */
                fs1.Close(); /* 关闭操作句柄，防止文件占用 */
            }
            using (StreamWriter sw = File.AppendText(filePath)) /* 追加写入的方式打开流 */
            {
                sw.WriteLine(massage); /* 写入 */
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
    }
}
