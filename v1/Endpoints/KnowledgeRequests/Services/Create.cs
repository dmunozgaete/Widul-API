using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.KnowledgeRequests.Services
{
    /// <summary>
    /// Create a new Event
    /// </summary>
    public class Create : Gale.REST.Http.HttpCreateActionResult<Models.NewKnowledgeRequest>
    {

        private String _creator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="newKnowledgeREquest"></param>
        public Create(String creator , Models.NewKnowledgeRequest newKnowledgeRequest) : base(newKnowledgeRequest) {
            this._creator = creator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {

            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => this.Model == null, "EMPTY_BODY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => !Model.knowledge.isGuid(), "KNOWLEDGE_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(Model.excerpt), "EXCERPT_EMPTY", API.Errors.ResourceManager);


            using (var svc = new Gale.Db.DataService("SP_INS_KnowledgeRequest"))
            {
                svc.Parameters.Add("USER_Token", _creator);
                svc.Parameters.Add("KNOW_Token", this.Model.knowledge);

                svc.Parameters.Add("REQU_Excerpt", this.Model.excerpt);

                this.ExecuteAction(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Created
                });
            }
        }
    }
}