$(function () {
    initGrid();
    initDropDownList();

    $("#AddEducationToCv").on("click",
        function () {
            const educationId = parseInt($("#AddEducationToCvDropDownList").data("kendoDropDownList").value());
            addEducationToCv(educationId);
        });

    function addEducationToCv(educationId) {
        if (isNaN(educationId)) {
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
            url: `/Education/AddEducationToCv/`,
            data: {
                educationId: educationId
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

    function removeEducationFromCv(educationId) {
        if (isNaN(educationId)) {
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
            url: `/Education/RemoveEducationFromCv/`,
            data: {
                educationId: educationId
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
        var grid = $("#EducationList").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Education/GetEducationsToShowInCv",
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
            rowTemplate: window.kendo.template($("#EducationRowTemplate").html()),
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
            container: "#EducationList tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionEducationId = $("#EducationList").data().kendoGrid.dataSource.data()[oldIndex].EducationId;
                const newPositionEducationId = $("#EducationList").data().kendoGrid.dataSource.data()[newIndex].EducationId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/Education/ReorderEducationsPositionsInCv/`,
                    data: {
                        oldPositionEducationId: oldPositionEducationId,
                        newPositionEducationId: newPositionEducationId
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
        $("#EducationList").data("kendoGrid").dataSource.read();
        $("#EducationList").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddEducationToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddEducationToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddEducationToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz wykształcenie do dodania...",
            noDataTemplate: "Brak wykształcenia do dodania",
            dataTextField: "SchoolName",
            dataValueField: "EducationId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/Education/GetEducationsNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".removeFromCvEducation").on("click",
            function() {
                const educationId = $(this).data("id");
                removeEducationFromCv(educationId);
            });
    }

});