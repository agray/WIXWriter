using System.IO;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    public class FileMover {
        public static int MoveFiles(string path) {
            var fromDir = new DirectoryInfo(".");
            var toDir = new DirectoryInfo(path);

            var fromFiles = fromDir.GetFiles("*.wx*");
            if (fromFiles.Length > 0) {
                foreach (var file in fromFiles) {
                    var targetFile = toDir + Constants.Dirsep + file.Name;
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