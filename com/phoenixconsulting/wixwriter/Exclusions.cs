using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WIXWriter.Properties;

namespace com.phoenixconsulting.wixwriter {
    class Exclusions {
        private static readonly List<object> FileExclusionList = new List<object>();
        private static readonly List<object> FileExtensionExclusionList = new List<object>();
        private static readonly List<object> DirectoryExclusionList = new List<object>();

        public static void PopulateExclusionLists() {
            Settings settings = Settings.Default;
            string[] files = settings.files.Split(',');
            string[] extensions = settings.fileextensions.Split(',');
            string[] dirs = settings.directories.Split(',');

            foreach(string file in files) {
                FileExclusionList.Add(file);
            }

            foreach(string pattern in extensions) {
                FileExtensionExclusionList.Add(pattern);
            }

            foreach(string directory in dirs) {
                DirectoryExclusionList.Add(directory);
            }
        }

        //******************************
        // BOOLEAN METHODS
        //******************************
        public static bool IsValidFile(string filename) {
            return !IsInFileExclusionList(filename) && !IsInFileExtensionExclusionList(filename);
        }

        public static bool IsValidDirectory(string directory) {
            string lastDirectory = FileUtil.GetLastDirectory(directory);
            return !IsInDirectoryExclusionList(lastDirectory);
        }

        public static bool HasValidFiles(string appRoot, string root, string directory) {
            int validFileCount = 0;
            string absolutePath = root + Constants.DIRSEP + directory;
            if (!appRoot.Equals(root)) {
                absolutePath = appRoot + Constants.DIRSEP + absolutePath;
            }
            if(!Directory.Exists(absolutePath)) {
                return false;
            } 
            FileInfo[] files = FileUtil.GetAllFilesInTree(absolutePath);
            foreach(FileInfo file in files) {
                if(IsValidFile(file.Name)) {
                    validFileCount++;
                }
            }
            return validFileCount > 0;
        }

        public static bool HasValidSubdirectories(string directory) {
            int validDirectoryCount = 0;
            string[] directories = Directory.GetDirectories(directory, Constants.ALLFILES, SearchOption.AllDirectories);

            foreach(string inputDir in directories) {
                if(IsValidDirectory(inputDir)) {
                    validDirectoryCount++;
                }
            }

            return validDirectoryCount > 0;
        }

        private static bool IsInFileExclusionList(string filename) {
            return FileExclusionList.Contains(filename);
        }

        private static bool IsInFileExtensionExclusionList(string filename) {
            foreach(string extension in FileExtensionExclusionList) {
                Regex fileregex = new Regex(CreateRegExString(extension));
                if(fileregex.Match(filename).Success) {
                    return true;
                }
            }
            return false;
        }

        private static bool IsInDirectoryExclusionList(string directoryName) {
            return DirectoryExclusionList.Contains(directoryName);
        }

        private static string CreateRegExString(string extension) {
            return "^.*\\.(" + extension.ToLower() + "|" + extension.ToUpper() + ")$";
        }
    }
}