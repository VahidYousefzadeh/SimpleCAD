using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Viewer.Dialogs
{
    /// <summary>
    /// Interaction logic for PdfExportDialog.xaml
    /// </summary>
    public partial class PdfExportDialog : Window
    {
        public PdfExportDialog()
        {
            InitializeComponent();
        }

        private void OkCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OkCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsValid(sender as DependencyObject);
        }

        private bool IsValid(DependencyObject obj)
        {
            return !Validation.GetHasError(obj) && LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().All(IsValid);
        }
    }
}
