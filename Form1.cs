using System;
using System.IO;
using System.Windows.Forms;
using com.domaintransformations.WIXGenerator;
using System.Drawing;

namespace WIXWriter {
    public partial class frmWIXGenerator : Form {
        //private void frmWIXGenerator_Load(object sender, EventArgs e) {
        //    Icon icoMain = new Icon("~/favicon.ico");
        //    this.Icon = icoMain;
        //}

        private void Form1_Load(object sender, EventArgs e) {
            this.Icon = new Icon("favicon.ico");
        }

        public frmWIXGenerator() {
            InitializeComponent();
            setMoveFunctionVisibility(false);
        }

        private void setMoveFunctionVisibility(bool b) {
            btnMove.Visible = b;
            btnOutputPath.Visible = b;
            txtOutputPath.Visible = b;
            lblOutputPath.Visible = b;
        }

        public void ChooseFolder(TextBox tb) {
            folderBrowserDialog1.SelectedPath = @"D:\GitRepositories";
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                tb.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            ChooseFolder(txtRoot);
        }

        private void btnGenerate_Click(object sender, EventArgs e) {
            if(txtRoot.Text.ToLower().Equals("help")) {
                txtConsole.Text = "Enter the root directory of the solution to be processed, the product name, the content output filename, the config output filename and the product output filename." + Environment.NewLine +
                                      "For Example: WIXContentWriter C:\\MySolutionRoot ProductName ProductContent.wxs Config.wxi Product.wxs";
            } else {
                if(txtRoot.Text.Equals("") ||
                    txtProductName.Text.Equals("") ||
                    txtContentName.Text.Equals("") ||
                    txtConfigName.Text.Equals("") ||
                    txtProductFileName.Text.Equals("")) {
                    txtConsole.Text = "Must enter 5 arguments.  Enter help as first parameter for assistance.";
                } else {
                    if(!Directory.Exists(txtRoot.Text)) {
                        txtConsole.Text = "Solution directory specified does not exist. Try again.";
                    } else {
                        //Correct number of arguments - ready to attempt processing.
                        removeExistingFiles();
                        
                        ContentWriter cw = new ContentWriter();
                        cw.writeContentOutputFile(txtRoot.Text, txtContentName.Text, txtConfigName.Text);
                        txtConsole.Text = "Successfully completed writing WIX Content file." + Environment.NewLine;
                        cw.writeConfigOutputFile(txtProductName.Text, txtConfigName.Text);
                        txtConsole.Text = txtConsole.Text + "Successfully completed writing WIX Config file." + Environment.NewLine;
                        cw.writeProductOutputFile(txtConfigName.Text, txtProductFileName.Text);
                        txtConsole.Text = txtConsole.Text + "Successfully completed writing WIX Product file." + Environment.NewLine;
                        txtConsole.Text = txtConsole.Text + "Completed Processing." + Environment.NewLine;

                        cw.finalise();
                        cw = null;

                        setMoveFunctionVisibility(true);
                    }
                }
            }
        }

        private void removeExistingFiles(){
            removeFile(txtContentName.Text);
            removeFile(txtConfigName.Text);
            removeFile(txtProductFileName.Text);
        }

        private void removeFile(string filename) {
            if(File.Exists(filename)) {
                File.Delete(filename);
            }
        }

        private void btnMove_Click(object sender, EventArgs e) {
            if(txtOutputPath.Text.Equals("")) {
                txtConsole.Text = txtConsole.Text + "Output path must by specified to move the files." + Environment.NewLine;
            } else {
                if(!Directory.Exists(txtOutputPath.Text)) {
                    txtConsole.Text = txtConsole.Text + "Output directory does not exist.  Try again" + Environment.NewLine;
                } else {
                    int filesMoved = FileUtil.moveFiles(txtOutputPath.Text);
                    txtConsole.Text = txtConsole.Text + "Finished moving " + filesMoved + " files to " + txtOutputPath.Text + Environment.NewLine;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            ChooseFolder(txtOutputPath);
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtConsole.Text = "";
        }
    }
}