using System.Windows.Input;

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
            RandomShapesCommand = new Command(p => { View = Commands.RandomShapes(60, 5000, 5000); }, p => true);
            ClearCommand = new Command(p => { View = Commands.Clear(); }, p => true);
            LoadJsonCommand = new Command(p => { View = Commands.LoadJson(); }, p => true);
            LoadXmlCommand = new Command(p => { View = Commands.LoadXml(); }, p => true);
            SaveJsonCommand = new Command(p => { Commands.SaveJson(View); }, p => true);
            SaveXmlCommand = new Command(p => { Commands.SaveXml(View); }, p => true);
            SavePdfCommand = new Command(p => { Commands.SavePdf(View); }, p => true);
        }

        public ICommand RandomShapesCommand { get; }
        public ICommand LoadJsonCommand { get; }
        public ICommand LoadXmlCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand SaveJsonCommand { get; }
        public ICommand SaveXmlCommand { get; }
        public ICommand SavePdfCommand { get; }
    }
}