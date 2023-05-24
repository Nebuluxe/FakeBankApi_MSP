using System.Collections.Generic;

namespace FolderERP.ANA.Model
{
    public class TasaMoneda
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ListUf
        {
            public List<UF> UFs { get; set; }
        }

        public class UF
        {
            public string Valor { get; set; }
            public string Fecha { get; set; }
        }

        public class ListDolar
        {
            public List<Dolar> Dolares { get; set; }
        }

        public class Dolar
        {
            public string Valor { get; set; }
            public string Fecha { get; set; }
        }

        public class ListEuro
        {
            public List<Euro> Euros { get; set; }
        }

        public class Euro
        {
            public string Valor { get; set; }
            public string Fecha { get; set; }
        }
    }
}
