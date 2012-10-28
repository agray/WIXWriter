using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WIXWriter {
    public class FileUtil {

        public static int moveFiles(string path) {
            DirectoryInfo fromDir = new DirectoryInfo(".");
            DirectoryInfo toDir = new DirectoryInfo(path);

            FileInfo[] fromFiles = fromDir.GetFiles("*.wx*");
            if(fromFiles.Length > 0) {
                foreach(FileInfo file in fromFiles) {
                    if(File.Exists(toDir.ToString() + "\\" + file.Name)) {
                        File.Delete(toDir.ToString() + "\\" + file.Name);
                        file.MoveTo(toDir.ToString() + "\\" + file.Name);
                    } else {
                        file.MoveTo(toDir.ToString() + "\\" + file.Name);
                    }
                }
                return fromFiles.Length;
            } else {
                return 0;
            }
        }
    }
}