using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class CrossSymbols : DrawingVisual
    {
        private const double Size = 7d;

        private static readonly Vector s_p = new Vector(+0.25 * Math.PI, +0.25 * Math.PI) * Size;
        private static readonly Vector s_q = new Vector(+0.25 * Math.PI, -0.25 * Math.PI) * Size;

        public CrossSymbols(Pen pen, IEnumerable<Point> locations)
        {
            if (pen.CanFreeze)
                pen.Freeze();

            using (DrawingContext drawingContext = RenderOpen())
            {
                foreach (Point location in locations)
                {
                    drawingContext.DrawLine(pen, location + s_p, location - s_p);
                    drawingContext.DrawLine(pen, location + s_q, location - s_q);
                }
            }
        }
    }
}