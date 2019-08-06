using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Viewer.Graphics;

namespace Viewer
{
    public sealed class RandomShapeGenerator
    {
        private readonly double m_screenWidth;
        private readonly double m_screenHeight;
        private readonly double m_seed;
        private readonly Random m_random;

        public RandomShapeGenerator(double screenWidth, double screenHeight)
        {
            m_random= new Random((int)DateTime.Now.Ticks);
            m_screenWidth = screenWidth;
            m_screenHeight = screenHeight;
            m_seed = 0.04 * Math.Min(m_screenWidth, m_screenHeight);
        }

        public IEnumerable<Shape> Generate(int numberOfShapes)
        {
            for (int i = 0; i < numberOfShapes; i++)
            {
                int type = m_random.Next(0, 4);

                if (type == 0)
                    yield return RandomLine();

                if (type == 1)
                    yield return RandomCircle();

                if (type == 2)
                    yield return RandomTriangle();

                if (type == 3)
                    yield return RandomRectangle();
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
            double minimumRadius = m_seed;
            double maximumRadius = 2 * m_seed;

            double radius = RandomDouble(minimumRadius, maximumRadius);
            double x = RandomDouble(radius, m_screenWidth - radius);
            double y = RandomDouble(radius, m_screenHeight - radius);

            return WithStyle(
                new Circle(new Point(x, y), radius)
                {
                    Filled = RandomBoolean()
                });
        }

        private Shape RandomTriangle()
        {
            double x1 = RandomDouble(0d, m_screenWidth);
            double y1 = RandomDouble(0d, m_screenHeight);

            double x2 = x1 + RandomDouble(2 * m_seed, 5 * m_seed);
            double y2 = y1 + RandomDouble(0, 5 * m_seed);

            double x3 = x1 + RandomDouble(2 * m_seed, 5 * m_seed);
            double y3 = y1 - RandomDouble(m_seed, 5 * m_seed);

            return WithStyle(
                new Triangle(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3))
                {
                    Filled = RandomBoolean()
                });
        }

        private Shape RandomRectangle()
        {
            double minimumWidth = m_seed;
            double maximumWidth = 3 * m_seed;
            double minimumDepth = m_seed;
            double maximumDepth = 3 * m_seed;

            double x1 = RandomDouble(0d, m_screenWidth);
            double y1 = RandomDouble(0d, m_screenHeight);
            double x2 = x1 + RandomDouble(minimumWidth, maximumWidth);
            double y2 = y1 + RandomDouble(minimumDepth, maximumDepth);

            return WithStyle(
                new Rectangle(new Point(x1, y1), new Point(x2, y2))
                {
                    Filled = RandomBoolean()
                });
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