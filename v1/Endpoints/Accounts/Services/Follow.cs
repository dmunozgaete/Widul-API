using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Accounts.Services
{
    /// <summary>
    /// Follow Account
    /// </summary>
    public class Follow : Gale.REST.Http.HttpUpdateActionResult<String>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user">Current User</param>
        /// <param name="account">Account to Follow</param>
        public Follow(String user, String account) : base(user, account) { }

        /// <summary>
        /// Async Process
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(string token, System.Threading.CancellationToken cancellationToken)
        {


            using (Gale.Db.DataService svc = new Gale.Db.DataService("[SP_INS_FollowAccount]"))
            {
                svc.Parameters.Add("USER_Token", token);
                svc.Parameters.Add("USER_Token_Follower", this.Model);

                this.ExecuteAction(svc);

                //----------------------------------------------------------------------------------------------------
                //Create Response
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.Created);

                //Return Task
                return Task.FromResult(response);
                //----------------------------------------------------------------------------------------------------

            }
        }
    }
}