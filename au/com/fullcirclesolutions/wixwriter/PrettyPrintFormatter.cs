using System.IO;
using System.Xml;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    class PrettyPrintFormatter {
        //[STAThread]
        public static string Format(string s) {
            var stringWriter = new StringWriter();
            var doc = new XmlDocument();
            
            //get your document
            doc.LoadXml(s);
            
            //create reader and writer
            var xmlReader = new XmlNodeReader(doc);
            var xmlWriter = new XmlTextWriter(stringWriter) {Formatting = Formatting.Indented, Indentation = 2, IndentChar = ' '};
            
            //set formatting options

            //write the document formatted
            xmlWriter.WriteNode(xmlReader, true);
            return stringWriter.ToString();
        }
    }
}