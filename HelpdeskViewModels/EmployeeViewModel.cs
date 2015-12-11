﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using HelpDeskDAL;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel : ViewModelUtils
    {
        private EmployeeDAO _dao;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Entity64 { get; set; }
        public string Id { get; set; }
        public string DepartmentId { get; set; }
        public string StaffPicture64 { get; set; }
        public bool IsTech { get; set; }

        //constructor
        public EmployeeViewModel()
            {
                _dao = new EmployeeDAO();
            }

        public void GetById(string id)
        {
            try
            {
                Employee emp = _dao.GetById(id);
                Id = emp._id.ToString();
                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                Email = emp.Email;
                DepartmentId = emp.DepartmentId.ToString();
                StaffPicture64 = emp.StaffPicture64;
                IsTech = emp.IsTech;
                Entity64 = Convert.ToBase64String(Serializer(emp));
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeViewModel", "GetById");
            }
        }

        public List<EmployeeViewModel> GetAllTech()
        {
            List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();

            try
            {
                List<Employee> employees = _dao.GetAll();

                foreach (Employee e in employees)
                {
                    //return only fields for display, subsequent get will other fields
                    EmployeeViewModel viewModel = new EmployeeViewModel();
                    if (viewModel.IsTech == true)
                    {
                        viewModel.Id = e._id.ToString();
                        viewModel.Lastname = e.Lastname;
                        viewModels.Add(viewModel);  // add to list
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmplyeeViewModel", "GetAll");
            }
            return viewModels;
        }

        public int Update()
        {
            int rowsUpdated = -1;

            try
            {
                byte[] bytEmp = Convert.FromBase64String(Entity64);
                Employee emp = (Employee)Deserializer(bytEmp);
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Phoneno = Phoneno;
                emp.Email = Email;
                emp.IsTech = IsTech;
                emp.DepartmentId = new ObjectId(DepartmentId);
                emp.StaffPicture64 = StaffPicture64;
                rowsUpdated = _dao.Update(emp);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeViewModel", "Update");
            }
            return rowsUpdated;
        }

        public void Create()
        {
            try
            {
                Employee emp = new Employee();
                emp.DepartmentId = new ObjectId(DepartmentId);
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Phoneno = Phoneno;
                emp.Email = Email;
                emp.StaffPicture64 = StaffPicture64;
                emp.IsTech = IsTech;
                Id = _dao.Create(emp);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeViewModel", "Create");
            }
        }

        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();

            try
            {
                List<Employee> employees = _dao.GetAll();

                foreach (Employee e in employees)
                {
                    //return only fields for display, subsequent get will other fields
                    EmployeeViewModel viewModel = new EmployeeViewModel();
                    viewModel.Id = e._id.ToString();
                    viewModel.Title = e.Title;
                    viewModel.Firstname = e.Firstname;
                    viewModel.Lastname = e.Lastname;
                    viewModel.StaffPicture64 = e.StaffPicture64;
                    viewModels.Add(viewModel);  // add to list
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmplyeeViewModel", "GetAll");
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
            catch(Exception ex)
            {
                ErrorRoutine(ex, "EmployeeViewModel", "Delete");
            }

            return deleteOk;

        }
    }


}
