using System;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    internal sealed class Marker : DrawingVisual
    {
        private const double Size = 7d;

        private static readonly Vector s_p = new Vector(+0.25 * Math.PI, +0.25 * Math.PI) * Size;
        private static readonly Vector s_q = new Vector(+0.25 * Math.PI, -0.25 * Math.PI) * Size;

        public Marker(Pen pen, Point location)
        {
            using (DrawingContext drawingContext = RenderOpen())
            {
                drawingContext.DrawLine(pen, location + s_p, location - s_p);
                drawingContext.DrawLine(pen, location + s_q, location - s_q);
            }
        }
    }
}