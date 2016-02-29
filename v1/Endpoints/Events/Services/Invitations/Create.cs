using RazorTemplates.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mime;

namespace API.Endpoints.Events.Services.Invitations
{
    /// <summary>
    /// Create a new Event
    /// </summary>
    public class Create : Gale.REST.Http.HttpCreateActionResult<List<String>>
    {
        private String _creator;
        private String _host;
        private String _event;
        private String _apiUrl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventTarget"></param>
        /// <param name="creator"></param>
        /// <param name="guests"></param>
        /// <param name="host"></param>
        public Create(String eventTarget, String creator, List<string> guests, string host, string apiUrl)
            : base(guests)
        {
            this._creator = creator;
            this._event = eventTarget;
            this._host = host;
            this._apiUrl = apiUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => this.Model == null, "EMPTY_BODY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => !_creator.isGuid(), "CREATOR_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => !_event.isGuid(), "EVENT_EMPTY", API.Errors.ResourceManager);

            //------------------------------------------------
            //PARSE ALL GUEST VIA EMAIL (CAN BE SEND A NON USER IN WIDUL)
            List<String> users = new List<string>();
            List<String> nonUsers = new List<string>();
            this.Model.ForEach((guest) =>
            {
                if (guest.IndexOf("@") > 0)
                {
                    //NOT AND USER (YET ^^)
                    nonUsers.Add(guest);
                }
                else
                {
                    //IS USER
                    users.Add(guest);
                }
            });
            //------------------------------------------------


            using (var svc = new Gale.Db.DataService("SP_INS_EventInvitations"))
            {
                svc.Parameters.Add("USER_Token", _creator);
                svc.Parameters.Add("EVNT_Token", _event);
                if (users.Count > 0)
                {
                    svc.Parameters.Add("GUESTS", String.Join(",", users));
                }

                //Add the invitations (and Notification), 
                // retrieve the Event Details, and Users details, to Send Mail's
                var repo = this.ExecuteQuery(svc);

                var guests = repo.GetModel<Models.Guest>();
                Models.EventDetails eventDetail = repo.GetModel<Models.EventDetails>(1).FirstOrDefault();

                //----------------------------------------------------------------------------------------------------
                //Guard Exception's
                Gale.Exception.RestException.Guard(() => eventDetail == null, "EVENT_DONT_EXISTS", API.Errors.ResourceManager);
                //----------------------------------------------------------------------------------------------------

                //Setting Values ;)!
                eventDetail.comments_latest = repo.GetModel<Models.VW_EventComment>(2);
                eventDetail.tags = repo.GetModel<Models.EventTag>(3);
                eventDetail.place = repo.GetModel<Models.Place>(4).FirstOrDefault();
                eventDetail.participants_tops = repo.GetModel<Models.TopParticipant>(5);

                //TODO: Send Mail in Async (Fire && Forget)
                //http://stackoverflow.com/questions/22864367/fire-and-forget-approach
                //http://stackoverflow.com/questions/22301852/sending-two-emails-async-from-webapi
                Task.Factory.StartNew(() =>
                {

                    //WIDUL USERS
                    foreach (var guest in guests)
                    {
                        //----------------------------------------------------------------------
                        //(USABILITY - WIDUL USER)
                        //Create a Temporary Token for approved via same mail
                        List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>();

                        claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, guest.email));
                        claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.PrimarySid, guest.token.ToString()));
                        claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, guest.fullname));
                        claims.Add(new System.Security.Claims.Claim("photo", guest.photo.ToString()));
                        claims.Add(new System.Security.Claims.Claim("host", this._host));
                        var token = Gale.Security.Oauth.Jwt.Manager.CreateToken(claims, eventDetail.date);  //To Execution date for the event		
                        //----------------------------------------------------------------------

                        //----------------------------------------------------------------------
                        //Invitation Email
                        var message = new MailMessage()
                        {
                            Subject = String.Format(Templates.Mail.Invitation_Subject, eventDetail.creator_name),
                        };
                        message.To.Add(new MailAddress(guest.email));

                        //Embed Images , and send
                        new Mail.Invitation(message, new
                        {
                            Event = eventDetail,
                            Guest = guest,
                            Host = this._host,
                            apiUrl = this._apiUrl,
                            AccessToken = token.access_token
                        });
                        //----------------------------------------------------------------------
                    }

                    //USERS (Email's)
                    foreach (var email in nonUsers)
                    {
                        //----------------------------------------------------------------------
                        //(USABILITY - NON WIDUL USER)
                        //Create a Temporary Token for non-widul user, (need to register)
                        List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>();

                        claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, email));
                        claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.PrimarySid, email));
                        claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, email));
                        claims.Add(new System.Security.Claims.Claim("host", this._host));
                        var token = Gale.Security.Oauth.Jwt.Manager.CreateToken(claims, eventDetail.date);  //To Execution date for the event		
                        //----------------------------------------------------------------------

                        //----------------------------------------------------------------------
                        //Invitation Email
                        MailMessage message = new MailMessage()
                        {
                            Subject = String.Format(Templates.Mail.Invitation_Subject, eventDetail.creator_name),
                        };
                        message.To.Add(new MailAddress(email));

                        //Embed Images , and send
                        new Mail.Invitation(message, new
                        {
                            Event = eventDetail,
                            Guest =  new Models.Guest()
                            {
                                email = email,
                                fullname = email,
                                photo = null,
                                token = System.Guid.Empty
                            },
                            Host = this._host,
                            apiUrl = this._apiUrl,
                            AccessToken = token.access_token
                        });
                        //----------------------------------------------------------------------
                    }
                });

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Created
                });
            }
        }
    }
}