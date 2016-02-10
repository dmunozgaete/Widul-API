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
    public class GetByIdentifier : Gale.REST.Http.HttpReadActionResult<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Model</param>
        public GetByIdentifier(string model) : base(model) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            //------------------------------------------------------------------------------------------------------
            // DB Execution
            using (Gale.Db.DataService svc = new Gale.Db.DataService("PA_BPM_OBT_Documento"))
            {
                svc.Parameters.Add("DOCU_Identificador", this.Model);
                var document = this.ExecuteQuery(svc).GetModel<Models.V_Document>().FirstOrDefault();

                //Send Response
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(
                        new
                        {
                            name = document.identifier,
                            token = document.token,
                            createdAt = document.createdAt,
                            createdBy = new
                            {
                                token = document.entity_token,
                                name = document.entity_identifier,
                                type = document.entityType_name
                            },
                            state = new
                            {
                                token = document.state_token,
                                name = document.state_name,
                                identifier = document.state_identifier
                            },
                            type = new
                            {
                                token = document.type_token,
                                name = document.type_name,
                                identifier = document.type_identifier
                            },
                            lastObservation = document.lastObservation
                        },
                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter
                    )
                };

                return System.Threading.Tasks.Task.FromResult(response);
            }
            //------------------------------------------------------------------------------------------------------
        }
    }
}