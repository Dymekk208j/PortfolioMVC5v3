$(function () {
    initSwitches();
    initDatePicker();


    $("#SaveBtn").on("click",
        function () {
            save();
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
    $("#CurrentPlaceOfEmployment").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.currentPlaceOfEmployment,
        change: function (e) {
            $("#EndDate").data("kendoDatePicker").enable(!e.checked);
        }
    });

    $("#ShowInCv").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.showInCv
    });
}

function checkValidation() {
    const position = $("#Position").val();
    if (position.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Stanowisko nie może być pusta!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const companyName = $("#CompanyName").val();
    if (companyName.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Nazwa firmy nie może być pusta!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const cityOfEmployment = $("#CityOfEmployment").val();
    if (cityOfEmployment.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Miejscowość nie może być pusta!',
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
            "EmploymentHistoryId": $("#EmploymentHistoryId").val(),
            "CompanyName": $("#CompanyName").val(),
            "CityOfEmployment": $("#CityOfEmployment").val(),
            "Position": $("#Position").val(),
            "CurrentPlaceOfEmployment": $("#CurrentPlaceOfEmployment").data("kendoSwitch").value(),
            "ShowInCv": $("#ShowInCv").data("kendoSwitch").value(),
            "StartDate": $("#StartDate").data("kendoDatePicker").value().toJSON(),
            "EndDate": $("#EndDate").data("kendoDatePicker").value().toJSON()
        };

        $.ajax({
            type: "POST",
            url: `/EmploymentHistory/SaveEmploymentHistory/`,
            data: { employmentHistory: model },
            success: function () {
                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                }).then(() => {
                    location.href = "/EmploymentHistory/List";
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

function initDatePicker() {
    $("#StartDate").kendoDatePicker();
    const endDate = $("#EndDate").kendoDatePicker();

    endDate.data("kendoDatePicker").enable(!window.currentPlaceOfEmployment);

}