using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    /// <summary>
    /// Retrieve a Target User
    /// </summary>
    public class Participants : Gale.REST.Http.HttpReadActionResult<String>
    {
        private string _event;
        private int _offset;
        private int _limit;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="query"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public Participants(String user, String eventTarget, int offset, int limit)
            : base(user)
        {
            _event = eventTarget;
            _offset = offset;
            _limit = limit;
        }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            using (Gale.Db.DataService svc = new Gale.Db.DataService("SP_GET_EventParticipants"))
            {
                svc.Parameters.Add("EVNT_Token", _event);
                svc.Parameters.Add("USER_Token", this.Model);
                svc.Parameters.Add("Limit", _limit);
                svc.Parameters.Add("Offset", _offset);

                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                var pagination = repo.GetModel<Models.Pagination>().FirstOrDefault();
                List<Models.Participant> persons = repo.GetModel<Models.Participant>(1);

                pagination.items = persons;

                //----------------------------------------------------------------------------------------------------
                //Create Response
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(
                        pagination,
                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                    )
                };

                //Return Task
                return Task.FromResult(response);
                //----------------------------------------------------------------------------------------------------

            }
        }
    }
}