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
            RandomShapesCommand = new Command(p => { View = Commands.RandomShapes(); }, p => true);
            ClearCommand = new Command(p => { View = Commands.Clear(); }, p => true);
            LoadJsonCommand = new Command(p => { View = Commands.LoadJson(); }, p => true);
            LoadXmlCommand = new Command(p => { View = Commands.LoadXml(); }, p => true);
            SaveJsonCommand = new Command(p => { Commands.SaveJson(View); }, p => true);
        }

        public ICommand RandomShapesCommand { get; }
        public ICommand LoadJsonCommand { get; }
        public ICommand LoadXmlCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand SaveJsonCommand { get; }
    }
}