using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services.Notifications
{
    public class EventAdvice : Gale.REST.Http.HttpBaseActionResult
    {
        public EventAdvice() { }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var svc = new Gale.Db.DataService("[SP_GET_EventNotification]"))
            {
                //svc.Parameters.Add("USER_Token", _creator);
                //svc.Parameters.Add("EVNT_Token", _event);

                //Add the invitations (and Notification), 
                // retrieve the Event Details, and Users details, to Send Mail's
                var repo = this.ExecuteQuery(svc);

                var events = repo.GetModel<Models.EventAdvice>();
                var participants = repo.GetModel<Models.EventParticipant>(1);


                //TODO: Send Mail in Async (Fire && Forget)
                Task.Factory.StartNew(() =>
                {

                    //WIDUL USERS
                    foreach (var cEvent in events)
                    {


                        var eventParticipants = (from participant in participants
                                                 where participant.event_token == cEvent.token
                                                 select participant);

                        //WIDUL USERS
                        foreach (var participant in eventParticipants)
                        {
                            //----------------------------------------------------------------------
                            //Invitation Email
                            var message = new MailMessage()
                            {
                                Subject = String.Format(Templates.Mail.EventAdvice_Subject, cEvent.name),
                            };
                            message.To.Add(new MailAddress("sebastianmorenoe@gmail.com"));

                            //Embed Images , and send
                            new Mail.EventAdvice(message, new
                            {
                                Event = cEvent,
                                Participant = participant,
                                Host = System.Configuration.ConfigurationManager.AppSettings["Platform:Url"],
                                
                            });
                            //----------------------------------------------------------------------
                        }
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