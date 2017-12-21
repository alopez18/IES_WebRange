using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tests {
    [TestClass]
    public class SQLGeneradorTests {



        [TestMethod]
        public void ConsultaBasica() {

            List<String> campos = new List<string>() {
                "*"
            };

            //ALC.IES.WebRange.DataLayer.Consultas


            //String select = "select * from F55V137C limit 20";
            //String select = "SELECT DISTINCT LCZON from F55DS53";
            String select = "select * from F55V126A";

            DataSet ds = ALC.IES.WebRange.DataLayer.Consultas.Execute(select);

            Assert.IsNotNull(ds);


            Assert.IsTrue(ds.Tables.Count > 0);
            ds.Dispose();
        }


        [TestMethod]
        public void ConsultaCatalogo() {


            List<String> campos = new List<string>();
            campos.Add("LCZON");
            campos.Add("LC$PBLM");
            campos.Add("LCC9EADJ1");
            campos.Add("LCC9EADJ2");
            campos.Add("LCC9EADJ3");
            campos.Add("LCC9EADJ4");
            campos.Add("LC$CPD1");
            campos.Add("LC$CPD2");
            campos.Add("LC$CPD3");
            campos.Add("LC$CPD4");
            campos.Add("LC$CPD5");
            campos.Add("LCC9CAT2");
            campos.Add("LC$MPRE");
            campos.Add("LCCCD07DES");
            campos.Add("LC$PDEV");
            campos.Add("LC$MDPR");
            campos.Add("LC$CRAT");
            campos.Add("LC$PERI");
            campos.Add("LC$PRAT");
            campos.Add("LC$PACP");
            campos.Add("LC$TPROV");
            campos.Add("LC$BAP1");
            campos.Add("LCACD01DES");
            campos.Add("LCC9LVNO");



            StringBuilder sb = new StringBuilder();
            sb.Append(" LCZON = '8V1' ");
            ALC.IES.WebRange.DataLayer.SQLGenerador generador = new ALC.IES.WebRange.DataLayer.SQLGenerador(campos, "F55DS53", sb.ToString());
            String select = generador.GetConsulta(1, 10);
            DataSet ds = ALC.IES.WebRange.DataLayer.Consultas.Execute(select);
            Assert.IsNotNull(ds);
            Assert.IsTrue(ds.Tables.Count > 0);
            ds.Dispose();
        }



    }//Class Finish
}//Namespace
