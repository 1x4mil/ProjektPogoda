using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjektPogoda.Models;
using ProjektPogoda.Services;
using ProjektPogoda.Views;

using System.Threading.Tasks;

namespace ProjektPogoda.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly PogodaAPI _pogodaApi = new();

        [ObservableProperty]
        private string miasto = string.Empty;

        [ObservableProperty]
        private Pogoda pogoda = new();

        [ObservableProperty]
        private string komunikatBledu = "";

        [ObservableProperty]
        private bool pokazPrzyciskWiecejInformacji;

        [RelayCommand]
        public void PokazWiecejInformacji()
        {
            var okno = new Window
            {
                Width = 800,
                Height = 500,
                Title = Pogoda?.Miasto ?? "Szczegóły",
                Background = Brushes.LightBlue,
                Content = new WiecejInformacjiView(),
                DataContext = new WiecejInformacjiViewModel(Miasto)
            };

            okno.Show();
        }

        [RelayCommand]
        public async Task PokazPogodeAsync()
        {
            KomunikatBledu = "";
            Pogoda = null!;

            if (!string.IsNullOrWhiteSpace(Miasto))
            {
                var wynik = await _pogodaApi.PobierzPogodeAsync(Miasto);

                if (wynik is null)
                {
                    KomunikatBledu = "Sprawdź pisownię i spróbuj jeszcze raz!";
                    PokazPrzyciskWiecejInformacji = false;
                }
                else
                {
                    Pogoda = wynik;
                    PokazPrzyciskWiecejInformacji = true;
                }
            }
        }
    }
}       
    

