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


        #region --> JOIN

        /// <summary>
        /// Join a Knowledge Request by its GUID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        [HierarchicalRoute("/{id:Guid}/Join")]
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult JoinEvent(String id)
        {
            return new Services.Join(id, this.User.PrimarySid().ToString());
        }

        /// <summary>
        /// Left a Knowledge Request by its GUID
        /// </summary>
        /// <param name="id">Event GUID</param>
        /// <returns></returns>
        [HttpDelete]
        [HierarchicalRoute("/{id:Guid}/Join")]
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult LeftEvent(String id)
        {
            return new Services.Left(id, this.User.PrimarySid().ToString());
        }

        #endregion


    }
}