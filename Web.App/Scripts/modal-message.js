$(document)
    .ready(function () {
        $("#modal-message-btn")
            .click(function () {
                $.ajax({
                    type: "GET",
                    url: "/DataManage/Message/",
                    contentType: "application/json",
                    cache: false,
                    dataType: "html",
                    success: function (data) {
                        $("#message-modal").html(data);
                    },
                    error: function (xhr, textStatus, error) {
                        alert(xhr.responseText);
                        alert(textStatus);
                        alert(error);
                    }
                });
            });
    });