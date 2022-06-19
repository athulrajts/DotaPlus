using DotaPlus.Core;
using DotaPlus.Models;
using DotaPlus.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace DotaPlus.Views
{
    public sealed partial class PlayerGamesPage : Page, IViewFor<PlayerGamesViewModel>
    {
        public PlayerGamesPage()
        {
            InitializeComponent();
        }

        public PlayerGamesViewModel ViewModel
        {
            get => DataContext as PlayerGamesViewModel;
            set => DataContext = value;
        }
    }

    public class MatchPagesDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MatchSummary { get; set; }
        public DataTemplate GpmXp { get; set; }

        public MatchPagesDataTemplateSelector()
        {

        }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            return item switch
            {
                MatchSummaryModel => MatchSummary,
                GpmXpChartsModel => GpmXp,
                _ => throw new ArgumentException(null, nameof(item))
            };
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return item switch
            {
                MatchSummaryModel => MatchSummary,
                GpmXpChartsModel => GpmXp,
                _ => throw new ArgumentException(null, nameof(item))
            };
        }
    }
}
