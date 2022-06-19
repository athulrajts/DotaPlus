using DotaPlus.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace DotaPlus.Controls;

public sealed partial class TeamStats : UserControl
{
    public IEnumerable<PlayerModel> Players
    {
        get { return (IEnumerable<PlayerModel>)GetValue(PlayersProperty); }
        set { SetValue(PlayersProperty, value); }
    }

    public static readonly DependencyProperty PlayersProperty =
        DependencyProperty.Register("Players", typeof(IEnumerable<PlayerModel>), typeof(TeamStats), new PropertyMetadata(null));

    public TeamStats()
    {
        this.InitializeComponent();
    }
}
