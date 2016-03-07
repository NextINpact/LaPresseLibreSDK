using System;

namespace sdk_lpl_mvc5.Models
{
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
