using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.KnowledgeRequests.Services
{
    public class Left :Gale.REST.Http.HttpDeleteActionResult
    {

        String _user;

        public Left(String knowledgeRequetsToken, String user) : base(knowledgeRequetsToken) {

            _user = user;

        }

        public override Task<HttpResponseMessage> ExecuteAsync(string token, CancellationToken cancellationToken)
        {


            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => token == null, "EMPTY_KNOWLEDGE_REQUEST", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);

            using (var svc = new Gale.Db.DataService("SP_DEL_LeftKnowledgeRequest"))
            {
                svc.Parameters.Add("USR_Token", _user);
                svc.Parameters.Add("REQU_Token", token);

                this.ExecuteAction(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                });
            }


        }
    }
}