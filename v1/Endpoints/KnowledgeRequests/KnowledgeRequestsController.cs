using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Endpoints.KnowledgeRequests
{

    [Gale.Security.Oauth.Jwt.Authorize]
    public class KnowledgeRequestsController : Gale.REST.RestController
    {

        
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            return new Services.Get(id.ToString());
        }
        

        [HttpPost]
        public IHttpActionResult Post(Models.NewKnowledgeRequest newKnowledgeRequest)
        {

            return new Services.Create(this.User.PrimarySid(), newKnowledgeRequest);
        }




    }
}