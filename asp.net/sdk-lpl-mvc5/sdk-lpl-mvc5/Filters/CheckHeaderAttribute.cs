using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using sdk_lpl_mvc5.Utils;
using sdk_lpl_mvc5;


/**
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
* 
* The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
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
