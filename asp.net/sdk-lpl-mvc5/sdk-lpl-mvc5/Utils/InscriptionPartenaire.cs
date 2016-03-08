using System;
using System.Collections.Generic;
using System.Text;
using sdk_lpl_mvc5;
using sdk_lpl_mvc5.Models;
using Newtonsoft.Json;
using System.Web;

namespace sdk_lpl_mvc5.Utils
{
    /// <summary>
    /// Utilitaire pour l'inscription partenaire
    /// </summary>
    public static class InscriptionPartenaire
    {
        /// <summary>
        /// Génération de l'url de l'inscription partenaire
        /// Inclure cette url dans une balise <a> pour rediriger l'utilisateur connecté vers une page d'inscription de la plateforme LPL avec les champs mail et pseudo déjà remplis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GenerateUrl(InformationCompteModel model)
        {
            var json = JsonConvert.SerializeObject(model);

            return "http://www.lapresselibre.fr/inscription-partenaire?" +
                    $"user={HttpUtility.UrlEncode(Encrypt.EncryptRJ256(json, Config.AESKey, Config.IV))}" +
                    $"&partId={Config.PartID}";
        }
    }
}
