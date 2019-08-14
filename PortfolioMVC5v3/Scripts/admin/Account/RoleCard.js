$(function () {
    $("#SaveBtn").on("click",
        function () {
            save();
        });

    function save() {
        if (checkValidation()) {
            const model = {
                "Id": $("#Id").val(),
                "Name": $("#Name").val()
            };

            $.ajax({
                type: "POST",
                url: `/Account/SaveRole/`,
                data: { role: model },
                success: function () {
                    window.Swal.fire({
                        title: 'Sukces!',
                        text: 'Poprawnie zapisano!',
                        type: 'success',
                        heightAuto: false
                    }).then(() => {
                        location.href = "/Account/RoleManagement";
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

});