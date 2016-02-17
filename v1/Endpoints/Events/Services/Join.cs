using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventToken"></param>
        /// <param name="user"></param>
        public Join(String eventToken,String user):base(eventToken) {
            _user = user;
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
               
                String state = (String)this.ExecuteScalar(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(new
                    {
                        state = state
                    },
                    new Gale.REST.Http.Formatter.KqlFormatter()),
                    StatusCode = System.Net.HttpStatusCode.Created
                });


            }


        }
    }
}