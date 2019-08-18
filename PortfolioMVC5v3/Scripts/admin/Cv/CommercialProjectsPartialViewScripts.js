$(function () {
    initGrid();
    initDropDownList();

    $("#AddCommercialProjectToCv").on("click",
        function () {
            const projectId = parseInt($("#AddCommercialProjectToCvDropDownList").data("kendoDropDownList").value());
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
        var grid = $("#CommercialProjectList").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Project/GetCommercialProjectsToShowInCv",
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
            rowTemplate: window.kendo.template($("#CommercialProjectRowTemplate").html()),
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
            container: "#CommercialProjectList tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    data = grid.dataSource.data(),
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#CommercialProjectList").data().kendoGrid.dataSource.data()[oldIndex].ProjectId;
                const newPositionProjectId = $("#CommercialProjectList").data().kendoGrid.dataSource.data()[newIndex].ProjectId;


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
        $("#CommercialProjectList").data("kendoGrid").dataSource.read();
        $("#CommercialProjectList").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddCommercialProjectToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddCommercialProjectToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddCommercialProjectToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz projekt do dodania...",
            noDataTemplate: "Brak projektów do dodania",
            dataTextField: "Title",
            dataValueField: "ProjectId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/Project/GetCommercialProjectsNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".outlineRemoveBtn").on("click",
            function() {
                const projectId = $(this).data("id");
                removeProjectFromCv(projectId);
            });
    }

});