using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Places.Services
{
    /// <summary>
    /// Get Place Details
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
            using (var svc = new Gale.Db.DataService("SP_GET_Place"))
            {
                svc.Parameters.Add("PLAC_Token", this.Model);
                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables
                Models.Place place = repo.GetModel<Models.Place>().FirstOrDefault();
                
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Models.Place>(place,
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                });

            }
        }
    }
}