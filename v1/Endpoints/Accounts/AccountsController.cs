using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Endpoints.Accounts
{
    /// <summary>
    /// Account API
    /// </summary>
    [Gale.Security.Oauth.Jwt.Authorize]
    public class AccountsController : Gale.REST.RestController
    {

        /// <summary>
        /// Retrieve Account's
        /// </summary>
        /// <returns></returns>
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK)]
        [Gale.Security.Oauth.Jwt.Authorize(Roles = API.WebApiConfig.RootRoles)]
        public IHttpActionResult Get()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.VW_Users>(this.Request);
        }

        /// <summary>
        /// Retrieve Target Account Information
        /// </summary>
        /// <param name="id">Account Token</param>
        /// <returns></returns>
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Get(String id)
        {
            //------------------------------------------------------------------------------------------------------
            // GUARD EXCEPTIONS
            Gale.Exception.RestException.Guard(() => !id.isGuid(), "ID_INVALID_GUID", API.Errors.ResourceManager);
            //------------------------------------------------------------------------------------------------------
            return new Services.Get(id);
        }

        /// <summary>
        /// Retrieve Current Account Information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/Me")]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Current()
        {
            return new Services.Get(this.User.PrimarySid());
        }

        /// <summary>
        /// Retrieve Account's Friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK)]
        [HierarchicalRoute("/Me/Friends")]
        public IHttpActionResult MyFriends()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.VW_Users>(this.Request);
        }


        /// <summary>
        /// Retrieve Account's Friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK)]
        [HierarchicalRoute("/{id:Guid}/Friends")]
        public IHttpActionResult Friends(String id)
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.VW_Users>(this.Request);
        }


        /// <summary>
        /// Follow Accounts
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Created)]
        [HierarchicalRoute("/{account:Guid}/Follow")]
        public IHttpActionResult Follow(String account)
        {
            //------------------------------------------------------------------------------------------------------
            // GUARD EXCEPTIONS
            Gale.Exception.RestException.Guard(() => account == null, "EMPTY_FOLLOWER", API.Errors.ResourceManager);
            //------------------------------------------------------------------------------------------------------

            return new Services.Follow(this.User.PrimarySid(), account);
        }

        /// <summary>
        /// UnFollow Accounts
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK)]
        [HierarchicalRoute("/{account:Guid}/Follow")]
        public IHttpActionResult Unfollow(String account)
        {
            //------------------------------------------------------------------------------------------------------
            // GUARD EXCEPTIONS
            Gale.Exception.RestException.Guard(() => account == null, "EMPTY_FOLLOWER", API.Errors.ResourceManager);
            //------------------------------------------------------------------------------------------------------

            return new Services.Unfollow(this.User.PrimarySid(), account);
        }

    }
}