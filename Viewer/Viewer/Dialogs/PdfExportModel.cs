using System.Windows.Controls;

namespace Viewer.Dialogs
{
    public sealed class PdfExportModel : ObservableObject
    {
        private int m_pdfPageWidth = 700;
        public int PdfPageWidth
        {
            get => m_pdfPageWidth;
            set => UpdateAndNotify(out m_pdfPageWidth, value);
        }

        private int m_pdfPageHeight = 700;
        public int PdfPageHeight
        {
            get => m_pdfPageHeight;
            set => UpdateAndNotify(out m_pdfPageHeight, value);
        }
    }

    public class PageSizeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool canConvert = int.TryParse(value as string, out int result);
            canConvert = canConvert && result >= 100;
            return new ValidationResult(canConvert, "Not a valid value");
        }
    }
}