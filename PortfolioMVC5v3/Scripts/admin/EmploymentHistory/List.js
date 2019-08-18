$(function () {
    initGrid();

    $("button[name='RemoveEmploymentHistoryBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            removeEmploymentHistory(id);
        });

    $("button[name='EditEmploymentHistoryBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editEmploymentHistory(id);
        });

    $("#AddEmploymentHistoryBtn").on("click",
        function () {
            editEmploymentHistory(0);
        });
});

function removeEmploymentHistory(id) {
    $.ajax({
        type: "GET",
        url: `/EmploymentHistory/RemoveEmploymentHistory/${id}`,
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

function editEmploymentHistory(id) {
    location.href = `/EmploymentHistory/Card?id=${id}`;
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
            field: "EmploymentHistoryId",
            title: "Id",
            width: 50
        },
        {
            field: "CompanyName",
            title: "Nazwa firmy"
        },
        {
            field: "Position",
            title: "Stanowisko"
        },
        {

            field: "StartDate",
            title: "Data od",
            template: "#= kendo.toString(kendo.parseDate(StartDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"

        }, {

            field: "EndDate",
            title: "Data do",
            template: "#= !CurrentPlaceOfEmployment ? kendo.toString(kendo.parseDate(EndDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') : '-' #"

        },
        {
            field: "CurrentPlaceOfEmployment",
            title: "Aktualne miejsce pracy",
            template: '<input class="k-checkbox" id="checkbox#:EmploymentHistoryId#" type="checkbox" #= CurrentPlaceOfEmployment ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkbox#:EmploymentHistoryId#"></label>',
            width: 150
        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input class="k-checkbox" id="checkboxB#:EmploymentHistoryId#" type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkboxB#:EmploymentHistoryId#"></label>',
            width: 150
        },
        {
            field: "EmploymentHistoryId",
            title: "Akcje",
            template: '<button name="EditEmploymentHistoryBtn" class="btn btn-sm btn-primary mr-2" data-id="#=EmploymentHistoryId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveEmploymentHistoryBtn" class="btn btn-sm btn-danger" data-id="#=EmploymentHistoryId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200
        }]
    });
}