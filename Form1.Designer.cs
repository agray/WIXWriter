namespace com.phoenixconsulting.wixwriter {
    partial class frmWIXGenerator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWIXGenerator));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtContentName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.txtProductFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(143, 15);
            this.txtRoot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(360, 22);
            this.txtRoot.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Root Directory";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Product Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(118, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Content Filename";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(143, 47);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(360, 22);
            this.txtProductName.TabIndex = 2;
            // 
            // txtContentName
            // 
            this.txtContentName.Location = new System.Drawing.Point(143, 81);
            this.txtContentName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtContentName.Name = "txtContentName";
            this.txtContentName.Size = new System.Drawing.Size(360, 22);
            this.txtContentName.TabIndex = 3;
            this.txtContentName.Text = "WixContent.wxs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(109, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Config Filename";
            // 
            // txtConfigName
            // 
            this.txtConfigName.Location = new System.Drawing.Point(143, 114);
            this.txtConfigName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(360, 22);
            this.txtConfigName.TabIndex = 4;
            this.txtConfigName.Text = "Config.wxi";
            // 
            // txtProductFileName
            // 
            this.txtProductFileName.Location = new System.Drawing.Point(143, 146);
            this.txtProductFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProductFileName.Name = "txtProductFileName";
            this.txtProductFileName.Size = new System.Drawing.Size(360, 22);
            this.txtProductFileName.TabIndex = 5;
            this.txtProductFileName.Text = "Product.wxs";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 149);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Product Filename";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(545, 318);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(83, 28);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(143, 178);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(484, 131);
            this.txtConsole.TabIndex = 9;
            this.txtConsole.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 182);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(106, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Console Output";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(143, 353);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(360, 22);
            this.txtOutputPath.TabIndex = 8;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(16, 357);
            this.lblOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOutputPath.Size = new System.Drawing.Size(84, 17);
            this.lblOutputPath.TabIndex = 2;
            this.lblOutputPath.Text = "Output Path";
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(545, 351);
            this.btnMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(83, 28);
            this.btnMove.TabIndex = 8;
            this.btnMove.Text = "Move Files";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(505, 351);
            this.btnOutputPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(32, 28);
            this.btnOutputPath.TabIndex = 7;
            this.btnOutputPath.Text = "...";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(453, 318);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 28);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmWIXGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 367);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtProductFileName);
            this.Controls.Add(this.txtConfigName);
            this.Controls.Add(this.txtContentName);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(646, 412);
            this.MinimumSize = new System.Drawing.Size(646, 412);
            this.Name = "frmWIXGenerator";
            this.Text = "WIX Content Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtContentName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.TextBox txtProductFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.Button btnClear;
    }
}

