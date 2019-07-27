using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        }

        public ICommand LoadCommand { get; }
        public ICommand ClearCommand { get; }

        private void Clear()
        {
            View = new View();
        }

        private void Load()
        {
            View = new View();
            Pen pen = new Pen(Brushes.Red, 9);
            View.DrawLine(pen, new Point(0, 0), new Point(1000, 1000));
            View.DrawLine(pen, new Point(0, 1000), new Point(1000, 0));
            //View.DrawLine(pen, new Point(0, 0), new Point(1000, 300) );
            //View.DrawCircle(Brushes.Blue, pen, new Point(300, 300), 10);
            //View.DrawTriangle(Brushes.GreenYellow, pen, new Point(0, 0), new Point(100, 100), new Point(50, 100));
        }
    }
}