using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    /// <summary>
    /// Get Event Details
    /// </summary>
    public class Get : Gale.REST.Http.HttpReadActionResult<String>
    {
       
        /// <summary>
        /// Get Event Details
        /// </summary>
        /// <param name="id">Event Token</param>
        public Get(string id) : base(id) {}

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            using (var svc = new Gale.Db.DataService("SP_GET_Event"))
            {
                svc.Parameters.Add("EVNT_Token", this.Model);
                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables
                Models.EventDetails eventDetail = repo.GetModel<Models.EventDetails>().FirstOrDefault();

                //----------------------------------------------------------------------------------------------------
                //Guard Exception's
                Gale.Exception.RestException.Guard(() => eventDetail == null, "EVENT_DONT_EXISTS", API.Errors.ResourceManager);
                //----------------------------------------------------------------------------------------------------

                //Setting Values ;)!
                eventDetail.comments_latest = repo.GetModel<Models.VW_EventComment>(1);
                eventDetail.tags = repo.GetModel<Models.EventTag>(2);
                eventDetail.place = repo.GetModel<Models.Place>(3).FirstOrDefault();
                eventDetail.participants_tops = repo.GetModel<Models.TopParticipant>(4);

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Models.EventDetails>(eventDetail,
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                });

            }
        }
    }
}