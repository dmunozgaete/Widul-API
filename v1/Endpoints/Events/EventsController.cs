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
    public class EventsController : Gale.REST.RestController
    {
        #region EVENT

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
        /// Get Event Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Gale.Security.Oauth.Jwt.Authorize]
        [HierarchicalRoute("{id:Guid}/Personal")]
        public IHttpActionResult Personal(Guid id)
        {
            return new Services.Personal(id.ToString(), this.User.PrimarySid());
        }

        /// <summary>
        /// Create a Event
        /// </summary>
        /// <param name="newEvent">Model</param>
        /// <returns></returns>
        [HttpPost]
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult Post(Models.NewEvent newEvent)
        {

            return new Services.Create(this.User.PrimarySid(), newEvent);
        }

        /// <summary>
        /// Build a Thumbnails for the Event (For Sharing via Social)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("{id:Guid}/Thumbnail")]
        public IHttpActionResult Thumbnail(String id)
        {
            return new Services.Thumbnail(id);
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
        /// <param name="id">Event Token</param>
        /// <param name="offset">Offset</param>
        /// <param name="limit">Limit</param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/{id:Guid}/Comments")]
        public IHttpActionResult GetComments(String id, int offset = 0, int limit = 10)
        {
            return new Services.Comments.Get(id, offset, limit);
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
        public IHttpActionResult SearchEvent(String q = null)
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

        #region --> INVITES
        /// <summary>
        /// Invites User's to Join the Event (via Email And Notification)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guests"></param>
        /// <returns></returns>
        [HttpPost]
        [HierarchicalRoute("/{id:Guid}/Invitations")]
        [Gale.Security.Oauth.Jwt.Authorize]
        public IHttpActionResult CreateInvitations(String id, List<String> guests)
        {
            var me = this.User.PrimarySid().ToString();
            string host = this.Request.Headers.Referrer.ToString();
            return new Services.Invitations.Create(id, me, guests, host);
        }

        /// <summary>
        /// Test Invitation Mail (Only for Root Roles)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailTo"></param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/{id:Guid}/Mail/Invitations")]
        [Gale.Security.Oauth.Jwt.Authorize(Roles = WebApiConfig.RootRoles)]
        public IHttpActionResult TestInvitation(String id, [FromUri]List<String> guests)
        {
            var me = this.User.PrimarySid().ToString();
            string host = this.Request.Headers.Referrer.ToString();
            return new Services.Invitations.Create(id, me, guests, host);
        }
        #endregion
    }
}