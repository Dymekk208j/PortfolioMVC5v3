$(function () {
    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function checkValidation() {
    const title = $("#Title").val();
    if (title.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Tytuł nie może być pusta!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const imageLink = $("#ImageLink").val();
    if (imageLink.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Link do zdjęcia nie może być pusty!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const description = $("#Description").val();
    if (description.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Opis nie może być pusty!',
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
            "Title": $("#Title").val(),
            "ImageLink": $("#ImageLink").val(),
            "Description": $("#Description").val()
        };
       
        $.ajax({
            type: "POST",
            url: `/Home/Update/`,
            dataType: "json",
            data: {
                mainPage: model
            },
            success: function (e) {
                if (e.success) {
                    window.Swal.fire({
                        title: 'Sukces!',
                        text: 'Poprawnie zapisano!',
                        type: 'success',
                        heightAuto: false
                    });
                } else {
                    window.Swal.fire({
                        title: 'Błąd!',
                        text: 'Zapisywanie nie powiodło się!',
                        type: 'error',
                        heightAuto: false
                    });
                }
            }
        });
    }
}