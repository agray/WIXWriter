using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.phoenixconsulting.wixwriter {
    public class FileWriter {
        private static readonly StringBuilder ConfigFile = new StringBuilder();
        private static readonly StringBuilder ProductFile = new StringBuilder();
        private static readonly StringBuilder ContentFile = new StringBuilder();
        private static readonly Dictionary<string, string> DirectoryGUIDs = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> ComponentGUIDs = new Dictionary<string, string>();

        public static void WriteConfigFile(string productName, string configFilename) {
            ConfigFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                      .AppendLine("<Include>")
                      .AppendLine("<!-- Product name as you want it to appear in Add/Remove Programs-->")
                      .AppendLine("<?if $(var.Platform) = x64 ?>")
                      .AppendLine("<?define ProductName = \"" + productName + " (64 bit)\" ?>")
                      .AppendLine("<?define Win64 = \"yes\" ?>")
                      .AppendLine("<?define PlatformProgramFilesFolder = \"ProgramFiles64Folder\" ?>")
                      .AppendLine("<?else ?>")
                      .AppendLine("<?define ProductName = \"" + productName + "\" ?>")
                      .AppendLine("<?define ProductRootDir = \"$(sys.CURRENTDIR)..\\" + productName + "Web\" ?>")
                      .AppendLine("<?define Win64 = \"no\" ?>")
                      .AppendLine("<?define PlatformProgramFilesFolder = \"C:\\Program Files\" ?>")
                      .AppendLine("<?endif ?>")
                      .Append("</Include>");
            WriteFile(configFilename, ConfigFile.ToString());
        }

        public static void WriteProductFile(string configFilename, string productFilename) {
            ProductFile.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
                       .AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\"")
                       .AppendLine("\n")
                       .AppendLine(" xmlns:iis=\"http://schemas.microsoft.com/wix/IIsExtension\">")
                       .AppendLine("<?include $(sys.CURRENTDIR)\\" + configFilename + "?>")
                       .AppendLine("<Product Id=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\" Name=\"$(var.ProductName)\" Language=\"1033\" Version=\"1.0.0.0\" Manufacturer=\"Pheonix Systems\" UpgradeCode=\"b6b32a5a-0a7b-492a-9c32-c4c773635fdf\">")
                       .AppendLine("<Package InstallerVersion=\"200\" InstallPrivileges=\"elevated\" InstallScope=\"perMachine\" Platform=\"x86\" Compressed=\"yes\"  Description=\"$(var.ProductName)\" />")
                       .AppendLine("<Media Id=\"1\" Cabinet=\"media1.cab\" EmbedCab=\"yes\" />")
                       .AppendLine("<Directory Id=\"TARGETDIR\" Name=\"SourceDir\">")
                       .AppendLine("<Directory Id=\"ROOT_DRIVE\" Name=\"root\">")
                       .AppendLine("<Directory Id=\"IISMain\" Name=\"Inetpub\" >")
                       .AppendLine("<Directory Id=\"WWWMain\" Name=\"wwwroot\" >")
                       .AppendLine("<Directory Id=\"ProductWeb.Content\" Name=\"$(var.ProductName)\"  />")
                       .AppendLine("<Component Id=\"$(var.ProductName)_productcomponent\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\"><iis:WebAppPool Id=\"$(var.ProductName)_webapppool\" Name=\"$(var.ProductName)_webapppool\" /><CreateFolder/></Component>")
                       .AppendLine("<Component Id =\"$(var.ProductName)_websitecomponent\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<iis:WebSite Id=\"$(var.ProductName)_website\" Description=\"$(var.ProductName) Website\" Directory=\"ProductWeb.Content\" WebApplication=\"$(var.ProductName)_webapp\">")
                       .AppendLine("<iis:WebAddress Id=\"$(var.ProductName)_webAddress\" IP=\"127.0.0.1\" Port=\"80\" />")
                       .AppendLine("<iis:WebDirProperties Id=\"$(var.ProductName)_webdirprops\" DefaultDocuments=\"Default.aspx\" Script=\"yes\" Read=\"yes\" Write=\"yes\"/>")
                       .AppendLine("</iis:WebSite><CreateFolder/></Component>")
                       .AppendLine("<Component Id=\"Manual\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<File Id=\"Manual\" Name=\"readme.doc\" DiskId=\"1\" Source=\"readme.doc\" KeyPath=\"yes\"><Shortcut Id=\"startmenuManual\" Directory=\"ProgramMenuDir\" Name=\"Instruction Manual\" Advertise=\"yes\" /></File>")
                       .AppendLine("</Component>")
                       .AppendLine("<Component Id=\"WebShortcut\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<File Id=\"$(var.ProductName).txt\" Name=\"$(var.ProductName).url\" Vital=\"yes\" KeyPath=\"yes\" Source=\"$(var.ProductName).txt\">")
                       .AppendLine("<Shortcut Id=\"WebShortcut\" Directory=\"$(var.ProductName)ProgramMenuDir\" IconIndex=\"0\" Hotkey=\"0\" Name=\"$(var.ProductName)\" WorkingDirectory=\"INTERNETEXPLORER\" Advertise=\"yes\" Icon=\"eStoreLogo.ico\">")
                       .AppendLine("<Icon Id=\"eStoreLogo.ico\" SourceFile=\"$(sys.CURRENTDIR)..\\$(var.ProductName)Web\\Icons\\eStoreLogo.ico\" />")
                       .AppendLine("</Shortcut></File></Component>")
                       .AppendLine("</Directory></Directory></Directory>")
                       .AppendLine("<Directory Id=\"ProgramMenuFolder\" Name=\"Programs\">")
                       .AppendLine("<Directory Id=\"ProgramMenuDir\" Name=\"PheonixSystems\">")
                       .AppendLine("<Component Id=\"ProgramMenuDir\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\"><RemoveFolder Id=\"ProgramMenuDir\" On=\"uninstall\" /><RegistryValue Root=\"HKCU\" Key=\"SOFTWARE\\PheonixSystems\\$(var.ProductName)\" Type=\"string\" Value=\"\" KeyPath=\"yes\" /></Component>")
                       .AppendLine("<Directory Id=\"$(var.ProductName)ProgramMenuDir\" Name=\"eStoreAdmin\">")
                       .AppendLine("<Component Id=\"$(var.ProductName)ProgramMenuDir\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<RemoveFolder Id=\"$(var.ProductName)ProgramMenuDir\" On=\"uninstall\"/>")
                       .AppendLine("<RegistryValue Root=\"HKCU\" Key=\"SOFTWARE\\PheonixSystems\\$(var.ProductName)\" Type=\"string\" Value=\"\" KeyPath=\"yes\" />")
                       .AppendLine("</Component>")
                       .AppendLine("</Directory></Directory></Directory></Directory>")
                       .AppendLine("<iis:WebApplication Id=\"$(var.ProductName)_webapp\" Name=\"$(var.ProductName)_site\" WebAppPool=\"$(var.ProductName)_webapppool\" />")
                       .AppendLine("<Feature Id=\"$(var.ProductName)Complete\" Title=\"$(var.ProductName) Website\" Level=\"1\">")
                       .AppendLine("<ComponentGroupRef Id=\"ProductWeb.Content\" />")
                       .AppendLine("<ComponentGroupRef Id=\"$(var.ProductName)_productcomponentref\" />")
                       .AppendLine("<ComponentRef Id=\"ProgramMenuDir\"/>")
                       .AppendLine("<ComponentRef Id=\"$(var.ProductName)ProgramMenuDir\"/>")
                       .AppendLine("<ComponentRef Id=\"WebShortcut\"/>")
                       .AppendLine("<ComponentRef Id=\"Manual\"/>")
                       .AppendLine("</Feature>")
                       .AppendLine("<Feature Id=\"Documentation\" Title=\"Documentation\" Description=\"The user manual.\" Level=\"1000\">")
                       .AppendLine("<ComponentRef Id=\"ProgramMenuDir\"/>")
                       .AppendLine("<ComponentRef Id=\"Manual\" />")
                       .AppendLine("</Feature>")
                       .AppendLine("<ComponentGroup Id=\"$(var.ProductName)_productcomponentref\">")
                       .AppendLine("<ComponentRef Id=\"$(var.ProductName)_productcomponent\" />")
                       .AppendLine("<ComponentRef Id=\"$(var.ProductName)_websitecomponent\" />")
                       .AppendLine("</ComponentGroup>")
                       .AppendLine("<UIRef Id=\"WixUI_Mondo\" />")
                       .AppendLine("<UIRef Id=\"WixUI_ErrorProgressText\" />")
                       .AppendLine("<CustomAction Id=\"Assign_ROOT_DRIVE\" Property=\"ROOT_DRIVE\" Value=\"C:\\\"/>")
                       .AppendLine("<UI>")
                       .AppendLine("<InstallUISequence>")
                       .AppendLine("<Custom Action=\"Assign_ROOT_DRIVE\" Before=\"CostInitialize\"></Custom>")
                       .AppendLine("</InstallUISequence>")
                       .AppendLine("</UI>")
                       .AppendLine("<InstallExecuteSequence>")
                       .AppendLine("<Custom Action=\"Assign_ROOT_DRIVE\" Before=\"CostInitialize\"></Custom>")
                       .AppendLine("</InstallExecuteSequence>")
                       .AppendLine("</Product>")
                       .Append("</Wix>");
            WriteFile(productFilename, ProductFile.ToString());
        }

        public static void WriteContentFile(string rootDir, string contentFilename, string configFilename) {
            Exclusions.PopulateExclusionLists();
            BuildContentFile(rootDir, configFilename);
            WriteFile(contentFilename, ContentFile.ToString());
        }

        private static void BuildContentFile(string rootDir, string configFilename) {
            EmitXMLHeader(configFilename);
            EmitRootDirFiles(rootDir, string.Empty);
            EmitDirectoryStructure(rootDir);
            EmitComponentsByDirectory(rootDir);
            EmitComponentGroup();
            EmitXMLFooter();
        }

        private static void EmitXMLHeader(string configFilename) {
          ContentFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                     .AppendLine()
                     .AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\">")
                     .AppendLine("<?include $(sys.CURRENTDIR)\\" + configFilename + "?>");
        }

        public static void EmitXMLFooter() {
            ContentFile.AppendLine("</Wix>");
        }

        private static void EmitRootDirFiles(string rootDir, string relativePath) {
            EmitDSXMLHeader();

            FileInfo[] files = FileUtil.GetAllFilesInDirectory(rootDir);
            ContentFile.AppendLine("\n\n" + "<!--Root Directory Files-->");

            foreach (FileInfo file in files) {
                if (Exclusions.IsValidFile(file.Name)) {
                    EmitComponentXML(rootDir, relativePath, file.Name);
                }
            }
        }

        private static void EmitDSXMLHeader() {
            ContentFile.AppendLine("<Fragment>")
                       .AppendLine("<DirectoryRef Id=\"ProductWeb.Content\">");
        }

        private static void EmitDirectoryStructure(string rootDir) {
            ContentFile.AppendLine("<!--Subdirectories-->");
            WalkDirectoryTree(rootDir, new DirectoryInfo(rootDir));
            EmitDSXMLFooter();
        }

        private static void WalkDirectoryTree(string appRoot, DirectoryInfo root) {
            // Now find all the subdirectories under this directory.
            DirectoryInfo[] subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs) {
                // Recursive call for each subdirectory.
                if (Exclusions.IsValidDirectory(dirInfo.Name) && Exclusions.HasValidFiles(appRoot, root.ToString(), dirInfo.Name)) {
                    string directoryGUID = GUIDGenerator.GenerateTimeBasedGuidStringWithNoDashes();
                    DirectoryGUIDs.Add(dirInfo.FullName, directoryGUID);
                    EmitDirectoryEntryBody(dirInfo.Name, directoryGUID, Exclusions.HasValidSubdirectories(dirInfo.FullName));
                    WalkDirectoryTree(appRoot, dirInfo);

                    if (Exclusions.HasValidSubdirectories(dirInfo.FullName)) {
                        EmitDirectoryEntryClosing();
                    }
                }
            }
        }

        private static void EmitDirectoryEntryBody(string lastDirectory, string directoryGUID, bool hasValidSubDirectories) {
            ContentFile.Append("<Directory Id=\"dir" + directoryGUID + "\" Name=\"" + lastDirectory + "\"")
                       .AppendLine(hasValidSubDirectories ? ">" : " />");
        }

        private static void EmitDirectoryEntryClosing() {
            ContentFile.AppendLine("</Directory>");
        }

        private static void EmitComponentGroup() {
            EmitCGXMLHeader();
            foreach (KeyValuePair<string, string> c in ComponentGUIDs) {
                EmitCGComponentItemXML(c.Value);
            }
            EmitCGXMLFooter();
        }

        private static void EmitCGXMLHeader() {
            ContentFile.AppendLine("<Fragment><ComponentGroup Id=\"ProductWeb.Content\">");
        }

        private static void EmitCGComponentItemXML(string guid) {
            ContentFile.AppendLine("<ComponentRef Id=\"cmp" + guid + "\" />");
        }

        private static void EmitCGXMLFooter() {
            ContentFile.AppendLine("</ComponentGroup>")
                       .AppendLine("</Fragment>");
        }

        private static void EmitDSXMLFooter() {
            ContentFile.AppendLine("</DirectoryRef>")
                       .AppendLine("</Fragment>");
        }

        private static void EmitComponentsByDirectory(string rootDir) {
            foreach (KeyValuePair<string, string> d in DirectoryGUIDs) {
                string directoryName = d.Key;
                string relativeDirectoryName = RemoveProjectRoot(directoryName, rootDir);
                if (Exclusions.IsValidDirectory(directoryName)) {
                    FileInfo[] files = FileUtil.GetAllFilesInDirectory(directoryName);
                    if (FileUtil.HasFiles(files)) {
                        EmitCBDXMLHeader(relativeDirectoryName, d.Value);
                        foreach (FileInfo file in files) {
                            if (Exclusions.IsValidFile(file.Name)) {
                                EmitComponentXML(directoryName, relativeDirectoryName, file.Name);
                            }
                        }
                        EmitCBDXMLFooter();
                    }
                }
            }
        }

        private static string RemoveProjectRoot(string directory, string rootDir) {
            int startIndex = rootDir.Length + 1;
            int endIndex = directory.Length - rootDir.Length - 1;
            return directory.Substring(startIndex, endIndex);
        }

        private static void EmitComponentXML(string directory, string relativePath, string name) {
            string fullPath = directory + "\\" + name;
            string componentGUID = GUIDGenerator.GenerateTimeBasedGuidStringWithNoDashes();

            if (!ComponentGUIDs.ContainsKey(fullPath)) {
                ComponentGUIDs.Add(fullPath, componentGUID);
            }

            EmitSingleComponentXML(name, relativePath, componentGUID);
        }

        private static void EmitSingleComponentXML(string fullName, string relativePath, string componentGUID) {
            ContentFile.AppendLine("<Component Id=\"cmp")
                       .AppendLine(componentGUID + "\"")
                       .AppendLine(" Guid=\"")
                       .AppendLine(GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<File Id=\"fil")
                       .AppendLine(GUIDGenerator.GenerateTimeBasedGuidStringWithNoDashes());

            if(relativePath.Equals(string.Empty)) {
                ContentFile.AppendLine("\" Source=\"$(var.ProductRootDir)\\" + relativePath + fullName + "\" />");
            } else {
                ContentFile.AppendLine("\" Source=\"$(var.ProductRootDir)\\" + relativePath + "\\" + fullName + "\" />");
            }
            ContentFile.AppendLine("</Component>");
        }

        private static void EmitCBDXMLHeader(string name, string guid) {
            ContentFile.AppendLine("<!--" + name + " directory-->")
                       .AppendLine("<Fragment>")
                       .AppendLine("<DirectoryRef Id=\"dir" + guid + "\">");
        }

        private static void EmitCBDXMLFooter() {
            ContentFile.AppendLine("</DirectoryRef>")
                       .AppendLine("</Fragment>");
        }

        private static void WriteFile(string filename, string content) {
            File.WriteAllText(filename, PrettyPrintFormatter.Format(content));
        }
    }
}