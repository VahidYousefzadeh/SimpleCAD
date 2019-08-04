using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Viewer.Reader;
using Viewer.Writer;


namespace Viewer
{
    public static class Commands
    {
        private readonly static IFormatProvider s_formatProvider = new CultureInfo("de");

        public static View Clear()
        {
            return new View();
        }

        public static View LoadXml()
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"XML files (*.xml)|*.xml",
                Title = @"Open XML file"
            };

            dialog.ShowDialog();


            ShapeReader xmlReader = new XmlReader(s_formatProvider);
            return !File.Exists(dialog.FileName) 
                ? new View() 
                : new View(xmlReader.Read(dialog.FileName));
        }

        public static View LoadJson()
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"JSON files (*.json)|*.json",
                Title = @"Open JSON file"
            };

            dialog.ShowDialog();

            ShapeReader jsonReader = new JsonReader(s_formatProvider);
            return !File.Exists(dialog.FileName) 
                ? new View() 
                : new View(jsonReader.Read(dialog.FileName));
        }

        public static View RandomShapes(int numberOfShapes, double width, double height)
        {
            var generator = new RandomShapeGenerator(width , height);
            return new View(generator.Generate(numberOfShapes));
        }

        public static void SaveJson(View view)
        {
            IWriter<string> jsonWriter = new JsonWriter(s_formatProvider);
            string json = jsonWriter.WriteShapes(view.Shapes);

            Clipboard.SetText(json);
        }

        public static void SaveXml(View view)
        {
            IWriter<XElement> xmlWriter = new XmlWriter(s_formatProvider);
            XElement xml = xmlWriter.WriteShapes(view.Shapes);
            xml.Save("c:/backup/dada.xml");
        }

        public static void SavePdf(View view)
        {
            string filename = "c:/backup/test.pdf";
            IWriter<PdfWriter> pdfWriter = new PdfWriter(filename, 1000, 1000);
            pdfWriter.WriteShapes(view.Shapes).Close();
        }
    }
}