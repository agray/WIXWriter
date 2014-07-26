using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WIXWriter.Properties;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    class Exclusions {
        private static readonly List<object> FileExclusionList = new List<object>();
        private static readonly List<object> FileExtensionExclusionList = new List<object>();
        private static readonly List<object> DirectoryExclusionList = new List<object>();

        public static void PopulateExclusionLists() {
            var settings = Settings.Default;
            var files = settings.files.Split(',');
            var extensions = settings.fileextensions.Split(',');
            var dirs = settings.directories.Split(',');

            foreach(var file in files) {
                FileExclusionList.Add(file);
            }

            foreach(var pattern in extensions) {
                FileExtensionExclusionList.Add(pattern);
            }

            foreach(var directory in dirs) {
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
            var lastDirectory = FileUtil.GetLastDirectory(directory);
            return !IsInDirectoryExclusionList(lastDirectory);
        }

        public static bool HasValidFiles(string appRoot, string root, string directory) {
            var absolutePath = root + Constants.Dirsep + directory;
            if (!appRoot.Equals(root)) {
                absolutePath = appRoot + Constants.Dirsep + absolutePath;
            }
            if(!Directory.Exists(absolutePath)) {
                return false;
            } 
            var files = FileUtil.GetAllFilesInTree(absolutePath);
            var validFileCount = files.Count(file => IsValidFile(file.Name));
            return validFileCount > 0;
        }

        public static bool HasValidSubdirectories(string directory) {
            var directories = Directory.GetDirectories(directory, Constants.Allfiles, SearchOption.AllDirectories);

            var validDirectoryCount = directories.Count(IsValidDirectory);

            return validDirectoryCount > 0;
        }

        private static bool IsInFileExclusionList(string filename) {
            return FileExclusionList.Contains(filename);
        }

        private static bool IsInFileExtensionExclusionList(string filename) {
            return (from string extension in FileExtensionExclusionList select new Regex(CreateRegExString(extension))).Any(fileregex => fileregex.Match(filename).Success);
        }

        private static bool IsInDirectoryExclusionList(string directoryName) {
            return DirectoryExclusionList.Contains(directoryName);
        }

        private static string CreateRegExString(string extension) {
            return "^.*\\.(" + extension.ToLower() + "|" + extension.ToUpper() + ")$";
        }
    }
}