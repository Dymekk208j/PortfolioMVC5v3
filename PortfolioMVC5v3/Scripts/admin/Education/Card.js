$(function () {
    initSwitches();
    initDatePicker();
    
    $("#SaveBtn").on("click",
        function () {
            save();
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
    $("#CurrentPlaceOfEducation").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.currentPlaceOfEducation,
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
    const schoolName = $("#SchoolName").val();
    if (schoolName.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Nazwa szkoły nie może być pusta!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const department = $("#Department").val();
    if (department.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Wydział nie może być pusty!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const specialization = $("#Specialization").val();
    if (specialization.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Specjalizacja nie może być pusta!',
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
            "EducationId": $("#EducationId").val(),
            "SchoolName": $("#SchoolName").val(),
            "Department": $("#Department").val(),
            "Specialization": $("#Specialization").val(),
            "CurrentPlaceOfEducation": $("#CurrentPlaceOfEducation").data("kendoSwitch").value(),
            "ShowInCv": $("#ShowInCv").data("kendoSwitch").value(),
            "StartDate": $("#StartDate").data("kendoDatePicker").value().toJSON(),
            "EndDate": $("#EndDate").data("kendoDatePicker").value().toJSON()
        };

        $.ajax({
            type: "POST",
            url: `/Education/SaveEducation/`,
            data: { education: model },
            success: function () {
                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                }).then(() => {
                    location.href = "/Education/List";
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

    endDate.data("kendoDatePicker").enable(!window.currentPlaceOfEducation);

}