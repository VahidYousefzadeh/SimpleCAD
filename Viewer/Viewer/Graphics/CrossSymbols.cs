using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Viewer.Graphics
{
    public sealed class CrossSymbols : DrawingVisual
    {
        private const double Size = 5d;
        public CrossSymbols(double scale, IEnumerable<Point> locations)
        {
            Pen pen = Pen(scale).AsFrozen();

            Vector p = new Vector(+0.25 * Math.PI, +0.25 * Math.PI) * Size / scale;
            Vector q = new Vector(+0.25 * Math.PI, -0.25 * Math.PI) * Size / scale;

            using (DrawingContext drawingContext = RenderOpen())
            {
                foreach (Point location in locations)
                {
                    drawingContext.DrawLine(pen, location + p, location - p);
                    drawingContext.DrawLine(pen, location + q, location - q);
                }
            }
        }

        private static Pen Pen(double scale)
        {
            return new Pen(Brushes.Gold, 3d / scale);
        }
    }
}