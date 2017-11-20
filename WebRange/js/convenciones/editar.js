﻿
$(function () {
    //punteros al DOM.
    convencion.$btnFilter = $(".btnFilter");


    //Eventos objetos del DOM.
    convencion.$btnFilter.on("click", function (e) {
        e.preventDefault();
        var $this = $(this);
        var id = $this.data("id");
        $(".navbar-top-links>li.active").removeClass("active");
        $this.parent().addClass("active");
        convencion.loadData(id);
    });



    ///Funcion que carga los datos en la handsontable dado un identificador.
    convencion.loadData = function (id) {
        gl.loadGlobal.css("display", "block");
        $.ajax({
            url: '/Convencion/Datos/' + convencion.Id + '?filtroId=' + id,
            type: 'POST',
            cache: false,
            dataType: 'json',
            success: function (data) {
                gl.loadGlobal.hide();
                console.log(data.ColsConfig);
                convencion.hot.updateSettings({
                    colHeaders: data.Headers,
                    data: data.Datos,
                    columns: data.ColsConfig,
                    hiddenColumns: {
                        columns: [0],
                        indicators: true
                    }
                });
            },
            error: function (err) {
                gl.loadGlobal.css("display", "none");
                alert("ERROR");
            }
        });
    }


    convencion.container = document.getElementById('tablaExcel');
    convencion.hot = new Handsontable(convencion.container, {
        //data: data, //Handsontable.helper.createSpreadsheetData(1000, 1000),
        startRows: 30,
        startCols: 20,
        width: "100%",
        height: 480,
        rowHeaders: true,
        colHeaders: true,
        filters: true,
        dropdownMenu: true,
        allowInsertRow: true,
        allowInsertColumn: false,
        allowRemoveColumn: false,
        allowRemoveRow: false,
        afterChange: function (change, source) {
            if (source === 'loadData') {
                return; //don't save this change
            }
            console.log("Guardamos los datos " + source);
            console.log(change);
        }
    });

    convencion.loadData();

    //ajax('/HandsonTable/GetData', 'GET', '', function (res) {
    //    var data = JSON.parse(res.response);

    //    console.log('Data loaded');
    //});
});


function customDropdownRenderer(instance, td, row, col, prop, value, cellProperties) {
    var selectedId;
    var optionsList = cellProperties.chosenOptions.data;

    var values = (value + "").split(",");
    var value = [];
    for (var index = 0; index < optionsList.length; index++) {
        if (values.indexOf(optionsList[index].id + "") > -1) {
            selectedId = optionsList[index].id;
            value.push(optionsList[index].label);
        }
    }
    value = value.join(", ");

    Handsontable.TextCell.renderer.apply(this, arguments);
}