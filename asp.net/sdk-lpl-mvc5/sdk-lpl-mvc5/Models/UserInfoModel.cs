using System;

namespace sdk_lpl_mvc5.Models
{
    /// <summary>
    /// Modèle de réponse pour la vérification de compte
    /// Représente le modèle de données envoyé en réponse d'une requête de vérification de compte partenaire
    /// </summary>
    public class UserInfoModel
    {
        public int PartenaireID { get; set; }
        public string CodeUtilisateur { get; set; }
        public string Mail { get; set; }
        public string TypeAbonnement { get; set; }
        public DateTime? DateExpiration { get; set; }
        public DateTime? DateSouscription { get; set; }
        public bool AccountExist { get; set; }

        public void CreateDummyModel()
        {
            model.AccountExist = true;
            model.Mail = "dummy@gmail.com";
            model.CodeUtilisateur = "dummy1234";
        }
    }
}
