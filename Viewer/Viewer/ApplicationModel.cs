using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Viewer
{
    public sealed class ApplicationModel : ObservableObject
    {
        private View m_view;
        public View View
        {
            get => m_view;
            set => UpdateAndNotify(out m_view, value);
        }

        public ApplicationModel()
        {
            LoadCommand = new Command(p => Load(), p => true);
            ClearCommand = new Command(p => Clear(), p => true);
            SaveJSONCommand = new Command(p => Save(), p => true);
        }

        public ICommand LoadCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand SaveJSONCommand { get; }

        private void Clear()
        {
            View = new View();
        }

        private void Save()
        {
            string path = "C:/backup/json.txt";

            string jsonString = File.ReadAllText(path);

            IList<ShapeDefinition> oo = JsonConvert.DeserializeObject<IList<ShapeDefinition>>(jsonString, new ShapeConverter());

            IEnumerable<Shape> shapes = oo.Select(o => o.Convert());

            var view = new View();
            foreach (Shape shape in shapes)
            {
                view.AddShape(shape);
            }

            View = view;


        }

        private void Load()
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

            View = view;
        }
    }
}