$(function () {
    $("#ButtonCreate").click(function () {
        $("#lblstatus").text("Database is loading...");
        ajaxCall("Delete", "api/util", "")
        .done(function (data) {
            $("#lblstatus").text("Database created");
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    });
});

function ajaxCall(type, url, data) {
    return $.ajax({ //return the promise that '$.ajax' returns
        type: type,
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true
    });
}

function errorRoutine(jqXHR) { //common error
    if (jqXHR.responseJSON == null) {
        $("#lblstatus").text(jqXHR.responseText);
    }
    else {
        $("#lblstatus").text(jqXHR.responseJSON.Message);
    }
}