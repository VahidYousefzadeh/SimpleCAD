using System.Windows.Input;
using Viewer.Graphics;

namespace Viewer.Dialogs
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
            ClearCommand = new Command(_ => View = Commands.Clear(), _ => true);
            RandomShapesCommand = new Command(_ => View = Commands.RandomShapes(30, 1000, 1000), _ => true);
            LoadJsonCommand = new Command(_ => View = Commands.LoadJson(View), _ => true);
            LoadXmlCommand = new Command(_ => View = Commands.LoadXml(View), _ => true);
            SaveJsonCommand = new Command(_ => Commands.SaveJson(View), _ => Commands.CanExecuteSaveJson(View));
            SaveXmlCommand = new Command(_ => Commands.SaveXml(View), _ => Commands.CanExecuteSaveXml(View));
            SavePdfCommand = new Command(_ => Commands.SavePdf(View), _ => Commands.CanExecuteSavePdf(View));
        }

        public ICommand ClearCommand { get; }
        public ICommand RandomShapesCommand { get; }
        public ICommand LoadJsonCommand { get; }
        public ICommand LoadXmlCommand { get; }
        public ICommand SaveJsonCommand { get; }
        public ICommand SaveXmlCommand { get; }
        public ICommand SavePdfCommand { get; }
    }
}