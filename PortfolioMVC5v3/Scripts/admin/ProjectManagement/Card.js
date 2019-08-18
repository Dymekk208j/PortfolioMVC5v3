$(function () {
    initSwitches();
    initDatePicker();
    initMultiSelect();
    initIconSelectionModal();

    $("#SaveBtn").on("click",
        function () {
            createOrUpdateProject();
        });

    $("#SaveAsTempBtn").on("click",
        function () {
            const projectId = parseInt($("#ProjectId").val());
            if (projectId === 0) {
                window.isTemp = true;
            } else {
                window.isTemp = !window.isTemp;
            }
            createOrUpdateProject();
        });
});

function initIconSelectionModal() {
    var dataSource = new window.kendo.data.DataSource({
        pageSize: 28,
        transport: {
            read: {
                url: "/Icon/GetIcons",
                dataType: "json"
            }
        }
    });

    $("#pager").kendoPager({
        dataSource: dataSource
    });

    $("#listView").kendoListView({
        dataSource: dataSource,
        template: window.kendo.template($("#template").html()),
        dataBound: function () {
            $(".projectIconP").on("click",
                function () {
                    const id = parseInt($(this).data("id"));
                    $("#IconId").val(id);
                    $("#ProjectIconPreview").attr("src", $(this).data("src"));
                    $("#SelectIconModal").modal("hide");

                });
        }
    });

}
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
    const iconId = parseInt($("#IconId").val());
    if (isNaN(iconId) || iconId === 0) {
        window.Swal.fire({
            title: 'Błąd!',
            text: 'Musisz wybrać ikonę dla projektu!',
            type: 'error',
            heightAuto: false
        });

        return false;
    }
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

function createOrUpdateProject() {
    if (checkValidation()) {
        const model = {
            ProjectId: $("#ProjectId").val(),
            AuthorId: $("#AuthorId").val(),
            IconId: $("#IconId").val(),
            Title: $("#Title").val(),
            GitHubLink: $("#GitHubLink").val(),
            DateTimeCreated: $("#DateTimeCreated").data("kendoDatePicker").value().toJSON(),
            ShortDescription: $("#ShortDescription").val(),
            FullDescription: $("#FullDescription").val(),
            Commercial: $("#Commercial").data("kendoSwitch").value(),
            ShowInCv: $("#ShowInCv").data("kendoSwitch").value(),
            TempProject: window.isTemp
        };

        const selectedTechnologiesIds = $("#Technologies").data("kendoMultiSelect").value();

        var files = upload.cachedFileArray;
        var data = new FormData();

        // Add the uploaded file to the form data collection  
        if (files.length > 0) {
            for (var f = 0; f < files.length; f++) {
                data.append("UploadedImage", files[f]);
            }
        }

        data.append("ProjectId", model.ProjectId);
        data.append("AuthorId", model.AuthorId);
        data.append("Title", model.Title);
        data.append("GitHubLink", model.GitHubLink);
        data.append("DateTimeCreated", model.DateTimeCreated);
        data.append("ShortDescription", model.ShortDescription);
        data.append("FullDescription", model.FullDescription);
        data.append("Commercial", model.Commercial);
        data.append("ShowInCv", model.ShowInCv);
        data.append("TempProject", model.TempProject);
        data.append("IconId", model.IconId);

        data.append("projectTechnologiesIds", JSON.stringify(selectedTechnologiesIds));


        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: `/Project/CreateOrUpdateProject/`,
            processData: false, 
            contentType: false,
            data: data,
            success: function (e) {
                var result = JSON.parse(e);
                if (result.Success === true) {
                    window.Swal.fire({
                        title: 'Sukces!',
                        text: 'Poprawnie zapisano!',
                        type: 'success',
                        heightAuto: false
                    }).then(() => {
                        if (result.Type === "Normal") {
                            location.href = "/Project/ProjectManagement";
                        } else {
                            location.href = "/Project/TempProjectManagement";
                        }
                    });
                } else {
                    window.Swal.fire({
                        title: 'Błąd!',
                        text: 'Zapisywanie nie powiodło się!',
                        type: 'error',
                        heightAuto: false
                    }).then(() => {
                        location.reload();
                    });
                }

            }
        });


    }
}

function initDatePicker() {
    $("#DateTimeCreated").kendoDatePicker();
}