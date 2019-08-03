using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class RandomShapeGenerator
    {
        private readonly double m_screenWidth;
        private readonly double m_screenHeight;
        private readonly Random m_random;

        public RandomShapeGenerator(double screenWidth, double screenHeight)
        {
            m_random= new Random((int)DateTime.Now.Ticks);
            m_screenWidth = screenWidth;
            m_screenHeight = screenHeight;
        }

        public IEnumerable<Shape> Generate(int numberOfShapes)
        {
            for (int i = 0; i < numberOfShapes; i++)
            {
                int type = m_random.Next(0, 2);

                if (type == 0)
                    yield return RandomLine();

                if (type == 1)
                    yield return RandomCircle();
            }
        }

        private Shape RandomLine()
        {
            double x1 = RandomDouble(0d, m_screenWidth);
            double x2 = RandomDouble(0d, m_screenWidth);
            double y1 = RandomDouble(0d, m_screenHeight);
            double y2 = RandomDouble(0d, m_screenHeight);

            return WithStyle(new Line(new Point(x1, y1), new Point(x2, y2)));
        }

        private Shape RandomCircle()
        {
            double x = RandomDouble(0d, m_screenWidth);
            double y = RandomDouble(0d, m_screenHeight);
            double radius = RandomDouble(10d, 100d);

            return WithStyle(new Circle(new Point(x, y), radius, RandomBoolean()));
        }

        private Shape WithStyle(Shape shape)
        {
            shape.LineStyle = RandomDashStyle().AsFrozen();
            shape.Color = RandomColor();

            return shape;
        }

        public double RandomDouble(double minimum, double maximum)
        {
            return m_random.NextDouble() * (maximum - minimum) + minimum;
        }


        private bool RandomBoolean()
        {
            return m_random.Next(0, 2) != 0;
        }

        private Color RandomColor()
        {
            return Color.FromArgb(
                (byte)m_random.Next(130, 256),
                (byte)m_random.Next(130, 256),
                (byte)m_random.Next(130, 256),
                (byte)m_random.Next(130, 256));
        }

        private DashStyle RandomDashStyle()
        {
            switch (m_random.Next(0, 4))
            {
                case 0:
                    return DashStyles.DashDot;
                case 1:
                    return DashStyles.Dash;
                case 2:
                    return DashStyles.Dot;
                case 3:
                    return DashStyles.Solid;
                default:
                    return DashStyles.Solid;
            }
        }
    }
}