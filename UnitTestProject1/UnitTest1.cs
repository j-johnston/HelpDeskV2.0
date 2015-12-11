using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpDeskDAL;
using HelpdeskViewModels;
using MongoDB.Bson;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        // Load database
        [TestMethod]
        public void CreateCollectionsShouldReturnTrue()
        {
            DALUtils util = new DALUtils();
            Assert.IsTrue(util.LoadCollections());
        }


        ////Employee tests

        //[TestMethod]
        //public void EmployeeDAOUpdateShouldReturnTrue()
        //{
        //    EmployeeDAO dao = new EmployeeDAO();

        //    //simulate user 1 getting an employee
        //    Employee emp = dao.GetById("561ff4bd50b7621560b47e01");
        //    emp.Phoneno = "555-555-5551";
        //    int rowsUpdated = dao.Update(emp);

        //    //user 1 makes update
        //    Assert.IsTrue(rowsUpdated == 1);
        //}

        //[TestMethod]
        //public void EmployeeDAOUpdateTwiceShouldReturnNegative()
        //{
        //    EmployeeDAO dao = new EmployeeDAO();

        //    //simulate 2 users getting an employee
        //    Employee emp = dao.GetById("561ff4bd50b7621560b47e01");
        //    Employee emp2 = dao.GetById("561ff4bd50b7621560b47e01");
        //    emp.Phoneno = "555-555-5551";
        //    int rowsUpdated = dao.Update(emp);
        //    if (rowsUpdated == 1)
        //    {
        //        rowsUpdated = dao.Update(emp2);
        //    }
        //    Assert.IsTrue(rowsUpdated == -2);
        //}

        //[TestMethod]
        //public void EmployeeDAOGetAllShouldReturnList()
        //{
        //    EmployeeDAO dao = new EmployeeDAO();
        //    List<Employee> emps = dao.GetAll();
        //    Assert.IsTrue(emps.Count > 0);
        //}

        //[TestMethod]
        //public void EmployeeDAOCreateAndDeleteShouldReturnTrue()
        //{
        //    bool deleteOk = false;
        //    Employee emp = new Employee();
        //    EmployeeDAO dao = new EmployeeDAO();

        //    //use an existing departmentid here
        //    emp.DepartmentId = new ObjectId("561ff4bd50b7621560b47dfc");

        //    //and some hardcoded data
        //    emp.Email = "someemp@here.com";
        //    emp.Firstname = "Some";
        //    emp.Lastname = "Employee";
        //    emp.Phoneno = "555-555-5555";
        //    emp.Title = "Mr.";
        //    string newid = dao.Create(emp);
        //    if (newid.Length == 24)     // new ids are a 24 byte hex string
        //    {
        //        deleteOk = dao.Delete(newid);
        //    }
        //    Assert.IsTrue(deleteOk);
        //}



        //// Department tests

        //[TestMethod]
        //public void DepartmentDAOUpdateShouldReturnTrue()
        //{
        //    DepartmentDAO dao = new DepartmentDAO();

        //    //simulate user 1 getting an department
        //    Department dept = dao.GetById("561ff4bd50b7621560b47dfc");
        //    dept.DepartmentName = "Management";
        //    int rowsUpdated = dao.Update(dept);

        //    //user 1 makes update
        //    Assert.IsTrue(rowsUpdated == 1);
        //}

        //[TestMethod]
        //public void DepartmentDAOUpdateTwiceShouldReturnNegative()
        //{
        //    DepartmentDAO dao = new DepartmentDAO();

        //    //simulate 2 users getting an department
        //    Department dept = dao.GetById("561ff4bd50b7621560b47dfc");
        //    Department dept2 = dao.GetById("561ff4bd50b7621560b47dfc");
        //    dept.DepartmentName = "Management";
        //    int rowsUpdated = dao.Update(dept);
        //    if (rowsUpdated == 1)
        //    {
        //        rowsUpdated = dao.Update(dept2);
        //    }
        //    Assert.IsTrue(rowsUpdated == -2);
        //}

        //[TestMethod]
        //public void DepartmentDAOGetAllShouldReturnList()
        //{
        //    DepartmentDAO dao = new DepartmentDAO();
        //    List<Department> depts = dao.GetAll();
        //    Assert.IsTrue(depts.Count > 0);
        //}

        //[TestMethod]
        //public void DepartmentDAOCreateAndDeleteShouldReturnTrue()
        //{
        //    bool deleteOk = false;
        //    Department dept = new Department();
        //    DepartmentDAO dao = new DepartmentDAO();

        //    //and some hardcoded data
        //    dept.DepartmentName = "Throwing stuff";
        //    string newid = dao.Create(dept);
        //    if (newid.Length == 24)     // new ids are a 24 byte hex string
        //    {
        //        deleteOk = dao.Delete(newid);
        //    }
        //    Assert.IsTrue(deleteOk);
        //}



        ////Problem tests


        //[TestMethod]
        //public void ProblemDAOUpdateShouldReturnTrue()
        //{
        //    ProblemDAO dao = new ProblemDAO();

        //    //simulate user 1 getting an department
        //    Problem prob = dao.GetById("561ff4bd50b7621560b47e0b");
        //    prob.Description = "Beer truck crashed out front. Need to drink all the beer";
        //    int rowsUpdated = dao.Update(prob);

        //    //user 1 makes update
        //    Assert.IsTrue(rowsUpdated == 1);
        //}

        //[TestMethod]
        //public void ProblemDAOUpdateTwiceShouldReturnNegative()
        //{
        //    ProblemDAO dao = new ProblemDAO();

        //    //simulate 2 users getting an department
        //    Problem prob = dao.GetById("561ff4bd50b7621560b47e0b");
        //    Problem prob2 = dao.GetById("561ff4bd50b7621560b47e0b");
        //    prob.Description = "Beer truck crashed out front. Need to drink all the beer";
        //    int rowsUpdated = dao.Update(prob);
        //    if (rowsUpdated == 1)
        //    {
        //        rowsUpdated = dao.Update(prob2);
        //    }
        //    Assert.IsTrue(rowsUpdated == -2);
        //}

        //[TestMethod]
        //public void ProblemDAOGetAllShouldReturnList()
        //{
        //    ProblemDAO dao = new ProblemDAO();
        //    List<Problem> probs = dao.GetAll();
        //    Assert.IsTrue(probs.Count > 0);
        //}

        //[TestMethod]
        //public void ProblemDAOCreateAndDeleteShouldReturnTrue()
        //{
        //    bool deleteOk = false;
        //    Problem prob = new Problem();
        //    ProblemDAO dao = new ProblemDAO();

        //    //and some hardcoded data
        //    prob.Description = "Beer truck crashed out front. Need to drink all the beer";
        //    string newid = dao.Create(prob);
        //    if (newid.Length == 24)     // new ids are a 24 byte hex string
        //    {
        //        deleteOk = dao.Delete(newid);
        //    }
        //    Assert.IsTrue(deleteOk);
        //}




        // Employee view model tests




        //[TestMethod]
        //public void EmployeeVMUpdateShouldReturnTrue()
        //{
        //    EmployeeViewModel vm = new EmployeeViewModel();

        //   // simulate user 1 getting an employee
        //     vm.GetById("5620118f50b76231c8ad4574");
        //    vm.Phoneno = "555-555-5551";
        //    int rowsUpdated = vm.Update();

        //    //user 1 makes update
        //     Assert.IsTrue(rowsUpdated == 1);
        //}

        //[TestMethod]
        //public void EmployeeVMUpdateTwiceShouldReturnNegative()
        //{
        //    EmployeeViewModel vm = new EmployeeViewModel();
        //    EmployeeViewModel vm2 = new EmployeeViewModel();

        //   // simulate 2 users getting an employee
        //     vm.GetById("5620118f50b76231c8ad4574");
        //    vm2.GetById("5620118f50b76231c8ad4574");
        //    vm.Phoneno = "555-555-5551";
        //    int rowsUpdated = vm.Update();
        //    if (rowsUpdated == 1)
        //    {
        //        rowsUpdated = vm2.Update();
        //    }
        //    Assert.IsTrue(rowsUpdated == -2);
        //}

        //[TestMethod]
        //public void EmployeeVMGetAllShouldReturnList()
        //{
        //    EmployeeViewModel vm = new EmployeeViewModel();
        //    List<EmployeeViewModel> emps = vm.GetAll();
        //    Assert.IsTrue(emps.Count > 0);
        //}

        //[TestMethod]
        //public void EmployeeVMCreateAndDeleteShouldReturnTrue()
        //{
        //    bool deleteOk = false;
        //    EmployeeViewModel vm = new EmployeeViewModel();

        //   // use an existing departmentid here
        //     vm.DepartmentId = "561ff4bd50b7621560b47dfc";

        //   // and some hardcoded data
        //     vm.Email = "someemp@here.com";
        //    vm.Firstname = "Some";
        //    vm.Lastname = "Employee";
        //    vm.Phoneno = "555-555-5555";
        //    vm.Title = "Mr.";
        //    vm.Create();
        //    if (vm.Id.Length == 24)     // new ids are a 24 byte hex string
        //    {
        //        deleteOk = vm.Delete();
        //    }
        //    Assert.IsTrue(deleteOk);
        //}




        //// Department view model tests

        //[TestMethod]
        //public void DepartmentVMUpdateShouldReturnTrue()
        //{
        //    DepartmentViewModel vm = new DepartmentViewModel();

        //    //simulate user 1 getting an department
        //    vm.GetById("5620118f50b76231c8ad456f");
        //    vm.DepartmentName = "Management";
        //    int rowsUpdated = vm.Update();

        //    //user 1 makes update
        //    Assert.IsTrue(rowsUpdated == 1);
        //}

        //[TestMethod]
        //public void DepartmentVMUpdateTwiceShouldReturnNegative()
        //{
        //    DepartmentViewModel vm = new DepartmentViewModel();
        //    DepartmentViewModel vm2 = new DepartmentViewModel();

        //    //simulate 2 users getting an department
        //    vm.GetById("5620118f50b76231c8ad456f");
        //    vm2.GetById("5620118f50b76231c8ad456f");
        //    vm.DepartmentName = "Management";
        //    int rowsUpdated = vm.Update();
        //    if (rowsUpdated == 1)
        //    {
        //        rowsUpdated = vm2.Update();
        //    }
        //    Assert.IsTrue(rowsUpdated == -2);
        //}

        //[TestMethod]
        //public void DepartmentVMGetAllShouldReturnList()
        //{
        //    DepartmentViewModel vm = new DepartmentViewModel();
        //    List<DepartmentViewModel> depts = vm.GetAll();
        //    Assert.IsTrue(depts.Count > 0);
        //}

        //[TestMethod]
        //public void DepartmentVMCreateAndDeleteShouldReturnTrue()
        //{
        //    bool deleteOk = false;
        //    DepartmentViewModel vm = new DepartmentViewModel();

        //    //and some hardcoded data
        //    vm.DepartmentName = "Throwing stuff";
        //    vm.Create();
        //    if (vm.Id.Length == 24)     // new ids are a 24 byte hex string
        //    {
        //        deleteOk = vm.Delete();
        //    }
        //    Assert.IsTrue(deleteOk);
        //}



        //// problem view model tests

        //[TestMethod]
        //public void ProblemVMUpdateShouldReturnTrue()
        //{
        //    ProblemViewModel vm = new ProblemViewModel();

        //    //simulate user 1 getting an department
        //    vm.GetById("5620118f50b76231c8ad457e");
        //    vm.Description = "Management";
        //    int rowsUpdated = vm.Update();

        //    //user 1 makes update
        //    Assert.IsTrue(rowsUpdated == 1);
        //}

        //[TestMethod]
        //public void ProblemVMUpdateTwiceShouldReturnNegative()
        //{
        //    ProblemViewModel vm = new ProblemViewModel();
        //    ProblemViewModel vm2 = new ProblemViewModel();

        //    //simulate 2 users getting an department
        //    vm.GetById("5620118f50b76231c8ad457e");
        //    vm2.GetById("5620118f50b76231c8ad457e");
        //    vm.Description = "Management";
        //    int rowsUpdated = vm.Update();
        //    if (rowsUpdated == 1)
        //    {
        //        rowsUpdated = vm2.Update();
        //    }
        //    Assert.IsTrue(rowsUpdated == -2);
        //}

        //[TestMethod]
        //public void ProblemVMGetAllShouldReturnList()
        //{
        //    ProblemViewModel vm = new ProblemViewModel();
        //    List<ProblemViewModel> probs = vm.GetAll();
        //    Assert.IsTrue(probs.Count > 0);
        //}

        //[TestMethod]
        //public void ProblemVMCreateAndDeleteShouldReturnTrue()
        //{
        //    bool deleteOk = false;
        //    ProblemViewModel vm = new ProblemViewModel();

        //    //and some hardcoded data
        //    vm.Description = "Throwing stuff";
        //    vm.Create();
        //    if (vm.Id.Length == 24)     // new ids are a 24 byte hex string
        //    {
        //        deleteOk = vm.Delete();
        //    }
        //    Assert.IsTrue(deleteOk);
        //}
    }
}
