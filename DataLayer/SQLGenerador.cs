using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.DataLayer {
    public class SQLGenerador {
        private List<String> _campos { get; set; }
        private String _nombreTabla { get; set; }
        private String _condicion { get; set; }
        private String _orden { get; set; }



        public SQLGenerador(List<String> campos, String nombreTabla, String condicion = null, String orden = null) {
            this._campos = campos;
            this._nombreTabla = nombreTabla;
            this._condicion = condicion;
            this._orden = orden;
        }



        #region Métodos Públicos
        public String GetConsulta(int? pageNumber, int? pageSize) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            stringBuilder.Append(String.Join(", ", _campos));

            stringBuilder.AppendFormat(" from {0} ", _nombreTabla);
            stringBuilder.Append(this.GetCondicionConWhere());

            if (pageNumber.HasValue && pageSize.HasValue && pageSize.Value > 0) {
                int offset = (pageNumber.Value - 1) * pageSize.Value;
                stringBuilder.AppendFormat("limit {0} offset {1}", pageSize.Value, offset);
            }
            return stringBuilder.ToString();
        }

        public String GetConsultaCount() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(*) ");

            stringBuilder.AppendFormat(" from {0} ", _nombreTabla);
            stringBuilder.Append(this.GetCondicionConWhere());

            //if (pageNumber.HasValue && pageSize.HasValue && pageSize.Value > 0) {
            //    int offset = (pageNumber.Value - 1) * pageSize.Value;
            //    stringBuilder.AppendFormat("limit {0} offset {1}", pageSize.Value, offset);
            //}
            return stringBuilder.ToString();
        }
        #endregion


        #region Metodos Privados
        private String GetCondicionConWhere() {
            if (!string.IsNullOrWhiteSpace(_condicion)) {
                _condicion = _condicion.Trim();
                if (!_condicion.StartsWith("where", StringComparison.InvariantCultureIgnoreCase)) {
                    this._condicion = " where ";
                }
                this._condicion += _condicion;
            }
            return _condicion;
        }
        #endregion

    }//Class Finish
}//Namespace Finish
