using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Kennedy;

namespace HelpDeskDAL
{
    public class DepartmentDAO
    {
        public string Create(Department dept)
        {
            string newid = "";

            try
            {
                DbContext ctx = new DbContext();
                ctx.Save(dept, "departments");
                newid = dept._id.ToString();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Create");
            }

            return newid;
        }

        public Department GetById(string id)
        {
            Department retDept = null;
            DbContext _ctx;
            ObjectId _id = new ObjectId(id);
            try
            {
                _ctx = new DbContext();
                retDept = _ctx.Departments.FirstOrDefault(d => d._id == _id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }
            return retDept;
        }

        public List<Department> GetAll()
        {
            List<Department> allDept = new List<Department>();
            try
            {
                DbContext ctx = new DbContext();
                allDept = ctx.Departments.ToList();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetAll");
            }
            return allDept;

        }

        public int Update(Department dept)
        {
            int updateOK = -1;
            try
            {
                DbContext ctx = new DbContext();
                ctx.Save<Department>(dept, "departments");
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
            ObjectId deptid = new ObjectId(id);

            try
            {
                DbContext ctx = new DbContext();
                Department dept = ctx.Departments.FirstOrDefault(d => d._id == deptid);
                ctx.Delete<Department>(dept, "departments");
                deleteOk = true;
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Delete");
            }

            return deleteOk;
        }
    }
}