using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using Viewer.Writer;
using Clipboard = System.Windows.Forms.Clipboard;

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
            var dialog = new OpenFileDialog()
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
            var dialog = new OpenFileDialog()
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
            Random random = new Random((int)DateTime.Now.Ticks);
            var generator = new RandomShapeGenerator(random, width , height);

            IList<Shape> shapes = new List<Shape>();
            for (int i = 0; i < numberOfShapes; i++)
            {
                shapes.Add(generator.Generate());
            }

            return new View(shapes);
        }

        public static void SaveJson(View view)
        {
            string json = view.Write(new JsonWriter(s_formatProvider));
            Clipboard.SetText(json);
        }

        public static void SaveXml(View view)
        {
            XElement xml = view.Write(new XmlWriter(s_formatProvider));
        }

        public static void SavePdf(View view)
        {
            var dest = "c:/backup/test.pdf";


            //PdfWriter writer = new PdfWriter("c:/backup/test.pdf");
            //PdfDocument pdf = new PdfDocument(writer);
            //pdf.SetDefaultPageSize(new PageSize(1000, 1000));

            //PdfCanvas c = new PdfCanvas(pdf.AddNewPage());

            //foreach (Shape shape in view.Shapes)
            //{
            //    if (shape is Line line)
            //    {
            //        line.WritePdf(c);
            //    }
            //}

            //pdf.Close();

            //PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            //PdfCanvas canvas = new PdfCanvas(pdfDoc.AddNewPage());
            //Color magentaColor = new DeviceCmyk(0f, 1f, 0f, 0f);
            //canvas.SetStrokeColor(magentaColor)
            //    .MoveTo(36, 36)
            //    .LineTo(36, 806)
            //    .LineTo(559, 36)
            //    .LineTo(559, 806)
            //    .ClosePathStroke();
            //pdfDoc.Close();
        }


    }
}