using RazorTemplates.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventTarget"></param>
        /// <param name="creator"></param>
        /// <param name="guests"></param>
        /// <param name="host"></param>
        public Create(String eventTarget, String creator, List<string> guests, string host) : base(guests) {
            this._creator = creator;
            this._event = eventTarget;
            this._host = host;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string RenderView(dynamic model)
        {
            //----------------------------------
            var assembly = this.GetType().Assembly;
            String resourcePath = "API.Endpoints.Events.Templates.Invitation.cshtml";

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                //------------------------------------------------------------------------------------------------------
                // GUARD EXCEPTIONS
                Gale.Exception.RestException.Guard(() => stream == null, "TEMPLATE_DONT_EXIST", API.Errors.ResourceManager);
                //------------------------------------------------------------------------------------------------------
                
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    var view = Template.Compile(reader.ReadToEnd());
                    return view.Render(model);
                }
            }
            //----------------------------------
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

            using (var svc = new Gale.Db.DataService("SP_INS_EventInvitations"))
            {
                svc.Parameters.Add("USER_Token", _creator);
                svc.Parameters.Add("EVNT_Token", _event);
                svc.Parameters.Add("GUESTS", String.Join("," ,this.Model));
                
                //Add the invitations (and Notification), 
                // retrieve the Event Details, and Users details, to Send Mail's
                var repo = this.ExecuteQuery(svc);

                var targetEvent = repo.GetModel<Models.EventDetails>().FirstOrDefault();
                var creator = repo.GetModel<Models.Guest>(1).FirstOrDefault();
                var guests = repo.GetModel<Models.Guest>(2);

                //TODO: Send Mail in Async (Fire && Forget)
                //http://stackoverflow.com/questions/22864367/fire-and-forget-approach
                //http://stackoverflow.com/questions/22301852/sending-two-emails-async-from-webapi
                Task.Factory.StartNew(() =>
                {
                    foreach(var guest in guests)
                    {
                        //----------------------------------------------------------------------
                        //Invitation Email
                        MailMessage message = new MailMessage()
                        {
                            IsBodyHtml = true,
                            From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["Mail:Account"]),
                            Subject = String.Format(Templates.Mail.Invitation_Subject,creator.fullname) ,
                            Body = RenderView(new
                            {
                                Event = targetEvent,
                                Creator = creator,
                                Guest = guest,
                                Url = String.Format("{0}#/public/home", this._host)
                            })
                        };
                        message.To.Add(new MailAddress(guest.email));
                        SmtpClient client = new SmtpClient();
                        client.Send(message);
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