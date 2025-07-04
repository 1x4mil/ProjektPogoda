using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPogoda.Models;

public class Pogoda
{
    public string? Miasto { get; set; }
    public string? Kraj { get; set; }
    public double? Temperatura { get; set; }
    public double? Odczuwalna { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
    public string? Opis { get; set; }
    public double? Cisnienie { get; set; }
    public int? Wilgotnosc { get; set; }
    public double? PredkoscWiatru { get; set; }
    public int? Zachmurzenie { get; set; }
    public DateTime? CzasPomiaru { get; set; }
}