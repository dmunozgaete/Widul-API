using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Accounts.Services
{
    /// <summary>
    /// Retrieve a Target User
    /// </summary>
    public class Find : Gale.REST.Http.HttpReadActionResult<String>
    {
        private string _query;
        private int _offset;
        private int _limit;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="query"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public Find(String user, String query, int offset, int limit)
            : base(user)
        {
            _query = query;
            _offset = offset;
            _limit = limit;
        }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            using (Gale.Db.DataService svc = new Gale.Db.DataService("SP_GET_FindAccounts"))
            {
                svc.Parameters.Add("USER_Token", this.Model);
                svc.Parameters.Add("Query", _query);
                svc.Parameters.Add("Limit", _limit);
                svc.Parameters.Add("Offset", _offset);

                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                var pagination = repo.GetModel<Models.Pagination>().FirstOrDefault();
                List<Models.Friend> accounts = repo.GetModel<Models.Friend>(1);

                pagination.items = accounts;

                //----------------------------------------------------------------------------------------------------
                //Create Response
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(
                        pagination,
                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                    )
                };

                //Return Task
                return Task.FromResult(response);
                //----------------------------------------------------------------------------------------------------

            }
        }
    }
}