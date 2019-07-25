using System.Windows;
using System.Windows.Controls;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for ViewPanel.xaml
    /// </summary>
    public partial class ViewPanel
    {
        public View View
        {
            get => (View)GetValue(s_viewProperty);
            set => SetValue(s_viewProperty, value);
        }

        public static readonly DependencyProperty s_viewProperty = DependencyProperty.Register(
            nameof(View), typeof(View), typeof(ViewPanel), null);


        public ViewPanel()
        {
            InitializeComponent();
        }
    }
}
