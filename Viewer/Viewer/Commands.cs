using System;
using System.Globalization;
using System.IO;
using System.Windows;
using Microsoft.Win32;
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
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            var dialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                Title = "Open XML file"
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
                MessageBox.Show("An error occurred while loading the file.");
                Console.WriteLine(e);
                return new View();
            }
        }

        public static View LoadJson(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Open JSON file"
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
                MessageBox.Show("An error occurred while loading the file.");
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
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON File (*.json)|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            try
            {
                var jsonWriter = new JsonWriter(s_formatProvider);
                jsonWriter.WriteShapes(saveFileDialog.FileName, view.Shapes());

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

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "XML File (*.xml)|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            try
            {
                var xmlWriter = new XmlWriter(s_formatProvider);
                xmlWriter.WriteShapes(saveFileDialog.FileName, view.Shapes());

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

            var dialog = new PdfExportDialog();
            dialog.ShowDialog();

            if (dialog.DialogResult != true) return;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF File (*.pdf)|*.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == null || result != true) return;

            PdfWriter pdfWriter = null;
            try
            {
                var exportModel = (PdfExportModel) dialog.DataContext;

                pdfWriter = new PdfWriter(
                    saveFileDialog.FileName,
                    new Size(exportModel.PdfPageWidth, exportModel.PdfPageHeight),
                    view.Bounds());

                pdfWriter.WriteShapes(view.Shapes());
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