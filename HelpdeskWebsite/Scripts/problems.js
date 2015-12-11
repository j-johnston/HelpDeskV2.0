$(function () {
    $("#lblstatus").text("Please be patient...");
    ajaxCall("Get", "api/problems")
    .done(function (data) {
        buildTable(data);
        $("#lblstatus").text("Problems Found");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });

    $("#main").click(function (p) {
        if (!p) p = window.event;
        var probId = p.target.parentNode.id;

        if (probId == "main") {
            probId = p.target.id;    //clicked on row somewhere
        }

        if (probId != "problem") {                          //Existing Employee
            GetById(probId);
            $("#ButtonAction").prop("value", "Update");
            $("#ButtonDelete").show();
        }

        else {                                                   //New Employee
            $("#ButtonDelete").hide();
            $("#ButtonAction").prop("value", "Add");
            $("#HiddenId").val("new");
            $("#TextBoxName").val("");
            $("#TextBoxUpdate").prop("value", "Add");
            $("#ButtonDelete").hide();
        }

    }); //main

    $("#ButtonAction").click(function () {
        if ($("#ButtonAction").val() === "Update") {
            update();
        }
        else {
            create();
        }
    });

    $("#ButtonDelete").click(function () {
        var deleteProb = confirm("Do you really want to delete this Problem? Think of all the work that will have to go into re-creating the problem if you were wrong about deleting this....");
        if (deleteProb) {
            ajaxCall("Delete", "api/problems/" + $("#HiddenId").val(), "")
            .done(function (data) {
                $("#lblstatus").text("Problem Deleted!");
                $("#myModal").modal("hide");
            })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    errorRoutine(jqXHR);
                });
            return !deleteProb
        }
        else {
            return deleteProb;
        }
    });
}); //function

function buildTable(data) {
    $("#main").empty();
    var bg = false;
    problems = data;
    div = $("<div id=\"problem\" data-toggle=\"modal\" data-target=\"#myModal\" class=\"row trWhite\">");
    div.html("<div class=\"col-lg-12\" id=\"id0\">...Click Here to add</div>");
    div.appendTo($("#main"));
    $.each(data, function (index, prob) {
        var cls = "rowWhite";
        bg ? cls = "rowWhite" : cls = "rowLightGray";
        bg = !bg;
        div = $("<div id=\"" + prob.Id + "\" data-toggle=\"modal\" data-target=\"#myModal\" class=\"row col-lg-12 " + cls + "\">");
        var probId = prob.Id;
        div.html(
            "<div class=\"col-xs-12\" id=\"name" + probId + "\">" + prob.Description + "</div>"
            );
        div.appendTo($("#main"));
    }); //each
}


function update() {
    prob = new Object();
    prob.Description = $("#TextBoxName").val();
    prob.Id = $("#HiddenId").val();
    prob.Entity64 = $("#HiddenEntity").val();

    ajaxCall("Put", "api/problems", prob)
    .done(function (data) {
        $("#lblstatus").text(data);
        $("#myModal").modal("hide");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function create() {
    prob = new Object();
    prob.Description = $("#TextBoxName").val();
    prob.Id = $("#HiddenId").val();
    prob.Entity64 = $("#HiddenEntity").val();

    ajaxCall("Post", "api/problems", prob)
    .done(function (data) {
        $("#lblstatus").text(data);
        $("#myModal").modal("hide");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function GetById(probId) {
    $("#lblstatus").text("please be patient...");
    ajaxCall("Get", "api/problems/" + probId, "")
    .done(function (data) {
        CopyInfoToModal(data);
    })

    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function CopyInfoToModal(prob) {
    $("#HiddenId").val(prob.Id);
    $("#TextBoxName").val(prob.Description);
    $("#HiddenEntity").val(prob.Entity64);
}

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