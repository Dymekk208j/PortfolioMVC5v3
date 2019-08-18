$(function () {
    initGrid();

    $("button[name='RemoveEducationBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            removeEducation(id);
        });

    $("button[name='EditEducationBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editEducation(id);
        });

    $("#AddEducationBtn").on("click",
        function () {
            editEducation(0);
        });
});

function removeEducation(id) {
    $.ajax({
        type: "GET",
        url: `/Education/RemoveEducation/${id}`,
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

function editEducation(id) {
    location.href = `/Education/Card?id=${id}`;
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
            field: "EducationId",
            title: "Id",
            width: 50
        },
        {
            field: "SchoolName",
            title: "Nazwa szkoły"
        },
        {
            field: "Department",
            title: "Wydział"
        },
        {
            field: "Specialization",
            title: "Specjalizacja"
        },
        {

            field: "StartDate",
            title: "Data od",
            template: "#= kendo.toString(kendo.parseDate(StartDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"

        }, {

            field: "EndDate",
            title: "Data do",
            template: "#= !CurrentPlaceOfEducation ? kendo.toString(kendo.parseDate(EndDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') : '-' #"

        },
        {
            field: "CurrentPlaceOfEducation",
            title: "Aktualne miejsce nauki",
            template: '<input class="k-checkbox" id="checkbox#:EducationId#" type="checkbox" #= CurrentPlaceOfEducation ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkbox#:EducationId#"></label>'
        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input class="k-checkbox" id="checkboxB#:EducationId#" type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkboxB#:EducationId#"></label>'
        },
        {
            field: "EducationId",
            title: "Akcje",
            template: '<button name="EditEducationBtn" class="btn btn-sm btn-primary mr-2" data-id="#=EducationId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveEducationBtn" class="btn btn-sm btn-danger" data-id="#=EducationId#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
        }]
    });
}