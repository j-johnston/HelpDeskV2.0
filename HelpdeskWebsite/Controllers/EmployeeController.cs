using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite
{
    public class EmployeeController : ApiController
    {
        //GET api/<controller>
        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.GetById(id);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieval failed of GetById - " + ex.Message);
            }
        }

        [Route("api/employees/{id}")]
        public IHttpActionResult Delete(string id)
        {
            EmployeeViewModel emp = new EmployeeViewModel();
            try
            {
                emp.GetById(id);
                return Ok(emp.Delete());
            }
            catch (Exception ex)
            {
                return BadRequest("Deletion failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Create();
                return Ok("Employee " + emp.Lastname + " created!");

            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                if (emp.Update() == 1)
                {
                    return Ok("Employee " + emp.Lastname + " updated!");
                }
                else if (emp.Update() == -2)
                {
                    return Ok("Data is stale for " + emp.Lastname + ". Employee not updated!");
                }
                else
                {
                    return Ok("employee " + emp.Lastname + " not updated");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update not happened - " + ex.Message);
            }
        }
    }
}
