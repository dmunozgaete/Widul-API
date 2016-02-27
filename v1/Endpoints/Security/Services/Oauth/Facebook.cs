using RazorTemplates.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Security.Services.Oauth
{
    /// <summary>
    /// Create or Authorize a Facebook User into the Application (Always Create a User Application)
    /// </summary>
    public class Facebook : Gale.REST.Http.HttpCreateActionResult<Models.FacebookCredentials>
    {
        HttpRequestMessage _request;    //Only for Content Negotiation
        private string _host;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="host"></param>
        /// <param name="credentials"></param>
        public Facebook(HttpRequestMessage request, String host, Models.FacebookCredentials credentials)
            : base(credentials)
        {
            _request = request;
            _host = host;
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
                if (imageBytes != null)
                {
                    svc.Parameters.Add("FILE_Binary", imageBytes);
                    svc.Parameters.Add("FILE_Size", imageBytes.Length);
                }
                svc.Parameters.Add("EXAU_Identifier", "fbook");  //Facebook

                Gale.Db.EntityRepository rep = this.ExecuteQuery(svc);

                Models.Status status = rep.GetModel<Models.Status>().FirstOrDefault();
                Models.Account user = rep.GetModel<Models.Account>(1).FirstOrDefault();
                Gale.Db.EntityTable<Models.Role> profiles = rep.GetModel<Models.Role>(2);

                //------------------------------------------------------------------------------------------------------------------------
                //GUARD EXCEPTION
                Gale.Exception.RestException.Guard(() => user == null, "USERNAME_OR_PASSWORD_INCORRECT", Resources.Security.ResourceManager);
                //------------------------------------------------------------------------------------------------------------------------


                //------------------------------------------------------------------------------------------------------------------------
                //GUARD EXCEPTION
                if (status.isNew)
                {
                    //TODO: Send Mail in Async (Fire && Forget)
                    //http://stackoverflow.com/questions/22864367/fire-and-forget-approach
                    //http://stackoverflow.com/questions/22301852/sending-two-emails-async-from-webapi
                    Task.Factory.StartNew(() =>
                    {
                        //----------------------------------------------------------------------
                        //Welcome Email
                        MailMessage message = new MailMessage();
                        message.To.Add(new MailAddress(Model.email));
                        
                        new Mail.WelcomeMail(message, new
                        {
                            Name = Model.name,
                            Url = String.Format("{0}#/public/home", this._host)
                        });
                        //----------------------------------------------------------------------
                    });
                }
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