using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    public class FileWriter {
        private static readonly StringBuilder ConfigFile = new StringBuilder();
        private static readonly StringBuilder BundleFile = new StringBuilder();
        private static readonly StringBuilder ProductFile = new StringBuilder();
        private static readonly StringBuilder ContentFile = new StringBuilder();

        private static readonly Dictionary<string, string> DirectoryGuiDs = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> ComponentGuiDs = new Dictionary<string, string>();

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
            WriteFile(configFilename, ConfigFile);
        }

        public static void WriteBundleFile(string bundleFilename, string productName) {
            BundleFile.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
                .AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\"")
                .AppendLine("     xmlns:bal=\"http://schemas.microsoft.com/wix/BalExtension\">")
                .AppendLine("<?include $(var." + productName + ".SetupLibrary.ProjectDir)\\Config.wxi?>\"")
                .AppendLine("<Bundle Name=\"" + productName + " Installer\"")
                .AppendLine("SplashScreenSourceFile=\"Splash/OaktonSplash.bmp\"")
                .AppendLine("IconSourceFile=\"Icons/oakton128x128.ico\"")
                .AppendLine("Version=\"$(var.Version)\"")
                .AppendLine("Manufacturer=\"Full Circle Solutions\"")
                .AppendLine("Copyright=\"Copyright © " + DateTime.Now.Year + " Full Circle Solutions. All Rights Reserved.\"")
                .AppendLine("UpgradeCode=\"$(var.UpgradeCode)\">")
                .AppendLine("<BootstrapperApplicationRef Id=\"WixStandardBootstrapperApplication.RtfLicense\">")
                .AppendLine("  <bal:WixStandardBootstrapperApplication LicenseFile=\"Licences\\eula.rtf\"")
                .AppendLine("                                          LogoFile=\"Logos\\Oakton.png\"")
                .AppendLine("                                          SuppressOptionsUI=\"yes\"/>")
                .AppendLine("</BootstrapperApplicationRef>")
                .AppendLine("<Chain>")
                .AppendLine("  <MsiPackage SourceFile=\"$(var." + productName + ".Standard.Setup.Msi.TargetPath)\" />")
                .AppendLine("</Chain>")
                .AppendLine("</Bundle>")
                .AppendLine("</Wix>");
            WriteFile(bundleFilename, BundleFile);
        }

        public static void WriteProductFile(string product) {
            ProductFile.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
                       .AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\"")
                       .AppendLine("xmlns:util=\"http://schemas.microsoft.com/wix/UtilExtension\"")
                       .AppendLine("xmlns:fx=\"http://schemas.microsoft.com/wix/NetFxExtension\">")
                       .AppendLine(" xmlns:iis=\"http://schemas.microsoft.com/wix/IIsExtension\">")
                       .AppendLine("<?include $(var." + product + ".SetupLibrary.ProjectDir)\\Config.wxi?>")
                       .AppendLine("<Product Id=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\" Name=\"$(var.ProductName)\" Language=\"1033\" Version=\"1.0.0.0\" Manufacturer=\"Full Circle Solutions\" UpgradeCode=\"b6b32a5a-0a7b-492a-9c32-c4c773635fdf\">")
                       .AppendLine("<Package Keywords=\"Installer\" Description=\"$(var.ProductName) $(var.Version) Installer\" InstallerVersion=\"500\" InstallPrivileges=\"elevated\" Compressed=\"yes\" InstallScope=\"perMachine\" Platform=\"x86\" />")
                       .AppendLine("<!--<MajorUpgrade DowngradeErrorMessage=\"A newer version of $(var.ProductName) is already installed.\" />-->")
                       .AppendLine("<MediaTemplate EmbedCab=\"yes\" />")
                       .AppendLine("<PropertyRef Id=\"NETFRAMEWORK45\"/>")
                       
                       .AppendLine("<Condition Message=\"This application requires .NET Framework 4.5. Please install the .NET Framework then run this installer again.\">")
                       .AppendLine("  <![CDATA[Installed OR NETFRAMEWORK45]]>")
                       .AppendLine("</Condition>")
                       .AppendLine("<!--Uninstall old versions-->")
                       .AppendLine("<Upgrade Id=\"$(var.UpgradeCode)\">")
                       .AppendLine("  <UpgradeVersion Minimum=\"0.0.0.0\"") 
                       .AppendLine("                  IncludeMinimum=\"yes\"")
                       .AppendLine("                  OnlyDetect=\"no\"")
                       .AppendLine("                  Maximum=\"$(var.Version)\"") 
                       .AppendLine("                  IncludeMaximum=\"no\"")
                       .AppendLine("                  Property=\"PREVIOUSFOUND\" />")
                       .AppendLine("</Upgrade>")
                       .AppendLine("<InstallExecuteSequence>")
                       .AppendLine("  <RemoveExistingProducts After=\"InstallInitialize\"/>")
                       .AppendLine("</InstallExecuteSequence>")
                       
                       .AppendLine("<!-- Program Installation -->")
                       .AppendLine("<Directory Id=\"TARGETDIR\" Name=\"SourceDir\">")

                       .AppendLine("  <!--Add EventSource-->")
                       .AppendLine("<Component Id=\""+ product + "EventSource\" Guid=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("  <util:EventSource Name=\"$(var.ProductName)\"")
                       .AppendLine("                    Log=\"Application\"")
                       .AppendLine("                    EventMessageFile=\"[NETFRAMEWORK40FULLINSTALLROOTDIR]EventLogMessages.dll\"")
                       .AppendLine("                    KeyPath='yes'/>")
                       .AppendLine("</Component>")

                       .AppendLine("<!-- Define the directory structure -->")
                       .AppendLine("<Directory Id=\"$(var.PlatformProgramFilesFolder)\">")
                       .AppendLine("<Directory Id=\"ProductContent.Manifest\" Name=\"$(var.ProductName)\">")

                       .AppendLine("<Component Id=\"ProductContent.Manifest\" Guid=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\" Feature=\"Complete\"")
                       .AppendLine("           SharedDllRefCount=\"no\" KeyPath=\"yes\" NeverOverwrite=\"no\" Permanent=\"no\" Transitive=\"no\"")
                       .AppendLine("           Win64=\"yes\" Location=\"either\">")
                       .AppendLine("  <CreateFolder>")
                       .AppendLine("    <util:PermissionEx User=\"Everyone\"")
                       .AppendLine("                       Read=\"yes\"")
                       .AppendLine("                       GenericAll=\"yes\"")
                       .AppendLine("                       Delete=\"yes\"")
                       .AppendLine("                       CreateChild=\"yes\"")
                       .AppendLine("                       CreateFile=\"yes\"")
                       .AppendLine("                       DeleteChild=\"yes\"")
                       .AppendLine("                       Traverse=\"yes\"/>")
                       .AppendLine("  </CreateFolder>")
                       .AppendLine("</Component>")
                       .AppendLine("</Directory>") //this is supposed to close off ProductContent.Manifest
                       .AppendLine("</Directory>") //this is supposed to close off PlatformProgramFilesFolder
                       .AppendLine("<Directory Id=\"ProgramMenuFolder\">")
                       .AppendLine("<Directory Id=\"ApplicationProgramsFolder\" Name=\"$(var.ProductName)\"/>")
                       .AppendLine("</Directory>")
                       .AppendLine("<Directory Id=\"DesktopFolder\" Name=\"Desktop\" />")
                       .AppendLine("</Directory>")
                       .AppendLine("<!-- Add files to installer package -->")
                       .AppendLine("<DirectoryRef Id=\"ProductContent.Manifest\">")
                       .AppendLine("</DirectoryRef>")

                       .AppendLine("<!-- Add the shortcuts-->")
                       .AppendLine("<DirectoryRef Id=\"ApplicationProgramsFolder\">")
                       .AppendLine("<Component Id=\"UninstallShortcut\" Guid=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<Shortcut Id=\"UninstallProduct\"")
                       .AppendLine("Name=\"Uninstall $(var.ProductName)\"")
                       .AppendLine("Description=\"Uninstalls $(var.ProductName)\"")
                       .AppendLine("Target=\"[System64Folder]msiexec.exe\"")
                       .AppendLine("Arguments=\"/x [ProductCode]\"")
                       .AppendLine("Icon=\"WebIcon\"/>")
                       .AppendLine("<RemoveFolder Id=\"ApplicationProgramsFolder\" On=\"uninstall\"/>")
                       .AppendLine("<RegistryValue Root=\"HKCU\" Key=\"Software\\Microsoft\\$(var.ProductName)\" Name=\"installed\" Type=\"integer\" Value=\"1\" KeyPath=\"yes\"/>")
                       .AppendLine("</Component>")
                       .AppendLine("<!---Create Environment variable-->")
                       .AppendLine("<Component Id=\"PRODUCTHOMEENVVAR\" Guid=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<Environment Id=\"ProductHomeVar\"")
                       .AppendLine("             Action=\"set\"")
                       .AppendLine("             Part=\"all\"")
                       .AppendLine("             Name=\"$(var.HomeDirectory)\"")
                       .AppendLine("             Permanent=\"no\"")
                       .AppendLine("             System=\"yes\"")
                       .AppendLine("             Value=\"[ProductContent.Manifest]\"/>")
                       .AppendLine("<RegistryValue Root=\"HKCU\" Key=\"Software\\Microsoft\\$(var.ProductName)\" Name=\"installed\" Type=\"integer\" Value=\"1\" KeyPath=\"yes\"/>")
                       .AppendLine("<CreateFolder />")
                       .AppendLine("<RemoveFolder Id=\"ProductHomeVar\" On=\"uninstall\"/>")
                       .AppendLine("</Component>")
                       .AppendLine("<!--Add PRODUCTHOMEENVVAR to Path-->")
                       .AppendLine("<Component Id=\"PATHENVVAR\" Guid=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<Environment Id=\"PathVar\"")
                       .AppendLine("             Action=\"set\"")
                       .AppendLine("             Part=\"first\"")
                       .AppendLine("             Name=\"Path\"")
                       .AppendLine("             Permanent=\"no\"")
                       .AppendLine("             System=\"yes\"")
                       .AppendLine("             Value=\"[ProductContent.Manifest]\" />")
                       .AppendLine("<RegistryValue Root=\"HKCU\" Key=\"Software\\Microsoft\\$(var.ProductName)\" Name=\"installed\" Type=\"integer\" Value=\"1\" KeyPath=\"yes\"/>")
                       .AppendLine("<CreateFolder />")
                       .AppendLine("<RemoveFolder Id=\"PathVar\" On=\"uninstall\"/>")
                       .AppendLine("</Component>")
                       .AppendLine("</DirectoryRef>")

                       .AppendLine("<DirectoryRef Id=\"DesktopFolder\">")
                       .AppendLine("<Component Id=\"DesktopShortcut\" Guid=\"" + GuidGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<Shortcut Id=\"ApplicationDesktopShortcut\"")
                       .AppendLine("          Name=\"StackedBrowsers Config\"")
                       .AppendLine("          Description=\"StackedBrowser Config Tool\"")
                       .AppendLine("         Target=\"[ProductContent.Manifest]$(var.ProductName).exe\"")
                       .AppendLine("          WorkingDirectory=\"ProductContent.Manifest\"/>")
                       .AppendLine("<RemoveFolder Id=\"DesktopFolder\" On=\"uninstall\"/>")
                       .AppendLine("<RegistryValue Root=\"HKCU\"")
                       .AppendLine("               Key=\"Software/$(var.ProductName)\"")
                       .AppendLine("               Name=\"installed\"")
                       .AppendLine("               Type=\"integer\"")
                       .AppendLine("               Value=\"1\"")
                       .AppendLine("                KeyPath=\"yes\"/>")
                       .AppendLine("</Component>")
                       .AppendLine("</DirectoryRef>")
                       .AppendLine("  <Icon Id=\"WebIcon\" SourceFile=\"Icons\\oakton128x128.ico\" />")
                       .AppendLine("</Product>")
                       .Append("</Wix>");
            WriteFile(product + Constants.ProductFileSuffix, ProductFile);
        }

        public static void WriteContentFile(string rootDir, string contentFilename, string configFilename) {
            Exclusions.PopulateExclusionLists();
            BuildContentFile(rootDir, configFilename);
            WriteFile(contentFilename, ContentFile);
        }

        private static void BuildContentFile(string rootDir, string configFilename) {
            EmitXmlHeader(configFilename);
            EmitRootDirFiles(rootDir, string.Empty);
            EmitDirectoryStructure(rootDir);
            EmitComponentsByDirectory(rootDir);
            EmitComponentGroup();
            EmitXmlFooter();
        }

        private static void EmitXmlHeader(string configFilename) {
          ContentFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                     .AppendLine()
                     .AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\">")
                     .AppendLine("<?include $(sys.CURRENTDIR)\\" + configFilename + "?>");
        }

        public static void EmitXmlFooter() {
            ContentFile.AppendLine("</Wix>");
        }

        private static void EmitRootDirFiles(string rootDir, string relativePath) {
            EmitDsxmlHeader();

            var files = FileUtil.GetAllFilesInDirectory(rootDir);
            ContentFile.AppendLine("\n\n" + "<!--Root Directory Files-->");

            foreach (var file in files) {
                if (Exclusions.IsValidFile(file.Name)) {
                    EmitComponentXml(rootDir, relativePath, file.Name);
                }
            }
        }

        private static void EmitDsxmlHeader() {
            ContentFile.AppendLine("<Fragment>")
                       .AppendLine("<DirectoryRef Id=\"ProductContent.Manifest\">");
        }

        private static void EmitDirectoryStructure(string rootDir) {
            ContentFile.AppendLine("<!--Subdirectories-->");
            WalkDirectoryTree(rootDir, new DirectoryInfo(rootDir));
            EmitDsxmlFooter();
        }

        private static void WalkDirectoryTree(string appRoot, DirectoryInfo root) {
            // Now find all the subdirectories under this directory.
            var subDirs = root.GetDirectories();

            foreach (var dirInfo in subDirs) {
                // Recursive call for each subdirectory.
                if (Exclusions.IsValidDirectory(dirInfo.Name) && Exclusions.HasValidFiles(appRoot, root.ToString(), dirInfo.Name)) {
                    var directoryGuid = GuidGenerator.GenerateTimeBasedGuidStringWithNoDashes();
                    DirectoryGuiDs.Add(dirInfo.FullName, directoryGuid);
                    EmitDirectoryEntryBody(dirInfo.Name, directoryGuid, Exclusions.HasValidSubdirectories(dirInfo.FullName));
                    WalkDirectoryTree(appRoot, dirInfo);

                    if (Exclusions.HasValidSubdirectories(dirInfo.FullName)) {
                        EmitDirectoryEntryClosing();
                    }
                }
            }
        }

        private static void EmitDirectoryEntryBody(string lastDirectory, string directoryGuid, bool hasValidSubDirectories) {
            ContentFile.Append("<Directory Id=\"dir" + directoryGuid + "\" Name=\"" + lastDirectory + "\"")
                       .AppendLine(hasValidSubDirectories ? ">" : " />");
        }

        private static void EmitDirectoryEntryClosing() {
            ContentFile.AppendLine("</Directory>");
        }

        private static void EmitComponentGroup() {
            EmitCgxmlHeader();
            foreach (var c in ComponentGuiDs) {
                EmitCgComponentItemXml(c.Value);
            }
            EmitCgxmlFooter();
        }

        private static void EmitCgxmlHeader() {
            ContentFile.AppendLine("<Fragment><ComponentGroup Id=\"ProductContent.Manifest\">");
        }

        private static void EmitCgComponentItemXml(string guid) {
            ContentFile.AppendLine("<ComponentRef Id=\"cmp" + guid + "\" />");
        }

        private static void EmitCgxmlFooter() {
            ContentFile.AppendLine("</ComponentGroup>")
                       .AppendLine("</Fragment>");
        }

        private static void EmitDsxmlFooter() {
            ContentFile.AppendLine("</DirectoryRef>")
                       .AppendLine("</Fragment>");
        }

        private static void EmitComponentsByDirectory(string rootDir) {
            foreach (var d in DirectoryGuiDs) {
                var directoryName = d.Key;
                var relativeDirectoryName = RemoveProjectRoot(directoryName, rootDir);
                if (Exclusions.IsValidDirectory(directoryName)) {
                    var files = FileUtil.GetAllFilesInDirectory(directoryName);
                    if (FileUtil.HasFiles(files)) {
                        EmitCbdxmlHeader(relativeDirectoryName, d.Value);
                        foreach(var file in files) {
                            if(Exclusions.IsValidFile(file.Name)) {
                                EmitComponentXml(directoryName, relativeDirectoryName, file.Name);
                            }
                        }
                        EmitCbdxmlFooter();
                    }
                }
            }
        }

        private static string RemoveProjectRoot(string directory, string rootDir) {
            var startIndex = rootDir.Length + 1;
            var endIndex = directory.Length - rootDir.Length - 1;
            return directory.Substring(startIndex, endIndex);
        }

        private static void EmitComponentXml(string directory, string relativePath, string name) {
            var fullPath = directory + "\\" + name;
            var componentGuid = GuidGenerator.GenerateTimeBasedGuidStringWithNoDashes();

            if (!ComponentGuiDs.ContainsKey(fullPath)) {
                ComponentGuiDs.Add(fullPath, componentGuid);
            }

            EmitSingleComponentXml(name, relativePath, componentGuid);
        }

        private static void EmitSingleComponentXml(string fullName, string relativePath, string componentGuid) {
            ContentFile.AppendLine("<Component Id=\"cmp")
                       .AppendLine(componentGuid + "\"")
                       .AppendLine(" Guid=\"")
                       .AppendLine(GuidGenerator.GenerateTimeBasedGuidString() + "\">")
                       .AppendLine("<File Id=\"fil")
                       .AppendLine(GuidGenerator.GenerateTimeBasedGuidStringWithNoDashes());

            if(relativePath.Equals(string.Empty)) {
                ContentFile.AppendLine("\" Source=\"$(var.ProductRootDir)\\" + relativePath + fullName + "\" />");
            } else {
                ContentFile.AppendLine("\" Source=\"$(var.ProductRootDir)\\" + relativePath + "\\" + fullName + "\" />");
            }
            ContentFile.AppendLine("</Component>");
        }

        private static void EmitCbdxmlHeader(string name, string guid) {
            ContentFile.AppendLine("<!--" + name + " directory-->")
                       .AppendLine("<Fragment>")
                       .AppendLine("<DirectoryRef Id=\"dir" + guid + "\">");
        }

        private static void EmitCbdxmlFooter() {
            ContentFile.AppendLine("</DirectoryRef>")
                       .AppendLine("</Fragment>");
        }

        private static void WriteFile(string filename, StringBuilder file) {
            File.WriteAllText(filename, PrettyPrintFormatter.Format(file.ToString()));
        }
    }
}