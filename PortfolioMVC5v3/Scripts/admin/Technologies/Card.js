$(function () {
    initSwitches();
    initDropDown();

    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function removeTechnology(id) {
    $.ajax({
        type: "GET",
        url: `/Technologies/RemoveTechnology/${id}`,
        success: function () {
            window.Swal.fire({
                title: 'Sukces!',
                text: 'Poprawnie usunięto!',
                type: 'success',
                heightAuto: false
            }).then(() => {
                location.reload();
            });
        },
        error: function () {
            window.Swal.fire({
                title: 'Błąd!',
                text: 'Usuwanie nie powiodło się!',
                type: 'error',
                heightAuto: false
            }).then(() => {
                location.reload();
            });
        }
    });
}

function initSwitches() {
    $("#ShowInCv").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.showInCv
    });


    $("#ShowInAboutMePage").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.showInAboutMePage
    });
}

function initDropDown() {
    var data = [
        { text: 'Brak', value: 0 },
        { text: 'Podstawowy poziom', value: 1 },
        { text: 'Średni poziom', value: 2 },
        { text: 'Dobry poziom', value: 3 },
        { text: 'Bardzo dobry poziom', value: 4 },
        { text: 'Perfekcyjny poziom', value: 5 }
    ];

    $("#KnowledgeLevel").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: data
    });
}

function checkValidation() {
    const name = $("#Name").val();
    if (name.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Nazwa nie może być pusta!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    return true;
}

function save() {
    if (checkValidation()) {
        const model = {
            "TechnologyId": $("#TechnologyId").val(),
            "Name": $("#Name").val(),
            "KnowledgeLevel": $("#KnowledgeLevel").data("kendoDropDownList").value(),
            "ShowInCv": $("#ShowInCv").data("kendoSwitch").value(),
            "ShowInAboutMePage": $("#ShowInAboutMePage").data("kendoSwitch").value()
        };

        $.ajax({
            type: "POST",
            url: `/Technologies/SaveTechnology/`,
            data: { technology: model },
            success: function () {
                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                }).then(() => {
                    location.href = "/Technologies/List";
                });
            },
            error: function () {
                window.Swal.fire({
                    title: 'Błąd!',
                    text: 'Zapisywanie nie powiodło się!',
                    type: 'error',
                    heightAuto: false
                }).then(() => {
                    location.reload();
                });
            }
        });


    }
}