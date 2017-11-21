
$(function () {
    //punteros al DOM.
    convencion.$btnFilter = $(".btnFilter");
    convencion.pagination = {};
    convencion.pagination.page = 1;
    convencion.pagination.NoMorePages = false;
    convencion.pagination.data = null;
    convencion.pagination.currentFilter = null;





    //Eventos objetos del DOM.
    convencion.$btnFilter.on("click", function (e) {
        e.preventDefault();
        var $this = $(this);
        var id = $this.data("id");
        $(".navbar-top-links>li.active").removeClass("active");
        $this.parent().addClass("active");
        convencion.pagination.currentFilter = null;
        convencion.loadData(id);
    });



    ///Funcion que carga los datos en la handsontable dado un identificador.
    convencion.loadData = function (id) {
        gl.loadGlobal.css("display", "block");

        if (convencion.pagination.currentFilter !== id) {
            convencion.pagination.page = 1;
            convencion.pagination.currentFilter = id;
            convencion.pagination.NoMorePages = false;
            convencion.pagination.data = null;
            convencion.hot.updateSettings({
                data: []
            });
        }
        if (!convencion.pagination.NoMorePages) {
            $.ajax({
                url: '/Convencion/Datos/' + convencion.Id + '?filtroId=' + convencion.pagination.currentFilter + '&page=' + convencion.pagination.page,
                type: 'POST',
                cache: false,
                dataType: 'json',
                success: function (data) {
                    if (data.Datos == null || data.Datos.length === 0) {
                        convencion.pagination.NoMorePages = true
                    } else {
                        if (convencion.pagination.data == null) {
                            convencion.pagination.data = data.Datos;
                        } else {
                            convencion.pagination.data = convencion.pagination.data.concat(data.Datos);

                        }
                        gl.loadGlobal.hide();
                        //console.log(data.ColsConfig);
                        
                        convencion.hot.updateSettings({
                            colHeaders: data.Headers,
                            data: convencion.pagination.data,
                            columns: data.ColsConfig
                            //,hiddenColumns: {
                            //    columns: [0],
                            //    indicators: true
                            //}
                        });
                    }
                },
                error: function (err) {
                    gl.loadGlobal.css("display", "none");
                    alert("ERROR");
                }
            });
        }
    }

    convencion.timeoutScroll = null;
    convencion.container = document.getElementById('tablaExcel');
    convencion.hot = new Handsontable(convencion.container, {
        //data: data, //Handsontable.helper.createSpreadsheetData(1000, 1000),
        startRows: 30,
        startCols: 20,
        width: "100%",
        height: function () {
            var wH = $(window).height();
            var hHeader = $(".navbar.navbar-default.navbar-static-top").outerHeight(true);
            var hH3 = $("#excelH3").outerHeight(true);
            var h = wH - (hH3 + hHeader);
            return h - 100;
            //return 420;
        },
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
        },
        afterScrollVertically: function () {
            if (!convencion.pagination.NoMorePages) {
                if (convencion.timeoutScroll) {
                    clearTimeout(convencion.timeoutScroll);
                }
                convencion.timeoutScroll = setTimeout(function () {
                    var $wtHolder = $(".wtHolder");
                    var sTop = $wtHolder[0].scrollTop;
                    var oH = $wtHolder[0].offsetHeight;
                    var sH = $wtHolder[0].scrollHeight;
                    //console.log(sH + " - " + oH + " - " + sTop);
                    if (oH + sTop >= sH) {
                        ++convencion.pagination.page;
                        convencion.loadData(convencion.pagination.currentFilter);

                    }
                }, 200);
            }
        }
    });

    convencion.loadData();

    //ajax('/HandsonTable/GetData', 'GET', '', function (res) {
    //    var data = JSON.parse(res.response);

    //    console.log('Data loaded');
    //});
});


//function customDropdownRenderer(instance, td, row, col, prop, value, cellProperties) {
//    var selectedId;
//    var optionsList = cellProperties.chosenOptions.data;

//    var values = (value + "").split(",");
//    var value = [];
//    for (var index = 0; index < optionsList.length; index++) {
//        if (values.indexOf(optionsList[index].id + "") > -1) {
//            selectedId = optionsList[index].id;
//            value.push(optionsList[index].label);
//        }
//    }
//    value = value.join(", ");

//    Handsontable.TextCell.renderer.apply(this, arguments);
//}