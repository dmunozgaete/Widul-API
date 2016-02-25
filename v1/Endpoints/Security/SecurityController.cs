using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Endpoints.Security
{
    /// <summary>
    /// Security Controller to grant JWT to Valid User's
    /// </summary>
    public class SecurityController : Gale.REST.RestController
    {
   
        #region SOCIAL AUTHENTICATOR'S

        /// <summary>
        /// Authorize via Facebook
        /// </summary>
        /// <param name="credentials">facebook data</param>
        /// <returns></returns>
        /// <response code="200">Authorized</response>
        /// <response code="500">Incorrect Access Token</response>
        [HttpPost]
        [HierarchicalRoute("/Oauth/Facebook")]
        public IHttpActionResult Authorize([FromBody]Models.FacebookCredentials credentials)
        {

            //------------------------------------------------------------------------------------------------------------------------
            //GUARD EXCEPTION
            Gale.Exception.RestException.Guard(() => credentials == null, "EMPTY_BODY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => credentials.accessToken == null, "EMPTY_ACCESSTOKEN", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => credentials.email == null, "EMPTY_EMAIL", API.Errors.ResourceManager);
            //------------------------------------------------------------------------------------------------------------------------

            string host = this.Request.Headers.Referrer.ToString();
            return new Services.Oauth.Facebook(this.Request, host, credentials);

        }

        /// <summary>
        /// Authorize via Google
        /// </summary>
        /// <param name="credentials">facebook data</param>
        /// <returns></returns>
        /// <response code="200">Authorized</response>
        /// <response code="500">Incorrect Access Token</response>
        [HttpPost]
        [HierarchicalRoute("/Oauth/Google")]
        public IHttpActionResult Authorize([FromBody]Models.GoogleCredentials credentials)
        {

            //------------------------------------------------------------------------------------------------------------------------
            //GUARD EXCEPTION
            Gale.Exception.RestException.Guard(() => credentials == null, "EMPTY_BODY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => credentials.accessToken == null, "EMPTY_ACCESSTOKEN", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => credentials.email == null, "EMPTY_EMAIL", API.Errors.ResourceManager);
            //------------------------------------------------------------------------------------------------------------------------

            return new Services.Oauth.Google(this.Request, credentials);

        }

        #endregion

    }
}