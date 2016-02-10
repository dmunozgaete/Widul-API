using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Endpoints.Knowledge
{
    [Gale.Security.Oauth.Jwt.Authorize]
    public class KnowledgesController : Gale.REST.RestController
    {

        [Swashbuckle.Swagger.Annotations.QueryableEndpoint(typeof(Models.Knowledge))]
        public IHttpActionResult Get()
        {


            return new Gale.REST.Http.HttpQueryableActionResult<Models.Knowledge>(this.Request);


        }

        public IHttpActionResult Post(Models.newKnowledge newKnowledge)
        {
            return new Services.Create(this.User.PrimarySid(),newKnowledge);
        }


    }
}