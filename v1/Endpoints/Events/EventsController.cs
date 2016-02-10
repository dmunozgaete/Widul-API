using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Endpoints.Events
{
    /// <summary>
    /// Event Controller
    /// </summary>
    [Gale.Security.Oauth.Jwt.Authorize]
    public class EventsController : Gale.REST.RestController
    {
        #region EVENT

        /// <summary>
        /// Get all Events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.QueryableEndpoint(typeof(Models.VW_Events))]
        public IHttpActionResult Get()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.VW_Events>(this.Request);
        }

        /// <summary>
        /// Get Event Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            return new Services.Get(id.ToString());
        }

        /// <summary>
        /// Create a Event
        /// </summary>
        /// <param name="newEvent">Model</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(Models.NewEvent newEvent){

            return new Services.Create(this.User.PrimarySid(), newEvent);
        }
        #endregion

        #region --> TAGS 

        /// <summary>
        /// Get all Events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("Tags")]
        [Swashbuckle.Swagger.Annotations.QueryableEndpoint(typeof(Models.VW_EventTags))]
        public IHttpActionResult Tags()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.VW_EventTags>(this.Request);
        }

        #endregion

        #region --> COMMENTS

        /// <summary>
        /// Get all comments from specific event
        /// </summary>
        /// <param name="id">Event guid</param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/{id:Guid}/Comments")]
        public IHttpActionResult GetComments(String id)
        {
            //---------------------------------------------------------------------------
            // Setting Static Values (Create a Base Gale Query Language Configuration)
            var config = new Gale.REST.Queryable.OData.Builders.GQLConfiguration();

            // Adding some Filter's ;)
            config.filters.Add(new Gale.REST.Queryable.OData.Builders.GQLConfiguration.Filter()
            {
                field = "event_token",
                operatorAlias = "eq",
                value = id
            });
            //---------------------------------------------------------------------------

            // Setup in the Queryable Endpoint
            return new Gale.REST.Http.HttpQueryableActionResult<Models.VW_EventComment>(this.Request, config);
        }

        /// <summary>
        /// Create a comment
        /// </summary>
        /// <param name="id">Event guid</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>

        [HttpPost]
        [HierarchicalRoute("/{id:Guid}/Comments")]
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult CreateComment(String id, Models.NewComment comment)
        {
            return new Services.Comments.Create(id, this.User.PrimarySid().ToString(), comment);
        }

        #endregion

        #region --> SEARCH

        /// <summary>
        /// Search all Events by queries
        /// </summary>
        /// <param name="q">Query</param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/Search")]
        public IHttpActionResult SearchEvent(String q)
        {
            return new Services.Search(q);
        }

        #endregion

        #region --> JOIN
        /// <summary>
        /// Join to a Event by its GUID
        /// </summary>
        /// <param name="id">Event GUID</param>
        /// <returns></returns>
        [HttpPost]
        [HierarchicalRoute("/{id:Guid}/Join")]
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult JoinEvent(String id)
        {
            return new Services.Join(id, this.User.PrimarySid().ToString());
        }

        /// <summary>
        /// Left a Event by its GUID
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