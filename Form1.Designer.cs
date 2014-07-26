namespace WIXWriter {
    partial class FrmWixGenerator {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWixGenerator));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtConfigFileName = new System.Windows.Forms.TextBox();
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtBundleFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(107, 12);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(271, 20);
            this.txtRoot.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Root Directory";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Name";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(107, 38);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(271, 20);
            this.txtProductName.TabIndex = 3;
            // 
            // txtConfigName
            // 
            this.txtConfigFileName.Location = new System.Drawing.Point(107, 62);
            this.txtConfigFileName.Name = "txtConfigName";
            this.txtConfigFileName.Size = new System.Drawing.Size(271, 20);
            this.txtConfigFileName.TabIndex = 4;
            this.txtConfigFileName.Text = "Config.wxi";
            // 
            // txtProductFileName
            // 
            this.txtProductFileName.Location = new System.Drawing.Point(107, 119);
            this.txtProductFileName.Name = "txtProductFileName";
            this.txtProductFileName.Size = new System.Drawing.Size(271, 20);
            this.txtProductFileName.TabIndex = 6;
            this.txtProductFileName.Text = "Product.wxs";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 121);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Product Filename";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(409, 260);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(62, 23);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(107, 147);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(364, 107);
            this.txtConsole.TabIndex = 7;
            this.txtConsole.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 147);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Console Output";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(107, 290);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(261, 20);
            this.txtOutputPath.TabIndex = 10;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(12, 292);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOutputPath.Size = new System.Drawing.Size(64, 13);
            this.lblOutputPath.TabIndex = 2;
            this.lblOutputPath.Text = "Output Path";
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(409, 287);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(62, 23);
            this.btnMove.TabIndex = 12;
            this.btnMove.Text = "Move Files";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(374, 289);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(24, 23);
            this.btnOutputPath.TabIndex = 11;
            this.btnOutputPath.Text = "...";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(340, 261);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(63, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 93);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bundle Filename";
            // 
            // txtBundleName
            // 
            this.txtBundleFileName.Location = new System.Drawing.Point(107, 93);
            this.txtBundleFileName.Name = "txtBundleName";
            this.txtBundleFileName.Size = new System.Drawing.Size(271, 20);
            this.txtBundleFileName.TabIndex = 5;
            this.txtBundleFileName.Text = "Bundle.wxs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 65);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Config Filename";
            // 
            // FrmWixGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 313);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtBundleFileName);
            this.Controls.Add(this.txtProductFileName);
            this.Controls.Add(this.txtConfigFileName);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(488, 500);
            this.MinimumSize = new System.Drawing.Size(488, 342);
            this.Name = "FrmWixGenerator";
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
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtConfigFileName;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBundleFileName;
        private System.Windows.Forms.Label label4;
    }
}

