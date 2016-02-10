using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Bpm.Services.Task
{
    /// <summary>
    /// Retrieve Task Information
    /// </summary>
    public class Get : Gale.REST.Http.HttpReadActionResult<String>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token">Account Token</param>
        public Get(String token) : base(token) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            //------------------------------------------------------------------------------------------------------
            // DB Execution
            using (Gale.Db.DataService svc = new Gale.Db.DataService("SP_BPM_GET_Tasks"))
            {
                svc.Parameters.Add("ENTI_Token", this.Model);
                var tasks = this.ExecuteQuery(svc).GetModel<Models.Task>();

                //Send Response
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(tasks,
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                };

                return System.Threading.Tasks.Task.FromResult(response);
            }
            //------------------------------------------------------------------------------------------------------
        }
    }
}