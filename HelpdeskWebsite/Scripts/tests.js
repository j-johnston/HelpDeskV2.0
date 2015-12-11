QUnit.test("MultiTier Tests", function (assert) {
    assert.async(9);    // 5 tests
    var first = {};
    var second = {};

    //Get all employees
    ajaxCall("Get", "api/employees", "")
    .then(function (data) {
        var numOfEmployees = data.length - 1;
        ok(numOfEmployees > 0, "Found " + numOfEmployees + " Employees");  // assert #1
    });

    //Add Employee
    emp = new Object();
    emp.Title = "Mr.";
    emp.Firstname = "John";
    emp.Lastname = "Smith";
    emp.Phoneno = "(555)555-6666";
    emp.Email = "th@the.com";
    emp.DepartmentId = "563bacdd50b76019281369fc";
    ajaxCall("Post", "api/employees", emp)
    .then(function (data) {
        var y = data.indexOf("failed");
        equal(y, -1, "Added employee");  // assert #2
        return (ajaxCall("Get", "api/employees", ""));
    }).then(function (employee) {
        var emp = employee[employee.length - 1];
        ok(emp.Firstname === "John", "New Employee was retrieved for deletion");
        ajaxCall("Delete", "api/employees/" + emp.Id, "");
        var x = employee.indexOf("not");
        ok(x === -1, "Employee Deleted");  //assert #3
    });

    //GEt employee then update him
    ajaxCall("Get", "api/employees", "").then(function (data) {
        first = data[0];
        var size = data.length - 1 > 0;
        return ajaxCall("Get", "api/employees/" + first.Id, "");
    }).then(function (data) {
        first = data;
        ok(data.Id.length === 24, "Employee 1 " + first.Id + " retrieved for regular update")  //assert #4
        return ajaxCall("Put", "api/employees", data);
    }).then(function (data) {
        var update = data.indexOf("not");
        ok(update === -1, "Employee was updated"); //assert #5
    });

    //update for concurrency
    ajaxCall("Get", "api/employees", "").then(function (data) {
        second = data[1];
        var size = data.length - 1 > 0;
        return ajaxCall("Get", "api/employees/" + second.Id, "");
    }).then(function (data) {
        second = data;
        ok(second.Id.length === 24, "Employee 2 " + second.Id + " retrieved for concurrency update"); //assert #6
        return ajaxCall("Put", "api/employees", second);
    }).then(function (data) {
        var updateOk = data.indexOf("not");
        ok(updateOk === -1, "First update for Employee " + second.Id + " concurrency completed");   //assert #7
        return ajaxCall("Put", "api/employees", second);
    }).then(function (data) {
        var updateNotOk = data.indexOf("stale");
        ok(updateNotOk !== -1, "Second update for Employee " + second.Id + " concurrency stale");  //assert #8
    });
});