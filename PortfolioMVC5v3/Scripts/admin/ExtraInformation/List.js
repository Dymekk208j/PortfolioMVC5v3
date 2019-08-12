$(function () {
    initGrid();

    $("button[name='RemoveExtraInformationBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            removeExtraInformation(id);
        });

    $("button[name='EditExtraInformationBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editExtraInformation(id);
        });

    $("#AddExtraInformationBtn").on("click",
        function () {
            editExtraInformation(0);
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

function removeExtraInformation(id) {
    $.ajax({
        type: "GET",
        url: `/ExtraInformations/RemoveExtraInformation/${id}`,
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

function editExtraInformation(id) {
    location.href = `/ExtraInformations/Card?id=${id}`;
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
            field: "ExtraInformationId",
            title: "Id",
            width: 50
        }, {
            field: "Type",
            title: "Typ",
            template: '#=window.getTypeText(Type)#',
            width: 200
        }, {
            field: "Title",
            title: "Nazwa"
        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>',
            width: 150
        },
        {
            field: "ExtraInformationId",
            title: "Akcje",
            template: '<button name="EditExtraInformationBtn" class="btn btn-sm k-primary mr-2" data-id="#=ExtraInformationId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveExtraInformationBtn" class="btn btn-sm btn-danger" data-id="#=ExtraInformationId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200
        }]
    });
}