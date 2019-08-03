using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Colorspace;
using iText.Layout.Properties;
using Viewer.Writer;
using Clipboard = System.Windows.Forms.Clipboard;
using PdfWriter = Viewer.Writer.PdfWriter;

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

            return !File.Exists(dialog.FileName) 
                ? new View() 
                : new View(XmlToShapeConverter.Parse(dialog.FileName));
        }

        public static View LoadJson()
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"JSON files (*.json)|*.json",
                Title = @"Open JSON file"
            };

            dialog.ShowDialog();

            return !File.Exists(dialog.FileName) 
                ? new View() 
                : new View(JsonToShapeConverter.Parse(dialog.FileName));
        }

        public static View RandomShapes(int numberOfShapes, double width, double height)
        {
            var generator = new RandomShapeGenerator(width , height);
            return new View(generator.Generate(numberOfShapes));
        }

        public static void SaveJson(View view)
        {
            IWriter<string> jsonWriter = new JsonWriter(s_formatProvider);
            string json = jsonWriter.WriteView(view);

            Clipboard.SetText(json);
        }

        public static void SaveXml(View view)
        {
            IWriter<XElement> xmlWriter = new XmlWriter(s_formatProvider);
            XElement xml = xmlWriter.WriteView(view);
        }

        public static void SavePdf(View view)
        {
            string filename = "c:/backup/test.pdf";
            IWriter<PdfWriter> pdfWriter = new PdfWriter(filename, 1000, 1000);
            pdfWriter.WriteView(view).Close();
        }


    }
}