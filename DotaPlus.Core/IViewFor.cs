using System.ComponentModel;

namespace DotaPlus.Core
{
    public interface IViewFor<T>
        where T : INotifyPropertyChanged
    {
        T ViewModel { get; set; }
    }
}
