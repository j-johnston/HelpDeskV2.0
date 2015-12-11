using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    public class ProblemController : ApiController
    {
        //GET api/<department>
        [Route("api/problems")]
        public IHttpActionResult Get()
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                List<ProblemViewModel> allProblems = prob.GetAll();
                return Ok(allProblems);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/problems/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                prob.GetById(id);
                return Ok(prob);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieval failed of GetById - " + ex.Message);
            }
        }

        [Route("api/problems/{id}")]
        public IHttpActionResult Delete(string id)
        {
            ProblemViewModel prob = new ProblemViewModel();
            try
            {
                prob.GetById(id);
                return Ok(prob.Delete());
            }
            catch (Exception ex)
            {
                return BadRequest("Deletion failed - " + ex.Message);
            }
        }

        [Route("api/problems")]
        public IHttpActionResult Post(ProblemViewModel prob)
        {
            try
            {
                prob.Create();
                return Ok("Problem " + prob.Description + " created!");

            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - " + ex.Message);
            }
        }

        [Route("api/problems")]
        public IHttpActionResult Put(ProblemViewModel prob)
        {
            try
            {
                if (prob.Update() == 1)
                {
                    return Ok("Problem " + prob.Description + " updated!");
                }
                else if (prob.Update() == -2)
                {
                    return Ok("Data is stale for " + prob.Description + ". Problem not updated!");
                }
                else
                {
                    return Ok("Error updating problem " + prob.Description);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }
    }
}