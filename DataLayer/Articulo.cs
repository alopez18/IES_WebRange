using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.DataLayer {
    public class Articulo {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataLayer.Articulo));
        private const String _SEPARADOR_PK = "#_#";
        private static readonly Dictionary<string, string> _JDE_MODEL_MAP = new Dictionary<string, string>{
            { "LCZON", "IdConvencion" },
            { "LC$PBLM", "PublicarModelo" },
            { "LCC9EADJ1", "FechaEntrega1" },
            { "LCC9EADJ2", "FechaEntrega2" },
            { "LCC9EADJ3", "FechaEntrega3" },
            { "LCC9EADJ4", "FechaEntrega4" },
            { "LCC9EADJ5", "FechaEntrega5" }
        };//Falta acabar e implementar para evitar los case en la medida de lo posible.







        internal static EntitiesLayer.Articulo Parse(DataRow dr) {
            EntitiesLayer.Articulo res = new EntitiesLayer.Articulo();
            String sAux = null;
            foreach (DataColumn col in dr.Table.Columns) {
                object valorDB = dr[col.ColumnName];
                if (valorDB == DBNull.Value) {
                    sAux = null;
                } else {
                    sAux = valorDB.ToString().Trim();
                }
                switch (col.ColumnName) {
                    case "LCZON":
                        res.IdConvencion = sAux;
                        break;
                    case "LC$PBLM":
                        res.PublicarModelo = ParseBooleanYN(sAux);
                        break;
                    case "LCC9EADJ1":
                        res.FechaEntrega1 = ParseDateTime(sAux);
                        break;
                    case "LCC9EADJ2":
                        res.FechaEntrega2 = ParseDateTime(sAux);
                        break;
                    case "LCC9EADJ3":
                        res.FechaEntrega3 = ParseDateTime(sAux);
                        break;
                    case "LCC9EADJ4":
                        res.FechaEntrega4 = ParseDateTime(sAux);
                        break;
                    case "LCC9EADJ5":
                        res.FechaEntrega5 = ParseDateTime(sAux);
                        break;
                    case "LC$CPD1":
                        res.FechaLimite1 = ParseDateTime(sAux);
                        break;
                    case "LC$CPD2":
                        res.FechaLimite2 = ParseDateTime(sAux);
                        break;
                    case "LC$CPD3":
                        res.FechaLimite3 = ParseDateTime(sAux);
                        break;
                    case "LC$CPD4":
                        res.FechaLimite4 = ParseDateTime(sAux);
                        break;
                    case "LC$CPD5":
                        res.FechaLimite5 = ParseDateTime(sAux);
                        break;
                    case "LCC9CAT2":
                        if (!String.IsNullOrWhiteSpace(sAux)) {
                            switch (sAux.Trim()) {
                                case "N":
                                    res.TipoServicio = EntitiesLayer.TipoServicioItem.Servicio_Central;
                                    break;
                                case "S":
                                    res.TipoServicio = EntitiesLayer.TipoServicioItem.Servicio_Directo;
                                    break;
                                default:
                                    res.TipoServicio = null;
                                    break;
                            }
                        } else {
                            res.TipoServicio = null;
                        }
                        break;
                    case "LC$MPRE":
                        res.MejorPrecio = ParseBooleanYN(sAux);
                        break;
                    case "LCCCD07DES":
                        res.Folleto = sAux;
                        break;
                    case "LC$PDEV":
                        res.PermitirDevolucion = ParseBooleanYN(sAux);
                        break;
                    case "LC$MDPR":
                        if (!String.IsNullOrWhiteSpace(sAux)) {
                            switch (sAux.Trim()) {
                                case "C":
                                    res.CompraSegura = EntitiesLayer.CompraSeguraItem.Condicional;
                                    break;
                                case "0":
                                    res.CompraSegura = EntitiesLayer.CompraSeguraItem.No;
                                    break;
                                case "1":
                                    res.CompraSegura = EntitiesLayer.CompraSeguraItem.Si;
                                    break;
                                default:
                                    res.CompraSegura = null;
                                    break;
                            }
                        } else {
                            res.CompraSegura = null;
                        }

                        break;
                    case "LC$CRAT":
                        res.PorcentajeDevolucion = ParseInt(sAux);
                        break;
                    case "LC$PERI":
                        res.PeriodoDevolucion = ParseInt(sAux);
                        break;
                    case "LC$PRAT":
                        res.PorcentajeCargoProveedor = ParseInt(sAux);
                        break;
                    case "LC$PACP":
                        int? nCalculoCargo = ParseInt(sAux);
                        if (nCalculoCargo.HasValue && nCalculoCargo.Value > 0) {
                            res.BaseCalculoCargo = (EntitiesLayer.BaseCalculoCargoItem)Enum.ToObject(typeof(EntitiesLayer.BaseCalculoCargoItem), nCalculoCargo.Value);
                        }
                        break;
                    case "LC$TPROV":
                        res.TarifaBruta = ParseInt(sAux);
                        break;
                    case "LC$BAP1":
                        res.DescuentoBase = ParseInt(sAux);
                        break;
                    case "LCACD01DES":
                        res.Observacion = sAux;
                        break;
                    case "LCC9LVNO":
                        res.Nivel = ParseInt(sAux);
                        break;

                    case "LCRMK2":
                        res.DescColor = sAux;
                        break;
                    case "LCSRP7":
                        res.Departamento = sAux;
                        break;
                    case "LCCYCD":
                        res.Capitulo = sAux;
                        break;
                    case "LCC9LVNO1":
                        res.Subcapitulo = sAux;
                        break;
                    case "LCCNO":
                        res.Orden = sAux;
                        break;
                    case "LCITM":
                        res.Cod_Articulo = sAux;
                        break;
                    case "LCNEJT":
                        res.DescCapitulo = sAux;
                        break;
                    case "LCC9LVL0":
                        res.Modelo = sAux;
                        break;
                    case "LCDSC1":
                        res.DescModelo = sAux;
                        break;
                    case "LCC9LVL1":
                        res.Color = sAux;
                        break;
                    case "LCC9LVL2":
                        res.Talla = sAux;
                        break;
                    case "LCDSC3":
                        res.DescTalla = sAux;
                        break;
                    default:
                        _log.WarnFormat("Columna '{0}' no reconocida para parsear la entidad {1}", col.ColumnName, "EntitiesLayer.Convencion");
                        break;
                }
            }

            res.Id = GetPrimaryKey(res);
            return res;
        }



        private static DateTime? ParseDateTime(String val) {
            DateTime? res = null;
            DateTime dAux;
            int nAux;
            if (!String.IsNullOrWhiteSpace(val)) {
                if (int.TryParse(val, out nAux)) {
                    res = Utils.UnixTimeStampToDateTime(nAux);
                } else {
                    if (DateTime.TryParse(val, out dAux)) {
                        res = dAux;
                    }
                }
            }
            return res;
        }

        private static Boolean? ParseBooleanYN(String val) {
            Boolean? res = null;
            if (!String.IsNullOrWhiteSpace(val)) {
                res = (val.Trim().ToUpper() == "Y");
            }
            return res;
        }

        private static int? ParseInt(String val) {
            int? res = null;
            int nAux;
            if (!String.IsNullOrWhiteSpace(val)) {
                if (int.TryParse(val, out nAux)) {
                    res = nAux;
                } else {
                    _log.WarnFormat("No se ha podido parsear a int el valor '{0}'", val);
                }
            }
            return res;
        }


        public static String GetPrimaryKey(EntitiesLayer.Articulo art) {
            String res = "";
            List<String> valores = new List<string>();
            valores.Add(art.IdConvencion);
            valores.Add(art.Departamento);
            valores.Add(art.Capitulo);
            valores.Add(art.Subcapitulo);
            valores.Add(art.Orden);
            valores.Add(art.Cod_Articulo);
            res = String.Join(_SEPARADOR_PK, valores);
            res = Convert.ToBase64String(Encoding.UTF8.GetBytes(res));
            return res;
        }


        public static void DecodePK(ref EntitiesLayer.Articulo art) {
            String decoded = Encoding.UTF8.GetString(Convert.FromBase64String(art.Id));
            String[] splitado = decoded.Split(new String[] { _SEPARADOR_PK }, StringSplitOptions.RemoveEmptyEntries);
            if (splitado.Length == 6) {
                art.IdConvencion = splitado[0];
                art.Departamento = splitado[1];
                art.Capitulo = splitado[2];
                art.Subcapitulo = splitado[3];
                art.Orden = splitado[4];
                art.Cod_Articulo = splitado[5];
            } else {
                String message = "El identificador de artuculo pasado no se ha podido decodificar debido a que no tiene la longitud deseada. ";
                message += String.Format("Los datos son: Id: '{0}'. La longitud decodificada es: {1}.", art.Id, splitado.Length);
                _log.Error(message);
                throw new FormatException(message);
            }
        }


        public static Boolean Save(EntitiesLayer.Articulo art, List<String> camposActualizar) {
            //Extraemos del Id los campos que conforman la primary key.
            DecodePK(ref art);

            String update = "UPDATE F55DS53 SET ";
            List<String> setters = new List<string>();
            foreach (String campo in camposActualizar) {
                switch (campo) {
                    case "IdConvencion":
                        setters.Add(String.Format(" LCZON = '{0}' ", art.IdConvencion));
                        break;
                    case "PublicarModelo":

                        setters.Add(String.Format(" LC$PBLM = '{0}' ", art.PublicarModelo.Value ? 1 : 0));
                        break;
                    case "FechaEntrega1":
                        setters.Add(String.Format(" LCC9EADJ1 = '{0}' ", Utils.DateTime2UnixTime(art.FechaEntrega1.Value)));
                        break;
                    case "FechaEntrega2":
                        setters.Add(String.Format(" LCC9EADJ2 = '{0}' ", Utils.DateTime2UnixTime(art.FechaEntrega2.Value)));
                        break;
                    case "FechaEntrega3":
                        setters.Add(String.Format(" LCC9EADJ3 = '{0}' ", Utils.DateTime2UnixTime(art.FechaEntrega3.Value)));
                        break;
                    case "FechaEntrega4":
                        setters.Add(String.Format(" LCC9EADJ4 = '{0}' ", Utils.DateTime2UnixTime(art.FechaEntrega4.Value)));
                        break;
                    case "FechaEntrega5":
                        setters.Add(String.Format(" LCC9EADJ5 = '{0}' ", Utils.DateTime2UnixTime(art.FechaEntrega5.Value)));
                        break;
                    case "FechaLimite1":
                        setters.Add(String.Format(" LC$CPD1 = '{0}' ", Utils.DateTime2UnixTime(art.FechaLimite1.Value)));
                        break;
                    case "FechaLimite2":
                        setters.Add(String.Format(" LC$CPD2 = '{0}' ", Utils.DateTime2UnixTime(art.FechaLimite2.Value)));
                        break;
                    case "FechaLimite3":
                        setters.Add(String.Format(" LC$CPD3 = '{0}' ", Utils.DateTime2UnixTime(art.FechaLimite3.Value)));
                        break;
                    case "FechaLimite4":
                        setters.Add(String.Format(" LC$CPD4 = '{0}' ", Utils.DateTime2UnixTime(art.FechaLimite4.Value)));
                        break;
                    case "FechaLimite5":
                        setters.Add(String.Format(" LC$CPD5 = '{0}' ", Utils.DateTime2UnixTime(art.FechaLimite5.Value)));
                        break;
                    case "TipoServicio":
                        switch (art.TipoServicio) {
                            case EntitiesLayer.TipoServicioItem.Servicio_Central:
                                setters.Add(" LCC9CAT2 = 'N' ");
                                break;
                            case EntitiesLayer.TipoServicioItem.Servicio_Directo:
                                setters.Add(" LCC9CAT2 = 'S' ");
                                break;
                            default:
                                break;
                        }
                        break;
                    case "MejorPrecio":
                        setters.Add(String.Format(" LC$MPRE = '{0}' ", art.MejorPrecio.Value?1:0));
                        break;
                    case "Folleto":
                        setters.Add(String.Format(" LCCCD07DES = '{0}' ", art.Folleto));
                        break;
                    case "PermitirDevolucion":
                        setters.Add(String.Format(" LC$PDEV = '{0}' ", art.PermitirDevolucion.Value ? 1 : 0));
                        break;
                    case "CompraSegura":
                        switch (art.CompraSegura.Value) {
                            case EntitiesLayer.CompraSeguraItem.Condicional:
                                setters.Add(" LC$MDPR = 'C' ");
                                break;
                            case EntitiesLayer.CompraSeguraItem.No:
                                setters.Add(" LC$MDPR = '0' ");
                                break;
                            case EntitiesLayer.CompraSeguraItem.Si:
                                setters.Add(" LC$MDPR = '1' ");
                                break;
                            default:
                                break;
                        }
                        break;
                    case "PorcentajeDevolucion":
                        setters.Add(String.Format(" LC$CRAT = {0} ", art.PorcentajeDevolucion));
                        break;
                    case "PeriodoDevolucion":
                        setters.Add(String.Format(" LC$PERI = {0} ", art.PeriodoDevolucion));
                        break;
                    case "PorcentajeCargoProveedor":
                        setters.Add(String.Format(" LC$PRAT = {0} ", art.PorcentajeCargoProveedor));
                        break;
                    case "BaseCalculoCargo":
                        switch (art.BaseCalculoCargo.Value) {
                            case EntitiesLayer.BaseCalculoCargoItem.Aplicar_sobre_PVS:
                                setters.Add(" LC$PACP = '1' ");
                                break;
                            case EntitiesLayer.BaseCalculoCargoItem.Aplicar_sobre_Tarifa_Bruta_Prov:
                                setters.Add(" LC$PACP = '2' ");
                                break;
                            case EntitiesLayer.BaseCalculoCargoItem.Aplicar_sobre_Tarifa_Neta_Prov:
                                setters.Add(" LC$PACP = '3' ");
                                break;
                            default:
                                break;
                        }
                        break;
                    case "TarifaBruta":
                        setters.Add(String.Format(" LC$TPROV = {0} ", art.TarifaBruta));
                        break;
                    case "DescuentoBase":
                        setters.Add(String.Format(" LC$BAP1 = {0} ", art.DescuentoBase));
                        break;
                    case "Observacion":
                        setters.Add(String.Format(" LCACD01DES = '{0}' ", art.Observacion));
                        break;
                    case "Nivel":
                        setters.Add(String.Format(" LCC9LVNO = {0} ", art.Nivel));
                        break;
                    case "DescColor":
                        setters.Add(String.Format(" LCRMK2 = '{0}' ", art.DescColor));
                        break;
                    case "Departamento":
                        setters.Add(String.Format(" LCSRP7 = '{0}' ", art.Departamento));
                        break;
                    case "Capitulo":
                        setters.Add(String.Format(" LCCYCD = '{0}' ", art.Capitulo));
                        break;
                    case "Subcapitulo":
                        setters.Add(String.Format(" LCC9LVNO1 = '{0}' ", art.Subcapitulo));
                        break;
                    case "Orden":
                        setters.Add(String.Format(" LCCNO = '{0}' ", art.Orden));
                        break;
                    case "Cod_Articulo":
                        setters.Add(String.Format(" LCITM = '{0}' ", art.Cod_Articulo));
                        break;
                    case "DescCapitulo":
                        setters.Add(String.Format(" LCNEJT = '{0}' ", art.DescCapitulo));
                        break;
                    case "Modelo":
                        setters.Add(String.Format(" LCC9LVL0 = '{0}' ", art.Modelo));
                        break;
                    case "DescModelo":
                        setters.Add(String.Format(" LCDSC1 = '{0}' ", art.DescModelo));
                        break;
                    case "Color":
                        setters.Add(String.Format(" LCC9LVL1 = '{0}' ", art.Color));
                        break;
                    case "Talla":
                        setters.Add(String.Format(" LCC9LVL2 = '{0}' ", art.Talla));
                        break;
                    case "DescTalla":
                        setters.Add(String.Format(" LCDSC3 = '{0}' ", art.DescTalla));
                        break;
                    default:
                        String message = String.Format("No se ha podido crear el update debido a que no se ha reconicido el campo'{0}'", campo);
                        _log.Warn(message);
                        break;
                }
            }

            update += String.Join(", ", setters);

            update += " WHERE ";
            update += String.Format(" LCZON='{0}' ", art.IdConvencion);
            update += String.Format(" AND LCSRP7='{0}' ", art.Departamento);
            update += String.Format(" AND LCCYCD='{0}' ", art.Capitulo);
            update += String.Format(" AND LCC9LVNO1='{0}' ", art.Subcapitulo);
            update += String.Format(" AND LCCNO='{0}' ", art.Orden);
            update += String.Format(" AND LCITM='{0}' ", art.Cod_Articulo);


            return Consultas.Update(update);


        }


    } //Class Finish

    public class Articulos {


        public static EntitiesLayer.Articulos Get(String idConvencion, List<String> campos, int? nivel, int? pageNumber, int? pageSize) {
            EntitiesLayer.Articulos res = null;

            String select = "";
            select += "select ";
            select += String.Join(", ", campos);
            select += String.Format(" from F55DS53 where LCZON = '{0}' ", idConvencion);

            if (nivel.HasValue) {
                select += String.Format(" and LCC9LVNO = {0}", nivel.Value);
            }

            if (pageNumber.HasValue && pageSize.HasValue) {
                int offset = (pageNumber.Value - 1) * pageSize.Value;
                select += String.Format(" limit {0} offset {1}", pageSize.Value, offset);
            }


            DataSet ds = ALC.IES.WebRange.DataLayer.Consultas.Execute(select);

            if (ds != null && ds.Tables.Count > 0) {
                res = new EntitiesLayer.Articulos();
                foreach (DataRow r in ds.Tables[0].Rows) {
                    EntitiesLayer.Articulo artAux = DataLayer.Articulo.Parse(r);
                    res.Add(artAux);
                }
            }
            return res;
        }

    }//Class Finish
}//Namespace Finish
