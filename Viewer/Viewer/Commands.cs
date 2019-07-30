using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Viewer
{
    public static class Commands
    {
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

        public static View RandomShapes()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            var generator = new RandomShapeGenerator(random, 1000d , 1000d);

            IList<Shape> shapes = new List<Shape>();
            for (int i = 0; i < 20; i++)
            {
                shapes.Add(generator.Generate());
            }

            return new View(shapes);
        }

        public static void SaveJson(View view)
        {
            string json = ShapeToJsonConverter.Convert(view.Shapes);
            Clipboard.SetText(json);
        }
    }
}