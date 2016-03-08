/**
* Modèle pour la création de compte
* Représente le modèle de données reçu lors de l'envoi d'une requete LPL de création de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/

module.exports = CreationCompteModel;

function CreationCompteModel(obj) {
    this.CodeUtilisateur = obj.CodeUtilisateur;
    this.Pseudo = obj.Pseudo;
    this.Mail = obj.Mail;
    this.Password = obj.Password;
    this.TypeAbonnement = obj.TypeAbonnement;
    this.DateSouscription = obj.DateSouscription;
    this.DateExpiration = obj.DateExpiration;
    this.Tarif = obj.Tarif;
    this.Statut = obj.Statut;
}