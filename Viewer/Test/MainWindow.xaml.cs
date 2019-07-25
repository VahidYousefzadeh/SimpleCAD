using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Viewer;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly View m_view;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationModel();
            m_view = ((ApplicationModel)DataContext).View;

            ViewPanel.Children.Add(m_view);
        }
    }
}