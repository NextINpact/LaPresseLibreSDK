using System;

namespace sdk_lpl_mvc5.Models
{
    /// <summary>
    /// Modèle pour la création de compte
    /// Représente le modèle de données reçu lors de l'envoi d'une requete LPL de création de compte partenaire
    /// </summary>
    public class CreationCompteModel
    {
        public string CodeUtilisateur { get; set; }
        public string Mail { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public string TypeAbonnement { get; set; }
        public DateTime DateExpiration { get; set; }
        public DateTime DateSouscription { get; set; }
        public double Tarif { get; set; }
        public int Statut { get; set; }
    }
}
