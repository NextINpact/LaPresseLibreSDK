using System;
using System.Collections.Generic;
using System.Text;

namespace sdk_lpl_mvc5
{
    /// <summary>
    /// A remplacer par les valeurs fournies par l'administration de La Presse Libre
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Clé secrete pour le chiffrage AES Rijndael 256
        /// À modifier
        /// </summary>
        public static int PartID => 2;

        /// <summary>
        /// Vecteur d'initialisation pour le chiffrage AES Rijndael 256
        /// À modifier
        /// </summary>
        public static string AESKey => "anjueolkdiwpoidaanjueolkdiwpoida";

        /// <summary>
        /// Numéro partenaire LPL
        /// À modifier
        /// </summary>
        public static string IV => "4528711254935489";

        /// <summary>
        /// Code secret pour le hachage du header X-LPL
        /// À modifier
        /// </summary>
        public static string CodeSecret => "af3zfA2qd";
    }
}
