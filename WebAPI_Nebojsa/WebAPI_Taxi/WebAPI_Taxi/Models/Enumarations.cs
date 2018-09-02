using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models
{
    public enum EGender : byte
    {
        MUSKO = 0x1,
        ZENSKO,
    }

    public enum EStatus : byte
    {
        KREIRANA = 0x1, OTKAZANA, FORMIRANA, UTOKU, OBRADJENA, PRIHVACENA, NEUSPESNA, USPESNA,
    }

    public enum ECarType : byte
    {
        PROIZVOLJNO = 0x1,
        PUTNICKI,
        KOMBI
    }

    public enum EUloga : byte
    {
        MUSTERIJA = 0x1,
        DISPECER,
        VOZAC
    }
}