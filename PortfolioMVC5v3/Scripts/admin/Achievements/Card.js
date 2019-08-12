$(function () {
    initSwitches();
    initDatePicker();


    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function removeAchievement(id) {
    $.ajax({
        type: "GET",
        url: `/Achievements/RemoveAchievement/${id}`,
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

    const description = $("#Description").val();
    if (description.length <= 0) {
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
            "AchievementId": $("#AchievementId").val(),
            "Title": $("#Title").val(),
            "Description": $("#Description").val(),
            "ShowInCv": $("#ShowInCv").data("kendoSwitch").value(),
            "Date": $("#myDate").data("kendoDatePicker").value().toJSON()
    };

        $.ajax({
            type: "POST",
            url: `/Achievements/SaveAchievement/`,
            data: { achievement: model },
            success: function () {
                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                }).then(() => {
                    location.href = "/Achievements/List";
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
    $("#myDate").kendoDatePicker();
}