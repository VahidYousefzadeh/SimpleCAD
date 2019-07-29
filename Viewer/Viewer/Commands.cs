using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

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

            if (!File.Exists(dialog.FileName)) return new View();

            IEnumerable<Shape> shapes = XmlToShapeDefinitionParser
                .Parse(dialog.FileName)
                .Select(o => o.Convert());

            return new View(shapes);
        }

        public static View LoadJson()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = @"JSON files (*.json)|*.json",
                Title = @"Open JSON file"
            };

            dialog.ShowDialog();

            if (!File.Exists(dialog.FileName)) return new View();

            IEnumerable<Shape> shapes = JsonToShapeDefinitionParser
                .Parse(dialog.FileName)
                .Select(o => o.Convert());

            return new View(shapes);
        }

        public static View RandomShapes()
        {
            var view = new View();

            Random r = new Random();
            int count = 0;
            while (count < 100)
            {
                double x1 = r.NextDouble() * 1000;
                double y1 = r.NextDouble() * 1000;
                double x2 = r.NextDouble() * 1000;
                double y2 = r.NextDouble() * 1000;

                Pen pen = new Pen(Brushes.Red, r.NextDouble() * 10);
                pen.Freeze();

                var line = new Line(pen, new Point(x1, y1), new Point(x2, y2));
                //view.DrawCircle(null, pen, new Point(r.NextDouble() * 1000, r.NextDouble() * 1000), r.NextDouble() * 100);


                var a = new Point(r.NextDouble() * 1000, r.NextDouble() * 1000);
                var b = new Point(r.NextDouble() * 1000, r.NextDouble() * 1000);
                var c = new Point(r.NextDouble() * 1000, r.NextDouble() * 1000);

                Polygon triangle = Polygon.Triangle(null, pen, a, b, c);

                view.AddShape(line);
                view.AddShape(triangle);

                count++;
            }

            return view;
        }
    }
}