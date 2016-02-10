using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Bpm.Services.Document
{
    /// <summary>
    /// Retrieves Document Information
    /// </summary>
    public class Get : Gale.REST.Http.HttpReadActionResult<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Model</param>
        public Get(System.Guid model) : base(model.ToString()) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            //------------------------------------------------------------------------------------------------------
            // DB Execution
            using (Gale.Db.DataService svc = new Gale.Db.DataService("SP_BPM_GET_Document"))
            {
                svc.Parameters.Add("DOCU_Token", this.Model);
                var document = this.ExecuteQuery(svc).GetModel<Models.V_Document>().FirstOrDefault();

                //Send Response
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(
                        document,
                        new Gale.REST.Http.Formatter.KqlFormatter()
                    )
                };

                return System.Threading.Tasks.Task.FromResult(response);
            }
            //------------------------------------------------------------------------------------------------------
        }
    }
}