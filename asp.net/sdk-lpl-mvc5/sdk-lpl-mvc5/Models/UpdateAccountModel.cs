using System;

namespace sdk_lpl_mvc5.Models
{
    /// <summary>
    /// Modèle pour la mise à jour de compte
    /// Représente le modèle de données reçu lors de l'envoi d'une requete LPL de mise à jour de compte partenaire
    /// </summary>
    public class UpdateAccountModel
    {
        public string CodeUtilisateur { get; set; }
        public DateTime DateExpiration { get; set; }
        public DateTime DateSouscription { get; set; }
        public string TypeAbonnement { get; set; }
        public double Tarif { get; set; }
        public int Statut { get; set; }
    }
}
