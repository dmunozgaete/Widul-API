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
    /// GET LIVE EVENTS
    /// </summary>
    public class Live : Gale.REST.Http.HttpReadActionResult<String>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="query"></param>
        public Live(String query) : base(query) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            //------------------------------------------------------------------------
            // Database Action's
            using (var svc = new Gale.Db.DataService("SP_GET_LiveEvents"))
            {
                svc.Parameters.Add("Query", this.Model);
                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables (According to the degradation algorithm)
                var pagination = repo.GetModel<Models.Pagination>().FirstOrDefault();
                List<Models.FindedEvent> events = repo.GetModel<Models.FindedEvent>(1);
                List<Models.FindedTag> tags = repo.GetModel<Models.FindedTag>(2);

                pagination.items = (from t in events
                                    select new
                                    {
                                        name = t.name,
                                        token = t.token,
                                        date = t.date,
                                        description = t.description,
                                        wasRecorded = t.wasRecorded,
                                        videoLink = t.videoLink,
                                        creator = new
                                        {
                                            name = t.creator_name,
                                            photo = t.creator_photo,
                                            token = t.creator_token
                                        },
                                        knowledge = new
                                        {
                                            name = t.knowledge_name,
                                            token = t.knowledge_token
                                        },
                                        tags = (from tag in tags where tag.event_id == t.id select tag.name)
                                    });

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(pagination,
                      System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter
                    )
                });
            }
            //------------------------------------------------------------------------

        }
    }
}