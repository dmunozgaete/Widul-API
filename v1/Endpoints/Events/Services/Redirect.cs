using RazorTemplates.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    /// <summary>
    /// Generate a blank page with the Facebook OG Metatags and then Redirect to the selected Event.
    /// </summary>
    public class Redirect : Gale.REST.Http.HttpCreateActionResult<String>
    {

        String _host;

        public Redirect(String eventToken, String host)
            : base(eventToken)
        {
            _host = host;
        }

        private string RenderView(dynamic model)
        {
            //----------------------------------
            var assembly = this.GetType().Assembly;
            String resourcePath = "API.Endpoints.Events.Templates.Redir.cshtml";

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

            using (var svc = new Gale.Db.DataService("SP_GET_Event"))
            {
                svc.Parameters.Add("EVNT_Token", this.Model);
                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables
                Models.EventDetails eventDetail = repo.GetModel<Models.EventDetails>().FirstOrDefault();

                //----------------------------------------------------------------------------------------------------
                //Guard Exception's
                Gale.Exception.RestException.Guard(() => eventDetail == null, "EVENT_DONT_EXISTS", API.Errors.ResourceManager);
                //----------------------------------------------------------------------------------------------------

                //Setting Values ;)!
                eventDetail.comments_latest = repo.GetModel<Models.VW_EventComment>(1);
                eventDetail.tags = repo.GetModel<Models.EventTag>(2);
                eventDetail.place = repo.GetModel<Models.Place>(3).FirstOrDefault();
                eventDetail.participants_tops = repo.GetModel<Models.TopParticipant>(4);

                //Create a Redirect View with facebook og metatags!
                String html = RenderView(new
                {
                    Event = eventDetail,
                    AppUrl = this._host,
                    EventToken = this.Model
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
}