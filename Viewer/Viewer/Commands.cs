using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Win32;
using Viewer.Dialogs;
using Viewer.Graphics;
using Viewer.Reader;
using Viewer.Writer;

namespace Viewer
{
    public static class Commands
    {
        private static readonly IFormatProvider SFormatProvider = new CultureInfo("de");

        public static View Clear()
        {
            return new View();
        }

        public static View LoadXml(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                Title = "Open XML file"
            };

            var result = dialog.ShowDialog();

            if (result == null || result != true) return view;

            try
            {
                ShapeReader xmlReader = new XmlReader(SFormatProvider);
                return !File.Exists(dialog.FileName)
                    ? new View()
                    : new View(xmlReader.Read(dialog.FileName));
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while loading the file.");
                Console.WriteLine(e);
                return new View();
            }
        }

        public static View LoadJson(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Open JSON file"
            };

            var result = dialog.ShowDialog();

            if (result == null || result != true) return view;

            try
            {
                ShapeReader jsonReader = new JsonReader(SFormatProvider);
                return !File.Exists(dialog.FileName)
                    ? new View()
                    : new View(jsonReader.Read(dialog.FileName));
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while loading the file.");
                Console.WriteLine(e);
                return new View();
            }
        }

        public static View RandomShapes(int numberOfShapes, double width, double height)
        {
            RandomShapeGenerator generator = new RandomShapeGenerator(width , height);
            return new View(generator.Generate(numberOfShapes));
        }

        public static void SaveJson(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON File (*.json)|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            var result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            try
            {
                JsonWriter jsonWriter = new JsonWriter(SFormatProvider);
                string json = jsonWriter.WriteShapes(view.Shapes());

                File.WriteAllText(saveFileDialog.FileName, json);
                MessageBox.Show("The file was saved successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while saving the file.");
                Console.WriteLine(e);
            }
        }

        public static void SaveXml(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML File (*.xml)|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            var result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            try
            {
                var xmlWriter = new XmlWriter(SFormatProvider);
                XElement xml = xmlWriter.WriteShapes(view.Shapes());
                xml.Save(saveFileDialog.FileName);
                MessageBox.Show("The file was saved successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while saving the file.");
                Console.WriteLine(e);
            }
        }

        public static void SavePdf(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            PdfExportDialog dialog = new PdfExportDialog();
            dialog.ShowDialog();

            if (dialog.DialogResult != true) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF File (*.pdf)|*.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            var result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            PdfWriter pdfWriter = null;
            try
            {
                PdfExportModel exportModel = (PdfExportModel) dialog.DataContext;

                pdfWriter = new PdfWriter(
                    saveFileDialog.FileName,
                    new Size(exportModel.PdfPageWidth, exportModel.PdfPageHeight),
                    view.Bounds());

                pdfWriter.WriteShapes(view.Shapes()).Close();
                MessageBox.Show("The file was saved successfully.");
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while saving the file.");
                Console.WriteLine(e);
            }
            finally
            {
                pdfWriter?.Dispose();
            }
        }

        public static bool CanExecuteSaveJson(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return view.Shapes().Length > 0;
        }

        public static bool CanExecuteSaveXml(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return view.Shapes().Length > 0;
        }

        public static bool CanExecuteSavePdf(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return view.Shapes().Length > 0;
        }
    }
}