$(function () {
    //reset validator
    var validator = $("#EmployeeModalForm").validate();
    validator.resetForm();

    $("#lblstatus").text("Please be patient...");

    //Get all the employees and build a table of them
    ajaxCall("Get", "api/employees")
    .done(function (data) {
        buildTable(data);
        $("#lblstatus").text("Employees Found");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });

    //validation rules
    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, Email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        ignore: ".ignore, :hidden",
        errorElement: "div",
        wrapper: "div", // a wrapper around the error message
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", Email: "need valid email format"
            }
        }
    });

    //main function
    $("#main").click(function (e) {
        if (!e) e = window.event;
        var empId = e.target.parentNode.id;

        if (empId == "main") {
            empId = e.target.id;    //clicked on row somewhere
        }

        if (empId != "employee") {                          //Existing Employee
            // reset validation

            GetById(empId);
            $("#ButtonAction").prop("value", "Update");
            $("#ButtonDelete").show();
        }

        else
        {                                                   //New Employee
            $("#ButtonDelete").hide();
            $("#ButtonAction").prop("value", "Add");
            $("#HiddenId").val("new");
            $("#TextBoxTitle").val("");
            $("#TextBoxFirstname").val("");
            $("#TextBoxLastname").val("");
            $("#TextBoxPhone").val("");
            $("#TextBoxEmail").val("");
            $("#IsTech").val();
            $("#TextBoxUpdate").prop("value", "Add");
            $("#ButtonDelete").hide();
            loadDepartmentDDL(-1);
        }

    }); //main

    //add or update button event handler
    $("#ButtonAction").click(function () {
        var reader = new FileReader();
        var file = $('#fileUpload')[0].files[0];

        reader.onload = function (readerEvt) {  // event handler for FileReader
            var binaryString = readerEvt.target.result;
            $('#ImageHolder').val(btoa(binaryString));
            if ($('#ButtonAction').val() === "Update") {
                update();
            }
            else {
                create();
            }
        };


        if (file) {                            // are we adding or updating a pic?
            reader.readAsBinaryString(file);   // yes, read pic file, fire event 
        }
        else {                                 // no pic, regular add or update
            if ($('#ButtonAction').val() === "Update") {
                update();
            }
            else {
                create();
            }
        }

        return false;
    });

    //delete button event handler
    $("#ButtonDelete").click(function () {
        var deleteEmp = confirm("Do you really want to delete this Employee? Think of all the work that will have to go into re-creating the employee if you were wrong about deleting this....");
        if (deleteEmp) {
            ajaxCall("Delete", "api/employees/" + $("#HiddenId").val(), "")
            .done(function (data) {
                $("#lblstatus").text("Employee Deleted!");
                $("#EmployeeModalForm").modal("hide");
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

    //validate title custom rules
    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");

}); //function

//build table function
function buildTable(data) {
    $("#main").empty();
    var bg = false;
    employees = data;
    div = $("<div id=\"employee\" data-toggle=\"modal\" data-target=\"#myModal\" class=\"row trWhite\">");
    div.html("<div class=\"col-lg-12\" id=\"id0\">...Click Here to add</div>");
    div.appendTo($("#main"));
    $.each(data, function (index, emp) {
        var cls = "rowWhite";
        bg ? cls = "rowWhite" : cls = "rowLightGray";
        bg = !bg;
        div = $("<div id=\"" + emp.Id + "\" data-toggle=\"modal\" data-target=\"#myModal\" class=\"row col-lg-12 " + cls + "\">");
        var empId = emp.Id;
        div.html(
            "<div class=\"col-xs-4\" id=\"employeetitle" + empId + "\">" + emp.Title + "</div>" +
            "<div class=\"col-xs-4\" id=\"employeefname" + empId + "\">" + emp.Firstname + "</div>" +
            "<div class=\"col-xs-4\" id=\"emplastname" + empId + "\">" + emp.Lastname + "</div>"
            );
        div.appendTo($("#main"));
    }); //each
}

//update function
function update() {
        if ($("#EmployeeModalForm").valid()) {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.DepartmentId = $("#ddlDepts").val();
            emp.Id = $("#HiddenId").val();
            emp.IsTech = $("#tech").val();
            emp.Entity64 = $("#HiddenEntity").val();
            emp.StaffPicture64 = $("#ImageHolder").val();

            ajaxCall("Put", "api/employees", emp)
            .done(function (data) {
                $("#lblstatus").text(data);
                $("#EmployeeModalForm").modal("hide");
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
        }
        else {
            $("#lblstatus").text("fix existing problems");
        }
}

//create function
function create() {
        if ($("#EmployeeModalForm").valid()) {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.DepartmentId = $("#ddlDepts").val();
            emp.IsTech = $("#tech").val();
            emp.Id = $("#HiddenId").val();
            emp.StaffPicture64 = $("#ImageHolder").val();

            ajaxCall("Post", "api/employees", emp)
            .done(function (data) {
                $("#lblstatus").text(data);
                $("#EmployeeModalForm").modal("hide");
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
        }
        else {
            $("#lblstatus").text("fix existing problems");
        }
}

//GetById function
function GetById(empId) {
    $("#lblstatus").text("please be patient...");
    ajaxCall("Get", "api/employees/" + empId, "")
    .done(function (data) 
    {
        CopyInfoToModal(data);
    })
   
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

//load the department for the employee
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
        $("#ddlDepts").val(empdep);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        alert("error");
    });
}

//copy the employee info to the modal
function CopyInfoToModal(data) {
        $("#HiddenId").val(data.Id);
        $("#TextBoxEmail").val(data.Email);
        $("#TextBoxTitle").val(data.Title);
        $("#TextBoxFirstname").val(data.Firstname);
        $("#TextBoxLastname").val(data.Lastname);
        $("#TextBoxPhone").val(data.Phoneno);
        $("#Tech").val(data.IsTech);
        $('#ImageHolder').html('<img id="StaffPicture" height="120" width="110" src="data:image/png;base64,' + data.StaffPicture64 + '" />');
        $("#HiddenEntity").val(data.Entity64);
        loadDepartmentDDL(data.DepartmentId);
}

// call the ajax
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

//error routine
function errorRoutine(jqXHR) { //common error
    if (jqXHR.responseJSON == null) {
        $("#lblstatus").text(jqXHR.responseText);
    }
    else {
        $("#lblstatus").text(jqXHR.responseJSON.Message);
    }
}