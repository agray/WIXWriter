using System.IO;

namespace WIXWriter {
    public class FileUtil {

        public static int moveFiles(string path) {
            DirectoryInfo fromDir = new DirectoryInfo(".");
            DirectoryInfo toDir = new DirectoryInfo(path);

            FileInfo[] fromFiles = fromDir.GetFiles("*.wx*");
            if(fromFiles.Length > 0) {
                foreach(FileInfo file in fromFiles) {
                    if(File.Exists(toDir + "\\" + file.Name)) {
                        File.Delete(toDir + "\\" + file.Name);
                        file.MoveTo(toDir + "\\" + file.Name);
                    } else {
                        file.MoveTo(toDir + "\\" + file.Name);
                    }
                }
                return fromFiles.Length;
            }
            return 0;
        }
    }
}