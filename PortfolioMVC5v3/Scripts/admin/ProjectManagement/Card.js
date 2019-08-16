$(function () {
    initSwitches();
    initDatePicker();
    initMultiSelect();
    
    $("#SaveBtn").on("click",
        function () {
            save();
        });
});

function initMultiSelect() {
    $.ajax({
        type: "GET",
        url: `/Project/GetAllTechnologies/`,

        success: function (allTechnologiesResult) {
            const allTechnologies = $.parseJSON(allTechnologiesResult);

            $("#Technologies").kendoMultiSelect({
                placeholder: "Technologie użyte w projekcie...",
                dataTextField: "Name",
                dataValueField: "TechnologyId",
                autoBind: false,
                dataSource: allTechnologies,
                value: window.technologies
            });
        }
    });
}


function initSwitches() {
    $("#Commercial").kendoSwitch({
        messages: {
            checked: "Tak",
            unchecked: "Nie"
        },
        checked: window.commercial
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
    const title = $("#Title").val();
    if (title.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Tytuł nie może być pusty!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const shortDescription = $("#ShortDescription").val();
    if (shortDescription.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Krótki opis nie może być pusty!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }

    const fullDescription = $("#FullDescription").val();
    if (fullDescription.length <= 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Pełen opis nie może być pusty!',
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
            ProjectId: $("#ProjectId").val(),
            Title: $("#Title").val(),
            GitHubLink: $("#GitHubLink").val(),
            DateTimeCreated: $("#DateTimeCreated").data("kendoDatePicker").value().toJSON(),
            ShortDescription: $("#ShortDescription").val(),
            FullDescription: $("#FullDescription").val(),
            Commercial: $("#Commercial").data("kendoSwitch").value(),
            ShowInCv: $("#ShowInCv").data("kendoSwitch").value()
        };

        const selectedTechnologiesIds = $("#Technologies").data("kendoMultiSelect").value();

        $.ajax({
            type: "POST",
            url: `/Project/CreateOrUpdateProject/`,
            data: {
                projectModel: model,
                projectTechnologiesIds: selectedTechnologiesIds
            },
            success: function (e) {
                console.log(e);

                window.Swal.fire({
                    title: 'Sukces!',
                    text: 'Poprawnie zapisano!',
                    type: 'success',
                    heightAuto: false
                }).then(() => {
                 //   location.href = "/EmploymentHistory/List";
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
    $("#DateTimeCreated").kendoDatePicker();
}