﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    public class Left :Gale.REST.Http.HttpDeleteActionResult
    {

        String _user;

        public Left(String eventToken, String user) : base(eventToken) {


            _user = user;


        }

        public override Task<HttpResponseMessage> ExecuteAsync(string token, CancellationToken cancellationToken)
        {


            //------------------------------------------------
            // Guard's 
            Gale.Exception.RestException.Guard(() => token == null, "EMPTY_EVENT", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);

            using (var svc = new Gale.Db.DataService("SP_DEL_LeftEvent"))
            {
                svc.Parameters.Add("USR_Token", _user);
                svc.Parameters.Add("EVN_Token", token);

                this.ExecuteAction(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                });
            }


        }
    }
}