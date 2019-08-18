$(function () {
    initGrid();
    initDropDownList();

    $("#AddNotCommercialProjectToCv").on("click",
        function () {
            const projectId = parseInt($("#AddNotCommercialProjectToCvDropDownList").data("kendoDropDownList").value());
            addProjectToCv(projectId);
        });

    function addProjectToCv(projectId) {
        if (isNaN(projectId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać projekt do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/Project/AddProjectToCv/`,
            data: {
                projectId: projectId
            },
            success: function () {
                reloadGridData();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Dodawanie projektu nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function removeProjectFromCv(projectId) {
        if (isNaN(projectId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać projekt do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/Project/removeProjectFromCv/`,
            data: {
                projectId: projectId
            },
            success: function () {
                reloadGridData();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Usuwanie projektu nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function initGrid() {
        var grid = $("#NotCommercialProjectList").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Project/GetNotCommercialProjectsToShowInCv",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Title: { type: "string" },
                            ShortDescription: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function() {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#NotCommercialProjectRowTemplate").html()),
            columns: [
                "Projekty komercyjne"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#NotCommercialProjectList tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#NotCommercialProjectList").data().kendoGrid.dataSource.data()[oldIndex].ProjectId;
                const newPositionProjectId = $("#NotCommercialProjectList").data().kendoGrid.dataSource.data()[newIndex].ProjectId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/Project/ReorderProjectsPositionsInCv/`,
                    data: {
                        oldPositionProjectId: oldPositionProjectId,
                        newPositionProjectId: newPositionProjectId
                    },
                    success: function () {
                        reloadGridData();
                    },
                    error: function () {

                    }
                });


            }
        });

    }

    function reloadGridData() {
        $("#NotCommercialProjectList").data("kendoGrid").dataSource.read();
        $("#NotCommercialProjectList").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddNotCommercialProjectToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddNotCommercialProjectToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddNotCommercialProjectToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz projekt do dodania...",
            noDataTemplate: "Brak projektów do dodania",
            dataTextField: "Title",
            dataValueField: "ProjectId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/Project/GetNotCommercialProjectsNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".removeFromCvNotCommercialProject").on("click",
            function() {
                const projectId = $(this).data("id");
                removeProjectFromCv(projectId);
            });
    }

});