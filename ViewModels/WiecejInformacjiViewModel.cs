using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ProjektPogoda.Models;
using ProjektPogoda.Services;

namespace ProjektPogoda.ViewModels;

public partial class WiecejInformacjiViewModel : ViewModelBase
{
    private readonly PogodaAPI _api = new();

    [ObservableProperty]
    private Pogoda pogoda = new();

    public WiecejInformacjiViewModel(string miasto)
    {
        _ = ZaladujAsync(miasto);
    }

    private async Task ZaladujAsync(string miasto)
    {
        var wynik = await _api.PobierzPogodeAsync(miasto);
        if (wynik is not null)
            Pogoda = wynik;
    }
}