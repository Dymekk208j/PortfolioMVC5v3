$(function () {
    initMultiSelect();
    initSwitches();

    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function initSwitches() {
    $("#Blocked").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.blocked
    });
}

function initMultiSelect() {
    const userId = $("#Id").val();
    $.ajax({
        type: "GET",
        url: `/Account/GetAllRoles/`,

        success: function (allRolesResult) {
            const allRoles = $.parseJSON(allRolesResult);
            console.log(allRoles);
            $.ajax({
                type: "GET",
                url: `/Account/GetUserRoles/${userId}`,
                success: function (userRolesResult) {
                    const userRoles = $.parseJSON(userRolesResult);
                    console.log(userRoles);
                    $("#selectedRoles").kendoMultiSelect({
                        placeholder: "Role przypisane do użytkownika...",
                        dataTextField: "Name",
                        dataValueField: "Id",
                        autoBind: false,
                        dataSource: allRoles,
                        value: userRoles
                    });

                }
            });
        }
    });
}


function checkValidation() {
    const userName = $("#UserName").val();
    if (userName.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Nazwa użytkownika nie może być pusta!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const email = $("#Email").val();
    if (email.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Adres e-mail nie może być pusty!',
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
            "Id": $("#Id").val(),
            "UserName": $("#UserName").val(),
            "Email": $("#Email").val(),
            "FirstName": $("#FirstName").val(),
            "LastName": $("#LastName").val(),
            "Blocked": $("#Blocked").data("kendoSwitch").value()
        };
        const selectedRoles = $("#selectedRoles").data("kendoMultiSelect").value();

        $.ajax({
            type: "POST",
            url: `/Account/UpdateUser/`,
            data: {
                userViewModel: model,
                rolesIds: selectedRoles
            },
            success: function () {
                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                });
            },
            error: function (e) {
                console.log(e);
                window.Swal.fire({
                    title: 'Błąd!',
                    text: 'Zapisywanie nie powiodło się!',
                    type: 'error',
                    heightAuto: false
                });
            }
        });


    }
}