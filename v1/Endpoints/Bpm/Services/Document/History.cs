using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Bpm.Services.Document
{
    /// <summary>
    /// Retrieves the State's History of an specific Document
    /// </summary>
    public class History : Gale.REST.Http.HttpReadActionResult<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Model</param>
        public History(string model) : base(model) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {

            //------------------------------------------------------------------------------------------------------
            // DB ExecutionH
            using (Gale.Db.DataService svc = new Gale.Db.DataService("SP_BPM_GET_History"))
            {
                svc.Parameters.Add("DOCU_Token", this.Model);
                var items = this.ExecuteQuery(svc).GetModel<Models.History>();

                //Send Response
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(
                         items,
                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                    )
                };

                return System.Threading.Tasks.Task.FromResult(response);
            }
            //------------------------------------------------------------------------------------------------------
        }
    }
}