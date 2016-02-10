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
    [Gale.Security.Oauth.Jwt.Authorize]
    public class PlacesController :Gale.REST.RestController
    {
    
        /// <summary>
        /// Get all availables places
        /// </summary>
        /// <returns></returns>
        [Swashbuckle.Swagger.Annotations.QueryableEndpoint(typeof(Models.Place))]
        public IHttpActionResult Get()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.Place>(this.Request);
        }

        /// <summary>
        /// Add New Place
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Post(Models.NewPlace newPlace)
        {
            return new Services.Create(this.User.PrimarySid(), newPlace);
        }
    }
}