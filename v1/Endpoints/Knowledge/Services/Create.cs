using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Knowledge.Services
{
    /// <summary>
    /// Create a New Place
    /// </summary>
    public class Create : Gale.REST.Http.HttpCreateActionResult<Models.newKnowledge>
    {

        private String _creator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="knowledge"></param>
        public Create(String creator, Models.newKnowledge place) : base(place) {

            _creator = creator;
            
        }
        

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => this.Model == null, "EMPTY_BODY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(Model.name), "NAME_EMPTY", API.Errors.ResourceManager);
            
            using (var svc = new Gale.Db.DataService("SP_INS_Knowledge"))
            {


                svc.Parameters.Add("USR_Token", _creator);
                svc.Parameters.Add("KNOW_Name", this.Model.name);

                Guid newToken = (Guid)this.ExecuteScalar(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(new {
                        token = newToken
                    },
                    new Gale.REST.Http.Formatter.KqlFormatter()),
                    StatusCode = System.Net.HttpStatusCode.Created
                });
            }
        }
    }
}