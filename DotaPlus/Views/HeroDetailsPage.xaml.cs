using DotaPlus.Core;
using DotaPlus.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace DotaPlus.Views;

public sealed partial class HeroDetailsPage : Page, IViewFor<HeroDetailsViewModel>
{
    public HeroDetailsPage()
    {
        InitializeComponent();
    }

    public HeroDetailsViewModel ViewModel
    {
        get => DataContext as HeroDetailsViewModel;
        set => DataContext = value;
    }
}
