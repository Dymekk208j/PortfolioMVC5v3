$(function () {
    initMultiSelect();

    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function initMultiSelect() {
    $.ajax({
        type: "GET",
        url: `/AboutMe/GetAllTechnologiesListAsync/`,

        success: function (allTechnologiesResult) {
            const allTechnologies = $.parseJSON(allTechnologiesResult);

            $.ajax({
                type: "GET",
                url: `/AboutMe/GetTechnologiesToShowInAboutMePage/`,
                success: function (technologiesToShowInAboutMePageResult) {
                    const technologiesToShowInAboutMePage = $.parseJSON(technologiesToShowInAboutMePageResult);

                    $("#Technologies").kendoMultiSelect({
                        placeholder: "Technologie pokazywane na stronie 'o mnie'...",
                        dataTextField: "Name",
                        dataValueField: "TechnologyId",
                        autoBind: false,
                        dataSource: allTechnologies,
                        value: technologiesToShowInAboutMePage
                    });

                }
            });
        }
    });
}

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

    const text = $("#Text").val();
    if (text.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Opis nie może być pusty!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const selectedTechnologiesAmount = $("#Technologies").data("kendoMultiSelect").value().length;
    if (selectedTechnologiesAmount <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Musisz wybrać przynajmniej jedną technologię do wyświetlenia!',
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
            "AboutMeId": $("#AboutMeId").val(),
            "Title": $("#Title").val(),
            "ImageLink": $("#ImageLink").val(),
            "Text": $("#Text").val()
        };
        const selectedTechnologies = $("#Technologies").data("kendoMultiSelect").value();

        $.ajax({
            type: "POST",
            url: `/AboutMe/Update/`,
            dataType: "json",
            data: {
                aboutMe: model,
                selectedTechnologies: selectedTechnologies
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