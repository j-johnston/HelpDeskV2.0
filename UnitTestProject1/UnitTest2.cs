using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpDeskDAL;
using HelpdeskViewModels;
using MongoDB.Bson;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [TestMethod]
        public void CallDAOComprehensiveTestsReturnTrue()
        {
            CallDAO dao = new CallDAO();
            Call call = new Call();
            call.DateOpened = DateTime.Now;
            call.DateClosed = null;
            call.OpenStatus = true;
            call.EmployeeId = new MongoDB.Bson.ObjectId("5653de0e50b76263649f8f4c");
            call.TechId = new MongoDB.Bson.ObjectId("5653de0e50b76263649f8f52");
            call.ProblemId = new MongoDB.Bson.ObjectId("5653de0e50b76263649f8f56");
            call.Notes = "Bigshot can't plug in device. Burner plugged it in";
            string newId = dao.Create(call);
            this.testContextInstance.WriteLine("New Call Id == " + newId);
            call = dao.GetById(newId);
            this.testContextInstance.WriteLine("Call retrieved");
            call.Notes = call.Notes + "\nOrdered new Ram";

            if (dao.Update(call) == 1)
                this.testContextInstance.WriteLine("Call was updated " + call.Notes);
            else
                Trace.WriteLine("Call was not updated");

            if (dao.Delete(newId))
                this.testContextInstance.WriteLine("Call was deleted");
            else
                this.testContextInstance.WriteLine("Call was not deleted");
            call = dao.GetById(newId);
            Assert.IsNull(call);
        }

        [TestMethod]
        [ExpectedException(typeof(MongoDB.Driver.MongoException), "No Id Exists")]
        public void CallViewModelComprehensiveTestsReturnTrue()
        {
            CallViewModel vm = new CallViewModel();
            vm.DateOpened = DateTime.Now;
            vm.OpenStatus = true;
            vm.EmployeeId = "5653de0e50b76263649f8f4c";
            vm.TechId = "5653de0e50b76263649f8f52";
            vm.ProblemId = "5653de0e50b76263649f8f56";
            vm.Notes = "Bigshot can't plug in device. Burner plugged it in";
            vm.Create();
            this.testContextInstance.WriteLine("New Call id == " + vm.Id);
            vm.GetById(vm.Id);
            this.testContextInstance.WriteLine("Call retrieved");
            vm.Notes = vm.Notes + "\nOrdered new Ram";

            if(vm.Update() == 1)
                this.testContextInstance.WriteLine("Call was updated " + vm.Notes);
            else
                Trace.WriteLine("Call was not updated");

            if (vm.Delete())
                this.testContextInstance.WriteLine("Call was deleted");
            else
                this.testContextInstance.WriteLine("Call was not deleted");

            vm.Update();
        }
    }
}
