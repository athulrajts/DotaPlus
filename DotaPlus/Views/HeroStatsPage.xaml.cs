using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using DotaPlus.Core;
using DotaPlus.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace DotaPlus.Views
{
    public sealed partial class HeroStatsPage : Page, IViewFor<HeroStatsViewModel>
    {
        public HeroStatsPage()
        {
            this.InitializeComponent();
        }

        public HeroStatsViewModel ViewModel
        {
            get => DataContext as HeroStatsViewModel;
            set => DataContext = value;
        }

        private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {
            if (e.Column.Tag is null) return;

            var sortdir = e.Column.SortDirection is null
                ? SortDirection.Descending
                : e.Column.SortDirection == DataGridSortDirection.Ascending
                    ? SortDirection.Descending
                    : SortDirection.Ascending;

   
            ViewModel.Stats.SortDescriptions.Clear();
            ViewModel.Stats.SortDescriptions.Add(new SortDescription(e.Column.Tag?.ToString(), sortdir));
            ViewModel.Stats.RefreshSorting();
            e.Column.SortDirection = sortdir == SortDirection.Ascending 
                ? DataGridSortDirection.Ascending
                : DataGridSortDirection.Descending;

            var dg = sender as DataGrid;

            foreach (var col in dg.Columns)
            {
                if(col.Tag != e.Column.Tag)
                {
                    col.SortDirection = null;
                }
            }
        }
    }
}
