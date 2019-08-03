using System.Windows.Media;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace Viewer.Writer
{
    public sealed class PdfWriter : IWriter<PdfWriter>
    {
        private readonly PdfCanvas m_canvas;
        private readonly PdfDocument m_pdfDocument;
        public PdfWriter(string filename, double width, double height)
        {
            m_pdfDocument = new PdfDocument(new iText.Kernel.Pdf.PdfWriter(filename));
            m_canvas = new PdfCanvas(m_pdfDocument.AddNewPage(new PageSize((float) width, (float) height)));
        }

        public PdfWriter WriteLine(Point a, Point b, Color color, DashStyle dashStyle)
        {
            SetColor(color);
            WriteLineGeometry(a, b);

            return this;
        }

        public PdfWriter WriteCircle(Point center, double radius, Color color, DashStyle dashStyle, bool filled)
        {
            return this;
        }

        public PdfWriter WriteTriangle(Point a, Point b, Point c, Color color, DashStyle dashStyle, bool filled)
        {
            return this;
        }

        public PdfWriter WriteView(View view)
        {
            foreach (Shape shape in view.Shapes) shape.Write(this);

            return this;
        }

        public void Close()
        {
            m_pdfDocument.Close();
        }

        private void WriteLineGeometry(Point a, Point b)
        {
            m_canvas.MoveTo(a.X, a.Y).LineTo(b.X, b.Y).ClosePathStroke();
        }

        private void SetColor(Color color)
        {
            m_canvas.SetStrokeColor(new DeviceRgb(color.R, color.G, color.B));
        }
    }
}