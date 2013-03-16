using System.IO;
namespace com.phoenixconsulting.wixwriter {
    public class FileMover {
        public static int MoveFiles(string path) {
            DirectoryInfo fromDir = new DirectoryInfo(".");
            DirectoryInfo toDir = new DirectoryInfo(path);

            FileInfo[] fromFiles = fromDir.GetFiles("*.wx*");
            if (fromFiles.Length > 0) {
                foreach (FileInfo file in fromFiles) {
                    string targetFile = toDir + Constants.DIRSEP + file.Name;
                    if (File.Exists(targetFile)) {
                        File.Delete(targetFile);
                    }
                    file.MoveTo(targetFile);
                }
                return fromFiles.Length;
            }
            return 0;
        }
    }
}