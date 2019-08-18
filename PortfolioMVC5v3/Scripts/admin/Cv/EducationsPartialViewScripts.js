$(function () {
    initGrid();
    initDropDownList();

    $("#AddEmploymentHistoryToCv").on("click",
        function () {
            const employmentHistoryId = parseInt($("#AddEmploymentHistoryToCvDropDownList").data("kendoDropDownList").value());
            addEmploymentHistoryToCv(employmentHistoryId);
        });

    function addEmploymentHistoryToCv(employmentHistoryId) {
        if (isNaN(employmentHistoryId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać wykształcenie do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/EmploymentHistory/AddEmploymentHistoryToCv/`,
            data: {
                employmentHistoryId: employmentHistoryId
            },
            success: function () {
                reloadGridData();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Dodawanie wykształcenia nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function removeEmploymentHistoryFromCv(employmentHistoryId) {
        if (isNaN(employmentHistoryId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać wykształcenie do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/EmploymentHistory/RemoveEmploymentHistoryFromCv/`,
            data: {
                employmentHistoryId: employmentHistoryId
            },
            success: function () {
                reloadGridData();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Usuwanie wykształcenia nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function initGrid() {
        var grid = $("#EmploymentHistoryList").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/EmploymentHistory/GetEmploymentHistoriesToShowInCv",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Position: { type: "string" },
                            CompanyName: { type: "string" },
                            CityOfEmployment: { type: "string" },
                            StartDate: { type: "Date", format: "{0:dd/MM/yyyy}" },
                            EndDate: { type: "Date", format: "{0:dd/MM/yyyy}" },
                            CurrentPlaceOfEmployment: { type: "boolean" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function() {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#EmploymentHistoryRowTemplate").html()),
            columns: [
                "Osiągnięcia"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#EmploymentHistoryList tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionEmploymentHistoryId = $("#EmploymentHistoryList").data().kendoGrid.dataSource.data()[oldIndex].EmploymentHistoryId;
                const newPositionEmploymentHistoryId = $("#EmploymentHistoryList").data().kendoGrid.dataSource.data()[newIndex].EmploymentHistoryId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/EmploymentHistory/ReorderEmploymentHistoriesPositionsInCv/`,
                    data: {
                        oldPositionEmploymentHistoryId: oldPositionEmploymentHistoryId,
                        newPositionEmploymentHistoryId: newPositionEmploymentHistoryId
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
        $("#EmploymentHistoryList").data("kendoGrid").dataSource.read();
        $("#EmploymentHistoryList").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddEmploymentHistoryToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddEmploymentHistoryToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddEmploymentHistoryToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz wykształcenie do dodania...",
            noDataTemplate: "Brak wykształcenia do dodania",
            dataTextField: "CompanyName",
            dataValueField: "EmploymentHistoryId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/EmploymentHistory/GetEmploymentHistoriesNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".removeFromCvEmploymentHistory").on("click",
            function() {
                const employmentHistoryId = $(this).data("id");
                removeEmploymentHistoryFromCv(employmentHistoryId);
            });
    }

});