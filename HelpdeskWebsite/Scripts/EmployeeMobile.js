$(function () {
    getAll('');

    //   Main display click
    $('#main').click(function (e) {  // click on any row
        var empId = e.target.id.substr(3, e.target.id.length);

        if (empId.length === 24) {
            GetById(empId);
        }
        else
            return false;  // arrow wasn't clicked
    });

    $("#ButtonAction").click(function () {
        emp = new Object();
        emp.Title = $("#TextBoxTitle").val();
        emp.Firstname = $("#TextBoxFirstname").val();
        emp.Lastname = $("#TextBoxLastname").val();
        emp.Phoneno = $("#TextBoxPhone").val();
        emp.Email = $("#TextBoxEmail").val();
        emp.DepartmentId = $("#ddlDepts").val();
        emp.Id = $("#HiddenId").val();
        emp.Entity64 = $("#HiddenEntity").val();
        emp.StaffPicture64 = $("#HiddenPic").val();

        ajaxCall("Put", "api/employees", emp)
        .done(function (data) {
            $("#LabelStatus").text(data);

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    });

    $("#ButtonDelete").click(function () {
        var deleteEmp = confirm("Do you really want to delete this Employee? Think of all the work that will have to go into re-creating the employee if you were wrong about deleting this....");
        if (deleteEmp) {
            ajaxCall("Delete", "api/employees/" + $("#HiddenId").val(), "")
            .done(function (data) {
                $("#lblstatus").text("Employee Deleted!");
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
            return !deleteEmp
        }
        else {
            return deleteEmp;
        }
    });

});  // main jquery function

// build initial table
function buildTable(data) {
    $('#main').empty();
    var bg = false;
    employees = data; // copy to global var
    li = $("<li data-role=\"list-divider\" id=\"emphead\" role=\"heading\">" +
              "<fieldset class=\"ui-grid-c\"style=\"padding:3%;background-color:darkgray;color:white;\">" +
              "   <div class=\"ui-block-a\" style=\"width:15%;\">&nbsp;</div>" +
              "   <div class=\"ui-block-b\" style=\"width:20%;\">Title</div>" +
              "   <div class=\"ui-block-c\" style=\"width:20%;text-align:center;\">First</div>" +
              "   <div class=\"ui-block-d\" style=\"width:30%;text-align:center;\">Last</div>" +
              "</fieldset>" +
           "</li>");
    li.appendTo($('#main'));

    $.each(data, function (index, emp) {
        var empId = emp.Id;
        li = $("<li id=\"" + empId + "\" class=\"ui-li-divider ui-bar-inherit\" style=\"padding:2%\">" +
                      "<fieldset>" +
                      "   <div class=\"ui-block-a\" style=\"width:15%;\">" +
                      "      <a href=\"#mobilemodal\" data-transition=\"flip\" class=\"ui-btn-icon-right ui-icon-carat-r\" id=\"emp" + empId + "\">" +
                      "         <img src=\"data:image/png;base64," + emp.StaffPicture64 + "\" style=\"max-width:25px; max-height:25px;\" />" +
                      "     </a>" +
                      "   </div>" +
                      "   <div class=\"ui-block-b\" style=\"width:20%;\">" + emp.Title + "</div>" +
                      "   <div class=\"ui-block-c\" style=\"width:20%;\">" + emp.Firstname + "</div>" +
                      "   <div class=\"ui-block-d\" style=\"width:30%;\">" + emp.Lastname + "</div>" +
                      "</fieldset>" +
                   "</li>");
        li.appendTo($('#main'));
    }); // each
}

//  copy Employee info to modal
function copyInfoToModal(emp) {
    $('#TextBoxTitle').val(emp.Title);
    $('#TextBoxFirstname').val(emp.Firstname);
    $('#TextBoxLastname').val(emp.Lastname);
    $('#TextBoxPhone').val(emp.Phoneno);
    $('#TextBoxEmail').val(emp.Email);
    $('#CheckBoxIsTech').val(emp.IsTech);
    loadDepartmentDDL(emp.DepartmentId);
    $('#HiddenId').val(emp.Id);
    $('#HiddenEntity').val(emp.Entity64);
    $('#HiddenPic').val(emp.StaffPicture64);
}

function loadDepartmentDDL(empdep) {
    $.ajax({
        type: "Get",
        url: "api/departments",
        contentType: "application/json; charset-utf-8"
    })
    .done(function (data) {
        html = "";
        $("#ddlDepts").empty();
        $.each(data, function () {
            html += "<option value=\"" + this["Id"] + "\">" + this["DepartmentName"] + "</option>";
        });
        $("#ddlDepts").append(html);
        $("#ddlDepts").val(empdep).selectmenu('refresh');
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        alert("error");
    });
}

//   ajax calls
function getAll(msg) {
    $('#LabelStatus').text("Employees Loading...");

    ajaxCall('Get', 'api/employees', '')
    .done(function (data) {
        buildTable(data);
        if (msg == '')
            $('#LabelStatus').text('Employees Loaded');
        else
            $('#LabelStatus').text(msg + ' - ' + 'Employees Loaded');
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function GetById(empId) {
    ajaxCall('Get', 'api/employees/' + empId, '')
    .done(function (data) {
        copyInfoToModal(data);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

// ajax Call - returns promise
function ajaxCall(type, url, data) {
    return $.ajax({ // return the promise that `$.ajax` returns
        type: type,
        url: url,
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        processData: true,
    });
}

// commmon error
function errorRoutine(jqXHR) {
    if (jqXHR.responseJSON == null) {
        $('#LabelStatus').text(jqXHR.responseText);
    }
    else {
        $('#LabelStatus').text(jqXHR.responseJSON.Message);
    }
}