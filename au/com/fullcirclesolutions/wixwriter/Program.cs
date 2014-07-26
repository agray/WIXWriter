using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    static class Program {
        [STAThread]
        static void Main(string[] args) {
            if(args.Length == 0) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmWixGenerator { Icon = new Icon("favicon.ico") });
            } else {
                if(args.Length != 5) {
                    Console.WriteLine(@"Must enter 5 arguments. Enter help as first parameter for assistance.");
                    Environment.Exit(0);
                } else {
                    var rootDirectory = args[0];
                    if(rootDirectory.ToLower().Equals("help")) {
                        Console.WriteLine(@"Enter the root directory of the solution to be processed, the product name, the content output filename, the config output filename the product output filename and the directory to move the files to." +
                                          @"For Example: WIXContentWriter C:\MySolutionRoot ProductName Bundle.wxs Config.wxi C:\MyDestinationDir");
                        Environment.Exit(0);
                    } else {
                        if(!Directory.Exists(rootDirectory)) {
                            Console.WriteLine(@"Solution directory specified does not exist. Exiting.");
                            Environment.Exit(0);
                        } else {
                            //Correct number of arguments - ready to attempt processing.
                            var product = args[1];
                            var configFile = args[2];
                            var bundleFile = args[3];
                            var destDirectory = args[4];

                            FileWriter.WriteConfigFile(product, product + configFile);
                            Console.WriteLine(@"Successfully completed writing Config file.");
                            FileWriter.WriteBundleFile(product + bundleFile, product);
                            Console.WriteLine(@"Successfully completed writing Bundle file.");
                            FileWriter.WriteProductFile(product);
                            Console.WriteLine(@"Successfully completed writing Product file.");
                            FileWriter.WriteContentFile(rootDirectory, product + Constants.ContentFileSuffix, product + configFile);
                            Console.WriteLine(@"Successfully completed writing Product Content file.");

                            var filesMoved = FileMover.MoveFiles(destDirectory);
                            Console.WriteLine(@"Finished moving {0} files to {1}.", filesMoved, destDirectory);
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }
    }
}