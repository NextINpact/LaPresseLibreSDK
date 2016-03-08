using System;
using System.Collections.Generic;
using System.Text;

namespace sdk_lpl_mvc5.Models
{
    /// <summary>
    /// Modèle pour la vérification de compte
    /// Représente le modèle de données reçu lors de l'envoi d'une requete LPL de vérification de compte partenaire
    /// </summary>
    public class VerificationModel
    {
        public string CodeUtilisateur { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
