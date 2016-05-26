using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using sdk_lpl_mvc5;
using sdk_lpl_mvc5.Models;
using sdk_lpl_mvc5.Utils;
using sdk_lpl_mvc5.Filters;

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
namespace sdk_lpl_mvc5.Controllers
{
    /// <summary>
    /// Ce service a pour but de de fournir des informations concernant un utilisateur à la plateforme partenaire pour qu’elle puisse lui créer un compte et propager son abonnement. 
    /// À noter que ce service est exécuté après la création du compte sur la plateforme LPL (qui s’effectue après que l’utilisateur ait souscrit et payé son abonnement). Les données d’entrées fournies par LPL seront sérialisées en JSON dans le body de la requête. 
    /// </summary>
    /// <param name="message">Contient les informations de l'utilisateur, cryptées avec AES puis encodé en base64 </param>
    /// <returns>Retourne une chaine de caractère cryptée et encodée contenant les informations de validation</returns>
    public class CreationCompteController : ApiController
    {
        [HttpPost]
        [Route("ws/creationCompte")]
        [CheckHeader]
        public HttpResponseMessage CreateAccountService()
        {
            var body = Request.Content.ReadAsStringAsync().Result;

            CreationCompteModel compte =
                JsonConvert.DeserializeObject<CreationCompteModel>(Encrypt.DecryptRJ256(body,
                    Config.AESKey, Config.IV));

            ValidationResponseModel model = new ValidationResponseModel { PartenaireID = Config.PartID };

            if (Request.Headers.Contains("X-CTX"))
            {
                // Ne pas modifier
                model.CreateDummyModel();
            }
            else
            {
                // TODO : à modifier
                // Ajoutez ici votre logique de verification des donnees en base a partir de l'objet CreationCompteModel
                // Exemple de composition du modele a partir des donnees en base
                model.IsValid = true;
                model.PartenaireID = Config.PartID;
                model.CodeUtilisateur = compte.CodeUtilisateur;
                model.CodeEtat = Etat.Success;
            }

            string json = Encrypt.EncryptRJ256(JsonConvert.SerializeObject(model), Config.AESKey, Config.IV);

            string timeStampHeader = Timestamp.UnixTimestamp().ToString();
            string concat = string.Concat(Config.PartID.ToString(), "+", timeStampHeader, "+", Config.CodeSecret);

            HttpContext.Current.Response.AddHeader("X-TS", timeStampHeader);
            HttpContext.Current.Response.AddHeader("X-PART", Config.PartID.ToString());
            HttpContext.Current.Response.AddHeader("X-LPL", Hash.SHA1Hash(Encoding.UTF8.GetBytes(concat)));

            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "text/plain")
            };

            return resp;
        }
    }
}
