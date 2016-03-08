using System;
using System.Linq;
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
    /// Ce service a pour but de récupérer des informations utilisateur sur une plateforme partenaire à partir de paramètres fourni par LPL.
    /// </summary>
    /// <param name="crd">Contient les identifiants de l'utilisateur, cryptés avec AES puis encodés en base64</param>
    /// <returns>Retourne une chaine de caractère cryptée et encodée contenant les informations de l'utilisateur trouvé</returns>
    public class VerificationController : ApiController
    {
        [HttpGet]
        [Route("ws/verification")]
        [CheckHeader]
        public HttpResponseMessage CheckAccountService([FromUri] string crd)
        {
            VerificationModel obj =
                JsonConvert.DeserializeObject<VerificationModel>(Encrypt.DecryptRJ256(crd,
                    Config.AESKey, Config.IV));

            UserInfoModel model = new UserInfoModel {PartenaireID = Config.PartID};

            if (Request.Headers.Contains("X-CTX"))
            { 
                // Ne pas modifier
                model.CreateDummyModel();
            }
            else
            {
                // TODO : à modifier
                // Ajoutez ici votre logique de verification des donnees en base a partir de l'objet VerificationModel
                // Exemple de composition du modele a partir des donnees en base
                model.AccountExist = true;
                model.Mail = obj.Mail;
                model.CodeUtilisateur = obj.CodeUtilisateur;
            }

            string json = Encrypt.EncryptRJ256(JsonConvert.SerializeObject(model), Config.AESKey,
                Config.IV);

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
