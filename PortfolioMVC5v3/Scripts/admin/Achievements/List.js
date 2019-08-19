$(function () {
    initGrid();

    $("#AddAchievementBtn").on("click",
        function () {
            editAchievement(0);
        });
});

function initButtons() {
    $("button[name='RemoveAchievementBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            removeAchievement(id);
        });

    $("button[name='EditAchievementBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editAchievement(id);
        });
}

function removeAchievement(id) {
    $.ajax({
        type: "GET",
        url: `/Achievements/RemoveAchievement/${id}`,
        success: function () {
            window.Swal.fire({
                title: 'Sukces!',
                text: 'Poprawnie usunięto!',
                type: 'success'
            }).then(() => {
                $("#grid").data("kendoGrid").dataSource.read();
                $("#grid").data("kendoGrid").refresh();
            });
        },
        error: function () {
            window.Swal.fire({
                title: 'Błąd!',
                text: 'Usuwanie nie powiodło się!',
                type: 'error'
            }).then(() => {
                $("#grid").data("kendoGrid").dataSource.read();
                $("#grid").data("kendoGrid").refresh();
            });
        }
    });
}

function editAchievement(id) {
    location.href = `/Achievements/Card?id=${id}`;
}

function initGrid() {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: "/Achievements/GetAllAchievements",
                    dataType: "json"
                }
            }
        },
        dataBound: function () {
            initButtons();
        },
        height: "94%",
        sortable: true,
        filterable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5,
            pageSize: 20
        },
        columns: [{
            field: "Title",
            title: "Nazwa"
        },
        {
            field: "Description",
            title: "Opis"
        },
        {

            field: "Date",
            title: "Data osiągnięcia",
            template: "#= kendo.toString(kendo.parseDate(Date, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"

        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input class="k-checkbox" id="checkbox#:AchievementId#" type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkbox#:AchievementId#"></label>',
            width: 150
        },
        {
            field: "AchievementId",
            title: "Akcje",
            template: '<button name="EditAchievementBtn" class="btn btn-sm btn-primary mr-2" data-id="#=AchievementId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveAchievementBtn" class="btn btn-sm btn-danger" data-id="#=AchievementId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200
        }]
    });
}