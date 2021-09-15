using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MandateTool
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.MaximizeBox = false;
            //this.ControlBox = false; /* 不显示右上角控制按键 */
            //this.FormBorderStyle = FormBorderStyle.None; /* 不显示标题栏 */
            // this.ShowInTaskbar = false; /* 任务栏不显示图标 */
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBox_InputSerialNum.Text.Trim().Equals("") || txtBox_InputSerialNum.Text.Trim().Length != 32)
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
                    (tmp_InputSerialNum[counter] > 'F' || tmp_InputSerialNum[counter] < 'A') &&
                    (tmp_InputSerialNum[counter] > 'f' || tmp_InputSerialNum[counter] < 'a'))
                {
                    label_TipMessage.Text = "请输入正确的序列号！";
                    label_TipMessage.ForeColor = Color.Red;
                    return;
                }
            }

            /*************************************************************************************************
                将十六进制字符串的授权码输出到界面
            **************************************************************************************************/
            txtBox_outputMandateCode.Text = Form1.Authorization_Code_Generation(txtBox_InputSerialNum.Text.Trim());

            /* 更新提示框的内容 */
            label_TipMessage.Text = "生成授权码成功！";
            label_TipMessage.ForeColor = Color.Green;
        }

        Boolean textboxHasText = false;//判断输入框是否有文本
        //textbox获得焦点
        private void txtBox_InputSerialNum_Enter(object sender, EventArgs e)
        {
            /* 消除提示框中显示内容 */
            label_TipMessage.Text = "";
            txtBox_outputMandateCode.Text = "";

            txtBox_InputSerialNum.Font = new Font("Courier New", (float)12.5, FontStyle.Bold); /* 设置字体和大小 */

            if (textboxHasText == false)
                txtBox_InputSerialNum.Text = "";

            txtBox_InputSerialNum.ForeColor = Color.Black;
        }
        // textbox失去焦点
        private void txtBox_InputSerialNum_Leave(object sender, EventArgs e)
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

        private void Form2_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;/* 将回车与生成按键绑定 */

            txtBox_InputSerialNum.Font = new Font("黑体", (float)12.5, FontStyle.Regular); /* 设置字体和大小 */
            txtBox_InputSerialNum.Text = "请输入序列号";
            txtBox_InputSerialNum.ForeColor = Color.LightGray;
            textboxHasText = false;
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
