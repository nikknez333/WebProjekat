using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi_MVC.Models
{
    public enum EPol { MUSKI, ZENSKI}

    public enum EUloga { MUSTERIJA, DISPECER, VOZAC}

    public enum ETipAutomobila { NEOZNACEN, PUTNICKI, KOMBI}

    public enum EStatus { KREIRANA, FORMIRANA, OBRADJENA, PRIHVACENA, OTKAZANA, NEUSPESNA, USPESNA}
}