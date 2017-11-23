using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.EntitiesLayer {
    public class Articulo {
        public String Id { get; set; }
        public String IdConvencion { get; set; }
        public String Departamento { get; set; }
        public String Capitulo { get; set; }
        public String DescCapitulo { get; set; }
        public String Subcapitulo { get; set; }
        public String Orden { get; set; }
        public String Cod_Articulo { get; set; }
        public String Modelo { get; set; }
        public String DescModelo { get; set; }
        public String Color { get; set; }
        public String DescColor { get; set; }
        public String Talla { get; set; }
        public String DescTalla { get; set; }


        public Boolean? PublicarModelo { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaEntrega1 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaEntrega2 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaEntrega3 { get; set; }
        
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaEntrega4 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaEntrega5 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaLimite1 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaLimite2 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaLimite3 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaLimite4 { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime? FechaLimite5 { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoServicioItem? TipoServicio { get; set; }
        public Boolean? MejorPrecio { get; set; }
        public String Folleto { get; set; }
        public Boolean? PermitirDevolucion { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CompraSeguraItem? CompraSegura { get; set; }
        public int? PorcentajeDevolucion { get; set; }
        public int? PeriodoDevolucion { get; set; }
        public int? PorcentajeCargoProveedor { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BaseCalculoCargoItem? BaseCalculoCargo { get; set; }
        public int? TarifaBruta { get; set; }
        public int? DescuentoBase { get; set; }
        public String Observacion { get; set; }
        public int? Nivel { get; set; }



    }//Class Finish


    public class ShortDateConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer) {
            return DateTime.ParseExact((string)reader.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer) {
            DateTime d = (DateTime)value;
            writer.WriteValue(d.ToString("dd/MM/yyyy"));
        }
    }

    public enum TipoServicioItem { Servicio_Central, Servicio_Directo }
    public enum CompraSeguraItem { Condicional, No, Si }
    public enum BaseCalculoCargoItem { Aplicar_sobre_PVS = 1, Aplicar_sobre_Tarifa_Bruta_Prov = 2, Aplicar_sobre_Tarifa_Neta_Prov = 3 }

    public class Articulos : List<Articulo> {






    }//Class Finish
}//Namespace Finish
