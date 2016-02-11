using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventToken">Event token</param>
        /// <param name="user">User Token</param>
        /// <param name="comment">Comment</param>
        public Create(String eventToken, String user, Models.NewComment comment) : base(comment) {

            _user = user;
            _evenToken = eventToken;

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
                var newlyComment = repo.GetModel<Models.VW_EventComment>().FirstOrDefault();

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