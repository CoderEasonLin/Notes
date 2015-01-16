namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.lbTest = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.Icon = ((System.Drawing.Icon)(resources.GetObject("icon.Icon")));
            this.icon.Visible = true;
            this.icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // timerHide
            // 
            this.timerHide.Enabled = true;
            this.timerHide.Interval = 5000;
            this.timerHide.Tick += new System.EventHandler(this.timerHide_Tick);
            // 
            // lbTest
            // 
            this.lbTest.AutoSize = true;
            this.lbTest.Location = new System.Drawing.Point(12, 9);
            this.lbTest.Name = "lbTest";
            this.lbTest.Size = new System.Drawing.Size(11, 12);
            this.lbTest.TabIndex = 4;
            this.lbTest.Text = "0";
            this.lbTest.Visible = false;
            // 
            // outputBox
            // 
            this.outputBox.Font = new System.Drawing.Font("MingLiU", 8F);
            this.outputBox.Location = new System.Drawing.Point(1, 0);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(290, 58);
            this.outputBox.TabIndex = 5;
            this.outputBox.Text = "";
            // 
            // inputBox
            // 
            this.inputBox.Font = new System.Drawing.Font("MingLiU", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.inputBox.Location = new System.Drawing.Point(1, 63);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(290, 33);
            this.inputBox.TabIndex = 6;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBox_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 97);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.lbTest);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.5;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "備忘事項";
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.Label lbTest;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.TextBox inputBox;

    }
}

