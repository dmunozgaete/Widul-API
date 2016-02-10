using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Security.Services.Oauth
{
    /// <summary>
    /// Create or Authorize a Google User into the Application (Always Create a User Application)
    /// </summary>
    public class Google : Gale.REST.Http.HttpCreateActionResult<Models.GoogleCredentials>
    {
        HttpRequestMessage _request;    //Only for Content Negotiation

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="credentials"></param>
        public Google(HttpRequestMessage request, Models.GoogleCredentials credentials)
            : base(credentials)
        {
            _request = request;
        }


        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            //Check the Debug Access Token (Future Implementation)
            //http://stackoverflow.com/questions/8605703/how-to-verify-facebook-access-token

            //---------------------------------------------
            //Get Stream Image from the User
            byte[] imageBytes = null;
            if (Model.image != null)
            {
                try
                {
                    //Try to download the image
                    var webClient = new WebClient();
                    imageBytes = webClient.DownloadData(Model.image);
                }
                catch
                {

                }
            }

            using (Gale.Db.DataService svc = new Gale.Db.DataService("SP_COR_GET_AutenticateUser"))
            {
                svc.Parameters.Add("USER_FullName", Model.name);
                svc.Parameters.Add("USER_Email", Model.email);
                svc.Parameters.Add("USEX_Identifier", Model.id);
                svc.Parameters.Add("FILE_Binary", imageBytes);
                svc.Parameters.Add("FILE_Size", imageBytes.Length);
                svc.Parameters.Add("EXAU_Identifier", "gmail");  //Google

                Gale.Db.EntityRepository rep = this.ExecuteQuery(svc);

                Models.Account user = rep.GetModel<Models.Account>().FirstOrDefault();
                Gale.Db.EntityTable<Models.Role> profiles = rep.GetModel<Models.Role>(1);

                //------------------------------------------------------------------------------------------------------------------------
                //GUARD EXCEPTION
                Gale.Exception.RestException.Guard(() => user == null, "USERNAME_OR_PASSWORD_INCORRECT", Resources.Security.ResourceManager);
                //------------------------------------------------------------------------------------------------------------------------

                List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>();

                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.email));
                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.PrimarySid, user.token.ToString()));
                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.fullname));
                claims.Add(new System.Security.Claims.Claim("photo", user.photo.ToString()));
                profiles.ForEach((perfil) =>
                {
                    claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, perfil.identifier));
                });

                int expiration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Gale:Security:TokenTmeout"]);

                //RETURN TOKEN
                return Task.FromResult(_request.CreateResponse<Gale.Security.Oauth.Jwt.Wrapper>(
                    Gale.Security.Oauth.Jwt.Manager.CreateToken(claims, DateTime.Now.AddMinutes(expiration))
                ));
            }

        }
    }
}