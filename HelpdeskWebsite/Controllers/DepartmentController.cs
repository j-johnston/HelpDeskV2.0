using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    public class DepartmentController : ApiController
    {
        //GET api/<department>
        [Route("api/departments")]
        public IHttpActionResult Get()
        {
            try
            {
                DepartmentViewModel dep = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartments = dep.GetAll();
                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/departments/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DepartmentViewModel dep = new DepartmentViewModel();
                dep.GetById(id);
                return Ok(dep);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieval failed of GetById - " + ex.Message);
            }
        }

        [Route("api/departments/{id}")]
        public IHttpActionResult Delete(string id)
        {
            DepartmentViewModel dep = new DepartmentViewModel();
            try
            {
                dep.GetById(id);
                return Ok(dep.Delete());
            }
            catch (Exception ex)
            {
                return BadRequest("Deletion failed - " + ex.Message);
            }
        }

        [Route("api/departments")]
        public IHttpActionResult Post(DepartmentViewModel dep)
        {
            try
            {
                dep.Create();
                return Ok("Department " + dep.DepartmentName + " created!");

            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - " + ex.Message);
            }
        }

        [Route("api/departments")]
        public IHttpActionResult Put(DepartmentViewModel dep)
        {
            try
            {
                if (dep.Update() == 1)
                {
                    return Ok("Department " + dep.DepartmentName + " updated!");
                }
                else if (dep.Update() == -2)
                {
                    return Ok("Data is stale for " + dep.DepartmentName + ". Department not updated!");
                }
                else
                {
                    return Ok("Error updating department " + dep.DepartmentName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }
    }
}
