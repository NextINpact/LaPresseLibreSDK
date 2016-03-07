using System;

namespace sdk_lpl_mvc5.Models
{
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
