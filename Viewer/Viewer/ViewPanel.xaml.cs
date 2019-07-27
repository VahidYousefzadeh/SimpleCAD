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
            nameof(View), typeof(View), typeof(ViewPanel), new PropertyMetadata(OnUiViewPropertyChanged));

        private static void OnUiViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (ViewPanel) d;
            panel.Annotations.Content = new Annotations(panel.View);
        }


        public ViewPanel()
        {
            InitializeComponent();
        }
    }
}
