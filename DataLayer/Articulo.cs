using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.DataLayer {
    public class Articulo {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataLayer.Articulo));

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
                        res.IdConvencion = dr[col.ColumnName].ToString();
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

    }//Class Finish

    public class Articulos {


        public static EntitiesLayer.Articulos Get(String idConvencion, List<String> campos, int? pageNumber, int? pageSize) {
            EntitiesLayer.Articulos res = null;

            String select = "";
            select += "select ";
            select += String.Join(", ", campos);
            select += String.Format(" from F55DS53 where LCZON = '{0}' ", idConvencion);
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
