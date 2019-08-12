$(function () {
    initSwitches();
    initDropDown();

    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function removeExtraInformation(id) {
    $.ajax({
        type: "GET",
        url: `/ExtraInformations/RemoveExtraInformation/${id}`,
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
}

function initDropDown() {
    var data = [
        { text: 'Języki obce', value: 0 },
        { text: 'Dodatkowe umiejętności', value: 1 },
        { text: 'Zainteresowania', value: 2 }
    ];

    $("#Type").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: data
    });
}

function checkValidation() {
    const title = $("#Title").val();
    if (title.length <= 0) {
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
            "ExtraInformationId": $("#ExtraInformationId").val(),
            "Title": $("#Title").val(),
            "Type": $("#Type").data("kendoDropDownList").value(),
            "ShowInCv": $("#ShowInCv").data("kendoSwitch").value()
        };

        $.ajax({
            type: "POST",
            url: `/ExtraInformations/SaveExtraInformation/`,
            data: { extraInformation: model },
            success: function () {
                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                }).then(() => {
                    location.href = "/ExtraInformations/List";
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

