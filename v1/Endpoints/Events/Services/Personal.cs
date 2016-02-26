using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    /// <summary>
    /// Get Event Personal Details 
    /// </summary>
    public class Personal : Gale.REST.Http.HttpReadActionResult<String>
    {
        String _user = null;

        /// <summary>
        /// Get Event Details
        /// </summary>
        /// <param name="id">Event Token</param>
        public Personal(string id, String user)
            : base(id)
        {
            _user = user;
        }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            using (var svc = new Gale.Db.DataService("SP_GET_EventPersonal"))
            {
                svc.Parameters.Add("EVNT_Token", this.Model);
                svc.Parameters.Add("USER_Token", _user);

                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables
                Models.EventPersonal eventPersonal = repo.GetModel<Models.EventPersonal>().FirstOrDefault();

                //----------------------------------------------------------------------------------------------------
                //Guard Exception's
                Gale.Exception.RestException.Guard(() => eventPersonal == null, "EVENT_DONT_EXISTS", API.Errors.ResourceManager);
                //----------------------------------------------------------------------------------------------------

                //Setting Values ;)!                
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Models.EventPersonal>(eventPersonal,
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                });

            }
        }
    }
}