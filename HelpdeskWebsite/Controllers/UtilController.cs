using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    //controller to load the collections for the database
    public class UtilController : ApiController
    {
        [Route("api/util")]
        public IHttpActionResult Delete()
        {
            try
            {
                ViewModelUtils vm = new ViewModelUtils();
                return Ok(vm.LoadCollections());
            }
            catch (Exception ex)
            {
                return BadRequest("failed to load collections - " + ex.Message);
            }
        }
    }
}