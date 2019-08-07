using System;
using System.Windows;
using System.Windows.Media;
using Viewer.Graphics;
using ITextColor = iText.Kernel.Colors;
using ITextGeom = iText.Kernel.Geom;
using ITextPdf = iText.Kernel.Pdf;
using ITextCanvas = iText.Kernel.Pdf.Canvas;


namespace Viewer.Writer
{
    public sealed class PdfWriter : IWriter<PdfWriter>, IDisposable
    {
        private readonly ITextCanvas.PdfCanvas m_canvas;
        private readonly ITextPdf.PdfDocument m_pdfDocument;

        private readonly double m_scale;
        private readonly double m_dx;
        private readonly double m_dy;

        public PdfWriter(string fileName, Size pageSize, Rect drawingBounds)
        {
            double ymax = drawingBounds.Bottom;
            double ymin = drawingBounds.Top;
            double xmax = drawingBounds.Right;
            double xmin = drawingBounds.Left;

            m_scale = Math.Min(pageSize.Width / (xmax - xmin), pageSize.Height / (ymax - ymin));
            m_dx = -xmin;
            m_dy = -ymin;

            m_pdfDocument = new ITextPdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(fileName));
            m_canvas = new ITextCanvas.PdfCanvas(m_pdfDocument.AddNewPage(new ITextGeom.PageSize((float) pageSize.Width, (float) pageSize.Height)));
        }

        public PdfWriter WriteLine(Point startPoint, Point endPoint, Color color, DashStyle dashStyle)
        {
            SetDashStyle(dashStyle);
            SetColor(color, false);
            WriteLineGeometry(startPoint, endPoint);

            return this;
        }

        public PdfWriter WriteCircle(Point center, double radius, Color color, DashStyle dashStyle, bool filled)
        {
            SetDashStyle(dashStyle);
            SetColor(color, filled);
            WriteCircleGeometry(center, radius, filled);

            return this;
        }

        public PdfWriter WriteTriangle(Point firstCorner, Point secondCorner, Point thirdCorner, Color color, DashStyle dashStyle, bool filled)
        {
            SetDashStyle(dashStyle);
            SetColor(color, filled);
            WriteTriangleGeometry(firstCorner, secondCorner, thirdCorner, filled);

            return this;
        }

        public PdfWriter WriteRectangle(Point firstCorner, Point secondCorner, Color color, DashStyle dashStyle, bool filled)
        {
            SetDashStyle(dashStyle);
            SetColor(color, filled);
            WriteRectangleGeometry(firstCorner, secondCorner, filled);

            return this;
        }

        public void WriteShapes(Shape[] shapes)
        {
            if (shapes == null)
                throw new ArgumentNullException(nameof(shapes));

            foreach (Shape shape in shapes) shape.Write(this);

            m_pdfDocument.Close();
            Dispose();
        }

        private void WriteLineGeometry(Point a, Point b)
        {
            m_canvas
                .MoveTo(Tx(a.X), Ty(a.Y))
                .LineTo(Tx(b.X), Ty(b.Y))
                .ClosePathStroke();
        }

        private void WriteCircleGeometry(Point center, double radius, bool filled)
        {
            m_canvas.Circle(Tx(center.X), Ty(center.Y), radius * m_scale);

            if (filled)
                m_canvas.ClosePathFillStroke();
            else
                m_canvas.ClosePathStroke();
        }

        private void WriteTriangleGeometry(Point a, Point b, Point c, bool filled)
        {
            m_canvas
                .MoveTo(Tx(a.X), Ty(a.Y))
                .LineTo(Tx(b.X), Ty(b.Y))
                .LineTo(Tx(c.X), Ty(c.Y))
                .LineTo(Tx(a.X), Ty(a.Y));

            if (filled)
                m_canvas.ClosePathFillStroke();
            else
                m_canvas.ClosePathStroke();
        }

        private void WriteRectangleGeometry(Point a, Point b, bool filled)
        {
            var c = new Point(b.X, a.Y);
            var d = new Point(a.X , b.Y);

            m_canvas
                .MoveTo(Tx(a.X), Ty(a.Y))
                .LineTo(Tx(c.X), Ty(c.Y))
                .LineTo(Tx(b.X), Ty(b.Y))
                .LineTo(Tx(d.X), Ty(d.Y))
                .LineTo(Tx(a.X), Ty(a.Y));

            if (filled)
                m_canvas.ClosePathFillStroke();
            else
                m_canvas.ClosePathStroke();
        }

        private void SetColor(Color color, bool filled)
        {
            m_canvas.SetStrokeColor(new ITextColor.DeviceRgb(color.R, color.G, color.B));

            if (filled) m_canvas.SetFillColor(new ITextColor.DeviceRgb(color.R, color.G, color.B));
        }

        private void SetDashStyle(DashStyle dashStyle)
        {
            float[] floats = new float[dashStyle.Dashes.Count];
            for (int i = 0; i < dashStyle.Dashes.Count; i++) floats[i] = (float) dashStyle.Dashes[i];

            m_canvas.SetLineDash(floats, 0f);
        }

        private double Tx(double x)
        {
            return (x + m_dx) * m_scale;
        }

        private double Ty(double y)
        {
            return (y + m_dy) * m_scale;
        }

        public void Dispose()
        {
            ((IDisposable) m_pdfDocument)?.Dispose();
        }
    }
}