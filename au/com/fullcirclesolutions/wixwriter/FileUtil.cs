using System.IO;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    public class FileUtil {
        public static FileInfo[] GetAllFilesInDirectory(string rootDir) {
            var root = new DirectoryInfo(rootDir);
            return root.GetFiles();
        }

        public static FileInfo[] GetAllFilesInTree(string rootDir) {
            var root = new DirectoryInfo(rootDir);
            return root.GetFiles(Constants.Allfiles, SearchOption.AllDirectories);
        }

        public static bool HasFiles(FileInfo[] files) {
            return files.Length > 0;
        }

        public static string GetLastDirectory(string subDir) {
            return subDir.Substring((subDir.LastIndexOf(Constants.Dirsep, System.StringComparison.Ordinal) + 1),
                                    subDir.Length - subDir.LastIndexOf(Constants.Dirsep, System.StringComparison.Ordinal) - 1);
        }
    }
}
