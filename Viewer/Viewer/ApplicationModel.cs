using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var path = "C:/backup/json.txt";

            JsonSerializer serializer = new JsonSerializer();


            var json = new StreamReader(path).ReadToEnd();

            JArray o = JsonConvert.DeserializeObject<JArray>(json);
            foreach (JToken token in o)
            {
                
            }

            var oo = JsonConvert.DeserializeObject<IList<ShapeDefinition>>(json, new ShapeConverter());

            var g = JsonConvert.SerializeObject(oo);
            //using (StreamWriter sw = new StreamWriter(path))
            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    serializer.Serialize(writer, shapes);
            //    // {"ExpiryDate":new Date(1230375600000),"Price":0}
            //}
        }

        private void Load()
        {
            var view = new View();

            Random r = new Random();
            int count = 0;
            while (count < 10)
            {
                var x1 = r.NextDouble() * 1000;
                var y1 = r.NextDouble() * 1000;
                var x2 = r.NextDouble() * 1000;
                var y2 = r.NextDouble() * 1000;

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


            //using (var sr = new StreamReader("C:/lines2.csv"))
            //{
            //    int counter = 0;

            //    while (true)
            //    {
            //        string line = sr.ReadLine();

            //        if (line == null)
            //            break;

            //        if (counter > 1000)
            //            break;

            //        line = line.Trim();

            //        counter++;

            //        string[] tokens = line.Split(',');

            //        double x1 = double.Parse(tokens[1]);
            //        double y1 = double.Parse(tokens[2]);
            //        double x2 = double.Parse(tokens[3]);
            //        double y2 = double.Parse(tokens[4]);

            //        Pen pen = new Pen(Brushes.Red, double.Parse(tokens[8]));
            //        pen.Freeze();

            //        view.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));

            //        //var line2D = new Line2D(x1, y1, x2, y2);
            //        //var uiLine = new UiLine(line2D)
            //        //{
            //        //    Color = new Color
            //        //    {
            //        //        R = byte.Parse(tokens[5]),
            //        //        G = byte.Parse(tokens[6]),
            //        //        B = byte.Parse(tokens[7])
            //        //    },

            //        //    StrokeWidth = float.Parse(tokens[8])
            //        //};

            //        //uiLayer.AddVisual(uiLine);
            //    }
            //}


            //View.DrawLine(pen, new Point(0, 0), new Point(1000, 300) );
            //View.DrawCircle(Brushes.Blue, pen, new Point(300, 300), 10);
            //View.DrawTriangle(Brushes.GreenYellow, pen, new Point(0, 0), new Point(100, 100), new Point(50, 100));

            View = view;
        }
    }
}