using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.KnowledgeRequests.Services
{
    public class Join :Gale.REST.Http.HttpCreateActionResult<String>
    {
        String _user;

        public Join(String knowledgeRequetsToken,String user):base(knowledgeRequetsToken) {


            _user = user;


        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {


            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => this.Model == null, "EMPTY_KNOWLEDGE_REQUEST", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);

            using (var svc = new Gale.Db.DataService("SP_INS_JoinKnowledgeRequest"))
            {
                svc.Parameters.Add("USER_Token", _user);
                svc.Parameters.Add("REQU_Token", this.Model);
               

                Object hasToken = this.ExecuteScalar(svc);

                Guid? newToken = null;

                if (hasToken != null)
                {
                    newToken = (Guid)hasToken;
                }

                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new ObjectContent<Object>(new
                    {
                        token = newToken
                    },
                    new Gale.REST.Http.Formatter.KqlFormatter()),
                    StatusCode = System.Net.HttpStatusCode.Created
                });


            }


        }
    }
}