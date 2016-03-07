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

            //TODO : à modifier
            //Vous devez effectuer ici des traitements bdd pour remplir le modèle ValidationResponseModel

            ValidationResponseModel model = new ValidationResponseModel
            {
                IsValid = true,
                PartenaireID = Config.PartID,
                CodeUtilisateur = compte.CodeUtilisateur
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
