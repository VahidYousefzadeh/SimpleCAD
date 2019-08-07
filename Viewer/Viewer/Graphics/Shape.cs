using System.Windows.Media;

namespace Viewer.Graphics
{
    public abstract class Shape : DrawingVisual
    {
        protected bool IsDirty { get; set; } = true;

        /// <summary>
        /// Caches the pen to improve performance.
        /// </summary>
        private Pen m_pen;

        private double m_thickness = 1d;
        private Color m_color = Colors.White;
        private DashStyle m_lineStyle = DashStyles.Solid;

        public Geometry.Geometry Geometry { get; protected set; }

        public DashStyle LineStyle
        {
            get => m_lineStyle;
            set
            {
                m_lineStyle = value;
                IsDirty = true;
            }
        }

        public Color Color
        {
            get => m_color;
            set
            {
                m_color = value;
                IsDirty = true;
            }
        }

        public double Thickness
        {
            get => m_thickness;
            set
            {
                m_thickness = value;
                IsDirty = true;
            }
        }

        public void InvalidateVisual()
        {
            if (!IsDirty) return;

            using (DrawingContext drawingContext = RenderOpen()) Render(drawingContext);

            IsDirty = false;
        }

        protected abstract void Render(DrawingContext drawingContext);

        protected Pen Pen()
        {
            if (IsDirty || m_pen == null)
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

        public abstract T Write<T>(IWriter<T> writer);
    }
}