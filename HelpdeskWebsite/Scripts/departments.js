$(function () {
    $("#lblstatus").text("Please be patient...");
    ajaxCall("Get", "api/departments")
    .done(function (data) {
        buildTable(data);
        $("#lblstatus").text("Departments Found");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });

    $("#main").click(function (d) {
        if (!d) d = window.event;
        var depId = d.target.parentNode.id;

        if (depId == "main") {
            depId = d.target.id;    //clicked on row somewhere
        }

        if (depId != "department") {                          //Existing Employee
            GetById(depId);
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
        var deleteDep = confirm("Do you really want to delete this Department? Think of all the work that will have to go into re-creating the department if you were wrong about deleting this....");
        if (deleteDep) {
            ajaxCall("Delete", "api/departments/" + $("#HiddenId").val(), "")
            .done(function (data) {
                $("#lblstatus").text("Department Deleted!");
                $("#myModal").modal("hide");
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    errorRoutine(jqXHR);
            });
            return !deleteDep
        }
        else {
            return deleteDep;
        }
    });
}); //function

function buildTable(data) {
    $("#main").empty();
    var bg = false;
    departments = data;
    div = $("<div id=\"department\" data-toggle=\"modal\" data-target=\"#myModal\" class=\"row trWhite\">");
    div.html("<div class=\"col-lg-12\" id=\"id0\">...Click Here to add</div>");
    div.appendTo($("#main"));
    $.each(data, function (index, dep) {
        var cls = "rowWhite";
        bg ? cls = "rowWhite" : cls = "rowLightGray";
        bg = !bg;
        div = $("<div id=\"" + dep.Id + "\" data-toggle=\"modal\" data-target=\"#myModal\" class=\"row col-lg-12 " + cls + "\">");
        var depId = dep.Id;
        div.html(
            "<div class=\"col-xs-12\" id=\"name" + depId + "\">" + dep.DepartmentName + "</div>"
            );
        div.appendTo($("#main"));
    }); //each
}


function update() {
    dep = new Object();
    dep.DepartmentName = $("#TextBoxName").val();
    dep.Id = $("#HiddenId").val();
    dep.Entity64 = $("#HiddenEntity").val();

    ajaxCall("Put", "api/departments", dep)
    .done(function (data) {
        $("#lblstatus").text(data);
        $("#myModal").modal("hide");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function create() {
    dep = new Object();
    dep.DepartmentName = $("#TextBoxName").val();
    dep.Id = $("#HiddenId").val();
    dep.Entity64 = $("#HiddenEntity").val();

    ajaxCall("Post", "api/departments", dep)
    .done(function (data) {
        $("#lblstatus").text(data);
        $("#myModal").modal("hide");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function GetById(depId) {
    $("#lblstatus").text("please be patient...");
    ajaxCall("Get", "api/departments/" + depId, "")
    .done(function (data) {
        CopyInfoToModal(data);
    })

    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function CopyInfoToModal(data) {
    $("#HiddenId").val(data.Id);
    $("#TextBoxName").val(data.DepartmentName);
    $("#HiddenEntity").val(data.Entity64);
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