using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Viewer.Reader;
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
            if (view == null) return;

            IWriter<string> jsonWriter = new JsonWriter(s_formatProvider);
            string json = jsonWriter.WriteShapes(view.Shapes);

            Clipboard.SetText(json);
        }

        public static void SaveXml(View view)
        {
            if (view == null) return;

            IWriter<XElement> xmlWriter = new XmlWriter(s_formatProvider);
            XElement xml = xmlWriter.WriteShapes(view.Shapes);
            xml.Save("c:/backup/dada.xml");
        }

        public static void SavePdf(View view)
        {
            string filename = "c:/backup/test.pdf";
            IWriter<PdfWriter> pdfWriter = new PdfWriter(
                filename, 
                new Size(300d, 300d), 
                view.Bounds());

            pdfWriter.WriteShapes(view.Shapes).Close();
        }

        public static void SaveImage(View view)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(300, 300, 96, 96, PixelFormats.Pbgra32);
            foreach (Shape shape in view.Shapes)
            {
                rtb.Render(shape);
            }

            var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, 1000, 1000));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            using (var fs = System.IO.File.OpenWrite("c:/backup/logo.png"))
            {
                pngEncoder.Save(fs);
            }
        }

        public static bool CanExecuteSaveJson(View view)
        {
            return view != null && view.Shapes.Length > 0;
        }

        public static bool CanExecuteSaveXml(View view)
        {
            return view != null && view.Shapes.Length > 0;
        }

        public static bool CanExecuteSavePdf(View view)
        {
            return view != null && view.Shapes.Length > 0;
        }
    }
}