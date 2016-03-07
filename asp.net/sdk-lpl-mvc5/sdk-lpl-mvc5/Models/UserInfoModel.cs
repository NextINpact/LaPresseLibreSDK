using System;

namespace sdk_lpl_mvc5.Models
{
    public class UserInfoModel
    {
        public int PartenaireID { get; set; }
        public string CodeUtilisateur { get; set; }
        public string Mail { get; set; }
        public string TypeAbonnement { get; set; }
        public DateTime? DateExpiration { get; set; }
        public DateTime? DateSouscription { get; set; }
        public bool AccountExist { get; set; }
    }
}
