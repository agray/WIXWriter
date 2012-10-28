using System.Collections;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace com.domaintransformations.util {
    class Exclusions {
        private ArrayList fileExclusionList = new ArrayList();
        private ArrayList fileExtensionExclusionList = new ArrayList();
        private ArrayList directoryExclusionList = new ArrayList();

        public void populateExclusionLists() {
            WIXWriter.Properties.Settings settings = WIXWriter.Properties.Settings.Default;
            string[] files = settings.files.Split(',');
            string[] extensions = settings.fileextensions.Split(',');
            string[] dirs = settings.directories.Split(',');

            foreach(string file in files) {
                fileExclusionList.Add(file);
            }

            foreach(string pattern in extensions) {
                fileExtensionExclusionList.Add(pattern);
            }

            foreach(string directory in dirs) {
                directoryExclusionList.Add(directory);
            }
        }

        //******************************
        // BOOLEAN METHODS
        //******************************
        public bool isValidFile(string filename) {
            return !isInFileExclusionList(filename) && !isInFileExtensionExclusionList(filename);
        }

        public bool isValidDirectory(string directory) {
            string lastDirectory = getLastDirectory(directory);
            return !isInDirectoryExclusionList(lastDirectory);
        }

        public bool hasValidFiles(string appRoot, string root, string directory) {
            int validFileCount = 0;
            string absolutePath;
            if(appRoot.Equals(root)) {
                absolutePath = root + "\\" + directory;
            } else {
                absolutePath = appRoot + "\\" + root + "\\" + directory;
            }
            if(!Directory.Exists(absolutePath)) {
                return false;
            } else {
                FileInfo[] files = getAllFilesInTree(absolutePath);
                foreach(FileInfo file in files) {
                    if(isValidFile(file.Name)) {
                        validFileCount++;
                    }
                }
                return validFileCount > 0;

            }
        }

        public bool hasFiles(FileInfo[] files) {
            return files.Length > 0;
        }

        public bool hasValidSubdirectories(string directory) {
            int validDirectoryCount = 0;
            string[] directories = Directory.GetDirectories(directory, "*.*", SearchOption.AllDirectories);

            foreach(string inputDir in directories) {
                if(isValidDirectory(inputDir)) {
                    validDirectoryCount++;
                }
            }

            return validDirectoryCount > 0;
        }

        private bool isInFileExclusionList(string filename) {
            return fileExclusionList.Contains(filename);
        }

        private bool isInFileExtensionExclusionList(string filename) {
            Regex fileregex;

            foreach(string extension in fileExtensionExclusionList) {
                fileregex = new Regex(createRegExString(extension));
                if(fileregex.Match(filename).Success) {
                    return true;
                }
            }
            return false;
        }

        public string getLastDirectory(string subDir) {
            return subDir.Substring((subDir.LastIndexOf('\\') + 1), subDir.Length - subDir.LastIndexOf('\\') - 1);
        }

        private string createRegExString(string extension) {
            return "^.*\\.(" + extension.ToLower() + "|" + extension.ToUpper() + ")$";
        }

        private bool isInDirectoryExclusionList(string directoryName) {
            return directoryExclusionList.Contains(directoryName);
        }

        public static FileInfo[] getAllFilesInDirectory(string rootDir) {
            DirectoryInfo root = new DirectoryInfo(rootDir);
            return root.GetFiles();
        }

        public static FileInfo[] getAllFilesInTree(string rootDir) {
            DirectoryInfo root = new DirectoryInfo(rootDir);
            return root.GetFiles("*.*", SearchOption.AllDirectories);
        }
    }
}