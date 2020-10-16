using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AvaloniaHugeTree
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetAndRaise<T>(ref T field, T newValue, [CallerMemberName]string name = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public void RaisePropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}