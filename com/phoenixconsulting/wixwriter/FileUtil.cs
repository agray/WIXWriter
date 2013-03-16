using System.IO;

namespace com.phoenixconsulting.wixwriter {
    public class FileUtil {
        public static FileInfo[] GetAllFilesInDirectory(string rootDir) {
            DirectoryInfo root = new DirectoryInfo(rootDir);
            return root.GetFiles();
        }

        public static FileInfo[] GetAllFilesInTree(string rootDir) {
            DirectoryInfo root = new DirectoryInfo(rootDir);
            return root.GetFiles(Constants.ALLFILES, SearchOption.AllDirectories);
        }

        public static bool HasFiles(FileInfo[] files) {
            return files.Length > 0;
        }

        public static string GetLastDirectory(string subDir) {
            return subDir.Substring((subDir.LastIndexOf(Constants.DIRSEP) + 1),
                                    subDir.Length - subDir.LastIndexOf(Constants.DIRSEP) - 1);
        }
    }
}
