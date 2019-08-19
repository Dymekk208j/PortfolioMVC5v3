$(function () {
    initGrid();

    $("#AddProjectBtn").on("click",
        function () {
            editProject(0);
        });
});

function initButtons()
{
    $("button[name='EditProjectBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editProject(id);
        });

    $("button[name='RemoveProjectBtn']").on("click",
        function () {
            const projectTitle = $(this).data("project-title");

            window.Swal.fire({
                title: `Czy na pewno chcesz usunąć projekt "${projectTitle}"?`,
                text: "Operacja ta będzie nieodwracalna i wiąże się z utratą wszystkich danych powiązanych z projektem!",
                type: "question",
                confirmButtonText: "Tak, usuń!",
                cancelButtonText: "Anuluj.",
                customClass: {
                    confirmButton: "btn btn-lg btn-danger mx-3",
                    cancelButton: "btn btn-lg btn-secondary mx-3"
                },
                buttonsStyling: false,
                showCancelButton: true,
                showCloseButton: true
            }).then((e) => {
                if (e.value === true) {
                    const id = parseInt($(this).data("id"));
                    RemoveProjectBtn(id);
                }
            });


        });
}

function RemoveProjectBtn(id) {
    $.ajax({
        type: "GET",
        url: `/Project/RemoveProject/${id}`,
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

function editProject(id) {
    location.href = `/Project/ManagementCard?id=${id}`;
}

function initGrid() {
    const url = window.isTempProjectPage ? "/Project/GetTempProjectsList" : "/Project/GetProjectsList";

    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: url,
                    dataType: "json"
                }
            },
            schema: {
                model: {
                    fields: {
                        Name: { type: "string" }
                    }
                }
            },
            pageSize: 16
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
            title: "Tytuł"
        },
        {

            field: "DateTimeCreated",
            title: "Data utworzenia",
            template: "#= kendo.toString(kendo.parseDate(DateTimeCreated, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"

        },
        {
            field: "Commercial",
            title: "Projekt komercyjny",
            template: '<input class="k-checkbox" id="checkbox#:ProjectId#" type="checkbox" #= Commercial ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkbox#:ProjectId#"></label>',
            width: 170
        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input class="k-checkbox" id="checkboxB#:ProjectId#" type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>' +
                '<label class="k-checkbox-label" for="checkboxB#:ProjectId#"></label>',
            width: 150
        },
        {
            field: "ProjectId",
            title: "Akcje",
            template: '<button name="EditProjectBtn" class="btn btn-sm btn-primary mr-2" data-id="#=ProjectId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="RemoveProjectBtn" class="btn btn-sm btn-danger" data-id="#=ProjectId#" data-project-title="#=Title#"><i class="fas fa-trash-alt mr-2"></i>Usuń</button>',
            width: 200

        }]
    });
}