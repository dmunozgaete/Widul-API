using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services.Comments
{
    /// <summary>
    /// Get Comments
    /// </summary>
    public class Get : Gale.REST.Http.HttpReadActionResult<String>
    {
        int _offset;
        int _limit;

        /// <summary>
        /// Get Event Details
        /// </summary>
        /// <param name="id">Event Token</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public Get(string id, int offset, int limit) : base(id)
        {
            _offset = offset;
            _limit = limit;
        }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            //------------------------------------------------------------------------
            // Database Action's
            using (var svc = new Gale.Db.DataService("SP_GET_EventComments"))
            {
                svc.Parameters.Add("EVNT_Token", this.Model);
                svc.Parameters.Add("Offset", _offset);
                svc.Parameters.Add("Limit", _limit);

                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get comments paginated
                var pagination = repo.GetModel<Models.Pagination>().FirstOrDefault();
                List<Models.VW_EventComment> comments = repo.GetModel<Models.VW_EventComment>(1);

                pagination.items = comments;

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(
                      pagination,
                      System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                    )
                });
            }
            //------------------------------------------------------------------------

        }
    }
}