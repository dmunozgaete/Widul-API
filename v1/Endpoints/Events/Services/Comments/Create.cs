using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services.Comments
{
    /// <summary>
    /// Create a new comment for the target event
    /// </summary>
    public class Create: Gale.REST.Http.HttpCreateActionResult<Models.NewComment>
    {

        String _user;
        String _evenToken;
        String _host;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventToken">Event token</param>
        /// <param name="user">User Token</param>
        /// <param name="comment">Comment</param>
        public Create(String eventToken, String user,String host, Models.NewComment comment) : base(comment) {

            _user = user;
            _evenToken = eventToken;
            _host= host;

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
            Gale.Exception.RestException.Guard(() => this.Model == null, "BODY_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(this.Model.comment), "EMPTY_COMMENT", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_evenToken), "EMPTY_EVENT", API.Errors.ResourceManager);

            using (var svc = new Gale.Db.DataService("SP_INS_EventComment"))
            {
                svc.Parameters.Add("USER_Token", _user);
                svc.Parameters.Add("EVNT_Token", _evenToken);
                svc.Parameters.Add("COMM_Comment", this.Model.comment);

                var repo = this.ExecuteQuery(svc);
                var creator = repo.GetModel<Models.EventCreator>().FirstOrDefault();
                var newlyComment = repo.GetModel<Models.VW_EventComment>(1).FirstOrDefault();

                //TODO: Send Mail in Async (Fire && Forget)
                Task.Factory.StartNew(() =>
                {

                    //----------------------------------------------------------------------
                    //Invitation Email
                    MailMessage message = new MailMessage()
                    {
                        Subject = String.Format(Templates.Mail.CreateNewCommnet_Subject, newlyComment.creator_name),
                    };
                    message.To.Add(new MailAddress(creator.email));

                    //Embed Images , and send
                    new Mail.NewComment(message, new
                    {
                        Creator = creator,
                        Comment = newlyComment,
                        Host = this._host
                    });
                    //----------------------------------------------------------------------
                    
                });


                return Task.FromResult(new HttpResponseMessage()
                {
                    Content =new  ObjectContent<Models.VW_EventComment>(
                        newlyComment, 
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    ),
                    StatusCode = System.Net.HttpStatusCode.Created
                });
            }
            //------------------------------------------------



        }
    }
}