namespace MandateTool
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_Generate = new System.Windows.Forms.Button();
            this.label_Tip_InputSerialNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Tip_outputMandateCode = new System.Windows.Forms.Label();
            this.label_TipMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_CloseForm = new System.Windows.Forms.Button();
            this.btn_Minmize = new System.Windows.Forms.Button();
            this.txtBox_outputMandateCode = new MandateTool.TransTextBox();
            this.txtBox_InputSerialNum = new MandateTool.TransTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Generate
            // 
            this.btn_Generate.BackColor = System.Drawing.Color.Transparent;
            this.btn_Generate.BackgroundImage = global::MandateTool.Properties.Resources.button1;
            this.btn_Generate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Generate.FlatAppearance.BorderSize = 0;
            this.btn_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Generate.Location = new System.Drawing.Point(141, 196);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(81, 27);
            this.btn_Generate.TabIndex = 0;
            this.btn_Generate.UseVisualStyleBackColor = false;
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            this.btn_Generate.MouseEnter += new System.EventHandler(this.btn_Generate_MouseEnter);
            this.btn_Generate.MouseLeave += new System.EventHandler(this.btn_Generate_MouseLeave);
            // 
            // label_Tip_InputSerialNum
            // 
            this.label_Tip_InputSerialNum.AutoSize = true;
            this.label_Tip_InputSerialNum.BackColor = System.Drawing.Color.Transparent;
            this.label_Tip_InputSerialNum.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Tip_InputSerialNum.Location = new System.Drawing.Point(17, 42);
            this.label_Tip_InputSerialNum.Name = "label_Tip_InputSerialNum";
            this.label_Tip_InputSerialNum.Size = new System.Drawing.Size(104, 16);
            this.label_Tip_InputSerialNum.TabIndex = 1;
            this.label_Tip_InputSerialNum.Text = "请输入序列号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("隶书", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(83, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "虚拟车授权工具V1.0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // label_Tip_outputMandateCode
            // 
            this.label_Tip_outputMandateCode.AutoSize = true;
            this.label_Tip_outputMandateCode.BackColor = System.Drawing.Color.Transparent;
            this.label_Tip_outputMandateCode.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Tip_outputMandateCode.Location = new System.Drawing.Point(16, 108);
            this.label_Tip_outputMandateCode.Name = "label_Tip_outputMandateCode";
            this.label_Tip_outputMandateCode.Size = new System.Drawing.Size(104, 16);
            this.label_Tip_outputMandateCode.TabIndex = 4;
            this.label_Tip_outputMandateCode.Text = "生成的授权码";
            // 
            // label_TipMessage
            // 
            this.label_TipMessage.AutoSize = true;
            this.label_TipMessage.BackColor = System.Drawing.Color.Transparent;
            this.label_TipMessage.Font = new System.Drawing.Font("华文行楷", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_TipMessage.Location = new System.Drawing.Point(20, 170);
            this.label_TipMessage.Name = "label_TipMessage";
            this.label_TipMessage.Size = new System.Drawing.Size(0, 17);
            this.label_TipMessage.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::MandateTool.Properties.Resources.userEnterBox;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.txtBox_InputSerialNum);
            this.panel1.Location = new System.Drawing.Point(12, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 35);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::MandateTool.Properties.Resources.userEnterBox;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.txtBox_outputMandateCode);
            this.panel2.Location = new System.Drawing.Point(12, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(353, 35);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.btn_CloseForm);
            this.panel3.Controls.Add(this.btn_Minmize);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(373, 27);
            this.panel3.TabIndex = 12;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(317, 27);
            this.panel4.TabIndex = 16;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(5, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 24);
            this.button4.TabIndex = 15;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.button4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // btn_CloseForm
            // 
            this.btn_CloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.BackgroundImage = global::MandateTool.Properties.Resources.close;
            this.btn_CloseForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_CloseForm.FlatAppearance.BorderSize = 0;
            this.btn_CloseForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseForm.Location = new System.Drawing.Point(348, 2);
            this.btn_CloseForm.Name = "btn_CloseForm";
            this.btn_CloseForm.Size = new System.Drawing.Size(20, 20);
            this.btn_CloseForm.TabIndex = 14;
            this.btn_CloseForm.UseVisualStyleBackColor = false;
            this.btn_CloseForm.Click += new System.EventHandler(this.btn_CloseForm_Click);
            this.btn_CloseForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnClosing);
            this.btn_CloseForm.MouseEnter += new System.EventHandler(this.btn_CloseForm_MouseEnter);
            this.btn_CloseForm.MouseLeave += new System.EventHandler(this.btn_CloseForm_MouseLeave);
            // 
            // btn_Minmize
            // 
            this.btn_Minmize.BackColor = System.Drawing.Color.Transparent;
            this.btn_Minmize.BackgroundImage = global::MandateTool.Properties.Resources.min;
            this.btn_Minmize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Minmize.FlatAppearance.BorderSize = 0;
            this.btn_Minmize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Minmize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Minmize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Minmize.Location = new System.Drawing.Point(321, 2);
            this.btn_Minmize.Name = "btn_Minmize";
            this.btn_Minmize.Size = new System.Drawing.Size(20, 20);
            this.btn_Minmize.TabIndex = 13;
            this.btn_Minmize.UseVisualStyleBackColor = false;
            this.btn_Minmize.Click += new System.EventHandler(this.btn_Minmize_Click);
            this.btn_Minmize.MouseEnter += new System.EventHandler(this.btn_Minmize_MouseEnter);
            this.btn_Minmize.MouseLeave += new System.EventHandler(this.btn_Minmize_MouseLeave);
            // 
            // txtBox_outputMandateCode
            // 
            this.txtBox_outputMandateCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_outputMandateCode.Font = new System.Drawing.Font("Courier New", 12.5F, System.Drawing.FontStyle.Bold);
            this.txtBox_outputMandateCode.Location = new System.Drawing.Point(15, 9);
            this.txtBox_outputMandateCode.Multiline = false;
            this.txtBox_outputMandateCode.Name = "txtBox_outputMandateCode";
            this.txtBox_outputMandateCode.ReadOnly = true;
            this.txtBox_outputMandateCode.Size = new System.Drawing.Size(334, 19);
            this.txtBox_outputMandateCode.TabIndex = 8;
            this.txtBox_outputMandateCode.Text = "";
            // 
            // txtBox_InputSerialNum
            // 
            this.txtBox_InputSerialNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox_InputSerialNum.Font = new System.Drawing.Font("Courier New", 12.5F, System.Drawing.FontStyle.Bold);
            this.txtBox_InputSerialNum.Location = new System.Drawing.Point(11, 9);
            this.txtBox_InputSerialNum.Multiline = false;
            this.txtBox_InputSerialNum.Name = "txtBox_InputSerialNum";
            this.txtBox_InputSerialNum.Size = new System.Drawing.Size(334, 19);
            this.txtBox_InputSerialNum.TabIndex = 8;
            this.txtBox_InputSerialNum.Text = "";
            this.txtBox_InputSerialNum.TextChanged += new System.EventHandler(this.transTextBox1_TextChanged);
            this.txtBox_InputSerialNum.Enter += new System.EventHandler(this.textBox1_Enter);
            this.txtBox_InputSerialNum.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MandateTool.Properties.Resources.myBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(373, 239);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_TipMessage);
            this.Controls.Add(this.label_Tip_outputMandateCode);
            this.Controls.Add(this.label_Tip_InputSerialNum);
            this.Controls.Add(this.btn_Generate);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "授权码生成工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.Label label_Tip_InputSerialNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_Tip_outputMandateCode;
        private System.Windows.Forms.Label label_TipMessage;
        private TransTextBox txtBox_InputSerialNum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private TransTextBox txtBox_outputMandateCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_CloseForm;
        private System.Windows.Forms.Button btn_Minmize;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel4;
    }
}

