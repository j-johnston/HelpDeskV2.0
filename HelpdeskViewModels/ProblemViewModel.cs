using System;
using System.Collections.Generic;
using MongoDB.Bson;
using HelpDeskDAL;

namespace HelpdeskViewModels
{
    public class ProblemViewModel : ViewModelUtils
    {
        private ProblemDAO _dao;
        public string Id { get; set; }
        public string Description { get; set; }
        public string Entity64 { get; set; }

        //constructor
        public ProblemViewModel()
        {
            _dao = new ProblemDAO();
        }

        public void GetById(string id)
        {
            try
            {
                Problem prob = _dao.GetById(id);
                Id = prob._id.ToString();
                Description = prob.Description;
                Entity64 = Convert.ToBase64String(Serializer(prob));
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProblemViewModel", "GetById");
            }
        }

        public int Update()
        {
            int rowsUpdated = -1;

            try
            {
                byte[] bytProb = Convert.FromBase64String(Entity64);
                Problem prob = (Problem)Deserializer(bytProb);
                prob.Description = Description;
                rowsUpdated = _dao.Update(prob);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProblemViewModel", "Update");
            }
            return rowsUpdated;
        }

        public void Create()
        {
            try
            {
                Problem prob = new Problem();
                prob.Description = Description;
                Id = _dao.Create(prob);
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProblemViewModel", "Create");
            }
        }

        public List<ProblemViewModel> GetAll()
        {
            List<ProblemViewModel> viewModels = new List<ProblemViewModel>();

            try
            {
                List<Problem> problems = _dao.GetAll();

                foreach (Problem p in problems)
                {
                    //return only fields for display, subsequent get will other fields
                    ProblemViewModel viewModel = new ProblemViewModel();
                    viewModel.Id = p._id.ToString();
                    viewModel.Description = p.Description;
                    viewModels.Add(viewModel);  // add to list
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProblemViewModel", "GetAll");
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
                ErrorRoutine(ex, "ProblemViewModel", "Delete");
            }

            return deleteOk;

        }
    }
}
