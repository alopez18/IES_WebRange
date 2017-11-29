$(function () {
    //punteros al DOM.
    convencion.$btnFilter = $(".btnFilter");
    convencion.$btnPaginacion = $(".btn-paginacion");
    convencion.$btnMarca = $(".btn-marca");
    convencion.$btn_nivel = $(".btn-nivel");
    convencion.$aRefresh = $("#aRefresh");


    convencion.$btnPaginacion.off().on("click", function (e) {
        e.preventDefault();
        $(".dropdown-menu.dropdown-messages.filtrosPaginacion > li.active").removeClass("active");
        var $this = $(this);
        $this.parent().addClass("active");
        $("#spnPaginacion").text($this.text());

        var pageSize = $this.data("val");
        convencion.loadData(convencion.pagination.currentFilter, convencion.pagination.currentLevel, pageSize);
    });


    convencion.$btnMarca.off().on("click", function (e) {
        e.preventDefault();
        $(".dropdown-menu.dropdown-messages.filtrosMarca > li.active").removeClass("active");
        var $this = $(this);
        $this.parent().addClass("active");
        $("#spnFiltroMarca").text($this.text());
        var field = $this.data("field");
        var marca = $this.data("val");
        var oAux = {
            campo: field,
            valor: marca
        };
        convencion.pagination.currentFiltersOwn = [];
        convencion.pagination.currentFiltersOwn.push(oAux);
        convencion.pagination.NewFilter = true;
        convencion.loadData(convencion.pagination.currentFilter, convencion.pagination.currentLevel, convencion.pagination.pageSize);
    });

    convencion.$btn_nivel.off().on("click", function (e) {
        e.preventDefault();
        $(".dropdown-menu.dropdown-messages.filtrosNivel > li.active").removeClass("active");
        var $this = $(this);
        $this.parent().addClass("active");
        $("#spnFiltroNivel").text($this.text());
        var idLevel = $this.data("val");
        convencion.pagination.currentLevel = null;
        convencion.loadData(convencion.pagination.currentFilter, idLevel, convencion.pagination.pageSize);
    });

    //Eventos objetos del DOM.
    convencion.$btnFilter.off().on("click", function (e) {
        e.preventDefault();
        var $this = $(this);
        var id = $this.data("id");
        $(".dropdown-menu.dropdown-messages.filtros > li.active").removeClass("active");
        $this.parent().addClass("active");
        $("#spnFiltro").text($this.text());
        convencion.pagination.currentFilter = null;
        convencion.loadData(id, convencion.pagination.currentLevel, convencion.pagination.pageSize);
    });

    convencion.$aRefresh.off().on("click", function (e) {
        e.preventDefault();
        convencion.loadData(convencion.pagination.currentFilter, convencion.pagination.currentLevel, convencion.pagination.pageSize);
    })



    ///Funcion que carga los datos en la handsontable dado un identificador.


    //convencion.timeoutScroll = null;
    convencion.container = document.getElementById('tablaExcel');
    convencion.hot = new Handsontable(convencion.container, {
        //data: data, //Handsontable.helper.createSpreadsheetData(1000, 1000),
        startRows: 30,
        //startCols: 20,
        width: "100%",
        height: convencion.calculeHTOHeight(false),
        rowHeaders: true,
        colHeaders: convencion.table.columnsHeaders,
        columns: convencion.table.columnsConfig,
        colWidths: 100,
        filters: true,
        dropdownMenu: true,
        manualColumnResize: true,
        manualRowResize: true,
        contextMenu: true,
        allowInvalid: false,
        allowInsertRow: true,
        allowInsertColumn: false,
        allowRemoveColumn: false,
        allowRemoveRow: false,
        hiddenColumns: {
            columns: [0, 1, 2],
            indicators: true
        },
        afterChange: function (changes, source) {
            if (source === 'loadData') {
                return; //don't save this change
            }
            var $this = $(this);
            switch (source) {
                case "edit":
                case "CopyPaste.paste":
                    for (var i = 0; i < changes.length; i++) {
                        var rowData = convencion.hot.getDataAtRow(changes[i][0]);
                        //var colIndex = GetColFromName(changes[i][1]);
                        //var column = convencion.table.columnsConfig[colIndex];
                        var cambios = [];
                        var idRow = rowData[0];
                        var DtoUpdate = {
                            Id: idRow,
                            Campo: changes[i][1],//changes[i][1]
                            Valor: changes[i][3]
                        };
                        if (changes[i][2] !== changes[i][3]) {
                            cambios.push(DtoUpdate);
                        }
                    }
                    if (cambios.length > 0) {
                        $.ajax({
                            url: '/Articulo/Update',
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(cambios),
                            dataType: 'json',
                            success: function (data) {
                                if (!data.Success) {
                                    alert(data.ErrorTecnico);
                                }
                            },
                            error: function (err) {
                                console.log(err);
                                //alert("Ha fallado la recuperación de datos.\r\nConsulte el log para más información.");
                            }
                        });
                    }
                    break;
                default:
                    console.log("Metodo de edición no controlado: '" + source + "'.");
                    break;
            }
            //console.log("Guardamos los datos " + source);
            //console.log(changes);
        }
        //,afterScrollVertically: function () {
        //    /*
        //    Para que no se solapen los eventos de scroll y carga de datos utilizamos
        //    el flag 'convencion.pagination.scrollCheck' para que no visualize el scroll en caso de estar cargarndo datos
        //    esto es debido a que al vaciar la tabla se establece que scroll está al final e intenta cargar la segunda página.
        //    */
        //    if (!convencion.pagination.NoMorePages && convencion.pagination.scrollCheck) {
        //        if (convencion.timeoutScroll) {
        //            clearTimeout(convencion.timeoutScroll);
        //        }
        //        convencion.timeoutScroll = setTimeout(function () {
        //            var $wtHolder = $(".wtHolder");
        //            var sTop = $wtHolder[0].scrollTop;
        //            var oH = $wtHolder[0].offsetHeight;
        //            var sH = $wtHolder[0].scrollHeight;
        //            //console.log(sH + " - " + oH + " - " + sTop);
        //            if (oH + sTop >= sH) {
        //                ++convencion.pagination.page;
        //                convencion.loadData(convencion.pagination.currentFilter, convencion.pagination.currentLevel, convencion.pagination.pageSize);
        //            }
        //        }, 200);
        //    }
        //}
    });

    //convencion.loadData();
    gl.loadGlobal.css("display", "none");



    var toResizeHOT = null;
    $(window).resize(function () {
        clearTimeout(toResizeHOT);
        toResizeHOT = setTimeout(function () {
            var h = convencion.calculeHTOHeight(true);
            convencion.hot.updateSettings({
                height: h
            });
        }, 300);
    });
});

convencion.calculeHTOHeight = function (withFooter) {
    var wH = $(window).height();
    var hHeader = $(".navbar.navbar-default.navbar-static-top").outerHeight(true);
    var hFooter = 0;
    if (withFooter) {
        hFooter = $("footer.fEditor").outerHeight(true);
    }
    var h = wH - (hHeader + hFooter);
    return h;
    //return 420;
}

convencion.loadData = function (idFilter, idLevel, pageSize) {
    gl.loadGlobal.css("display", "block");

    //Si hay cambio de filtro reseteamos las propiedades de la tabla.
    if (convencion.pagination.currentFilter !== idFilter
        || convencion.pagination.currentLevel !== idLevel
        || convencion.pagination.pageSize !== pageSize
        || convencion.pagination.NewFilter) {
        //convencion.pagination.scrollCheck = false; //Lo ponemos a false para que al vaciar la tabla no salte el final de scroll.
        convencion.pagination.page = 1;
        convencion.pagination.currentFilter = idFilter;
        convencion.pagination.currentLevel = idLevel;
        convencion.pagination.NoMorePages = false;
        convencion.pagination.data = null;
        convencion.pagination.pageSize = pageSize;
        convencion.hot.updateSettings({
            data: []
        });
    }
    if (!convencion.pagination.NoMorePages) {
        var datos = {
            filtroId: convencion.pagination.currentFilter,
            nivel: convencion.pagination.currentLevel,
            page: convencion.pagination.page,
            pageSize: convencion.pagination.pageSize,
            filtros: convencion.pagination.currentFiltersOwn
        }

        $.ajax({
            url: '/Convencion/Datos/' + convencion.Id,
            data: datos,
            type: 'POST',
            cache: false,
            dataType: 'json',
            success: function (data) {
                if (data.Datos === null || data.Datos.length === 0) {
                    convencion.pagination.NoMorePages = true
                } else {
                    convencion.pagination.loadPagination(data.Total, convencion.pagination.pageSize, convencion.pagination.page, data.Datos.length);
                    convencion.pagination.data = data.Datos;
                    //if (convencion.pagination.data === null) {
                    //    convencion.pagination.data = data.Datos;
                    //} else {
                    //    convencion.pagination.data = convencion.pagination.data.concat(data.Datos);
                    //}
                    gl.loadGlobal.hide();
                    //console.log(data.ColsConfig);
                    convencion.table.columnsConfig = data.ColsConfig;
                    convencion.hot.updateSettings({
                        colHeaders: data.Headers,
                        colWidths: undefined,
                        data: convencion.pagination.data,
                        columns: data.ColsConfig

                    });
                    //convencion.pagination.scrollCheck = true;
                }
            },
            error: function (err) {
                gl.loadGlobal.css("display", "none");
                console.log(err);
                alert("Ha fallado la recuperación de datos.\r\nConsulte el log para más información.");
            }
        });
    }
}


convencion.pagination.loadPagination = function (totalItems, pageSize, actualPage, numElements) {
    if (!convencion.pagination.$liPagingModel) {//Si no se ha cargado aun el modelo, lo cargamos.
        var $liPagingModel = $("#liPagingModel");
        convencion.pagination.$liPagingModel = $liPagingModel.clone();
        $liPagingModel.remove();
    }
    var howMany = 4;
    var pages = totalItems % pageSize === 0 ? (totalItems / pageSize) : (parseInt(totalItems / pageSize) + 1);
    if (pages < howMany) howMany = pages;
    var pageMin, pageMax;
    pageMin = actualPage - 2;
    if (pageMin < 1) {
        pageMin = 1;
    }
    pageMax = pageMin + howMany;
    if (pageMax > pages) {
        pageMax = pages;
    }
    var elements = [];
    $(".liPagingCloned").remove();
    for (var i = pageMin; i <= pageMax; i++) {
        var $element = convencion.pagination.$liPagingModel.clone();
        $element.removeAttr("id");
        var $a = $("a", $element);
        $a.text(i);
        $a.data("page", i);
        $a.addClass("aPaging");

        if (i === actualPage) {
            $element.addClass("active");
        }
        $element.addClass("liPagingCloned");
        elements.push($element);
    }

    $("#liPagingPrev").after(elements);


    //Before
    var pagebefore = actualPage + 1;
    if (pagebefore < 1) pagebefore = 1;
    $("#aPagingBefore").data("page", pagebefore);
    //next
    var pageNext = actualPage + 1;
    if (pageNext > pages) pageNext = pages;
    $("#aPagingNext").data("page", pageNext);
    //Last
    $("#aPagingLast").data("page", pages);



    //Texto de paginas
    $("#totalEls").text(totalItems);
    //var pageSizeCurrent = (parseInt(pageSize) > parseInt(totalItems)) ? totalItems : pageSize;
    $("#pageEls").text(numElements);

    //Añadimos eventos
    $(".aPaging").off().on("click", function (e) {
        e.preventDefault();
        var $this = $(this);
        var page = $this.data("page");
        convencion.pagination.page = page;
        convencion.loadData(convencion.pagination.currentFilter, convencion.pagination.currentLevel, convencion.pagination.pageSize);
    });

    var $footer = $("footer.fEditor");
    var fVisible = $footer.is(":visible")
    $footer.show();
    if (!fVisible) {
        var h = convencion.calculeHTOHeight(true);
        convencion.hot.updateSettings({
            height: h
        });
    }
}



//function GetColFromName(name) {
//    var n_cols = convencion.hot.countCols(); //convecion.$editorTableContainer.handsontable('countCols');
//    var i = 1;
//    for (i = 1; i <= n_cols; i++) {
//        if (name.toLowerCase() == convencion.hot.getColHeader(i).toLowerCase()) {
//            return i;
//        }
//    }
//    return -1; //return -1 if nothing can be found
//}