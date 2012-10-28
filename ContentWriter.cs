using System;
using System.Collections.Generic;
using System.IO;
using com.domaintransformations.util;

namespace com.domaintransformations.WIXGenerator {
    class ContentWriter {
        private StreamWriter contentWriter;
        private StreamWriter configWriter;
        private StreamWriter productWriter;

        private Exclusions exclusions = new Exclusions();
        private Dictionary<string, string> DirectoryGUIDs = new Dictionary<string, string>();
        private Dictionary<string, string> ComponentGUIDs = new Dictionary<string, string>();

        public void finalise() {
            contentWriter = null;
            configWriter = null;
            productWriter = null;
        }

        public void writeContentOutputFile(string rootDir, 
                                           string contentOutputFile, 
                                           string configOutputFile) {
            contentWriter = new StreamWriter(contentOutputFile);
            exclusions.populateExclusionLists();
            createContentFile(rootDir, configOutputFile);
            writeContentFile(contentWriter);
        }

        public void writeConfigOutputFile(string productName, string configOutputFile) {
            configWriter = new StreamWriter(configOutputFile);
            createConfigFile(productName);
            writeConfigFile(configWriter);
        }

        public void writeProductOutputFile(string configOutputFile, string productOutputFile) {
            productWriter = new StreamWriter(productOutputFile);
            createProductFile(configOutputFile);
            writeProductFile(productWriter);
        }

        private void createConfigFile(string productName) {
            FileContentBuilder.EmitConfigFile(productName);
        }

        private void createProductFile(string productName) {
            FileContentBuilder.EmitProductFile(productName);
        }

        private void createContentFile(string rootDir, string configOutputFile) {
            FileContentBuilder.EmitXMLHeader(configOutputFile);
            Console.Out.WriteLine("Written XML Header.");
            EmitRootDirFiles(rootDir, "");
            Console.Out.WriteLine("Written Root Directory Files.");
            EmitDirectoryStructure(rootDir);
            Console.Out.WriteLine("Written Directory Structure.");
            EmitComponentsByDirectory(rootDir);
            Console.Out.WriteLine("Written Components By Directory.");
            EmitComponentGroup();
            Console.Out.WriteLine("Written Component Group.");
            FileContentBuilder.EmitXMLFooter();
            Console.Out.WriteLine("Written XML Footer.");
        }

        private void EmitRootDirFiles(string rootDir, string relativePath) {
            FileContentBuilder.EmitDSXMLHeader();

            FileInfo[] files = Exclusions.getAllFilesInDirectory(rootDir);
            FileContentBuilder.AppendLine("\n\n" + "<!--Root Directory Files-->");

            foreach(FileInfo file in files) {
                if(exclusions.isValidFile(file.Name)) {
                    EmitComponentXML(rootDir, relativePath, file.Name);
                }
            }
        }

        private void EmitDirectoryStructure(string rootDir) {
            FileContentBuilder.AppendLine("<!--Subdirectories-->");
            WalkDirectoryTree(rootDir, new DirectoryInfo(rootDir));
            FileContentBuilder.EmitDSXMLFooter();
        }

        private void WalkDirectoryTree(string appRoot, DirectoryInfo root) {
            string directoryGUID;
            DirectoryInfo[] subDirs = null;

            // Now find all the subdirectories under this directory.
            subDirs = root.GetDirectories();

            foreach(DirectoryInfo dirInfo in subDirs) {
                // Recursive call for each subdirectory.
                if(exclusions.isValidDirectory(dirInfo.Name) && exclusions.hasValidFiles(appRoot, root.ToString(), dirInfo.Name)) {
                    directoryGUID = GUIDGenerator.GenerateTimeBasedGuidStringWithNoDashes();
                    DirectoryGUIDs.Add(dirInfo.FullName, directoryGUID);
                    FileContentBuilder.EmitDirectoryEntryBody(dirInfo.Name, directoryGUID, exclusions.hasValidSubdirectories(dirInfo.FullName));
                    WalkDirectoryTree(appRoot, dirInfo);

                    if(exclusions.hasValidSubdirectories(dirInfo.FullName)) {
                        FileContentBuilder.EmitDirectoryEntryClosing();
                    }
                }
            }
        }

        private void EmitComponentsByDirectory(string rootDir) {
            string directoryName;
            string guid;
            string relativeDirectoryName;
            foreach(KeyValuePair<string, string> d in DirectoryGUIDs){
                directoryName = d.Key;
                relativeDirectoryName = removeProjectRoot(directoryName, rootDir);
                if(exclusions.isValidDirectory(directoryName)){
                    FileInfo[] files = Exclusions.getAllFilesInDirectory(directoryName);
                    if(exclusions.hasFiles(files)) {
                        guid = d.Value;
                        FileContentBuilder.EmitCBDXMLHeader(relativeDirectoryName, guid);

                        foreach(FileInfo file in files) {
                            if(exclusions.isValidFile(file.Name)) {
                                EmitComponentXML(directoryName, relativeDirectoryName, file.Name);
                            }
                        }
                        FileContentBuilder.EmitCBDXMLFooter();
                    }
                }
            }
        }

        private string removeProjectRoot(string directory, string rootDir){
            int startIndex = rootDir.Length + 1;
            int endIndex = directory.Length - rootDir.Length - 1;
            return directory.Substring(startIndex, endIndex);
        }

        private void EmitComponentXML(string directory, string relativePath, string name) {
            string fullPath = directory + "\\" + name;
            string componentGUID = GUIDGenerator.GenerateTimeBasedGuidStringWithNoDashes();

            if(!ComponentGUIDs.ContainsKey(fullPath)) {
                ComponentGUIDs.Add(fullPath, componentGUID);
            }

            FileContentBuilder.EmitSingleComponentXML(name, relativePath, componentGUID);
        }

        private void EmitComponentGroup() {
            FileContentBuilder.EmitCGXMLHeader();
            foreach(KeyValuePair<string, string> c in ComponentGUIDs) {
                FileContentBuilder.EmitCGComponentItemXML(c.Value);
            }
            FileContentBuilder.EmitCGXMLFooter();
        }

        private void writeContentFile(StreamWriter writer) {
            FileContentBuilder fcb = new FileContentBuilder();
            writer.Write(PrettyPrintFormatter.format(fcb.contentToString()));
            writer.Flush();
            writer.Close();
        }

        private void writeConfigFile(StreamWriter writer) {
            FileContentBuilder fcb = new FileContentBuilder();
            writer.Write(PrettyPrintFormatter.format(fcb.configToString()));
            writer.Flush();
            writer.Close();
        }

        private void writeProductFile(StreamWriter writer) {
            FileContentBuilder fcb = new FileContentBuilder();
            writer.Write(PrettyPrintFormatter.format(fcb.productToString()));
            writer.Flush();
            writer.Close();
        }
    }
}