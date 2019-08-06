using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using Viewer.Dialogs;
using Viewer.Reader;
using Viewer.Writer;
using View = Viewer.Graphics.View;


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

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON File (*.json)|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            IWriter<string> jsonWriter = new JsonWriter(s_formatProvider);
            string json = jsonWriter.WriteShapes(view.Shapes);

            File.WriteAllText(saveFileDialog.FileName, json);
        }

        public static void SaveXml(View view)
        {
            if (view == null) return;

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "XML File (*.xml)|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            IWriter<XElement> xmlWriter = new XmlWriter(s_formatProvider);
            XElement xml = xmlWriter.WriteShapes(view.Shapes);
            xml.Save(saveFileDialog.FileName);
        }

        public static void SavePdf(View view)
        {
            var dialog = new PdfExportDialog();
            dialog.ShowDialog();

            if (dialog.DialogResult != true) return;

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF File (*.pdf)|*.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            var exportModel = (PdfExportModel)dialog.DataContext;

            IWriter<PdfWriter> pdfWriter = new PdfWriter(
                saveFileDialog.FileName,
                new Size(exportModel.PdfPageWidth, exportModel.PdfPageHeight),
                view.Bounds());

            pdfWriter.WriteShapes(view.Shapes).Close();
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