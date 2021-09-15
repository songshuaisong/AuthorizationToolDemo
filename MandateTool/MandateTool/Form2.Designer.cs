namespace MandateTool
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label_Tip_outputMandateCode = new System.Windows.Forms.Label();
            this.label_Tip_InputSerialNum = new System.Windows.Forms.Label();
            this.txtBox_InputSerialNum = new System.Windows.Forms.TextBox();
            this.txtBox_outputMandateCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label_TipMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Tip_outputMandateCode
            // 
            this.label_Tip_outputMandateCode.AutoSize = true;
            this.label_Tip_outputMandateCode.BackColor = System.Drawing.Color.Transparent;
            this.label_Tip_outputMandateCode.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Tip_outputMandateCode.Location = new System.Drawing.Point(11, 100);
            this.label_Tip_outputMandateCode.Name = "label_Tip_outputMandateCode";
            this.label_Tip_outputMandateCode.Size = new System.Drawing.Size(104, 16);
            this.label_Tip_outputMandateCode.TabIndex = 6;
            this.label_Tip_outputMandateCode.Text = "生成的授权码";
            // 
            // label_Tip_InputSerialNum
            // 
            this.label_Tip_InputSerialNum.AutoSize = true;
            this.label_Tip_InputSerialNum.BackColor = System.Drawing.Color.Transparent;
            this.label_Tip_InputSerialNum.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Tip_InputSerialNum.Location = new System.Drawing.Point(12, 34);
            this.label_Tip_InputSerialNum.Name = "label_Tip_InputSerialNum";
            this.label_Tip_InputSerialNum.Size = new System.Drawing.Size(104, 16);
            this.label_Tip_InputSerialNum.TabIndex = 5;
            this.label_Tip_InputSerialNum.Text = "请输入序列号";
            // 
            // txtBox_InputSerialNum
            // 
            this.txtBox_InputSerialNum.Font = new System.Drawing.Font("Courier New", 12.5F, System.Drawing.FontStyle.Bold);
            this.txtBox_InputSerialNum.Location = new System.Drawing.Point(15, 61);
            this.txtBox_InputSerialNum.MaxLength = 32;
            this.txtBox_InputSerialNum.Name = "txtBox_InputSerialNum";
            this.txtBox_InputSerialNum.Size = new System.Drawing.Size(335, 26);
            this.txtBox_InputSerialNum.TabIndex = 7;
            this.txtBox_InputSerialNum.Enter += new System.EventHandler(this.txtBox_InputSerialNum_Enter);
            this.txtBox_InputSerialNum.Leave += new System.EventHandler(this.txtBox_InputSerialNum_Leave);
            // 
            // txtBox_outputMandateCode
            // 
            this.txtBox_outputMandateCode.Font = new System.Drawing.Font("Courier New", 12.5F, System.Drawing.FontStyle.Bold);
            this.txtBox_outputMandateCode.Location = new System.Drawing.Point(15, 129);
            this.txtBox_outputMandateCode.MaxLength = 32;
            this.txtBox_outputMandateCode.Name = "txtBox_outputMandateCode";
            this.txtBox_outputMandateCode.ReadOnly = true;
            this.txtBox_outputMandateCode.Size = new System.Drawing.Size(335, 26);
            this.txtBox_outputMandateCode.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "生  成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_TipMessage
            // 
            this.label_TipMessage.AutoSize = true;
            this.label_TipMessage.BackColor = System.Drawing.Color.Transparent;
            this.label_TipMessage.Font = new System.Drawing.Font("华文行楷", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_TipMessage.Location = new System.Drawing.Point(19, 160);
            this.label_TipMessage.Name = "label_TipMessage";
            this.label_TipMessage.Size = new System.Drawing.Size(0, 17);
            this.label_TipMessage.TabIndex = 10;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 229);
            this.Controls.Add(this.label_TipMessage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBox_outputMandateCode);
            this.Controls.Add(this.txtBox_InputSerialNum);
            this.Controls.Add(this.label_Tip_outputMandateCode);
            this.Controls.Add(this.label_Tip_InputSerialNum);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "授权码生成工具";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Tip_outputMandateCode;
        private System.Windows.Forms.Label label_Tip_InputSerialNum;
        private System.Windows.Forms.TextBox txtBox_InputSerialNum;
        private System.Windows.Forms.TextBox txtBox_outputMandateCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_TipMessage;
    }
}