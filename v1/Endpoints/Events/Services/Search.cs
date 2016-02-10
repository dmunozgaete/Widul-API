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
    /// Search Event's by queries
    /// </summary>
    public class Search : Gale.REST.Http.HttpReadActionResult<String>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="query"></param>
        public Search(String query): base(query) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            //------------------------------------------------------------------------
            // Database Action's
            using (var svc = new Gale.Db.DataService("SP_GET_SearchEvents"))
            {
                svc.Parameters.Add("Query", this.Model);
                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables (According to the degradation algorithm)
                var pagination = repo.GetModel<Models.Pagination>().FirstOrDefault();
                List<Models.VW_Events> events = repo.GetModel<Models.VW_Events>(1);

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(new
                    {
                        limit = pagination.limit,
                        offset = pagination.offset,
                        total = pagination.total,
                        items = events
                    },
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                });
            }
            //------------------------------------------------------------------------

        }
    }
}