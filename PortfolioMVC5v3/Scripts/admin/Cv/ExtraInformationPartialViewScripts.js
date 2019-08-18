$(function () {
    initForeignLanguagesGrid();
    initInterestsGrid();
    initAdditionalSkillsGrid();
    initDropDownList();

    $("#AddExtraInformationToCvBtn").on("click",
        function () {
            const extraInformationId = parseInt($("#AddExtraInformationToCvDropDownList").data("kendoDropDownList").value());
            addExtraInformationToCv(extraInformationId);
        });

    function addExtraInformationToCv(extraInformationId) {
        if (isNaN(extraInformationId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz informacje do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/ExtraInformations/AddExtraInformationToCv/`,
            data: {
                extraInformationId: extraInformationId
            },
            success: function () {
                reloadGrids();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Dodawanie informacji nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function removeExtraInformationFromCv(extraInformationId) {
        if (isNaN(extraInformationId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać informacje do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/ExtraInformations/RemoveExtraInformationFromCv/`,
            data: {
                extraInformationId: extraInformationId
            },
            success: function () {
                reloadGrids();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Usuwanie informacji nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function initForeignLanguagesGrid() {
        var grid = $("#ForeignLanguagesGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/ExtraInformations/GetForeignLanguages",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Title: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function () {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#ExtraInformationRowTemplate").html()),
            columns: [
                "Dodatkowe informacje"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#ForeignLanguagesGrid tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#ForeignLanguagesGrid").data().kendoGrid.dataSource.data()[oldIndex].ExtraInformationId;
                const newPositionProjectId = $("#ForeignLanguagesGrid").data().kendoGrid.dataSource.data()[newIndex].ExtraInformationId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/ExtraInformations/ReorderExtraInformationPositionsInCv/`,
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

    function initInterestsGrid() {
        var grid = $("#InterestsGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/ExtraInformations/GetInterests",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Title: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function () {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#ExtraInformationRowTemplate").html()),
            columns: [
                "Dodatkowe informacje"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#InterestsGrid tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#InterestsGrid").data().kendoGrid.dataSource.data()[oldIndex].ExtraInformationId;
                const newPositionProjectId = $("#InterestsGrid").data().kendoGrid.dataSource.data()[newIndex].ExtraInformationId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/ExtraInformations/ReorderExtraInformationPositionsInCv/`,
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

    function initAdditionalSkillsGrid() {
        var grid = $("#AdditionalSkillsGrid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/ExtraInformations/GetAdditionalSkills",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Title: { type: "string" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function () {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#ExtraInformationRowTemplate").html()),
            columns: [
                "Dodatkowe informacje"
            ]
        }).data("kendoGrid");

        grid.table.kendoSortable({
            filter: ">tbody >tr",
            hint: $.noop,
            cursor: "move",
            placeholder: function (element) {
                return element.clone().addClass("k-state-hover").css("opacity", 0.65);
            },
            container: "#AdditionalSkillsGrid tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionProjectId = $("#AdditionalSkillsGrid").data().kendoGrid.dataSource.data()[oldIndex].ExtraInformationId;
                const newPositionProjectId = $("#AdditionalSkillsGrid").data().kendoGrid.dataSource.data()[newIndex].ExtraInformationId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/ExtraInformations/ReorderExtraInformationPositionsInCv/`,
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
        $("#ForeignLanguagesGrid").data("kendoGrid").dataSource.read();
        $("#ForeignLanguagesGrid").data("kendoGrid").refresh();
        $("#InterestsGrid").data("kendoGrid").dataSource.read();
        $("#InterestsGrid").data("kendoGrid").refresh();
        $("#AdditionalSkillsGrid").data("kendoGrid").dataSource.read();
        $("#AdditionalSkillsGrid").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddExtraInformationToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddExtraInformationToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddExtraInformationToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz informacje do dodania...",
            noDataTemplate: "Brak informacji do dodania",
            dataTextField: "Title",
            dataValueField: "ExtraInformationId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/ExtraInformations/GetExtraInformationNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".removeFromCvExtraInformation").on("click",
            function () {
                const extraInformationId = $(this).data("id");
                removeExtraInformationFromCv(extraInformationId);
            });
    }

});