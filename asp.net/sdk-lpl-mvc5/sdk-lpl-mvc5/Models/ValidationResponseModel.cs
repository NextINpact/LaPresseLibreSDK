namespace sdk_lpl_mvc5.Models
{
    /// <summary>
    /// Modèle de réponse
    /// Représente le modèle de données envoyé en réponse d'une requête de création ou de mise à jour de compte partenaire
    /// </summary>
    public class ValidationResponseModel
    {
        public bool IsValid { get; set; }
        public string CodeUtilisateur { get; set; }
        public int PartenaireID { get; set; }
        public int CodeEtat { get; set; }

        public void CreateDummyModel()
        {
            CodeUtilisateur = "dummy";
            IsValid = true;
            CodeEtat = Etat.Success;
        }
    }

    public enum Etat
    {
        Success = 1,
        EmailExist = 2,
        UsernameExist = 3,
        Fail = 4
    };
}
