using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    /// <summary>
    /// Join to an Event
    /// </summary>
    public class Join :Gale.REST.Http.HttpCreateActionResult<String>
    {
        String _user;
        String _host;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventToken"></param>
        /// <param name="user"></param>
        public Join(String eventToken,String user,String host) :base(eventToken) {
            _user = user;
            _host = host;
        }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {

            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => this.Model == null, "EMPTY_EVENT", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);

            using (var svc = new Gale.Db.DataService("SP_INS_JoinEvent"))
            {
                svc.Parameters.Add("USER_Token", _user);
                svc.Parameters.Add("EVNT_Token", this.Model);

                //String state = (String)this.ExecuteScalar(svc);
                var repo = this.ExecuteQuery(svc);

                var state = repo.GetModel<Models.participantState>().FirstOrDefault();
                var creator = repo.GetModel<Models.EventCreator>(1).FirstOrDefault();
                var newlyParticipant = repo.GetModel<Models.EventParticipant>(2).FirstOrDefault();
                
                //TODO: Send Mail in Async (Fire && Forget)
                Task.Factory.StartNew(() =>
                {

                    //----------------------------------------------------------------------
                    //Invitation Email
                    MailMessage message = new MailMessage()
                    {
                        Subject = String.Format(Templates.Mail.CreateNewParticipant_Subject, newlyParticipant.fullname),
                    };
                    message.To.Add(new MailAddress(creator.email));

                    //Embed Images , and send
                    new Mail.NewParticipant(message, new
                    {
                        Creator = creator,
                        Participant = newlyParticipant,
                        Host = this._host
                    });
                    //----------------------------------------------------------------------

                });

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(state,
                    new Gale.REST.Http.Formatter.KqlFormatter()),
                    StatusCode = System.Net.HttpStatusCode.Created
                });


            }


        }
    }
}