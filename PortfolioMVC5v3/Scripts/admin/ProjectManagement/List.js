$(function () {
    initGrid();

    $("button[name='EditProjectBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            editProject(id);
        });

    $("button[name='SetAsTempProjectBtn']").on("click",
        function () {
            const id = parseInt($(this).data("id"));
            setAsTempProjectBtn(id);
        });

    $("#AddProjectBtn").on("click",
        function () {
            editProject(0);
        });
});


function setAsTempProjectBtn(id) {
    $.ajax({
        type: "GET",
        url: `/Project/RemoveProject/${id}`,
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

function editProject(id) {
    location.href = `/Project/ManagementCard?id=${id}`;
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
            field: "ProjectId",
            title: "Id",
            width: 50
        },
        {
            field: "Title",
            title: "Tytuł"
        },
        {
            field: "ShortDescription",
            title: "Krótki opis"
        },
        {

            field: "DateTimeCreated",
            title: "Data utworzenia",
            template: "#= kendo.toString(kendo.parseDate(DateTimeCreated, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"

        },
        {
            field: "Commercial",
            title: "Projekt komercyjny",
            template: '<input type="checkbox" #= Commercial ? "checked=checked" : "" # disabled="disabled" ></input>',
            width: 170
        },
        {
            field: "ShowInCv",
            title: "Pokazuj w CV",
            template: '<input type="checkbox" #= ShowInCv ? "checked=checked" : "" # disabled="disabled" ></input>',
            width: 150
        },
        {
            field: "ProjectId",
            title: "Akcje",
            template: '<button name="EditProjectBtn" class="btn btn-sm btn-primary mr-2" data-id="#=ProjectId#"><i class="far fa-edit mr-2"></i>Edytuj</button> ' +
                '<button name="SetAsTempProjectBtn" class="btn btn-sm btn-secondary" data-id="#=ProjectId#"><i class="fas fa-ban mr-2"></i>Wycofaj do tymczasowych</button>', 

        }]
    });
}