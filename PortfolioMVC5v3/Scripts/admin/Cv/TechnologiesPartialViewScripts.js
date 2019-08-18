$(function () {
    initVeryWellKnowTechnologiesGrid();
    initWellKnowTechnologiesGrid();
    initKnowTechnologiesGrid();
    initDropDownList();

    $("#AddTechnologyToCvBtn").on("click",
        function () {
            const technologyId = parseInt($("#AddTechnologyToCvDropDownList").data("kendoDropDownList").value());
            addTechnologyToCv(technologyId);
        });

    function addTechnologyToCv(technologyId) {
        if (isNaN(technologyId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać technologie do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/Technologies/AddTechnologyToCv/`,
            data: {
                technologyId: technologyId
            },
            success: function () {
                reloadGrids();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Dodawanie technologi nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function removeTechnologyFromCv(technologyId) {
        if (isNaN(technologyId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać technologie do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/Technologies/RemoveTechnologyFromCv/`,
            data: {
                technologyId: technologyId
            },
            success: function () {
                reloadGrids();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Usuwanie technologi nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function initVeryWellKnowTechnologiesGrid() {
        var grid = $("#VeryWellKnowTechnologiesGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Technologies/GetVeryWellKnowTechnologies",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function() {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#TechnologiesRowTemplate").html()),
            columns: [
                "Technologie"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#VeryWellKnowTechnologiesGrid tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#VeryWellKnowTechnologiesGrid").data().kendoGrid.dataSource.data()[oldIndex].TechnologyId;
                const newPositionProjectId = $("#VeryWellKnowTechnologiesGrid").data().kendoGrid.dataSource.data()[newIndex].TechnologyId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/Technologies/ReorderTechnologiesPositionsInCv/`,
                    data: {
                        oldPositionProjectId: oldPositionProjectId,
                        newPositionProjectId: newPositionProjectId
                    },
                    success: function () {
                        reloadGrids();
                    },
                    error: function () {

                    }
                });


            }
        });

    }

    function initWellKnowTechnologiesGrid() {
        var grid = $("#WellKnowTechnologiesGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Technologies/GetWellKnowTechnologies",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function () {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#TechnologiesRowTemplate").html()),
            columns: [
                "Technologie"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#WellKnowTechnologiesGrid tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#WellKnowTechnologiesGrid").data().kendoGrid.dataSource.data()[oldIndex].TechnologyId;
                const newPositionProjectId = $("#WellKnowTechnologiesGrid").data().kendoGrid.dataSource.data()[newIndex].TechnologyId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/Technologies/ReorderTechnologiesPositionsInCv/`,
                    data: {
                        oldPositionProjectId: oldPositionProjectId,
                        newPositionProjectId: newPositionProjectId
                    },
                    success: function () {
                        reloadGrids();
                    },
                    error: function () {

                    }
                });


            }
        });

    }

    function initKnowTechnologiesGrid() {
        var grid = $("#KnowTechnologiesGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Technologies/GetKnowTechnologies",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function () {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#TechnologiesRowTemplate").html()),
            columns: [
                "Technologie"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#KnowTechnologiesGrid tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#KnowTechnologiesGrid").data().kendoGrid.dataSource.data()[oldIndex].TechnologyId;
                const newPositionProjectId = $("#KnowTechnologiesGrid").data().kendoGrid.dataSource.data()[newIndex].TechnologyId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/Technologies/ReorderTechnologiesPositionsInCv/`,
                    data: {
                        oldPositionProjectId: oldPositionProjectId,
                        newPositionProjectId: newPositionProjectId
                    },
                    success: function () {
                        reloadGrids();
                    },
                    error: function () {

                    }
                });


            }
        });

    }

    function reloadGrids() {
        $("#VeryWellKnowTechnologiesGrid").data("kendoGrid").dataSource.read();
        $("#VeryWellKnowTechnologiesGrid").data("kendoGrid").refresh();
        $("#WellKnowTechnologiesGrid").data("kendoGrid").dataSource.read();
        $("#WellKnowTechnologiesGrid").data("kendoGrid").refresh();
        $("#KnowTechnologiesGrid").data("kendoGrid").dataSource.read();
        $("#KnowTechnologiesGrid").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddTechnologyToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddTechnologyToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddTechnologyToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz technologie do dodania...",
            noDataTemplate: "Brak technologi do dodania",
            dataTextField: "Name",
            dataValueField: "TechnologyId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/Technologies/GetTechnologiesNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".removeFromCvTechnology").on("click",
            function() {
                const technologyId = $(this).data("id");
                removeTechnologyFromCv(technologyId);
            });
    }

});