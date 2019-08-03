using System;
using System.Windows.Media;
using System.Xml.Linq;

namespace Viewer
{
    public abstract class Shape : DrawingVisual
    {
        private bool m_isDirty = true;

        /// <summary>
        /// Caches the pen to improve performance.
        /// </summary>
        private Pen m_pen;

        private double m_thickness = 1d;
        private Color m_color = Colors.White;
        private DashStyle m_lineStyle = DashStyles.Solid;

        public Geometry Geometry { get; protected set; }

        public DashStyle LineStyle
        {
            get => m_lineStyle;
            set
            {
                m_lineStyle = value;
                m_isDirty = true;
            }
        }

        public Color Color
        {
            get => m_color;
            set
            {
                m_color = value;
                m_isDirty = true;
            }
        }

        public double Thickness
        {
            get => m_thickness;
            set
            {
                m_thickness = value;
                m_isDirty = true;
            }
        }

        public void InvalidateVisual()
        {
            if (!m_isDirty) return;

            using (DrawingContext drawingContext = RenderOpen()) Render(drawingContext);

            m_isDirty = false;
        }

        protected abstract void Render(DrawingContext drawingContext);

        protected Pen Pen()
        {
            if (m_isDirty || m_pen == null)
                m_pen = new Pen(Brush(), m_thickness) {DashStyle = DashStyle()}.AsFrozen();

            return m_pen;
        }

        protected Brush Brush()
        {
            return new SolidColorBrush(m_color).AsFrozen();
        }

        private DashStyle DashStyle()
        {
            return m_lineStyle.AsFrozen();
        }

        public override string ToString()
        {
            return $"Type: \t\t {GetType().Name} \n" +
                   $"Color: \t\t {m_color} \n" +
                   $"LineType: \t\t {m_lineStyle.AsString().ToUpper()} \n" +
                   Geometry;
        }

        public string ToJson(IFormatProvider provider)
        {
            return $"{{\n" +
                   $"{ToJsonInternal(provider)},\n" +
                   $"\"color\": \"{m_color.A}; {m_color.R}; {m_color.G}; {m_color.B}\",\n" +
                   $"\"lineType\": \"{m_lineStyle.AsString()}\"\n" +
                   $"}}";
        }

        public XElement ToXml(IFormatProvider provider)
        {
            return new XElement(
                "shape",
                ToXmlInternal(provider),
                new XElement("color", $"{m_color}"),
                new XElement("lineType", $"{m_lineStyle.AsString()}"));
        }

        protected abstract string ToJsonInternal(IFormatProvider provider);
        protected abstract XElement[] ToXmlInternal(IFormatProvider provider);
    }
}