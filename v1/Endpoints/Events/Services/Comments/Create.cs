using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services.Comments
{
    public class Create: Gale.REST.Http.HttpCreateActionResult<Models.NewComment>
    {

        String _user;
        String _evenToken;

        public Create(String eventToken, String user, Models.NewComment comment) : base(comment) {

            _user = user;
            _evenToken = eventToken;


        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {

            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => this.Model == null, "BODY_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(this.Model.comment), "EMPTY_COMMENT", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_evenToken), "EMPTY_EVENT", API.Errors.ResourceManager);

            using (var svc = new Gale.Db.DataService("SP_INS_EventComment"))
            {
                svc.Parameters.Add("USR_Token", _user);
                svc.Parameters.Add("EVN_Token", _evenToken);
                svc.Parameters.Add("COM_Comment", this.Model.comment);

                this.ExecuteAction(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Created
                });
            }



        }
    }
}