using System;
using System.Collections.Generic;
using System.Text;

namespace sdk_lpl_mvc5.Models
{
    /// <summary>
    /// Modèle pour la création d'un lien d'inscription partenaire
    /// Représente le modèle de données qui compose le paramètre <user> dans l'url d'inscription partenaire
    /// </summary>
    public class InformationCompteModel
    {
        public string Email { get; set; }
        public string Pseudo { get; set; }
    }
}
