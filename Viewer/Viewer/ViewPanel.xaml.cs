using System.Windows;

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
            nameof(View), typeof(View), typeof(ViewPanel), new PropertyMetadata(OnViewPropertyChanged));

        private static void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ViewPanel) d).Annotations.Content = new Annotations(((ViewPanel) d).View);
        }

        public ViewPanel()
        {
            InitializeComponent();
        }
    }
}
