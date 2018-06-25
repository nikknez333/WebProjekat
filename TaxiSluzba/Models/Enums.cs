using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
   
    public enum TipAutomobila { PODRAZUMEVANO, PUTNICKI, KOMBI }

    public enum Statusi { NaCekanju, Formirana, Obradjena, Prihvacena, Otkazana, Neuspesna, Uspesna }

    public enum Uloge { MUSTERIJA, DISPECER, VOZAC }

    public enum Polovi { MUSKI, ZENSKI}
}