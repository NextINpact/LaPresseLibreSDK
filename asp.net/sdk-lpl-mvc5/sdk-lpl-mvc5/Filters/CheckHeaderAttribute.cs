using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using sdk_lpl_mvc5.Utils;
using sdk_lpl_mvc5;

namespace sdk_lpl_mvc5.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CheckHeaderAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Vérification des informations contenus dans le header
        /// Permet de valider la transaction après comparaison du hachage SHA1 entre les valeurs contenues dans les headers X-PART et X-TS plus le code secret et la valeur du header X-LPL
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Headers.Contains("X-LPL") || !actionContext.Request.Headers.Contains("X-TS") ||
                !actionContext.Request.Headers.Contains("X-PART")) HandleUnauthorizedRequest(actionContext);
            else
            {
                string requestHash = actionContext.Request.Headers.GetValues("X-LPL").FirstOrDefault();
                string timeStamp = actionContext.Request.Headers.GetValues("X-TS").FirstOrDefault();
                string partID = actionContext.Request.Headers.GetValues("X-PART").FirstOrDefault();

                string expectedHash =
                    Hash.SHA1Hash(Encoding.UTF8.GetBytes(String.Concat(partID, "+", timeStamp, "+", Config.CodeSecret)));

                if (expectedHash != requestHash)
                {
                    HandleUnauthorizedRequest(actionContext);
                }

                base.OnAuthorization(actionContext);
            }
        }

        protected void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
