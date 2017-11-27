using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Collections.Generic;

namespace DataLayer.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void ConsultaBasica() {
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
            String select = "";
            select += "select ";

            //String camposTodos = "LCZON,LCSRP7,LCCYCD,LCC9LVNO1,LCCNO,LCITM,LCC9LVNO,LCC9LVL0,LCDSC1,LCC9LVL1,LCRMK2,LCC9LVL2,LCDSC3,LCPRRC,LCUPRC,LCC9RATE1,LCPAMT,LCUNCS,LCC9RATE2,LCC9RATE3,LCC9RATE4,LCPQOH,LCSRP3,LCSRP6,LCSRP4,LCPRP0,LCSRP5,LCSRP8,LCACD01DES,LCACD02DES,LCACD03DES,LCCCD01DES,LCCCD02DES,LCCCD03DES,LCCCD04DES,LCCCD05DES,LCCCD06DES,LCCCD07DES,LCCCD08DES,LCEV01,LCC9EADJ1,LCC9EADJ2,LCC9EADJ3,LCC9EADJ4,LCC9EADJ5,LCC9LTDJ1,LCC9LTDJ2,LCC9LTDJ3,LCC9LTDJ4,LCC9LTDJ5,LCC9RFDJ1,LCC9RFDJ2,LCC9RFDJ3,LCC9RFDJ4,LCC9RFDJ5,LCEV02,LCEDSP,LCPID,LCJOBN,LCUPMJ,LCUSER,LCTDAY,LCNEJT,LCNPDL01,LCMGTX,LCMATH01,LCMATH02,LCMATH03,LCMATH04,LCMATH05,LCDL01,LCDL02,LCDL03,LCDL04,LCDL05,LCWOMO,LCMQTY,LC$MAPR,LCAA03,LCCNID,LCEV03,LC$MREF,LC$MRFM,LC$MDPR,LC$CRAT,LC$PERI,LC$PDEV,LC$MPBN,LC$TPROV,LC$DINT,LC$PRAT,LC$PACP,LCABC0,LC$MDLV,LC$CPD1,LC$CPD2,LC$CPD3,LC$CPD4,LC$CPD5,LC$CPD6,LC$CPD7,LC$CPD8,LC$CPD9,LC$CPD10,LC$CPD11,LC$CPD12,LC$CPD13,LC$CPD14,LC$CPD15,LC$PBLM,LC$PBMD,LCAPPV,LC$UPF1,LC$UPF2,LC$UPF3,LC$DSOC,LC$DCOM,LC$DLIC,LC$DONL,LC$BAP1,LC$BAP2,LC$BAP3,LC$BAP4,LC$PVS01,LC$PVS02,LC$PVS03,LC$PVS04,LC$PVS05,LC$LC1,LC$MUF1,LC$MUF2,LC$MCF1,LC$MCF2,LC$CODE,LC$KILL,LC$CAT,LC$CAM,LC$COM,LC$MPRE";

            List<String> campos = new List<string>();
            //campos.Add("LCZON");
            //campos.Add("LC$PBLM");
            //campos.Add("LCC9EADJ1");
            //campos.Add("LCC9EADJ2");
            //campos.Add("LCC9EADJ3");
            //campos.Add("LCC9EADJ4");
            //campos.Add("LC$CPD1");
            //campos.Add("LC$CPD2");
            //campos.Add("LC$CPD3");
            //campos.Add("LC$CPD4");
            //campos.Add("LC$CPD5");
            //campos.Add("LCC9CAT2");
            //campos.Add("LC$MPRE");
            //campos.Add("LCCCD07DES");
            //campos.Add("LC$PDEV");
            //campos.Add("LC$MDPR");
            //campos.Add("LC$CRAT");
            //campos.Add("LC$PERI");
            //campos.Add("LC$PRAT");
            //campos.Add("LC$PACP");
            //campos.Add("LC$TPROV");
            //campos.Add("LC$BAP1");
            //campos.Add("LCACD01DES");
            //campos.Add("LCC9LVNO");


            //List<String> camposDefinitivo = new List<string>();
            //List<String> camposEliminados = new List<string>();
            //foreach (var campo in campos) {
            //    if (camposTodos.Contains(campo)) {
            //        camposDefinitivo.Add(campo);
            //    } else {
            //        camposEliminados.Add(campo);
            //    }
            //}

            if (campos.Count > 0) {
                select += String.Join(",", campos);
            } else {
                select += " * ";
            }

            select += " from F55DS53 where LCZON = '8V1' ";


            int pageNumber = 1;
            int pageSize = 10;

            int offset = (pageNumber - 1) * pageSize;

            select += String.Format(" limit {0} offset {1}", pageSize, offset);

            //String select2 = "select * from F55V137C limit 20";

            DataSet ds = ALC.IES.WebRange.DataLayer.Consultas.Execute(select);

            Assert.IsNotNull(ds);
            //List<String> listado = new List<string>();
            //foreach (var item in ds.Tables[0].Columns) {
            //    listado.Add(item.ToString());
            //}

            //String camposTotal = String.Join(",", listado);



            Assert.IsTrue(ds.Tables.Count > 0);
            ds.Dispose();
        }


    }//Class Finish
}//Namespace Finish
