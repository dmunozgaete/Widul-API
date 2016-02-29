using RazorTemplates.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services.Invitations
{
    /// <summary>
    /// Join to an Event
    /// </summary>
    public class Approve : Gale.REST.Http.HttpCreateActionResult<String>
    {
        String _user;
        String _host;
        String _name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventToken"></param>
        /// <param name="user"></param>
        /// <param name="host"></param>
        /// <param name="name"></param>
        public Approve(String eventToken, String user, String host, String name)
            : base(eventToken)
        {
            _user = user;
            _host = host;
            _name = name;
        }

        private string RenderView(dynamic model)
        {
            //----------------------------------
            var assembly = this.GetType().Assembly;
            String resourcePath = "API.Endpoints.Events.Templates.Approve.cshtml";

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                //------------------------------------------------------------------------------------------------------
                // GUARD EXCEPTIONS
                Gale.Exception.RestException.Guard(() => stream == null, "TEMPLATE_DONT_EXIST", API.Errors.ResourceManager);
                //------------------------------------------------------------------------------------------------------

                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    var view = Template.Compile(reader.ReadToEnd());
                    return view.Render(model);
                }
            }
            //----------------------------------
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
            Gale.Exception.RestException.Guard(() => this.Model == null, "EMPTY_EVENT", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(_user), "EMPTY_USER", API.Errors.ResourceManager);

            //Only create invitation via email if the user is a widul user =)!
            if (this._name.IndexOf("@") <= 0)
            {
                using (var svc = new Gale.Db.DataService("SP_INS_JoinEvent"))
                {
                    svc.Parameters.Add("USER_Token", _user);
                    svc.Parameters.Add("EVNT_Token", this.Model);

                    this.ExecuteAction(svc);
                }
            }

            //Send an Approved or register view =)!
            String html = RenderView(new
            {
                AppUrl = this._host,
                EventToken = this.Model,
                Guest = this._name
            });

            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new System.Net.Http.StringContent(html)

            };

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

            return Task.FromResult(response);

        }
    }
}