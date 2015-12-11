using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Kennedy;

namespace HelpDeskDAL
{
    public class EmployeeDAO
    {
        public string Create(Employee emp)
        {
            string newid = "";

            try
            {
                DbContext ctx = new DbContext();
                ctx.Save(emp, "employees");
                newid = emp._id.ToString();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Create");
            }

            return newid;
        }

        public Employee GetById(string id)
        {
            Employee retEmp = null;
            ObjectId _id = new ObjectId(id);
            try
            {
                DbContext _ctx = new DbContext();
                retEmp = _ctx.Employees.FirstOrDefault(e => e._id == _id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }
            return retEmp;
        }

        public List<Employee> GetAll()
        {
            List<Employee> allEmps = new List<Employee>();
            try
            {
                DbContext ctx = new DbContext();
                allEmps = ctx.Employees.ToList();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetAll");
            }
            return allEmps;

        }

        public int Update(Employee emp)
        {
            int updateOK = -1;
            try
            {
                DbContext ctx = new DbContext();
                ctx.Save<Employee>(emp, "employees");
                updateOK = 1;
            }
            catch (MongoConcurrencyException ex)
            {
                updateOK = -2;
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }
            return updateOK;
        }

        public bool Delete(string id)
        {
            bool deleteOk = false;
            ObjectId empid = new ObjectId(id);

            try
            {
                DbContext ctx = new DbContext();
                Employee emp = ctx.Employees.FirstOrDefault(e => e._id == empid);
                ctx.Delete<Employee>(emp, "employees");
                deleteOk = true;
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Delete");
            }

            return deleteOk;
        }
    }
}
