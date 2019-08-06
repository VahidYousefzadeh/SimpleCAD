using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Viewer.Dialogs
{
    /// <summary>
    /// The base class for all the objects that notify their changes.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void UpdateAndNotify<T>(out T obj, T value, [CallerMemberName] string propertyName = null)
        {
            obj = value;
            Notify(propertyName);
        }

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}