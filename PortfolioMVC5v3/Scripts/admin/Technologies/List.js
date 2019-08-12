$(function () {
    initGrid();

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

    $("#AddTechnologyBtn").on("click",
        function () {
            editTechnology(0);
        });
});

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
                location.reload();
            });
        },
        error: function () {
            window.Swal.fire({
                title: 'Błąd!',
                text: 'Usuwanie nie powiodło się!',
                type: 'error'
            }).then(() => {
                location.reload();
            });
        }
    });
}

function editTechnology(id) {
    location.href = `/Technologies/Card?id=${id}`;
}

function initGrid() {
    $("#grid").kendoGrid({
        dataSource: window.model,
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
            field: "TechnologyId",
            title: "Id",
            width: 50
        }, {
            field: "KnowledgeLevel",
            title: "Poziom znajomości",
            template: '#=window.getTypeText(KnowledgeLevel)#',
            width: 250
        }, {
            field: "Name",
            title: "Nazwa"
        }, {
            field: "ShowInAboutMePage",
            title: "Pokazuj w o mnie",
            template: '<input type="checkbox" #= ShowInAboutMePage ? "checked=checked" : "" # disabled="disabled" ></input>',
            width: 130

        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>',
            width: 130
        },
        {
            field: "TechnologyId",
            title: "Akcje",
            template: '<button name="EditTechnologyBtn" class="btn btn-sm k-primary mr-2" data-id="#=TechnologyId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveTechnologyBtn" class="btn btn-sm btn-danger" data-id="#=TechnologyId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200
        }]
    });
}