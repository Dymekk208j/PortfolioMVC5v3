$(function () {
    initGrid();
    initDropDownList();

    $("#AddAchievementToCv").on("click",
        function () {
            const achievementId = parseInt($("#AddAchievementToCvDropDownList").data("kendoDropDownList").value());
            addAchievementToCv(achievementId);
        });

    function addAchievementToCv(achievementId) {
        if (isNaN(achievementId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać osiągnięcie do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/Achievements/AddAchievementToCv/`,
            data: {
                achievementId: achievementId
            },
            success: function () {
                reloadGridData();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Dodawanie osiągnięcia nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function removeAchievementFromCv(achievementId) {
        if (isNaN(achievementId)) {
            window.Swal.fire({
                title: "Błąd!",
                text: "Musisz wybrać osiągnięcie do dodania!",
                type: "error",
                heightAuto: false
            });

            return false;
        }

        $.ajax({
            type: "GET",
            url: `/Achievements/RemoveAchievementFromCv/`,
            data: {
                achievementId: achievementId
            },
            success: function () {
                reloadGridData();
                reloadDropDownListData();
            },
            error: function () {
                window.Swal.fire({
                    title: "Błąd!",
                    text: "Usuwanie osiągnięcia nie powiodło się!",
                    type: "error",
                    heightAuto: false
                });

                return false;
            }
        });

        return true;
    }

    function initGrid() {
        var grid = $("#AchievementList").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "/Achievements/GetAchievementsToShowInCv",
                        dataType: "json"
                    }
                },
                schema: {
                    model: {
                        fields: {
                            Title: { type: "string" },
                            Description: { type: "string" },
                            Date: { type: "Date", format: "{0:dd/MM/yyyy}" }
                        }
                    }
                },
                pageSize: 16
            },
            dataBound: function() {
                initRemoveFromCvButtons();
            },
            scrollable: false,
            rowTemplate: window.kendo.template($("#AchievementRowTemplate").html()),
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
            container: "#AchievementList tbody",
            change: function (e) {
                var skip = grid.dataSource.skip(),
                    oldIndex = e.oldIndex + skip,
                    newIndex = e.newIndex + skip,
                    dataItem = grid.dataSource.getByUid(e.item.data("uid"));


                const oldPositionAchievementId = $("#AchievementList").data().kendoGrid.dataSource.data()[oldIndex].AchievementId;
                const newPositionAchievementId = $("#AchievementList").data().kendoGrid.dataSource.data()[newIndex].AchievementId;


                grid.dataSource.remove(dataItem);
                grid.dataSource.insert(newIndex, dataItem);

                $.ajax({
                    type: "GET",
                    url: `/Achievements/ReorderAchievementsPositionsInCv/`,
                    data: {
                        oldPositionAchievementId: oldPositionAchievementId,
                        newPositionAchievementId: newPositionAchievementId
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
        $("#AchievementList").data("kendoGrid").dataSource.read();
        $("#AchievementList").data("kendoGrid").refresh();
        initRemoveFromCvButtons();
    }

    function reloadDropDownListData() {
        $("#AddAchievementToCvDropDownList").data("kendoDropDownList").dataSource.read();
        $("#AddAchievementToCvDropDownList").data("kendoDropDownList").refresh();
    }

    function initDropDownList() {
        $("#AddAchievementToCvDropDownList").kendoDropDownList({
            optionLabel: "Wybierz osiągnięcie do dodania...",
            noDataTemplate: "Brak osiągnięć do dodania",
            dataTextField: "Title",
            dataValueField: "AchievementId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: "/Achievements/GetAchievementsNotShownInCv"
                    }
                }
            }
        });
    }

    function initRemoveFromCvButtons() {
        $(".removeFromCvAchievement").on("click",
            function() {
                const achievementId = $(this).data("id");
                removeAchievementFromCv(achievementId);
            });
    }

});