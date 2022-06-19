using DotaPlus.Core;
using DotaPlus.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DotaPlus.Views
{
    public sealed partial class HeroesPage : Page, IViewFor<HeroesViewModel>
    {
        public HeroesPage()
        {
            InitializeComponent();
        }

        public HeroesViewModel ViewModel
        {
            get => DataContext as HeroesViewModel;
            set => DataContext = value;
        }
    }
}
