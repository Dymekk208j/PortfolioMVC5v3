﻿$(function () {
    initGrid();
    
    $("#AddTechnologyBtn").on("click",
        function () {
            editTechnology(0);
        });
});

function initButtons() {
    $("button[name='RemoveTechnologyBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            removeTechnology(id);
        });

    $("button[name='EditTechnologyBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editTechnology(id);
        });
}

window.getTypeText = function (id) {
    const data = [
        { text: 'Brak', value: 0 },
        { text: 'Podstawowy poziom', value: 1 },
        { text: 'Średni poziom', value: 2 },
        { text: 'Dobry poziom', value: 3 },
        { text: 'Bardzo dobry poziom', value: 4 },
        { text: 'Perfekcyjny poziom', value: 5 }
    ];

    return data[id].text;
};


function removeTechnology(id) {
    $.ajax({
        type: "GET",
        url: `/Technologies/RemoveTechnology/${id}`,
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

function editTechnology(id) {
    location.href = `/Technologies/Card?id=${id}`;
}

function initGrid() {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: "/Technologies/GetAllTechnologies",
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
            pageSize: 12
        },
        columns: [{
            field: "Name",
            title: "Nazwa"
        }, {
            field: "KnowledgeLevel",
            title: "Poziom znajomości",
            template: '#=window.getTypeText(KnowledgeLevel)#',
            width: 250
        }, {
            field: "ShowInAboutMePage",
            title: "Pokazuj w o mnie",
            template: '<input class="k-checkbox" id="checkbox#:TechnologyId#" type="checkbox" #= ShowInAboutMePage ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkbox#:TechnologyId#"></label>',
            width: 130

        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input class="k-checkbox" id="checkboxB#:TechnologyId#" type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkboxB#:TechnologyId#"></label>',
            width: 130
        },
        {
            field: "TechnologyId",
            title: "Akcje",
            template: '<button name="EditTechnologyBtn" class="btn btn-sm btn-primary mr-2" data-id="#=TechnologyId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveTechnologyBtn" class="btn btn-sm btn-danger" data-id="#=TechnologyId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200
        }]
    });
}