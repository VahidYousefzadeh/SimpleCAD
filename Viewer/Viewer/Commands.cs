using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using Viewer.Dialogs;
using Viewer.Graphics;
using Viewer.Reader;
using Viewer.Writer;

namespace Viewer
{
    public static class Commands
    {
        private static readonly IFormatProvider s_formatProvider = new CultureInfo("de");

        public static View Clear()
        {
            return new View();
        }

        public static View LoadXml(View view)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = @"XML files (*.xml)|*.xml",
                Title = @"Open XML file"
            };

            bool? result = dialog.ShowDialog();

            if (result == null || result != true) return view;

            try
            {
                ShapeReader xmlReader = new XmlReader(s_formatProvider);
                return !File.Exists(dialog.FileName)
                    ? new View()
                    : new View(xmlReader.Read(dialog.FileName));
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error occurred while loading the file.");
                Console.WriteLine(e);
                return new View();
            }
        }

        public static View LoadJson(View view)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = @"JSON files (*.json)|*.json",
                Title = @"Open JSON file"
            };

            bool? result = dialog.ShowDialog();

            if (result == null || result != true) return view;

            try
            {
                ShapeReader jsonReader = new JsonReader(s_formatProvider);
                return !File.Exists(dialog.FileName)
                    ? new View()
                    : new View(jsonReader.Read(dialog.FileName));
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error occurred while loading the file.");
                Console.WriteLine(e);
                return new View();
            }
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

            try
            {
                IWriter<string> jsonWriter = new JsonWriter(s_formatProvider);
                string json = jsonWriter.WriteShapes(view.Shapes);

                File.WriteAllText(saveFileDialog.FileName, json);
                MessageBox.Show(@"The file was saved successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error occurred while saving the file.");
                Console.WriteLine(e);
            }
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

            try
            {
                IWriter<XElement> xmlWriter = new XmlWriter(s_formatProvider);
                XElement xml = xmlWriter.WriteShapes(view.Shapes);
                xml.Save(saveFileDialog.FileName);
                MessageBox.Show(@"The file was saved successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error occurred while saving the file.");
                Console.WriteLine(e);
            }
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

            try
            {
                var exportModel = (PdfExportModel)dialog.DataContext;

                IWriter<PdfWriter> pdfWriter = new PdfWriter(
                    saveFileDialog.FileName,
                    new Size(exportModel.PdfPageWidth, exportModel.PdfPageHeight),
                    view.Bounds());

                pdfWriter.WriteShapes(view.Shapes).Close();
                MessageBox.Show(@"The file was saved successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error occurred while saving the file.");
                Console.WriteLine(e);
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