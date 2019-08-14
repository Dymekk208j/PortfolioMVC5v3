$(function () {
    initGrid();

    $("button[name='BlockUserBtn']").on("click",
        function () {
            const id = $(this).data("id");
            blockUser(id);
        });

    $("button[name='EditUserBtn']").on("click",
        function () {
            const id = $(this).data("id");
            editUser(id);
        });

    //$("#AddTechnologyBtn").on("click",
    //    function () {
    //        editTechnology(0);
    //    });
});

function blockUser(id) {
    $.ajax({
        type: "GET",
        url: `/Account/BlockUser/${id}`,
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

function editUser(id) {
    location.href = `/Account/Card?id=${id}`;
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
            field: "UserName",
            title: "Nazwa użytkownika"
        }, {
            field: "FirstName",
            title: "Imie"
        }, {
            field: "LastName",
            title: "Nazwisko"
        }, {
            field: "Email",
            title: "Adres e-mail"
        },
        {
            field: "Blocked",
            title: "Zablokowany",
            template: '<input type="checkbox" #= Blocked ? "checked=checked" : "" # disabled="disabled" ></input>'
        },
        {
            field: "Id",
            title: "Akcje",
            template: '<button name="EditUserBtn" class="btn btn-sm k-primary mr-2" data-id="#=Id#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="#= Blocked ? "UnlockUserBtn" : "BlockUserBtn" #" class="btn btn-sm #= Blocked ? "btn-success" : "btn-danger" #" data-id="#=Id#"><i class="#= Blocked ? "far fa-check-circle" : "fas fa-ban" # mr-2"></i>Zablokuj</button>'
            , width: 200
        }
        ]
    });
    //#= Blocked ? "UnlockUserBtn" : "BlockUserBtn" #
}