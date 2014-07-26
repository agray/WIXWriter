using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WIXWriter.au.com.fullcirclesolutions.wixwriter;

namespace WIXWriter {
    public partial class FrmWixGenerator : Form {
        //private void frmWIXGenerator_Load(object sender, EventArgs e) {
        //    Icon icoMain = new Icon("~/favicon.ico");
        //    this.Icon = icoMain;
        //}

        private void Form1_Load(object sender, EventArgs e) {
            Icon = new Icon("favicon.ico");
        }

        public FrmWixGenerator() {
            InitializeComponent();
            SetMoveFunctionVisibility(false);
        }

        private void SetMoveFunctionVisibility(bool b) {
            btnMove.Visible = b;
            btnOutputPath.Visible = b;
            txtOutputPath.Visible = b;
            lblOutputPath.Visible = b;
        }

        public void ChooseFolder(TextBox tb) {
            folderBrowserDialog1.SelectedPath = @"C:\GitRepositories";
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                tb.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            ChooseFolder(txtRoot);
        }

        private void btnGenerate_Click(object sender, EventArgs e) {
            var rootDirectory = txtRoot.Text;
            var product = txtProductName.Text;
            var configFile = txtConfigFileName.Text;
            var bundleFile = txtBundleFileName.Text;
            var productFile = txtProductFileName.Text;
            if(rootDirectory.ToLower().Equals("help")) {
                txtConsole.Text = @"Enter the root directory of the solution to be processed, the product name, the content output filename, the config output filename and the product output filename." + Environment.NewLine +
                                  @"For Example: WIXContentWriter C:\MySolutionRoot ProductName ProductContent.wxs Config.wxi Product.wxs";
            } else {
                if(string.IsNullOrEmpty(rootDirectory) ||
                   string.IsNullOrEmpty(product) ||
                   string.IsNullOrEmpty(bundleFile) ||
                   string.IsNullOrEmpty(configFile) ||
                   string.IsNullOrEmpty(productFile)) {
                    txtConsole.Text = @"Must enter 5 arguments. Enter help as first parameter for assistance.";
                } else {
                    if(!Directory.Exists(rootDirectory)) {
                        txtConsole.Text = @"Solution directory specified does not exist. Try again.";
                    } else {
                        //Correct number of arguments - ready to attempt processing.
                        RemoveExistingFiles();
                        var text = new StringBuilder();
                        FileWriter.WriteConfigFile(product, configFile);
                        text.AppendLine("Successfully completed writing the Config file.");
                        FileWriter.WriteBundleFile(product + bundleFile, product);
                        text.AppendLine("Successfully completed writing the Bundle file.");
                        FileWriter.WriteProductFile(product + productFile);
                        text.AppendLine("Successfully completed writing the Product file.");
                        FileWriter.WriteContentFile(rootDirectory, product + Constants.ContentFileSuffix, product + configFile);
                        text.AppendLine("Successfully completed writing the Content file.");
                        text.AppendLine("Completed Processing.");
                        txtConsole.Text = text.ToString();
                        
                        SetMoveFunctionVisibility(true);
                    }
                }
            }
        }

        private void RemoveExistingFiles() {
            var product = txtProductName.Text;
            RemoveFile(product + txtConfigFileName.Text);
            RemoveFile(product + txtBundleFileName.Text);
            RemoveFile(product + txtProductFileName.Text);
            RemoveFile(product + Constants.ContentFileSuffix);
        }

        private static void RemoveFile(string filename) {
            if(File.Exists(filename)) {
                File.Delete(filename);
            }
        }

        private void btnMove_Click(object sender, EventArgs e) {
            if(string.IsNullOrEmpty(txtOutputPath.Text)) {
                AppendToConsole(@"Output path must by specified to move the files.");
            } else {
                if(!Directory.Exists(txtOutputPath.Text)) {
                    AppendToConsole(@"Output directory does not exist. Try again");
                } else {
                    var filesMoved = FileMover.MoveFiles(txtOutputPath.Text);
                    AppendToConsole(@"Finished moving " + filesMoved + @" files to " + txtOutputPath.Text);
                }
            }
        }

        private void AppendToConsole(string text) {
            txtConsole.Text += text + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e) {
            ChooseFolder(txtOutputPath);
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtConsole.Text = string.Empty;
        }
    }
}