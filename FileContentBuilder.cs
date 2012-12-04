using System.Text;

namespace com.domaintransformations.util {
    class FileContentBuilder {
        private static StringBuilder contentBuilder = new StringBuilder();
        private static StringBuilder configBuilder = new StringBuilder();
        private static StringBuilder productBuilder = new StringBuilder();

        public static void EmitXMLHeader(string configOutputFile) {
            contentBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                  "\n" +
                                  "<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\">\n")
                          .Append("<?include $(sys.CURRENTDIR)\\" + configOutputFile + "?>");
        }

        public static void EmitSingleComponentXML(string fullName, string relativePath, string componentGUID) {
            contentBuilder.Append("<Component Id=\"cmp")
                          .Append(componentGUID + "\"")
                          .Append(" Guid=\"")
                          .Append(GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                          .Append("\n<File Id=\"fil")
                          .Append(GUIDGenerator.GenerateTimeBasedGuidStringWithNoDashes());

            if(relativePath.Equals("")) {
                contentBuilder.Append("\" Source=\"$(var.ProductRootDir)\\" + relativePath + fullName + "\" />");
            } else {
                contentBuilder.Append("\" Source=\"$(var.ProductRootDir)\\" + relativePath + "\\" + fullName + "\" />");
            }
            contentBuilder.Append("\n</Component>\n");
        }

        public static void EmitDSXMLHeader() {
            contentBuilder.Append("<Fragment>\n<DirectoryRef Id=\"ProductWeb.Content\">");
        }

        public static void EmitDirectoryEntryBody(string lastDirectory, string directoryGUID, bool hasValidSubDirectories) {
            contentBuilder.Append("<Directory Id=\"dir")
                              .Append(directoryGUID)
                              .Append("\" Name=\"" + lastDirectory + "\"");
            
            if(hasValidSubDirectories){
                contentBuilder.Append(">");
            } else {
                contentBuilder.Append(" />");
            }
        }

        public static void EmitDirectoryEntryClosing() {
            contentBuilder.Append("</Directory>");
        }

        public static void EmitDSXMLFooter() {
            contentBuilder.Append("</DirectoryRef>\n</Fragment>");
        }

        public static void EmitCBDXMLHeader(string name, string guid) {
            contentBuilder.Append("<!--").Append(name).Append(" directory-->");
            contentBuilder.Append("\n<Fragment>");
            contentBuilder.Append("\n<DirectoryRef Id=\"dir" + guid + "\">");
        }

        public static void EmitCBDXMLFooter() {
            contentBuilder.Append("</DirectoryRef>\n</Fragment>");
        }

        public static void EmitCGXMLHeader() {
            contentBuilder.Append("<Fragment><ComponentGroup Id=\"ProductWeb.Content\">");
        }

        public static void EmitCGComponentItemXML(string guid) {
            contentBuilder.Append("<ComponentRef Id=\"cmp")
                              .Append(guid + "\" />\n");
        }

        public static void EmitCGXMLFooter() {
            contentBuilder.Append("</ComponentGroup>\n</Fragment>");
        }

        public static void EmitXMLFooter() {
            contentBuilder.Append("</Wix>");
        }

        public static void AppendLine(string s) {
            contentBuilder.Append(s + "\n");
        }

        //******************************
        // ToString Methods
        //******************************


        public string contentToString() {
            return contentBuilder.ToString();
        }

        public string configToString() {
            return configBuilder.ToString();
        }

        public string productToString() {
            return productBuilder.ToString();
        }

        //******************************
        // Other File Emission Methods
        //******************************
        public static void EmitConfigFile(string productName) {
            configBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                         .Append("<Include>")
                         .Append("<!-- Product name as you want it to appear in Add/Remove Programs-->")
                         .Append("<?if $(var.Platform) = x64 ?>")
                         .Append("<?define ProductName = \"" + productName + " (64 bit)\" ?>")
                         .Append("<?define Win64 = \"yes\" ?>")
                         .Append("<?define PlatformProgramFilesFolder = \"ProgramFiles64Folder\" ?>")
                         .Append("<?else ?>")
                         .Append("<?define ProductName = \"" + productName + "\" ?>")
                         .Append("<?define ProductRootDir = \"$(sys.CURRENTDIR)..\\" + productName + "Web\" ?>")
                         .Append("<?define Win64 = \"no\" ?>")
                         .Append("<?define PlatformProgramFilesFolder = \"C:\\Program Files\" ?>")
                         .Append("<?endif ?>")
                         .Append("</Include>");
        }

        public static void EmitProductFile(string productName) {
            productBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
                          .Append("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\"")
                          .Append("\n")
                          .Append(" xmlns:iis=\"http://schemas.microsoft.com/wix/IIsExtension\">")
                          .Append("<?include $(sys.CURRENTDIR)\\" + productName + "?>")
                          .Append("<Product Id=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\" Name=\"$(var.ProductName)\" Language=\"1033\" Version=\"1.0.0.0\" Manufacturer=\"Pheonix Systems\" UpgradeCode=\"b6b32a5a-0a7b-492a-9c32-c4c773635fdf\">")
                          .Append("<Package InstallerVersion=\"200\" InstallPrivileges=\"elevated\" InstallScope=\"perMachine\" Platform=\"x86\" Compressed=\"yes\"  Description=\"$(var.ProductName)\" />")
                          .Append("<Media Id=\"1\" Cabinet=\"media1.cab\" EmbedCab=\"yes\" />")
                          .Append("<Directory Id=\"TARGETDIR\" Name=\"SourceDir\">")
                          .Append("<Directory Id=\"ROOT_DRIVE\" Name=\"root\">")
                          .Append("<Directory Id=\"IISMain\" Name=\"Inetpub\" >")
                          .Append("<Directory Id=\"WWWMain\" Name=\"wwwroot\" >")
                          .Append("<Directory Id=\"ProductWeb.Content\" Name=\"$(var.ProductName)\"  />")
                          .Append("<Component Id=\"$(var.ProductName)_productcomponent\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\"><iis:WebAppPool Id=\"$(var.ProductName)_webapppool\" Name=\"$(var.ProductName)_webapppool\" /><CreateFolder/></Component>")
                          .Append("<Component Id =\"$(var.ProductName)_websitecomponent\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                          .Append("<iis:WebSite Id=\"$(var.ProductName)_website\" Description=\"$(var.ProductName) Website\" Directory=\"ProductWeb.Content\" WebApplication=\"$(var.ProductName)_webapp\">")
                          .Append("<iis:WebAddress Id=\"$(var.ProductName)_webAddress\" IP=\"127.0.0.1\" Port=\"80\" />")
                          .Append("<iis:WebDirProperties Id=\"$(var.ProductName)_webdirprops\" DefaultDocuments=\"Default.aspx\" Script=\"yes\" Read=\"yes\" Write=\"yes\"/>")
                          .Append("</iis:WebSite><CreateFolder/></Component>")
                          .Append("<Component Id=\"Manual\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                          .Append("<File Id=\"Manual\" Name=\"readme.doc\" DiskId=\"1\" Source=\"readme.doc\" KeyPath=\"yes\"><Shortcut Id=\"startmenuManual\" Directory=\"ProgramMenuDir\" Name=\"Instruction Manual\" Advertise=\"yes\" /></File>")
                          .Append("</Component>")
                          .Append("<Component Id=\"WebShortcut\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                          .Append("<File Id=\"$(var.ProductName).txt\" Name=\"$(var.ProductName).url\" Vital=\"yes\" KeyPath=\"yes\" Source=\"$(var.ProductName).txt\">")
                          .Append("<Shortcut Id=\"WebShortcut\" Directory=\"$(var.ProductName)ProgramMenuDir\" IconIndex=\"0\" Hotkey=\"0\" Name=\"$(var.ProductName)\" WorkingDirectory=\"INTERNETEXPLORER\" Advertise=\"yes\" Icon=\"eStoreLogo.ico\">")
                          .Append("<Icon Id=\"eStoreLogo.ico\" SourceFile=\"$(sys.CURRENTDIR)..\\$(var.ProductName)Web\\Icons\\eStoreLogo.ico\" />")
                          .Append("</Shortcut></File></Component>")
                          .Append("</Directory></Directory></Directory>")
                          .Append("<Directory Id=\"ProgramMenuFolder\" Name=\"Programs\">")
                          .Append("<Directory Id=\"ProgramMenuDir\" Name=\"PheonixSystems\">")
                          .Append("<Component Id=\"ProgramMenuDir\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\"><RemoveFolder Id=\"ProgramMenuDir\" On=\"uninstall\" /><RegistryValue Root=\"HKCU\" Key=\"SOFTWARE\\PheonixSystems\\$(var.ProductName)\" Type=\"string\" Value=\"\" KeyPath=\"yes\" /></Component>")
                          .Append("<Directory Id=\"$(var.ProductName)ProgramMenuDir\" Name=\"eStoreAdmin\">")
                          .Append("<Component Id=\"$(var.ProductName)ProgramMenuDir\" Guid=\"" + GUIDGenerator.GenerateTimeBasedGuidString() + "\">")
                          .Append("<RemoveFolder Id=\"$(var.ProductName)ProgramMenuDir\" On=\"uninstall\"/>")
                          .Append("<RegistryValue Root=\"HKCU\" Key=\"SOFTWARE\\PheonixSystems\\$(var.ProductName)\" Type=\"string\" Value=\"\" KeyPath=\"yes\" />")
                          .Append("</Component>")
                          .Append("</Directory></Directory></Directory></Directory>")
                          .Append("<iis:WebApplication Id=\"$(var.ProductName)_webapp\" Name=\"$(var.ProductName)_site\" WebAppPool=\"$(var.ProductName)_webapppool\" />")
                          .Append("<Feature Id=\"$(var.ProductName)Complete\" Title=\"$(var.ProductName) Website\" Level=\"1\">")
                          .Append("<ComponentGroupRef Id=\"ProductWeb.Content\" />")
                          .Append("<ComponentGroupRef Id=\"$(var.ProductName)_productcomponentref\" />")
                          .Append("<ComponentRef Id=\"ProgramMenuDir\"/>")
                          .Append("<ComponentRef Id=\"$(var.ProductName)ProgramMenuDir\"/>")
                          .Append("<ComponentRef Id=\"WebShortcut\"/>")
                          .Append("<ComponentRef Id=\"Manual\"/>")
                          .Append("</Feature>")
                          .Append("<Feature Id=\"Documentation\" Title=\"Documentation\" Description=\"The user manual.\" Level=\"1000\">")
                          .Append("<ComponentRef Id=\"ProgramMenuDir\"/>")
                          .Append("<ComponentRef Id=\"Manual\" />")
                          .Append("</Feature>")
                          .Append("<ComponentGroup Id=\"$(var.ProductName)_productcomponentref\">")
                          .Append("<ComponentRef Id=\"$(var.ProductName)_productcomponent\" />")
                          .Append("<ComponentRef Id=\"$(var.ProductName)_websitecomponent\" />")
                          .Append("</ComponentGroup>")
                          .Append("<UIRef Id=\"WixUI_Mondo\" />")
                          .Append("<UIRef Id=\"WixUI_ErrorProgressText\" />")
                          .Append("<CustomAction Id=\"Assign_ROOT_DRIVE\" Property=\"ROOT_DRIVE\" Value=\"C:\\\"/>")
                          .Append("<UI>")
                          .Append("<InstallUISequence>")
                          .Append("<Custom Action=\"Assign_ROOT_DRIVE\" Before=\"CostInitialize\"></Custom>")
                          .Append("</InstallUISequence>")
                          .Append("</UI>")
                          .Append("<InstallExecuteSequence>")
                          .Append("<Custom Action=\"Assign_ROOT_DRIVE\" Before=\"CostInitialize\"></Custom>")
                          .Append("</InstallExecuteSequence>")
                          .Append("</Product>")
                          .Append("</Wix>");
        }
    }
}