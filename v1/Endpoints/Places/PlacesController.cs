using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Endpoints.Places
{
    /// <summary>
    /// Places Controller
    /// </summary>
    public class PlacesController : Gale.REST.RestController
    {

        /// <summary>
        /// Get all availables places
        /// </summary>
        /// <returns></returns>
        [Gale.Security.Oauth.Jwt.Authorize]
        [Swashbuckle.Swagger.Annotations.QueryableEndpoint(typeof(Models.Place))]
        public IHttpActionResult Get()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.Place>(this.Request);
        }

        /// <summary>
        /// Get all availables places
        /// </summary>
        /// <returns></returns>
        [HierarchicalRoute("{id:Guid}")]
        public IHttpActionResult Get(String id)
        {
            return new Services.Get(id);
        }

        /// <summary>
        /// Add New Place
        /// </summary>
        /// <returns></returns>
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult Post(Models.NewPlace newPlace)
        {
            return new Services.Create(this.User.PrimarySid(), newPlace);
        }
    }
}