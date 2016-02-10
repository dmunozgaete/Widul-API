using System;
using System.Web.Http;

namespace API.Endpoints.Bpm
{
    /// <summary>
    /// Task API
    /// </summary>
    [Gale.Security.Oauth.Jwt.Authorize]
    public class TasksController : Gale.REST.RestController
    {
        /// <summary>
        /// Retrieve User Pending Task's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return new Services.Task.Get(this.User.PrimarySid());
        }

    }
}