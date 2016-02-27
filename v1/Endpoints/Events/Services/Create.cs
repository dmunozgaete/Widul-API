using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    /// <summary>
    /// Create a new Event
    /// </summary>
    public class Create : Gale.REST.Http.HttpCreateActionResult<Models.NewEvent>
    {

        private String _creator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="newEvent"></param>
        public Create(String creator, Models.NewEvent newEvent)
            : base(newEvent)
        {
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
            Gale.Exception.RestException.Guard(() => Model.date == DateTime.MinValue, "DATE_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => !Model.place.isGuid(), "PLACE_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(Model.name), "NAME_EMPTY", API.Errors.ResourceManager);
            Gale.Exception.RestException.Guard(() => String.IsNullOrEmpty(Model.description), "DESCRIPTION_EMPTY", API.Errors.ResourceManager);


            using (var svc = new Gale.Db.DataService("SP_INS_Event"))
            {
                svc.Parameters.Add("USER_Token", _creator);
                svc.Parameters.Add("KNOW_Token", this.Model.knowledge);
                svc.Parameters.Add("PLAC_Token", this.Model.place);

                svc.Parameters.Add("EVNT_Date", this.Model.date.ToLocalTime());
                svc.Parameters.Add("EVNT_Description", this.Model.description);
                svc.Parameters.Add("EVNT_IsRestricted", this.Model.isRestricted);
                svc.Parameters.Add("EVNT_ISPrivate", this.Model.isPrivate);

                svc.Parameters.Add("EVNT_Name", this.Model.name);

                if (this.Model.tags.Count > 0)
                {
                    svc.Parameters.Add("TAGS", String.Join(",", this.Model.tags));
                }

                Guid token = (Guid)this.ExecuteScalar(svc);

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Content = new ObjectContent<Object>(new {
                        token = token.ToString()
                    },
                    new Gale.REST.Http.Formatter.KqlFormatter())
                });
            }
        }
    }
}