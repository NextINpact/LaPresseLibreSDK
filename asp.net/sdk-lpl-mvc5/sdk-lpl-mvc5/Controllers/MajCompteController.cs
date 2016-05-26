using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
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
    /// Ce service a pour but de mettre à jour les comptes utilisateurs sur les plateformes partenaire pour la propagation de l’abonnement à partir des paramètres fourni par LPL. 
    /// Ce service sera exécuter lors de la reconduite d’un abonnement (si l’utilisateur a reconduit son paiement mensuel). Les données d’entrées fournies par LPL seront sérialisées en JSON dans le body de la requête.
    /// </summary>
    /// <param name="body">Contient les informations de l'utilisateur, cryptées avec AES puis encodé en base64</param>
    /// <returns>Retourne une chaine de caractère cryptée et encodée contenant les informations de validation </returns>
    public class MajCompteController : ApiController
    {
        [HttpPut]
        [Route("ws/majCompte")]
        [CheckHeader]
        public HttpResponseMessage UpdateAccount()
        {
            var body = Request.Content.ReadAsStringAsync().Result;

            UpdateAccountModel account =
                JsonConvert.DeserializeObject<UpdateAccountModel>(Encrypt.DecryptRJ256(body,
                    Config.AESKey, Config.IV));

            // TODO : à modifier
            // Ajoutez ici votre logique de verification des donnees en base à partir de l'objet UpdateAccountModel
            // Exemple de composition du modele à partir des donnees en base
            ValidationResponseModel model = new ValidationResponseModel
            {
                IsValid = true,
                CodeUtilisateur = account.CodeUtilisateur,
                PartenaireID = Config.PartID
            };

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
