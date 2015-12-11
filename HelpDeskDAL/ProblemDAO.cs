using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Kennedy;

namespace HelpDeskDAL
{
    public class ProblemDAO
    {
        public string Create(Problem prob)
        {
            string newid = "";

            try
            {
                DbContext ctx = new DbContext();
                ctx.Save(prob, "problems");
                newid = prob._id.ToString();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Create");
            }

            return newid;
        }

        public Problem GetById(string id)
        {
            Problem retProb = null;
            DbContext _ctx;
            ObjectId _id = new ObjectId(id);
            try
            {
                _ctx = new DbContext();
                retProb = _ctx.Problems.FirstOrDefault(p => p._id == _id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem " + ex.Message);
            }
            return retProb;
        }

        public List<Problem> GetAll()
        {
            List<Problem> allProb = new List<Problem>();
            try
            {
                DbContext ctx = new DbContext();
                allProb = ctx.Problems.ToList();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "GetAll");
            }
            return allProb;

        }

        public int Update(Problem prob)
        {
            int updateOK = -1;
            try
            {
                DbContext ctx = new DbContext();
                ctx.Save<Problem>(prob, "problems");
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
            ObjectId probid = new ObjectId(id);

            try
            {
                DbContext ctx = new DbContext();
                Problem prob = ctx.Problems.FirstOrDefault(p => p._id == probid);
                ctx.Delete<Problem>(prob, "problems");
                deleteOk = true;
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Delete");
            }

            return deleteOk;
        }
    }
}