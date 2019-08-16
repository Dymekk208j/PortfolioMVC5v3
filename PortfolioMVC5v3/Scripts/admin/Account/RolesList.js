$(function () {
    initGrid();

    $("button[name='RemoveRoleBtn']").on("click",
        function () {
            const id = $(this).data("id");
            removeRole(id);
        });

    $("button[name='EditRoleBtn']").on("click",
        function () {
            const id = $(this).data("id");
            editRole(id);
        });

    $("#AddRoleBtn").on("click",
        function () {
            editRole(0);
        });
});

function removeRole(id) {
    $.ajax({
        type: "GET",
        url: `/Account/RemoveRole/${id}`,
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

function editRole(id) {
    location.href = `/Account/RoleCard?id=${id}`;
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
            field: "Id",
            title: "Id"
        }, {
            field: "Name",
            title: "Nazwa"
        },
        {
            field: "Id",
            title: "Akcje",
            template: '<button name="EditRoleBtn" class="btn btn-sm btn-primary mr-2" data-id="#=Id#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveRoleBtn" class="btn btn-sm btn-danger" data-id="#=Id#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200
        }
        ]
    });
}