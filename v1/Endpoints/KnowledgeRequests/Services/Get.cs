using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.KnowledgeRequests.Services
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
        public Get(string id) : base(id) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            using (var svc = new Gale.Db.DataService("SP_GET_KnowledgeRequest"))
            {
                svc.Parameters.Add("REQU_Token", this.Model);
                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables
                Models.KnowledgeRequestDetail knowledgeRequest = repo.GetModel<Models.KnowledgeRequestDetail>().FirstOrDefault();

                    
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Models.KnowledgeRequestDetail>(knowledgeRequest,
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                });

            }
        }
    }
}