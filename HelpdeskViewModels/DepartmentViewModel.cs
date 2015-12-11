using System;
using System.Collections.Generic;
using MongoDB.Bson;
using HelpDeskDAL;

namespace HelpdeskViewModels
{
    public class DepartmentViewModel : ViewModelUtils
    {
        private DepartmentDAO _dao;
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public string Entity64 { get; set; }

        //constructor
        public DepartmentViewModel()
        {
            _dao = new DepartmentDAO();
        }

        public void GetById(string id)
        {
            try
            {
                Department dept = _dao.GetById(id);
                Id = dept._id.ToString();
                DepartmentName = dept.DepartmentName;
                Entity64 = Convert.ToBase64String(Serializer(dept));
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "DepartmentViewModel", "GetById");
            }
        }

        public int Update()
        {
            int rowsUpdated = -1;

            try
            {
                byte[] bytDept = Convert.FromBase64String(Entity64);
                Department dept = (Department)Deserializer(bytDept);
                dept.DepartmentName = DepartmentName;
                rowsUpdated = _dao.Update(dept);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "DepartmentViewModel", "Update");
            }
            return rowsUpdated;
        }

        public void Create()
        {
            try
            {
                Department dept = new Department();
                dept.DepartmentName = DepartmentName;
                Id = _dao.Create(dept);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "DepartmentViewModel", "Create");
            }
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> viewModels = new List<DepartmentViewModel>();

            try
            {
                List<Department> departments = _dao.GetAll();

                foreach (Department d in departments)
                {
                    //return only fields for display, subsequent get will other fields
                    DepartmentViewModel viewModel = new DepartmentViewModel();
                    viewModel.Id = d._id.ToString();
                    viewModel.DepartmentName = d.DepartmentName;
                    viewModel.Entity64 = d.Entity64;
                    viewModels.Add(viewModel);  // add to list
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "DepartmentViewModel", "GetAll");
            }
            return viewModels;
        }

        public bool Delete()
        {
            bool deleteOk = false;

            try
            {
                deleteOk = _dao.Delete(Id);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "DepartmentViewModel", "Delete");
            }

            return deleteOk;

        }
    }
}
