using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MandateTool
{
    class TransTextBox: RichTextBox
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr LoadLibrary(string lpFileName);

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams prams = base.CreateParams;
                if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                {
                    this.Multiline = false; // 设置多行属性为false
                    prams.ExStyle |= 0x020; // transparent 
                    prams.ClassName = "RICHEDIT50W";// TRANSTEXTBOXW
                }
                return prams;
            }
        }
        //private void TransTextBox_LostFocus(object sender, EventArgs e)
        //{
        //    this.Parent.Refresh();
        //}
        //private void TransTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    this.Parent.Refresh();
        //}
    }
}
