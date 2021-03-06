﻿$(function () {
    initGrid();

    $("#AddIconBtn").on("click",
        function () {
            addIcon();
        });
});

window.getTypeText = function (id) {
    const data = [
        { text: 'Języki obce', value: 0 },
        { text: 'Dodatkowe umiejętności', value: 1 },
        { text: 'Zainteresowania', value: 2 }
    ];

    return data[id].text;
};

function initButtons() {
    $("button[name='RemoveIconBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            removeIcon(id);
        });
}

function removeIcon(id) {
    $.ajax({
        type: "GET",
        url: `/Icon/RemoveIcon/${id}`,
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

function addIcon() {
    location.href = `/Icon/AddIcon`;
}

function initGrid() {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: "/Icon/GetAllIcons",
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
            pageSize: 5
        },
        columns: [
            {
                title: "Podgląd",
                template: '<img src="/Assets/Icons/#=Guid##=FileName#" height="100" width="100" />',
                width: 110
            }, {
                field: "FileName",
                title: "Nazwa pliku",
                width: 250
            }, {
                field: "Guid",
                title: "Guid"
            },
            {
                field: "IconId",
                title: "Akcje",
                template: '<button name="RemoveIconBtn" class="btn btn-danger" data-id="#=IconId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
                width: 100
            }]
    });
}